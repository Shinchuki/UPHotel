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

namespace UPHotel
{
    /// <summary>
    /// Логика взаимодействия для BookingPage.xaml
    /// </summary>
    public partial class HotelPage : Page
    {
        private CheckUsers HotelsUser_;
        public HotelPage(CheckUsers user)
        {
            InitializeComponent();
            ButtonAdd.Visibility = Visibility.Hidden;
            ButtonDelete.Visibility = Visibility.Hidden;
            DGTCEdit.Visibility = Visibility.Hidden;
            DGTCComment.Visibility = Visibility.Visible;
            HotelsUser_ = user;
            if (HotelsUser_.IsAdmin == true)
            {
                ButtonAdd.Visibility = Visibility.Visible;
                ButtonDelete.Visibility = Visibility.Visible;
                DGTCEdit.Visibility = Visibility.Visible;
                DGTCComment.Visibility = Visibility.Hidden;
            }
            DGridHotels.ItemsSource = UPEntitiesOchenMnogo.GetContext().Hotels.ToList();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MngrMainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Hotel));
        }

        private void ButtonComment_Click(object sender, RoutedEventArgs e)
        {
            Manager.MngrMainFrame.Navigate(new HotelCommentPage((sender as Button).DataContext as HotelComment));
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MngrMainFrame.Navigate(new AddEditPage(null));
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            var HotelsForRemoving = DGridHotels.SelectedItems.Cast<Hotel>().ToList();
            if (MessageBox.Show($"Вы уверены удалить следующие {HotelsForRemoving.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    UPEntitiesOchenMnogo.GetContext().Hotels.RemoveRange(HotelsForRemoving);
                    UPEntitiesOchenMnogo.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    DGridHotels.ItemsSource = UPEntitiesOchenMnogo.GetContext().Hotels.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                UPEntitiesOchenMnogo.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridHotels.ItemsSource = UPEntitiesOchenMnogo.GetContext().Hotels.ToList();
            }
        }
    } 
}
