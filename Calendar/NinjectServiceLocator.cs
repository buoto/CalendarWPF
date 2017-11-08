using Calendar.ViewModel;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    class NinjectServiceLocator
    {
        private readonly IKernel kernel;

        public NinjectServiceLocator() {
            this.kernel = new StandardKernel(new MainModule());
        }

        public MainWindowViewModel MainWindowViewModel {
            get { return kernel.Get<MainWindowViewModel>(); }
        }
        public DetailsWindowViewModel DetailsWindowViewModel {
            get { return kernel.Get<DetailsWindowViewModel>(); }
        }
    }
}
