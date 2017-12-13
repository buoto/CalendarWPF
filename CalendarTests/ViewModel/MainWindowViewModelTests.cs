using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Calendar.Model;
using Rhino.Mocks;
using System.Collections.Generic;

namespace Calendar.ViewModel.Tests
{
    [TestClass()]
    public class MainWindowViewModelTests
    {
        private Appointment appointment;
        private IStore store;
        private MainWindowViewModel mainWindowViewModel;
        private List<Day> daysList;

        [TestInitialize()]
        public void Initialize()
        {
            daysList = new List<Day>();
            DateTime monday = DateTime.Now.AddDays(DayOfWeek.Monday - DateTime.Now.DayOfWeek);

            for (int i = 0; i < 28; i++) {
                daysList.Add(new Day(monday.AddDays(i)));
            }
            appointment = new Appointment
            {
                Title = "FooBar",
                StartTime = daysList[3].DateTime,
                EndTime = daysList[3].DateTime.AddHours(1)
            };
            daysList[3].Appointments.Add(appointment);

            store = MockRepository.GenerateStub<IStore>();

            store.Stub(s => s.GetDays(DateTime.Now)).Return(daysList);
            store.Stub(s => s.GetDaysWithNow()).Return(daysList);

            mainWindowViewModel = new MainWindowViewModel(store);
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

        [TestMethod()]
        public void AddAppointmentPreservesOrderTest()
        {
            Appointment older = new Appointment { Title = "name", StartTime = DateTime.Now.AddSeconds(-1), EndTime = DateTime.Now };
            Appointment newer = new Appointment { Title = "name", StartTime = DateTime.Now, EndTime = DateTime.Now };

            // Add in reversed order
            mainWindowViewModel.AddAppointment(mainWindowViewModel.Days[0], newer);
            mainWindowViewModel.AddAppointment(mainWindowViewModel.Days[0], older);

            // Appointments are stored in correct order
            Assert.AreEqual(older, mainWindowViewModel.Days[0].Appointments[0]);
            Assert.AreEqual(newer, mainWindowViewModel.Days[0].Appointments[1]);
        }

        [TestMethod()]
        public void EditAppointmentTest()
        {
            Appointment edited = new Appointment { Title = "name", StartTime = DateTime.Now, EndTime = DateTime.Now };
            mainWindowViewModel.EditAppointment(appointment, edited);

            Assert.AreEqual(edited, mainWindowViewModel.Days[3].Appointments[0]);
        }

        [TestMethod()]
        public void DeleteAppointmentTest()
        {
            Assert.AreEqual(1, mainWindowViewModel.Days[3].Appointments.Count);

            mainWindowViewModel.DeleteAppointment(appointment);

            Assert.AreEqual(0, mainWindowViewModel.Days[3].Appointments.Count);
        }
    }
}