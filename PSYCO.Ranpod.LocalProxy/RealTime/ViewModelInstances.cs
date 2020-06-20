using DotNetify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSYCO.Ranpod.LocalProxy.RealTime
{
    public class ViewModelInstances
    {

        public static List< BaseVM> ViewModels { get; set; } = new List<BaseVM>();

        public static void Add(string name, BaseVM vm)
        {
            ViewModels.Add( vm);
        }
        //public static void Remove(string name)
        //{
        //    if (ViewModels.Any(v => v.Key == name)) ViewModels.Remove(name);
        //    else return;
        //}
    }
}
