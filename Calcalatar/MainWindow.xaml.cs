using System;
using System.Collections.Generic;
using System.Data;
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

namespace Calcalatar
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool IsAllowedInput(string input)
        {
            return input.All(c => char.IsDigit(c) || "+-*/.=<".Contains(c));
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsAllowedInput(e.Text))
            {
                e.Handled = true;
            }
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            string input = e.Key.ToString();
            AddTextToTextBox(input);
            e.Handled = true;
        }

        private void AddTextToTextBox(string text)
        {
            resultTextBox.Text += text;
        } 

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string input = button.Content.ToString();
            
            //AddTextToTextBox(input);

            if (!IsAllowedInput(input))
            {
                return;
            }

            resultTextBox.Text += input; // Добавляем вводимые символы к тексту в нижнем TextBox
        }

        private void CEButton_Click(object sender, RoutedEventArgs e)
        {
            resultTextBox.Text = string.Empty;
        }
        private void CButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string input = button.Content.ToString();

            if (input == "C") // Если нажата кнопка "C"
            {
                resultTextBox.Text = string.Empty; // Очищаем нижнее текстовое поле
                PrevTextBox.Text = string.Empty; // Очищаем верхнее текстовое поле
            }
            else
            {
                if (!IsAllowedInput(input))
                {
                    return;
                }

                resultTextBox.Text += input; // Добавляем вводимые символы к тексту в нижнем TextBox
            }
        }

        private string savedExpression = string.Empty;

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            string expression = resultTextBox.Text; // Получаем выражение из нижнего текстового поля
            DataTable dataTable = new DataTable();
            try
            {
                object result = dataTable.Compute(expression, "");
                resultTextBox.Text = result.ToString();
                savedExpression = expression;
                PrevTextBox.Text = savedExpression; // Отображаем сохраненное выражение в верхнем текстовом поле
            }
            catch (Exception ex)
            {
                resultTextBox.Text = "Ошибка: " + ex.Message;
            }
        }

    }
}
