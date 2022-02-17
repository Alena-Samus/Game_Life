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
                        a.ForeColor = Color.Black;
                    else
                        a.ForeColor = Color.Yellow;
                }
        }
         //Считаем количество живых соседей вокруг конкретной клетки на текущей итерации
         //Состояние 2 - клетка живая, но она умрет на следующей итерации; 3 - клетка мертвая, но она родится на следующей итерации
         public int Calc(int x, int y)//Координаты клетки, около которых считаем количество живых клеток
        {
            int k = 0;
            if (x > 0 && (A[x - 1, y] == 1 || A[x - 1, y] == 2))
                k++;
            if (y > 0 && (A[x, y - 1] == 1 || A[x, y - 1] == 2))
                k++;

            if (x < 19 && (A[x + 1, y] == 1 || A[x + 1, y] == 2))
                k++;
            if (y < 19 && (A[x, y + 1] == 1 || A[x, y + 1] == 2))
                k++;

            if (x > 0 && y > 0 && (A[x - 1, y - 1] == 1 || A[x - 1, y - 1] == 2))
                k++;

            if (x < 19 && y < 19 && (A[x + 1, y + 1] == 1 || A[x + 1, y + 1] == 2))
                k++;

            if (x > 0 && y < 19 && (A[x - 1, y + 1] == 1 || A[x - 1, y + 1] == 2))
                k++;

            if (x < 19 && y > 0 && (A[x + 1, y - 1] == 1 || A[x + 1, y - 1] == 2))
                k++;

            return k;
        }

        //Метод, который проживает жизнь колонии на текущей итерации.

        public void Evolution (object sender, EventArgs e)
        {//Первый проход массива - смотрим состояние клеток и определяем их возможные состояния
            for (int i = 0; i < 20; i++)
                for(int j = 0; j < 20; j++)
                {
                    int t = Calc(i, j);
                    if (A[i, j] == 0 && t == 3)
                        A[i, j] = 3; //Если клетка мертвая, но вокруг нее есть 3 живых соседа, переводим ее в состояние планирующей родиться
                    
                    if (A[i, j] == 1 && (t < 2 || t > 3))
                        A[i, j] = 2;//Если клетка живая, но вокруг нее есть меньше 2 живых клеток или больше 3, переводим ее в состояние планирующей умереть

                }
            //Второй проход по массиву - закрепляем планируемые состояния клеток
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                {
                    if (A[i, j] == 3)
                        A[i, j] = 1;//Если клетка планировала родиться, делаем ее живой

                    if (A[i, j] == 2)
                        A[i, j] = 0;//Если клетка планировала умереть, делаем ее мертвой
                }

            ShowMap();//Показываем карту
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
                    tableLayoutPanel1.Controls.Add(a, j, i);
                }

            //Запускаем генерацию карты
            GenerateMap();
            ShowMap();
        }
    }
}
