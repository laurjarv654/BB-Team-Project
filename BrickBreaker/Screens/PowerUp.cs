using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreaker
{

    public class PowerUp
    {
        public int x, y, xSpeed, ySpeed, size;
        public Color colour;
        public static Random randGen = new Random();

        public PowerUp(int _x, int _y, int _xSpeed, int _ySpeed, int _powerUpSize) 
        {
            x = _x;
            y = _y;
            xSpeed = _xSpeed;
            ySpeed = _ySpeed;
            size = _powerUpSize;
        }

        public void Move()
        {
            x = x + xSpeed;
            y = y + ySpeed;
        }

        public void Collision()
        {
            ySpeed *= -1;
            xSpeed *= -1;
            
        }

        public void PaddleCollision(Paddle p, PowerUp up)
        {
            Rectangle powerUpRec = new Rectangle(x, y, size, size);
            Rectangle paddleRec = new Rectangle(p.x, p.y, p.width, p.height);

            if (powerUpRec.IntersectsWith(paddleRec))
            {
                int value = randGen.Next(1, 101);

                if (value <= 50)
                {
                    int power = randGen.Next(1, 3);

                    if (power == 1)
                    {
                        Form1.powerUp = true;
                    }
                    else
                    {
                        Form1.powerUp = false; 
                    }
                }
                else 
                {
                    ySpeed *= -1;
                    xSpeed *= -1;
                }
            }
        }

    }
}
