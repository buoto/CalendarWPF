using Calendar.Model;
using Ninject.Modules;

namespace Calendar
{
    class MockModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IStore>().To<MockStore>();
        }
    }
}
