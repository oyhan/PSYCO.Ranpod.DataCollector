using DotNetify;
using PSYCO.Ranpod.LocalProxy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PSYCO.Ranpod.LocalProxy.RealTime.ViewModels
{
    public class MainViewModel : BaseVM
    {

        public List<SessionListItem> Sessions { get; set; } = new List<SessionListItem>();
        private Timer _timer;

        public MainViewModel()
        {
            //_timer = new Timer(state =>
            //{
            //    var item = new SessionListItem()
            //    {
            //        Number = Sessions.Count + 1,

            //    };
            //    Sessions.Add(item);
            //    Changed(nameof(Sessions));
            //    PushUpdates();
            //}, null, 0, 1000); // every 1000 ms.
            Sessions = ClientSessions.SessionsList;
            ViewModelInstances.Add(nameof(MainViewModel), this);
        }

    }
}
