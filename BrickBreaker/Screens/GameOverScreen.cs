using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrickBreaker
{
    public partial class GameOverScreen : UserControl
    {
        public GameOverScreen()
        {
            InitializeComponent();

            //Generating a random message
            int random = 1;

            Random randGen = new Random();
            random =  randGen.Next(1, 5);

            outputLabel.Text = "";

            switch(random)
            {
                case 1:
                    outputLabel.Text = "IS THAT ALL YOU'VE GOT?";
                    break;
                case 2:
                    outputLabel.Text = "HA! TRY AGAIN LOSER.";
                    break;
                case 3:
                    outputLabel.Text = "GIVING UP ALREADY?";
                    break;
                case 4:
                    outputLabel.Text = "LEAVING SO SOON?";
                    break;
                case 5:
                    outputLabel.Text = "I BET YOU CAN GET A HIGHER SCORE THAN THAT.";
                    break;


            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        { 
            Application.Exit();
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            // Goes to the game screen
            GameScreen gs = new GameScreen();
            Form form = this.FindForm();

            form.Controls.Add(gs);
            form.Controls.Remove(this);

            gs.Location = new Point((form.Width - gs.Width) / 2, (form.Height - gs.Height) / 2);
        }

        private void highscoresButton_Click(object sender, EventArgs e)
        {
            //Goes to high scores screen
            HighscoreScreen hs = new HighscoreScreen();
            Form form = this.FindForm();

            form.Controls.Add(hs);
            form.Controls.Remove(this);

            hs.Location = new Point((form.Width - hs.Width) / 2, (form.Height - hs.Height) / 2);
        }
    }
}
