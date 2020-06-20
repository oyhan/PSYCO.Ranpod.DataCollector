using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PSYCO.Ranpod.LocalProxy.Models.Database;
using SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PSYCO.Ranpod.LocalProxy.Helper;
using PSYCO.JsonDatastore;

namespace PSYCO.Ranpod.LocalProxy.Models.Services
{
    public class CheckSessionsAlive
    {
        private static AppJsonDataStore _db;

     
        public static void DoJob(object state)
        {
            var app = state as IApplicationBuilder;
            var settings = app.GetService<IOptionsSnapshot<AppSettings>>().Value;
            var db = app.GetService<IJsonDataStore<JsonDatabase>>();
            var expiredSessions = ClientSessions.SessionsList
                .Where(client =>
                (DateTime.Now - client.LastReceivedData).Minutes > settings.SessionsSendLogIntervalMinutes);
           
            if (expiredSessions.Any())
            {
                foreach (var client in ClientSessions.SessionsList)
                {
                   
                    if ((DateTime.Now - client.LastReceivedData).Minutes >= settings.SessionsSendLogIntervalMinutes)
                    {
                        client.IsRunning = TextResources.Disconnect;
                        client.Status = Constants.PMDLProStatusDisconnected;
                        client.LastReceivedData = DateTime.Now;
                    }
                   

                }
                _db = app.GetService<IJsonDataStore<JsonDatabase>>() as AppJsonDataStore;

                _db.Database.Sessions = ClientSessions.SessionsList;
                _db.ApplyChanges();
                ViewUpdater.Update();
            }
           
        }
    }
}
