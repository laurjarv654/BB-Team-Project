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

        public void YCollision()
        {
            ySpeed *= -1;
            
        }
        public void XCollision()
        {
            xSpeed *= -1;
        }

        public void Collision()
        {
            ySpeed *= -1;
            xSpeed *= -1;
        }

    }
}
