using Calendar.ViewModel;
using System;
using System.Windows;

namespace Calendar.View
{
    /// <summary>
    /// Interaction logic for DetailsWindow.xaml
    /// </summary>
    public partial class DetailsWindow : Window
    {
        public DetailsWindow()
        {
            InitializeComponent();
            ((DetailsWindowViewModel) DataContext).CloseAction = new Action(() => {
                this.DialogResult = true;
                this.Close();
            });
        }
    }
}
