using DotNetify;
using PSYCO.Ranpod.LocalProxy.RealTime;
using PSYCO.Ranpod.LocalProxy.RealTime.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSYCO.Ranpod.LocalProxy.Models.Services
{
    public class ViewUpdater
    {
        public static void Update()
        {
            List<MainViewModel> mainViewModels = ViewModelInstances.ViewModels.Cast<MainViewModel>().ToList();
            if (mainViewModels.Any())
            {
                foreach (var mainViewModel in mainViewModels)
                {
                    mainViewModel.Sessions = ClientSessions.SessionsList;
                    mainViewModel.Changed(nameof(MainViewModel.Sessions));
                    mainViewModel.PushUpdates();
                }


            }
        }
    }
}
