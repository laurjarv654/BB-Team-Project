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
        public void MoveWithPaddle(string direction)
        {
            if (direction == "left")
            {
                x -= xSpeed;
            }
            if (direction == "right")
            {
                x += xSpeed;
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
            Rectangle ballRecSL = new Rectangle(x, y, size, size);
            Rectangle ballRecSR = new Rectangle(x, y, size, size);
            Rectangle ballRecSB = new Rectangle(x, y, size, size);
            if (ballRec.IntersectsWith(blockRec))
            {
                ySpeed *= -1;
            }
            if (ballRecSL.IntersectsWith(blockRec))
            {
                ySpeed *= -1;
            }
            if (ballRecSR.IntersectsWith(blockRec))
            {
                ySpeed *= -1;
            }
            if (ballRecSB.IntersectsWith(blockRec))
            {
                ySpeed *= -1;
            }
            return blockRec.IntersectsWith(ballRec);         
        }

        public void PaddleCollision(Paddle p, bool pMovingLeft, bool pMovingRight)
        {
            Rectangle ballRec = new Rectangle(x, y, size, size);
            Rectangle paddleRec = new Rectangle(p.x, p.y, p.width, p.height);
            Rectangle paddleRecSL = new Rectangle(p.x + 10, p.y + 10, p.height, p.width + 20);
            if (ballRec.IntersectsWith(paddleRec))
            {
                xSpeed *= -1;
                ySpeed *= -1;

                if (pMovingLeft)
                {
                    if (p.y == y)
                    {
                        xSpeed = -Math.Abs(xSpeed);
                        ySpeed *= -1;
                    }
                    else if (p.y + p.height == y)
                    {
                        xSpeed = -Math.Abs(xSpeed);
                        ySpeed *= -1;
                    }
                    if (p.x == x)
                    {
                        xSpeed = -Math.Abs(xSpeed);
                        ySpeed *= -1;
                    }
                    else if (p.x + p.width == x)
                    {
                        xSpeed = -Math.Abs(xSpeed);
                        ySpeed *= -1;
                    }
                    
                } 
                else if (pMovingRight)
                {
                    if (p.y == y)
                    {
                        xSpeed = -Math.Abs(xSpeed);
                        ySpeed *= -1;
                    }
                    else if (p.y + p.height == y)
                    {
                        xSpeed = -Math.Abs(xSpeed);
                        ySpeed *= -1;
                    }
                    if (p.x == x)
                    {
                        xSpeed = -Math.Abs(xSpeed);
                        ySpeed *= -1;
                    }
                    else if (p.x + p.width == x)
                    {
                        xSpeed = -Math.Abs(xSpeed);
                        ySpeed *= -1;
                    }
                }
                else if(ballRec.IntersectsWith(paddleRecSL))
                {
                    xSpeed *= -1;
                    ySpeed *= -1;
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
