using Calendar.Model;
using Calendar.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calendar.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindowViewModel mainWindowViewModel = this.DataContext as MainWindowViewModel;
            Label label = sender as Label;
            Appointment appointment = label.DataContext as Appointment;

            DetailsWindow detailsWindow = new DetailsWindow();
            DetailsWindowViewModel detailsWindowViewModel = detailsWindow.DataContext as DetailsWindowViewModel;
            detailsWindowViewModel.Appointment = appointment;
            bool? res = detailsWindow.ShowDialog();
            if (res.HasValue && res.Value) {
                Appointment newAppointment = detailsWindowViewModel.Appointment;
                if (newAppointment != null) {
                    mainWindowViewModel.EditAppointment(appointment, detailsWindowViewModel.Appointment);
                } else {
                    mainWindowViewModel.DeleteAppointment(appointment);
                }
                System.Console.WriteLine(detailsWindowViewModel.Appointment);
            }
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                MainWindowViewModel mainWindowViewModel = this.DataContext as MainWindowViewModel;
                StackPanel stackPanel = sender as StackPanel;
                Day day = stackPanel.DataContext as Day;
                Console.WriteLine(day.DateTime);

                DetailsWindow detailsWindow = new DetailsWindow();
                DetailsWindowViewModel detailsWindowViewModel = detailsWindow.DataContext as DetailsWindowViewModel;
                detailsWindowViewModel.Day = day;
                bool? res = detailsWindow.ShowDialog();
                if (res.HasValue && res.Value) {
                    // TODO update state
                    mainWindowViewModel.AddAppointment(day, detailsWindowViewModel.Appointment);
                System.Console.WriteLine(detailsWindowViewModel.Appointment);
                }
            }
        }

        private void OpenEditWindow(Appointment appointment) {
        }

        private void OpenCreateWindow(Day day) {
        }
    }
}
