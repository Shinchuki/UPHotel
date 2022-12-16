using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    public partial class AuthorizePage : Page
    {
        DBConnect1 DB = new DBConnect1();
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();
        public AuthorizePage()
        {
            InitializeComponent();
            ImageVisible.Visibility = Visibility.Hidden;

        }

        private void ImageHide_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBoxPassword.Text = PasswordBx.Password; 
            TextBoxPassword.Visibility = Visibility.Visible; 
            PasswordBx.Visibility = Visibility.Hidden;
            ImageVisible.Visibility = Visibility.Visible;
            ImageHide.Visibility = Visibility.Hidden;
        }

        private void ImageVisible_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ImageHide.Visibility = Visibility.Visible;
            ImageVisible.Visibility = Visibility.Hidden;
            PasswordBx.Password = TextBoxPassword.Text; 
            TextBoxPassword.Visibility = Visibility.Hidden;
            PasswordBx.Visibility = Visibility.Visible; 
        }

        private void ButtonLogIn_Click(object sender, RoutedEventArgs e)
        {
            table.Rows.Clear();
            var loginUser = TextBoxLogin.Text;
            var passwordUser = PasswordBx.Password; 
           
            string QueryString = $"select IDuser, LoginUser, PasswordUser, IsAdmin, IsManager from Authorize where LoginUser = '{loginUser}' and PasswordUser ='{passwordUser}'";
            SqlCommand command = new SqlCommand(QueryString, DB.getConnection());
       
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (TextBoxLogin.Text.Length > 0)
            {
                if (PasswordBx.Password.Length > 0) 
                { 
                    if (table.Rows.Count == 1)
                    {
                        var user = new CheckUsers(table.Rows[0].ItemArray[1].ToString(), Convert.ToBoolean(table.Rows[0].ItemArray[3]));
                        if (user.IsAdmin == true)
                        {
                            MessageBox.Show("Вы успешно вошли как Администратор!");
                            Manager.MngrMainFrame.Navigate(new MainPage(user));
                        }
                        else 
                        {
                            MessageBox.Show("Вы успешно вошли как Пользователь!");
                            Manager.MngrMainFrame.Navigate(new MainPage(user));
                        }
                    }
                    else MessageBox.Show("Неверно введен логин или пароль!");
                
                }else { MessageBox.Show("Введите пароль!"); }
                
            }
            else { MessageBox.Show("Введите логин!"); }
        }
    }
}
