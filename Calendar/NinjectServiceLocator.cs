using Calendar.ViewModel;
using Ninject;
using Ninject.Modules;

namespace Calendar
{
    class NinjectServiceLocator
    {
        private IKernel kernel;
        public NinjectModule Module { set { kernel = new StandardKernel(value); } }

        public MainWindowViewModel MainWindowViewModel {
            get { return kernel.Get<MainWindowViewModel>(); }
        }
        public DetailsWindowViewModel DetailsWindowViewModel {
            get { return kernel.Get<DetailsWindowViewModel>(); }
        }
    }
}
