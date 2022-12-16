using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace UPHotel
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Hotel _CurrentHotel = new Hotel();
        public AddEditPage(Hotel SelectedHotel)
        {
            InitializeComponent();
            if (SelectedHotel != null)
            {
                _CurrentHotel = SelectedHotel;
            }
            DataContext = _CurrentHotel;
            ComboCountries.ItemsSource = UPEntitiesOchenMnogo.GetContext().Countries.ToList();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_CurrentHotel.Name))
            {
                errors.AppendLine("Укажите название отеля");
            }
            if (_CurrentHotel.CountOfStars < 1 || _CurrentHotel.CountOfStars > 5)
            {
                errors.AppendLine("Укажите кол-во звёзд от 1 до 5");
            }
            if (_CurrentHotel.Country == null)
            {
                errors.AppendLine("Укажите страну");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_CurrentHotel.ID_hotel == 0)
            {
                UPEntitiesOchenMnogo.GetContext().Hotels.Add(_CurrentHotel);
            }
            try
            {
                UPEntitiesOchenMnogo.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
                Manager.MngrMainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
