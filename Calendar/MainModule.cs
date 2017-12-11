using Calendar.Model;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    class MainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IStore>().To<XMLFileStore>();
        }
    }
}
