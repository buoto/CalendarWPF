﻿using Calendar.Model;

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
            Storage s = new Storage();
            var x = s.getPersons();
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindowViewModel mainWindowViewModel = this.DataContext as MainWindowViewModel;
            Label label = sender as Label;
            Event appointment = label.DataContext as Event;

            DetailsWindow detailsWindow = new DetailsWindow();
            DetailsWindowViewModel detailsWindowViewModel = detailsWindow.DataContext as DetailsWindowViewModel;
            detailsWindowViewModel.Appointment = appointment;
            bool? res = detailsWindow.ShowDialog();
            if (res.HasValue && res.Value) {
                Event newAppointment = detailsWindowViewModel.Appointment;
                if (newAppointment != null) {
                    mainWindowViewModel.EditAppointment(appointment, detailsWindowViewModel.Appointment);
                } else {
                    mainWindowViewModel.DeleteAppointment(appointment);
                }
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
                    Event newAppointment = detailsWindowViewModel.Appointment;
                    if (newAppointment != null)
                    {
                        mainWindowViewModel.AddAppointment(day, detailsWindowViewModel.Appointment);
                    }
                }
            }
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.FontSize = e.NewSize.Height * 0.04;
        }

        private void ItemsControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.FontSize = e.NewSize.Height * 0.02;
        }

        public bool PopupVisibility { get; set; }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Grid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Popup.IsOpen = !Popup.IsOpen;
        }

    }
}
