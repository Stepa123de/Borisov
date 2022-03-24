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
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Random ranGen = new Random();
        private DispatcherTimer timer = new DispatcherTimer();
        private int countStripes = 0,level = 1;
        public MainWindow()
        {
            InitializeComponent();

            timer.Tick += TimerTick;
            Title = "Start";
            addMainButton("Play",80);
        }

        private void addMainButton(string text,int fontsize) 
        {
            Button btn = new Button();
            btn.Margin = new Thickness(246, 0, 160, 0);
            btn.HorizontalAlignment = HorizontalAlignment.Left;
            btn.Height = 120;
            btn.Width = 300;
            btn.FontSize = fontsize;
            btn.Content = text;
            btn.Click += Button_Start_Click;
            grid.Children.Add(btn);
        }
        private void GameStart() 
        {
            Title = "Level" + level;
            CreateStripes(10);
            timer.Interval = TimeSpan.FromMilliseconds(2000);
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (countStripes == 0)
            {
                if (level == 1)
                {
                    CreateStripes(10);
                    timer.Interval = TimeSpan.FromMilliseconds(1600);
                    level++;
                    Title = "Level" + level;
                }
                else if (level == 2)
                {
                    CreateStripes(10);
                    timer.Interval = TimeSpan.FromMilliseconds(1300);
                    level++;
                    Title = "Level" + level;
                }
                else if (level == 3) 
                {
                    CreateStripes(10);
                    timer.Interval = TimeSpan.FromMilliseconds(900);
                    level++;
                    Title = "Level" + level;
                }
                else 
                {
                    level = 1;
                    countStripes = 0;
                    RePlay("You win!");
                }
                
            }
            else if (countStripes > 30)
            {
                grid.Children.Clear();
                level = 1;
                countStripes = 0;
                RePlay("You lose!");
            }
            else 
            {
                CreateStripes(1);
            }

        }

        private void CreateStripes(int num)
        {
            while (num-->0) 
            {

                Rectangle rect = new Rectangle();

                int flag = ranGen.Next(2);

                if (flag == 1)
                {
                    rect.Width = 250;
                    rect.Height = 50;
                }
                else 
                {
                    rect.Width = 50;
                    rect.Height = 250;
                }
                

                byte r =(byte) ranGen.Next(256);
                byte g = (byte)ranGen.Next(256);
                byte b = (byte)ranGen.Next(256);

                rect.Fill = new SolidColorBrush(Color.FromRgb(r,g,b));
                rect.Stroke = new SolidColorBrush(Colors.Black);
                rect.HorizontalAlignment = HorizontalAlignment.Left;
                rect.VerticalAlignment = VerticalAlignment.Top;

                int x = ranGen.Next((int)(this.Width - rect.Width) -50);
                int y = ranGen.Next((int)(this.Height - rect.Height) -50);

                rect.Margin = new Thickness(x, y, 0, 0);

                rect.MouseLeftButtonDown += RectMouseLeftButtonDown;

                grid.Children.Add(rect);

                countStripes++;
            }
        }


        private void RePlay(string text) 
        {
            timer.Stop();
            addMainButton(text + " Replay?",38);
            Title = "Replay";
        }

        private void RectMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rect = (Rectangle) sender;
            int index = grid.Children.IndexOf(rect);
            Rect rect1 = new Rect(rect.Margin.Left, rect.Margin.Top, rect.Width, rect.Height);

            for (int i = index + 1; i < grid.Children.Count; i++) 
            {
                Rectangle recti = (Rectangle)grid.Children[i];
                Rect rect2 = new Rect(recti.Margin.Left, recti.Margin.Top, recti.Width, recti.Height);

                if (rect1.IntersectsWith(rect2)) return;
            }

            grid.Children.Remove(rect);

            countStripes--;


        }
        private void Button_Start_Click(object sender, RoutedEventArgs e)
        {
            
            Button MyButton = (Button)sender;
            grid.Children.Remove(MyButton);
            GameStart();
            //grid.Children.Remove((Button)sender);
        }
    }
}
