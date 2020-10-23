using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreaker
{
    class HighScore
    {
        string name;
        string score;
        public void Score( string _name, string _score)
        {
            name = _name;
            score = _score;
        }
    }
}
