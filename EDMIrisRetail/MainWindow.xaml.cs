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

namespace EDMIrisRetail
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public  partial class MainWindow : Window
    {
      string Login = "";

      string Pass = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Enter_Click(object sender, RoutedEventArgs e)
        {
          Login = textBoxLogin.Text.Trim();

          Constants.LogIn = Login;

          Pass = textBoxPass.Password.Trim();

          Constants.PassWord = Pass;

            if (Login == Constants.DefaultLogin && Pass == Constants.DefaultPassword)
            {
                textBoxLogin.BorderBrush = Brushes.Transparent;
                textBoxPass.BorderBrush = Brushes.Transparent;
                textBoxLogin.ToolTip = "";
                textBoxPass.ToolTip = "";
                AdminPanel adminPanel = new AdminPanel();
                adminPanel.Show();
                Hide();
            }
            else
                if(Login.Length < 4 || !Login.Contains(Constants.DefaultLogin))
            {
                textBoxLogin.ToolTip = "Введен неверный логин!";
                textBoxLogin.BorderBrush = Brushes.Red;
            }
            else
                if(Pass.Length < 5 || !Pass.Contains(Constants.DefaultPassword))
            {
                textBoxPass.ToolTip = "Введен неверный пароль!";
                textBoxPass.BorderBrush = Brushes.Red;
            }
            else
                if(Login == Constants.DefaultLogin && Pass.Length < 5 || !Pass.Contains(Constants.DefaultPassword))
            {
                textBoxLogin.BorderBrush = Brushes.Transparent;
                textBoxLogin.ToolTip = "";
                textBoxPass.ToolTip = "Введен неверный пароль!";
                textBoxPass.BorderBrush = Brushes.Red;
            }
        }
    }
}
