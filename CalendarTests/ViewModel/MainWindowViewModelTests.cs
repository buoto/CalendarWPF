using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calendar.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calendar.Model;

namespace Calendar.ViewModel.Tests
{
    [TestClass()]
    public class MainWindowViewModelTests
    {
        private MainWindowViewModel mainWindowViewModel;

        [TestInitialize()]
        public void Initialize()
        {
            mainWindowViewModel = new MainWindowViewModel(new MockStore()); // TODO real mocks
        }

        [TestMethod()]
        public void MainWindowViewModelTest()
        {
            Assert.IsNotNull(mainWindowViewModel);
        }

        [TestMethod()]
        public void EditAppointmentTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteAppointmentTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddAppointmentTest()
        {
            Assert.Fail();
        }
    }
}