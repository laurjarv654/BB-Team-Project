using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrickBreaker
{
    public partial class PauseForm : Form
    {
        private static PauseForm pauseForm;
        private static DialogResult buttonResult = new DialogResult();
        int width = 1280;
        int height = 812;
        //Creating button selected
        private int buttonSelected; 

        public PauseForm()
        {
            InitializeComponent();
        }
        //creating dialog to show the pauseform
        public static DialogResult Show()
        {
            pauseForm = new PauseForm();
            pauseForm.StartPosition = FormStartPosition.Manual;
            MenuScreen ms = new MenuScreen();
            pauseForm.Location = new Point((GameScreen.width - ms.Width) / 2, (GameScreen.height - ms.Height) / 2);
            pauseForm.ShowDialog();
            return buttonResult;
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            buttonResult = DialogResult.Cancel;
            pauseForm.Close();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            buttonResult = DialogResult.Abort;
            pauseForm.Close();
        }
    }
}
