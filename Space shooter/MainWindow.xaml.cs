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
           
        }


        private void Playermovement(object sender, EventArgs e)
        {
            Rect playerhitbox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);
            enemycounter -= 1;

            scoretxt.Content = "Score: " + score;
            damagetxt.Content = "Damage: " + damage;

             // Adjust enemy spawning speed 
            if (score >= 50 && score < 100)
            {
                limit = 45; 
            }
            else if (score >= 100 && score < 150)
            {
                limit = 40; 
            }
            else if (score >= 150)
            {
                limit = 35; 
            }
            else if(score>=250) 
            {
                limit = 25;
            }


            if (enemycounter < 0)
            {
                enemyspawn();
                enemycounter = limit;
            }

            if (moveLeft && Canvas.GetLeft(player) > 0)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - leftmovespeed);
            }
            if (moveRight && Canvas.GetLeft(player) < 340)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + rightmovespeed);
            }

            // Bullet Firing and Collision Detection
            foreach (var x in mycanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "bullet")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) - 20);
                    Rect bullethitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (Canvas.GetTop(x) < 10)
                    {
                        itemremover.Add(x);
                    }

                    // Check collision between bullet and enemies
                    foreach (var y in mycanvas.Children.OfType<Rectangle>().Where(r => (string)r.Tag == "enemyspawn"))
                    {
                        Rect enemyhitbox = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);
                        if (bullethitbox.IntersectsWith(enemyhitbox))
                        {
                            itemremover.Add(x); 
                            itemremover.Add(y); 
                            score += 10; 
                        }
                    }
                }

                if ((string)x.Tag == "enemyspawn")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) + enemyspeed);
                    Rect enemyhitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                    if (Canvas.GetTop(x) > 480)
                    {
                        itemremover.Add(x);
                        damage += 5;
                    }

                    if (playerhitbox.IntersectsWith(enemyhitbox))
                    {
                        itemremover.Add(x);
                        damage += 10;
                    }
                }
            }

            foreach (Rectangle i in itemremover)
            {
                mycanvas.Children.Remove(i);
            }
            itemremover.Clear();

            if (damage >= 100)
            {
                EndGame();
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

        private void EndGame()
        {
            timer.Stop();
            MessageBoxResult result = MessageBox.Show("Game Over! Your score is " + score + ". Do you want to restart?", "Game Over", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                RestartGame();
            }
            else
            {
                Close();
            }
        }

        private void RestartGame()
        {
            
            score = 0;
            damage = 0;
            enemycounter = 100;
            mycanvas.Children.Clear();
            mycanvas.Children.Add(player);

            // Restart the Game
            timer.Start();
        }
    }
}
    