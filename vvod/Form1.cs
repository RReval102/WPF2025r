//Садрисламов Реваль ПМ-201, простейший ввод-вывод в окна
using System;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    // Главная форма приложения
    public partial class Form1 : Form
    {
        // Конструктор формы
        public Form1()
        {
            // Инициализация компонентов формы (созданных в дизайнере)
            InitializeComponent();

            // Настройки окна
            this.Text = "Reval App";      // Устанавливаем заголовок окна
            this.Width = 300;              // Ширина окна в пикселях
            this.Height = 200;             // Высота окна в пикселях

            // Создаем элемент Label (надпись) с текстом "reval"
            Label label = new Label();
            label.Text = "reval";           // Текст надписи
            label.AutoSize = true;         // Автоматический размер под текст
            label.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold); // Шрифт Arial 14pt жирный
            label.ForeColor = System.Drawing.Color.Blue; // Цвет текста - синий

            // Центрируем надпись относительно формы:
            // 1. Вычисляем позицию по горизонтали: (ширина формы - ширина надписи) / 2
            label.Left = (this.ClientSize.Width - label.Width) / 2;
            // 2. Вычисляем позицию по вертикали: (высота формы - высота надписи) / 2
            label.Top = (this.ClientSize.Height - label.Height) / 2;

            // Добавляем созданную надпись на форму
            this.Controls.Add(label);
        }
    }
}