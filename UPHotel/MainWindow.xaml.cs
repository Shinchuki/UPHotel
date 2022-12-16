using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace UPHotel
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new AuthorizePage());
            Manager.MngrMainFrame = MainFrame;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Manager.IsActualLogIn == false)
            {
                BackButton.Visibility = Visibility.Hidden;
            }
            Manager.MngrMainFrame.GoBack();
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                BackButton.Visibility = Visibility.Visible;
            }
            else
            {
                BackButton.Visibility = Visibility.Hidden;
            }
           
        }
    }
}
