using Calendar.Model;
using Calendar.Model.Store;
using Ninject.Modules;

namespace Calendar
{
    class MainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IStorage>().To<Storage>();
            Bind<IStore>().To<StorageStore>();
        }
    }
}
