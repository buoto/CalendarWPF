using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calendar.Model.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Mocks;
using log4net.Config;

namespace Calendar.Model.Store.Tests
{
    [TestClass()]
    public class StorageStoreTests
    {
        [AssemblyInitialize]
        public static void Configure(TestContext tc)
        {
            BasicConfigurator.Configure();
        }

        Appointment appointment = new Appointment { Title = "Very important meeting" };

        [TestInitialize()]
        public void Initialize()
        {
        }

        [TestMethod()]
        public void StorageStoreTest()
        {
            var storage = MockRepository.GenerateStrictMock<IStorage>();
            storage.Expect(s => s.GetPersonByUserID(null)).IgnoreArguments().Return(null);

            storage.Replay();

            var storageStore = new StorageStore(storage);

            storage.VerifyAllExpectations();
            Assert.IsNotNull(storageStore);
        }

        [TestMethod()]
        public void AddAppointmentTest()
        {
            var storage = MockRepository.GenerateMock<IStorage>();
            storage.Expect(s => s.CreateAppointment(appointment.Title, appointment.StartTime, appointment.EndTime)).Return(appointment);
            storage.Expect(s => s.CreateAttendance(appointment, null)).Return(null);

            storage.Replay();

            var storageStore = new StorageStore(storage);

            storageStore.AddAppointment(appointment);

            storage.VerifyAllExpectations();
        }


        [TestMethod()]
        public void EditAppointmentTest()
        {
            var storage = MockRepository.GenerateMock<IStorage>();
            storage.Expect(s => s.UpdateAppointment(appointment));

            storage.Replay();

            var storageStore = new StorageStore(storage);

            storageStore.EditAppointment(null, appointment);

            storage.VerifyAllExpectations();
        }

        [TestMethod()]
        public void DeleteAppointmentTest()
        {
            var storage = MockRepository.GenerateMock<IStorage>();
            storage.Expect(s => s.DeleteAppointment(appointment));

            storage.Replay();

            var storageStore = new StorageStore(storage);

            storageStore.DeleteAppointment(appointment);

            storage.VerifyAllExpectations();
        }

        [TestMethod()]
        public void GetDaysTest()
        {
            var day = DateTime.Now;

            var storage = MockRepository.GenerateMock<IStorage>();
            storage.Expect(s => s.GetAppointments(null)).Return(
                new List<Appointment> {
                    new Appointment { Title = "first", StartTime = day.AddDays(1), EndTime = day.AddDays(1).AddHours(1) },
                    new Appointment { Title = "second", StartTime = day.AddDays(2), EndTime = day.AddDays(2).AddHours(1) },
                });

            storage.Replay();

            var storageStore = new StorageStore(storage);

            var result = storageStore.GetDays(day);

            storage.VerifyAllExpectations();
        }

        [TestMethod()]
        public void GetDaysWithNowTest()
        {
            var day = DateTime.Now;

            var storage = MockRepository.GenerateMock<IStorage>();
            storage.Expect(s => s.GetAppointments(null)).Return(
                new List<Appointment> {
                    new Appointment { Title = "first", StartTime = day.AddDays(1), EndTime = day.AddDays(1).AddHours(1) },
                    new Appointment { Title = "second", StartTime = day.AddDays(2), EndTime = day.AddDays(2).AddHours(1) },
                });

            storage.Replay();

            var storageStore = new StorageStore(storage);

            var result = storageStore.GetDaysWithNow();

            storage.VerifyAllExpectations();
        }
    }
}