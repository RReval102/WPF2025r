using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        private Label revalLabel;
        private TextBox inputTextBox;
        private Button submitButton;
        private Label outputLabel;

        public MainWindow()
        {
            // Настройки окна
            Title = "Reval App";
            Width = 400;
            Height = 300;

            // Создаем Grid (основной контейнер)
            Grid grid = new Grid();
            Content = grid;

            // StackPanel для центрирования элементов
            StackPanel stackPanel = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            grid.Children.Add(stackPanel);

            // 1. Надпись "reval"
            revalLabel = new Label
            {
                Content = "reval",
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.Blue,
                Margin = new Thickness(0, 20, 0, 0)
            };
            stackPanel.Children.Add(revalLabel);

            // 2. Поле ввода
            inputTextBox = new TextBox
            {
                Width = 200,
                Margin = new Thickness(0, 20, 0, 0)
            };
            stackPanel.Children.Add(inputTextBox);

            // 3. Кнопка
            submitButton = new Button
            {
                Content = "Вывести текст",
                Width = 120,
                Margin = new Thickness(0, 20, 0, 0)
            };
            submitButton.Click += SubmitButton_Click;
            stackPanel.Children.Add(submitButton);

            // 4. Label для вывода
            outputLabel = new Label
            {
                FontSize = 12,
                Margin = new Thickness(0, 20, 0, 0)
            };
            stackPanel.Children.Add(outputLabel);
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            outputLabel.Content = "Вы ввели: " + inputTextBox.Text;
        }
    }
}