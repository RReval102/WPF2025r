using System;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        private TextBox inputTextBox;  // Поле для ввода
        private Button submitButton;    // Кнопка
        private Label outputLabel;     // Вывод текста
        private Label revalLabel;       // Надпись "reval" 
            
        public Form1()
        {
            InitializeComponent();

            // Настройки окна
            this.Text = "Reval App";
            this.Width = 400;
            this.Height = 300;

            // 1. Надпись "reval" (верхний Label)
            revalLabel = new Label();
            revalLabel.Text = "reval";
            revalLabel.AutoSize = true;
            revalLabel.Font = new Font("Arial", 14, FontStyle.Bold);
            revalLabel.ForeColor = Color.Blue;
            revalLabel.Location = new Point(
                (this.ClientSize.Width - revalLabel.Width) / 2,  // Центрирование по X
                20                                                // Отступ сверху 20px
            );
            this.Controls.Add(revalLabel);

            // 2. Поле ввода (TextBox)
            inputTextBox = new TextBox();
            inputTextBox.Width = 200;
            inputTextBox.Location = new Point(50, 70);  // Под надписью "reval"
            this.Controls.Add(inputTextBox);

            // 3. Кнопка (Button)
            submitButton = new Button();
            submitButton.Text = "Вывести текст";
            submitButton.Location = new Point(50, 110);
            submitButton.Click += SubmitButton_Click;
            this.Controls.Add(submitButton);

            // 4. Label для вывода
            outputLabel = new Label();
            outputLabel.AutoSize = true;
            outputLabel.Location = new Point(50, 150);
            outputLabel.Font = new Font("Arial", 12, FontStyle.Regular);
            this.Controls.Add(outputLabel);
        }

        // Обработчик нажатия кнопки
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            outputLabel.Text = "Вы ввели: " + inputTextBox.Text;
        }
    }
}