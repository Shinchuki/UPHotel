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
    /// Логика взаимодействия для HotelCommentPage.xaml
    /// </summary>
    public partial class HotelCommentPage : Page
    {
        private HotelComment _CurrentComment = new HotelComment();
        public HotelCommentPage(HotelComment SelectedComment)
        {
            InitializeComponent();
            if (SelectedComment != null)
            {
                _CurrentComment = SelectedComment;
            }
            DataContext = _CurrentComment;
            ComboHotels.ItemsSource = UPEntitiesOchenMnogo.GetContext().Hotels.ToList();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (_CurrentComment.Hotel == null)
            {
                errors.AppendLine("Укажите название отеля");
            }

            if (string.IsNullOrWhiteSpace(_CurrentComment.Text))
            {
                errors.AppendLine("Напишите отзыв");
            }
            if (string.IsNullOrWhiteSpace(_CurrentComment.Author))
            {
                errors.AppendLine("Укажите автора");
            }

            _CurrentComment.creationDate = DateTime.Now;
            
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_CurrentComment.ID_comment == 0)
            {
                UPEntitiesOchenMnogo.GetContext().HotelComments.Add(_CurrentComment);
            }

            try
            {
                UPEntitiesOchenMnogo.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MngrMainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
           
        }
    }
}
