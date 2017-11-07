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
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            MainWindowViewModel mainWindowViewModel = this.mwvm;
            Label label = sender as Label;
            Appointment appointment = label.DataContext as Appointment;
            Console.WriteLine(appointment);
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                MainWindowViewModel mainWindowViewModel = this.mwvm;
                StackPanel stackPanel = sender as StackPanel;
                Day day = stackPanel.DataContext as Day;
                Console.WriteLine(day.DateTime);
            }
        }
    }
}
