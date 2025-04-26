using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using angrybirds;
using Microsoft.Win32;
using System.Windows.Shapes;

namespace angrybirds
{
    public class Game : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new Game());
        }

        private List<TextBox> textBoxes = new List<TextBox>();
        private string[] astrLabel = { "_Скорость:",  "_Угол наклона:",
                "_Масса:",
                "_Коэфициент сопротивления воздуха:" };
        private TextBox txtOutput;
        private Canvas canv;

        public Game()
        {
            Title = "Game";

            Grid grid = new Grid();
            grid.Margin = new Thickness(5);
            grid.ShowGridLines = false;

            ColumnDefinition coldef = new ColumnDefinition();
            coldef.Width = new GridLength(200, GridUnitType.Auto);
            grid.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition();
            coldef.Width = new GridLength(100, GridUnitType.Auto);
            grid.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition();
            coldef.Width = new GridLength(400, GridUnitType.Star);
            grid.ColumnDefinitions.Add(coldef);

            coldef = new ColumnDefinition();
            coldef.Width = new GridLength(400, GridUnitType.Auto);
            grid.ColumnDefinitions.Add(coldef);

            for (int i = 0; i < 5; i++)
            {
                RowDefinition rowdef = new RowDefinition();
                rowdef.Height = GridLength.Auto;
                grid.RowDefinitions.Add(rowdef);
            }

            for (int i = 0; i < astrLabel.Length; i++)
            {
                Label lbl = new Label();
                lbl.Content = astrLabel[i];
                lbl.VerticalContentAlignment = VerticalAlignment.Center;
                grid.Children.Add(lbl);
                Grid.SetRow(lbl, i);
                Grid.SetColumn(lbl, 0);
                TextBox txtbox = new TextBox();
                txtbox.Margin = new Thickness(5);
                grid.Children.Add(txtbox);
                Grid.SetRow(txtbox, i);
                Grid.SetColumn(txtbox, 1);
                textBoxes.Add(txtbox);
            }

            Button btn = new Button();
            btn.Content = "Запуск";
            btn.Margin = new Thickness(5);
            btn.IsDefault = true;
            btn.Click += buttonFly_Click;
            grid.Children.Add(btn);
            Grid.SetRow(btn, 4);
            Grid.SetColumn(btn, 1);
            grid.Children[1].Focus();

            txtOutput = new TextBox
            {
                Margin = new Thickness(5),
                IsReadOnly = true,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                TextWrapping = TextWrapping.Wrap,
                AcceptsReturn = true,
                MaxHeight = 400,
            };

            grid.Children.Add(txtOutput);
            Grid.SetColumn(txtOutput, 2);
            Grid.SetRowSpan(txtOutput, 5);

            canv = new Canvas
            {
                Width = 300,
                Height = 300,
                Background = Brushes.LightGray,
                Margin = new Thickness(5)
            };

            grid.Children.Add(canv);
            Grid.SetRow(canv, 0);
            Grid.SetColumn(canv, 3);
            Grid.SetRowSpan(canv, 5);

            DockPanel dock = new DockPanel();
            Content = dock;

            Menu menu = new Menu();
            dock.Children.Add(menu);
            DockPanel.SetDock(menu, Dock.Top);
            dock.Children.Add(grid);
            DockPanel.SetDock(grid, Dock.Bottom);


            MenuItem itemFile = new MenuItem();
            itemFile.Header = "_File";
            menu.Items.Add(itemFile);

            MenuItem itemNew = new MenuItem();
            itemNew.Header = "_New";
            itemNew.Click += OpenOnClick;
            itemFile.Items.Add(itemNew);

            MenuItem itemOpen = new MenuItem();
            itemOpen.Header = "_Open";
            itemOpen.Click += OpenOnClick;
            itemFile.Items.Add(itemOpen);

            MenuItem itemSave = new MenuItem();
            itemSave.Header = "_Save";
            itemSave.Click += OpenOnClick;
            itemFile.Items.Add(itemSave);

            itemFile.Items.Add(new Separator()); //рисует горизонтальную разделительную линию
            MenuItem itemExit = new MenuItem();
            itemExit.Header = "_Exit";
            itemExit.Click += ExitOnClick;
            itemFile.Items.Add(itemExit);

            MenuItem itemWindow = new MenuItem();
            itemWindow.Header = "_Window";
            menu.Items.Add(itemWindow);

            MenuItem itemTaskbar = new MenuItem();
            itemTaskbar.Header = "_Show in Taskbar";
            itemTaskbar.IsCheckable = true;
            itemTaskbar.IsChecked = ShowInTaskbar;
            itemTaskbar.Click += TaskbarOnClick;
            itemWindow.Items.Add(itemTaskbar);

            MenuItem itemSize = new MenuItem();
            itemSize.Header = "Size to _Content";
            itemSize.IsCheckable = true;
            itemSize.IsChecked = SizeToContent == SizeToContent.WidthAndHeight;
            itemSize.Checked += SizeOnCheck;
            itemSize.Unchecked += SizeOnCheck;
            itemWindow.Items.Add(itemSize);

            MenuItem itemResize = new MenuItem();
            itemResize.Header = "_Resizable";
            itemResize.IsCheckable = true;
            itemResize.IsChecked = ResizeMode == ResizeMode.CanResize;
            itemResize.Click += ResizeOnClick;
            itemWindow.Items.Add(itemResize);

            MenuItem itemTopmost = new MenuItem();
            itemTopmost.Header = "_Topmost";
            itemTopmost.IsCheckable = true;
            itemTopmost.IsChecked = Topmost;
            itemTopmost.Checked += TopmostOnCheck;
            itemTopmost.Unchecked += TopmostOnCheck;
            itemWindow.Items.Add(itemTopmost);
        }
        void GetData(object sender, RoutedEventArgs args)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.CheckFileExists = true;

            if ((bool)dlg.ShowDialog(this))
            {
                Bird bird = new Bird();
                string inputFile = dlg.FileName;
                string[] inputData = bird.ReadInputData(inputFile);
                for (int i = 0; i < inputData.Length; i++)
                {
                    textBoxes[i].Text = inputData[i];
                }
            }
        }

        void SaveData(object sender, RoutedEventArgs args)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt";
            saveFileDialog.FileName = "Output.txt";

            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, txtOutput.Text);
            }
        }
        void OpenOnClick(object sender, RoutedEventArgs args)

        {
            MenuItem item = sender as MenuItem;
            if ((string) item.Header == "_Open")
            {
                GetData(sender, args);
                buttonFly_Click(sender, args);
            }
            if ((string) item.Header == "_New")
            {
                canv.Children.Clear();
                for (int i = 0; i < textBoxes.Count; i++)
                {
                    textBoxes[i].Text = "";
                }
                txtOutput.Clear();
            }
            if ((string) item.Header == "_Save")
            {
                SaveData(sender, args);
            }
        }

        void ExitOnClick(object sender, RoutedEventArgs args)
        {
            Close();
        }
        void TaskbarOnClick(object sender, RoutedEventArgs args)
        {
            MenuItem item = sender as MenuItem;
            ShowInTaskbar = item.IsChecked;
        }
        void SizeOnCheck(object sender, RoutedEventArgs args)
        {
            MenuItem item = sender as MenuItem;
            SizeToContent = item.IsChecked ? SizeToContent.WidthAndHeight :
                SizeToContent.Manual;
        }
        void ResizeOnClick(object sender, RoutedEventArgs args)
        {
            MenuItem item = sender as MenuItem;
            ResizeMode = item.IsChecked ? ResizeMode.CanResize :
                ResizeMode.NoResize;
        }
        void TopmostOnCheck(object sender, RoutedEventArgs args)
        {
            MenuItem item = sender as MenuItem;
            Topmost = item.IsChecked;
        }

        private void buttonFly_Click(object sender, EventArgs a)
        {
            Bird bird = new Bird();
            bird.Notify += DisplayMessage;

            string outputfile = "output.txt";
            bird.ReadFromTextBox(textBoxes);
            bird.WriteFlightData(outputfile);
            string outputContent = File.ReadAllText(outputfile);
            txtOutput.Text = outputContent;

            List<double> X = bird.GetX();
            List<double> Y = bird.GetY();

            DrawLines(canv, X, Y);
        }
        void DisplayMessage(Bird sender, BirdEventArg e, StreamWriter writer)
        {
            writer.WriteLine(e.Message);
        }
        async void DrawLines(Canvas canv, List<double> X, List<double> Y)
        {
            canv.Children.Clear();

            double maxX = X.Max();
            double maxY = Y.Max();
            double k_x = (canv.ActualWidth) / (maxX);
            double k_y = (canv.ActualHeight) / (maxY);
            double k = Math.Min(k_x, k_y);

            for (int i = 0; i < X.Count - 1 & Y[i + 1] >= 0; i++)
            {
                Line line = new Line
                {
                    X1 = X[i] * k,
                    Y1 = canv.ActualHeight - Y[i] * k,
                    X2 = X[i + 1] * k,
                    Y2 = canv.ActualHeight - Y[i + 1] * k,
                    Stroke = Brushes.Green,
                    StrokeThickness = 4
                };
                canv.Children.Add(line);
                await Task.Delay(1);
            }
        }
    }
}

