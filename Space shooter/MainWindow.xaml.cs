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
        List<Rectangle> itemremover = new List<Rectangle>();
        Random rand = new Random();   
        bool moveLeft,moveRight;
        int leftmovespeed=10,rightmovespeed =10;
        int enemyrandom;
        int enemyspeed = 5;
        int enemycounter = 100;
        int limit = 50;
        int score;
        int damage;
       
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromMilliseconds(20);

            timer.Tick += Playermovement;
          
            timer.Start();
            mycanvas.Focus();
            //enemyspawn();
        }

        private void Playermovement(object? sender, EventArgs e)
        {
            Rect playerhitbox= new Rect(Canvas.GetTop(player),Canvas.GetLeft(player),player.Height,player.Width);
            enemycounter -= 1;
            
            scoretxt.Content = "Score: " + score;
            damagetxt.Content = "Damage: " + damage;
            if (enemycounter < 0)
            {
                enemyspawn();
                enemycounter = limit;
            }


            if (moveLeft == true && Canvas.GetLeft(player) > 0)
            {

                Canvas.SetLeft(player, Canvas.GetLeft(player) - leftmovespeed);
            }
            if (moveRight == true && Canvas.GetLeft(player) < 340)
            {

                Canvas.SetLeft(player, Canvas.GetLeft(player) + rightmovespeed);
            }

            //Bullet Firring//
            foreach (var a in mycanvas.Children.OfType<Rectangle>())
            {

                if (a is Rectangle && (string)a.Tag == "bullet")
                {
                    Canvas.SetTop(a, Canvas.GetTop(a) - 20);
                    Rect bullethitbox = new Rect(Canvas.GetLeft(a), Canvas.GetTop(a), a.Height, a.Width);
                    if (Canvas.GetTop(a) < 10)
                    {
                        itemremover.Add(a);

                    }
                    
                }



                if (a is Rectangle && (string)a.Tag == "enemyspawn")
                {
                    Canvas.SetTop(a, Canvas.GetTop(a) + enemyspeed);

                    if (Canvas.GetTop(a) > Canvas.GetTop(player)+100)
                    {
                        itemremover.Add(a);
                        damage += 1;
                    }
                    Rect enemyhitbox = new Rect(Canvas.GetLeft(a), Canvas.GetTop(a), a.Height, a.Width);
                    

                    if (playerhitbox.IntersectsWith(enemyhitbox))
                    {
                        MessageBox.Show("sex sux");
                        itemremover.Add(a);
                        damage += 10;
                        
                    }
                    
                }
            }
            foreach(Rectangle i in itemremover)
            {
                mycanvas.Children.Remove(i);
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
                    enemy.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/1.png"));
                                       
                    break;
                    
                case 2:
                    enemy.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/2.png"));
                    
                    break;
                case 3:
                    enemy.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/3.png"));
                    
                    break;
                case 4:
                    enemy.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/4.png"));
                   
                    break;
                case 5:
                    enemy.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/5.png"));
                    
                    break;
               
            }
            Rectangle newenemy = new Rectangle
            {
                Tag = "enemyspawn",
                Height = 30,
                Width = 30,
                Fill = enemy

            };
            Canvas.SetTop(newenemy, -100);
            Canvas.SetLeft(newenemy,rand.Next(20,350)); 
            mycanvas.Children.Add(newenemy);
        }
      
    }
}