using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace BMR__Progect
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            List<string> values__sex = new List<string>
            {
                "Женский",
                "Мужской",
            };

            // Присваиваем список значений ComboBox
            Age__Combobox.ItemsSource = values__sex;

            List<string> values__activity = new List<string>
            {
                "Сидячий",
                "Немного активный",
                "Средняя активность",
                "Большая активность",
                "Экстра нагрузка"
            };

            // Присваиваем список значений ComboBox
            Activity__Combobox.ItemsSource = values__activity;

            
        }
        public double bmr;
        private void Weight__Input_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out int parsedValue))
            {
                e.Handled = true;
            }
            else
            {
                string newText = ((TextBox)sender).Text + e.Text;
                if (int.Parse(newText) > 250)
                {
                    e.Handled = true; 
                }
            } 
            
            Regex regex = new Regex("[^0-9,]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Age__Input_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out int parsedValue))
            {
                e.Handled = true; 
            }
            else
            {
                string newText = ((TextBox)sender).Text + e.Text;
                if (int.Parse(newText) > 85)
                {
                    e.Handled = true; 
                }
            }
            
            Regex regex = new Regex("[^0-9,]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Height__Input_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
           
            if (!int.TryParse(e.Text, out int parsedValue))
            {
                e.Handled = true;
            }
            else
            {
                string newText = ((TextBox)sender).Text + e.Text;
                if (int.Parse(newText) > 200)
                {
                    e.Handled = true;
                }
            }
            Regex regex = new Regex("[^0-9],");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void Weight__Input_GotFocus(object sender, RoutedEventArgs e)
        {
            Weight__Input.Text = "";
        }

        private void Age__Input_GotFocus(object sender, RoutedEventArgs e)
        {
            Age__Input.Text = "";
        }

        private void Height__Input_GotFocus(object sender, RoutedEventArgs e)
        {
            Height__Input.Text = "";
        }

 
        private void clear__button3_Click(object sender, RoutedEventArgs e)
        {
            Height__Input.Text = "";
            Weight__Input.Text = "";
            Age__Input.Text = "";
        }

        private void close__button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Age__Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Weight__Input.Text = "Вес/кг";
            Age__Input.Text = "Возраст/лет";
            Height__Input.Text = "Рост/см";
        }

        //private void Activity__Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    Weight__Input.Text = "Вес/кг";
        //    Age__Input.Text = "Возраст/лет";
        //    Height__Input.Text = "Рост/см";
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(Age__Combobox.Text))
            {
                MessageBox.Show("Пожалуйста, выберите ваш пол перед выполнением расчета BMR.");
                return;
            }

            else
            {
                if (!IsValidInput(Weight__Input.Text) ||
                !IsValidInput(Height__Input.Text) ||
                !IsValidInput(Age__Input.Text))
                {
                MessageBox.Show("Пожалуйста, введите корректные значения для веса, роста и возраста.");
                return;
                }

                double weight = double.Parse(Weight__Input.Text); 
                double height = double.Parse(Height__Input.Text); 
                int age = int.Parse(Age__Input.Text);


                string gender = Age__Combobox.Text; 

                double s = (gender == "Мужской") ? 5 : -161;
                bmr = 10 * weight + 6.25 * height - 5 * age + s;

                MessageBox.Show($"Ваш базовый метаболический показатель (BMR): {bmr} ккал/день");

            }

            

        }
        

        private bool IsValidInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            if (double.TryParse(input, out double value))
            {
                if (value == 0)
                    return false;
            }
            else
            {
                return false;
            }

            return true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Activity__Combobox.Text))
            {
                MessageBox.Show("Пожалуйста, выберите вашу активность перед рассчетом TDEE.");
                return;
            }

            if (bmr == 0)
            {
                MessageBox.Show("Пожалуйста, перед рассчетом TDEE рассчитайте BMR");
            }
            else
            {
                double tdee;
                switch (Activity__Combobox.Text)
                {
                    case "Сидячий":
                        tdee = bmr * 1.2;
                        MessageBox.Show($"Ваш показатель (TDEE): {tdee} ккал/день");
                        break;
                    case "Немного активный":
                        tdee = bmr * 1.375;
                        MessageBox.Show($"Ваш показатель (TDEE): {tdee} ккал/день");
                        break;
                    case "Средняя активность":
                        tdee = bmr * 1.55;
                        MessageBox.Show($"Ваш показатель (TDEE): {tdee} ккал/день");
                        break;
                    case "Большая активность":
                        tdee = bmr * 1.725;
                        MessageBox.Show($"Ваш показатель (TDEE): {tdee} ккал/день");
                        break;
                    case "Экстра нагрузка":
                        tdee = bmr * 1.9;
                        MessageBox.Show($"Ваш показатель (TDEE): {tdee} ккал/день");
                        break;
                    default:
                        Console.WriteLine(":-)");
                        return;
                }

            }
        }
    }
}
