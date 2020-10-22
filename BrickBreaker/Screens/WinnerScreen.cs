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
    public partial class WinnerScreen : UserControl
    {
        public WinnerScreen()
        {
            InitializeComponent();

            #region generating random message
            int random = 1;

            Random randGen = new Random();
            random = randGen.Next(1, 4);

            outputLabel.Text = "";

            switch (random)
            {
                case 1:
                    outputLabel.Text = "WE HAVE A WINNER!PLAY AGAIN?";
                    break;
                case 2:
                    outputLabel.Text = "CONGRADULATIONS! PLAY AGAIN?";
                    break;
                case 3:
                    outputLabel.Text = "NICE ONE! PLAY AGAIN?";
                    break;
                case 4:
                    outputLabel.Text = "GOOD JOB! PLAY AGAIN?";
                    break;
            }
            #endregion

            #region displaying score/time
            //TODO - get score and time variables from GameScreen
            //TODO - display they in the correct label with proper formatting

            #endregion

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

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
