using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Life
{

    public partial class Form1 : Form
    {//Создаем объект класса Random для генерации случайных чисел
        //Создаем массив по размеру равный количеству ячеек на поле 20х20
        Random rnd = new Random();
        int[,] A = new int[20, 20];

        //Генерируем карту и заполняем ее случайно сгенерированными числами 0(мертвая клетка) или 1(живая клетка)
        public void GenerateMap()
        {
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                    A[i, j] = rnd.Next(2);
        }

        //Показываем какие метки мертвые(черные), а какие живые(желные)

        public void ShowMap()
        {
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                {
                    Label a = tableLayoutPanel1.Controls[i * 20 + j] as Label;
                    if (A[i, j] == 0)
                        a.ForeColor = Color.Yellow;
                    else
                        a.ForeColor = Color.Black;
                }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Задаем свойства каждой ячейки на поле
            this.Width = 400;
            this.Height = 400;
            for(int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                {
                    //Формируем новую метку
                    Label a = new Label();
                    a.AutoSize = false;
                    a.Dock = DockStyle.Fill;
                    a.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    a.Font = new System.Drawing.Font("wingdings", 8);
                    a.Text = Convert.ToString((char)84);
                    a.ForeColor = Color.Yellow;
                    tableLayoutPanel1.Controls.Add(a, j, i);
                }

            //Запускаем генерацию карты
            GenerateMap();
            ShowMap();
        }
    }
}
