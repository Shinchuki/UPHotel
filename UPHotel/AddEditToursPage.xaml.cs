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
    /// Логика взаимодействия для AddEditToursPage.xaml
    /// </summary>
    public partial class AddEditToursPage : Page
    {
        private Tour _CurrentTour = new Tour();
        public AddEditToursPage(Tour SelectedTour)
        {
            InitializeComponent();
            if (SelectedTour != null)
            {
                _CurrentTour = SelectedTour;
            }
            DataContext = _CurrentTour;
            //ComboActual.ItemsSource = UPEntitiesMnogo.GetContext().Tours.ToList();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_CurrentTour.Name))
            {
                errors.AppendLine("Укажите название тура");
            }
            if (_CurrentTour.TicketCount < 0)
            {
                errors.AppendLine("Укажите положительное число билетов");
            }
            if (_CurrentTour.Price < 0)
            {
                errors.AppendLine("Укажите положительную цену");
            }
            if (ComboActual.SelectedItem == null) 
            {
                errors.AppendLine("Укажите актуальность тура");
            }
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_CurrentTour.ID_tour == 0)
            {
               UPEntitiesOchenMnogo.GetContext().Tours.Add(_CurrentTour);
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

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            _CurrentTour.IsActual = true;
        }

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            _CurrentTour.IsActual = false;
        }
    }
}
