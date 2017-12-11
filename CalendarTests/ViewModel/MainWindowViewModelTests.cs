using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            //MockRepository mockRepository = new MockRepository();
            //IStorage storage = mockRepository.StrictMock<IStorage>();
            mainWindowViewModel = new MainWindowViewModel(new MockStore());
        }

        [TestMethod()]
        public void MainWindowViewModelTest()
        {
            Assert.IsNotNull(mainWindowViewModel);
            Assert.AreEqual(28, mainWindowViewModel.Days.Count);
        }

        [TestMethod()]
        public void AddAppointmentTest()
        {
            Appointment appointment = new Appointment { Title = "name", StartTime = DateTime.Now, EndTime = DateTime.Now };
            mainWindowViewModel.AddAppointment(mainWindowViewModel.Days[0], appointment);

            Assert.AreEqual("name", mainWindowViewModel.Days[0].Appointments[0].Title);
        }
    }
}