using System;
using System.Drawing;
using System.Windows.Forms;

namespace BrickBreaker
{ //designed by Jayden Roddick
    
    public class Ball
    {
        public int x, y, xSpeed, ySpeed, size;
        public Color colour;
        

        public static Random rand = new Random();

        public Ball(int _x, int _y, int _xSpeed, int _ySpeed, int _ballSize)
        {
            x = _x;
            y = _y;
            xSpeed = _xSpeed;
            ySpeed = _ySpeed;
            size = _ballSize;
               
        }
        public void MoveWithPaddle(string direction, Paddle p)
        {
            if (direction == "left")
            {
                x -= p.speed;
            }
            if (direction == "right")
            {
                x += p.speed;
            }
        }
        
        public void Move()
        {
            x = x - xSpeed;
            y = y - ySpeed;
        }

        public bool BlockCollision(Block b)
        {
            Rectangle blockRec = new Rectangle(b.x, b.y, b.width, b.height);
            Rectangle ballRec = new Rectangle(x, y, size, size);

            if (ballRec.IntersectsWith(blockRec))
            {
                ySpeed *= -1;
            }
            return blockRec.IntersectsWith(ballRec);         
        }

        public void PaddleCollision(Paddle p)
        {
            Rectangle ballRec = new Rectangle(x + 5, y + 5, size + 5, size + 5);
            Rectangle paddleRec = new Rectangle(p.x + 5, p.y + 5, p.width + 5, p.height + 5);
            if (ballRec.IntersectsWith(paddleRec))
            {

                ySpeed *= -1;

                int random = rand.Next(1, 101); 

                if (random < 26)
                {
                    ySpeed += 1;
                    xSpeed += 1;
                }
  

                int value = rand.Next(1, 51);

                if (value <= 25)
                {
                    xSpeed *= -1;
                }
                else
                {
                    xSpeed *= 1;
                }

            }
        }

        public void WallCollision(UserControl UC)
        {
            // Collision with left wall
            if (x <= 0)
            {
                xSpeed *= -1;
            }
            // Collision with right wall
            if (x >= (UC.Width - size))
            {
                xSpeed *= -1;
            }
            // Collision with top wall
            if (y <= 2)
            {
                ySpeed *= -1;
            }
        }

        public bool BottomCollision(UserControl UC)
        {
            Form1.didCollide = false;

            if (y >= UC.Height)
            {
                Form1.didCollide = true;
            }

            return Form1.didCollide;
        }

        public void SheildCollistion ()
        {
            ySpeed *= -1;
            xSpeed *= -1;
        }
    }
}
