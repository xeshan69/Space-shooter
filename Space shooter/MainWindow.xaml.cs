using System.Text;
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

namespace Space_shooter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        Random rand = new Random();   
        bool moveLeft,moveRight;
        int leftmovespeed=15,rightmovespeed =15;
        int enemyrandom;
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromMilliseconds(20);

            timer.Tick += Playermovement;
            timer.Start();
            mycanvas.Focus();
        }

        private void Playermovement(object? sender, EventArgs e)
        {
            if (moveLeft == true && Canvas.GetLeft(player) > 0) 
            {
                //leftmovecount++;
                Canvas.SetLeft(player,Canvas.GetLeft(player)-leftmovespeed);
            }
            if (moveRight == true )
            {
                //rightmovecount++;
                Canvas.SetLeft(player,Canvas.GetLeft(player)+rightmovespeed);
            }
        }   
            

        private void mycanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                moveLeft = true;
              
            }
            if (e.Key == Key.Right)
            {
                moveRight = true;
                
            }
            if (e.Key == Key.Space)
            {
                Rectangle newbullet = new Rectangle
                {
                    Tag = "bullet",
                    Height = 15,
                    Width = 4,
                    Fill = Brushes.White,
                    Stroke = Brushes.Red
                };
                
                Canvas.SetLeft(newbullet, Canvas.GetLeft(player) + player.Width / 2-1 );
                Canvas.SetTop(newbullet, Canvas.GetTop(player) - newbullet.Height);
                mycanvas.Children.Add(newbullet);

            }

        }
        private void mycanvas_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                moveLeft = false;

            }
            if (e.Key == Key.Right)
            {
                moveRight = false;
            }
        }
        private void enemyspawn()
        {
            ImageBrush enemy = new ImageBrush();
            enemyrandom = rand.Next(1, 5);
            switch (enemyrandom) 
            { 

                case 1:
                    enemy.ImageSource = new BitmapImage(new Uri("/images/1.png"));
                    break;
                    
                case 2:
                    enemy.ImageSource = new BitmapImage(new Uri("/images/2.png"));
                    break;
                case 3:
                    enemy.ImageSource = new BitmapImage(new Uri("/images/3.png"));
                    break;
                case 4:
                    enemy.ImageSource = new BitmapImage(new Uri("/images/4.png"));
                    break;
                case 5:
                    enemy.ImageSource = new BitmapImage(new Uri("/images/5.png"));
                    break;
               
            }
            Rectangle newenemy = new Rectangle
            {
                Tag = "newenemy",
                Height = 20,
                Width = 20,
                Fill = enemy

            };
            Canvas.SetTop(newenemy, 40);
            Canvas.SetLeft(newenemy,rand.Next(30,400)); 
            mycanvas.Children.Add(newenemy);
        }
      
    }
}