/*  Created by: 
 *  Project: Brick Breaker
 *  Date: 10/21/2020
 */ 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Diagnostics;

namespace BrickBreaker
{
    public partial class GameScreen : UserControl
    {
        #region global values

        Random random = new Random();

        //variables for powerup
        int powerX;
        int powerY = 0;
        int powerXSpeed = 8;
        int powerYSpeed = 8;
        int powerUpSize = 20;

        //player1 button control keys - DO NOT CHANGE
        Boolean leftArrowDown, rightArrowDown;

        // Game values
        int lives;
        int xSpeed = 6;
        int ySpeed = 6;

        // Paddle and Ball objects
        Paddle paddle;
        Ball ball;
        PowerUp power;

        // list of all blocks for current level
        List<Block> blocks = new List<Block>();
        //List<Ball> balls = new List<Ball>();
        

        // Brushes
        SolidBrush paddleBrush = new SolidBrush(Color.White);
        SolidBrush ballBrush = new SolidBrush(Color.White);
        SolidBrush blockBrush = new SolidBrush(Color.Red);
        SolidBrush powerUpBrush = new SolidBrush(Color.Aquamarine);

        //timer for powerup spawn

        Stopwatch myWatch = new Stopwatch();

        int ballX, ballY;
        int ballSize = 20;

        #endregion

        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        }

        public void BreannaPowerUp ()
        {

            if (power.x == powerX)
            {
                power.Move();
                powerX = power.x;
                powerY = power.y;
            }
            else if (power.y == powerY)
            {
                power.Move();
                powerX = power.x;
                powerY = power.y;
            }
            

            if (power.x < 0 || power.x > this.Width)
            {
                power.XCollision();
            }
            else if (power.y < 0 || power.y > this.Height)
            {
                power.YCollision();
            }

            Rectangle powerUpRec = new Rectangle(power.x, power.y, power.size, power.size);
            Rectangle paddleRec = new Rectangle(paddle.x, paddle.y, paddle.width, paddle.height);

            if (powerUpRec.IntersectsWith(paddleRec))
            {
                power.PaddleCollision(paddle, power);

                if (Form1.powerUp == true && xSpeed > 4 && ySpeed > 4)
                {
                    xSpeed--;
                    ySpeed--;
                }
                else if (Form1.powerUp == false)
                {
                    //ball = new Ball(ballX, ballY, xSpeed, ySpeed, ballSize);
                    //balls.Add(ball);
                }
            }

            //foreach (Ball a in balls)
            //{
            //    ball.Move();
            //    ball.WallCollision(this);
            //    ball.PaddleCollision(paddle, leftArrowDown, rightArrowDown);
            //}
            //for (int i = 1; i < balls.Count(); i++)
            //{
            //    balls[i].BottomCollision(this);
            //    if (Form1.didCollide == true)
            //    {
            //        balls.RemoveAt(i);
            //    }
            //}





        }

        public void OnStart()
        {
            powerX = random.Next(30, this.Width - 29);
            power = new PowerUp(powerX, powerY, powerXSpeed, powerYSpeed, powerUpSize);

            //set life counter
            lives = 3;

            //set all button presses to false.
            leftArrowDown = rightArrowDown = false;

            // setup starting paddle values and create paddle object
            int paddleWidth = 80;
            int paddleHeight = 20;
            int paddleX = ((this.Width / 2) - (paddleWidth / 2));
            int paddleY = (this.Height - paddleHeight) - 60;
            int paddleSpeed = 8;
            paddle = new Paddle(paddleX, paddleY, paddleWidth, paddleHeight, paddleSpeed, Color.White);

            // setup starting ball values
            ballX = this.Width / 2 - 10;
            ballY = this.Height - paddle.height - 80;

            ball = new Ball(ballX, ballY, xSpeed, ySpeed, ballSize);
            //balls.Add(ball);

            #region Creates blocks for generic level. Need to replace with code that loads levels.
            
            //TODO - replace all the code in this region eventually with code that loads levels from xml files
            
            blocks.Clear();
            int x = 10;

            while (blocks.Count < 12)
            {
                x += 57;
                Block b1 = new Block(x, 10, 1, Color.White);
                blocks.Add(b1);
            }

            #endregion

            myWatch.Start();

            // start the game engine loop
            gameTimer.Enabled = true;
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                default:
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                default:
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            BreannaPowerUp();

            // Move the paddle
            if (leftArrowDown && paddle.x > 0)
            {
                paddle.Move("left");
            }
            if (rightArrowDown && paddle.x < (this.Width - paddle.width))
            {
                paddle.Move("right");
            }

            //// Move ball
            ball.Move();

            // Check for collision with top and side walls
            ball.WallCollision(this);

            // Check for ball hitting bottom of screen
            if (ball.BottomCollision(this))
            {
                lives--;

                // Moves the ball back to origin
                ball.x = ((paddle.x - (ball.size / 2)) + (paddle.width / 2));
                ball.y = (this.Height - paddle.height) - 85;
                
               

                if (lives == 0)
                {
                    gameTimer.Enabled = false;
                    OnEnd();
                }
            }

            // Check for collision of ball with paddle, (incl. paddle movement)
            ball.PaddleCollision(paddle, leftArrowDown, rightArrowDown);

            // Check if ball has collided with any blocks
            foreach (Block b in blocks)
            {
                if (ball.BlockCollision(b))
                {
                    blocks.Remove(b);

                    if (blocks.Count == 0)
                    {
                        gameTimer.Enabled = false;
                        OnEnd();
                    }

                    break;
                }
            }


           

            //redraw the screen
            Refresh();
        }

        public void OnEnd()
        {
            // Goes to the game over screen
            myWatch.Stop();
            Form form = this.FindForm();
            MenuScreen ps = new MenuScreen();
            
            ps.Location = new Point((form.Width - ps.Width) / 2, (form.Height - ps.Height) / 2);

            form.Controls.Add(ps);
            form.Controls.Remove(this);
        }

        public void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            // Draws paddle
            paddleBrush.Color = paddle.colour;
            e.Graphics.FillRectangle(paddleBrush, paddle.x, paddle.y, paddle.width, paddle.height);

            // Draws blocks
            foreach (Block b in blocks)
            {
                e.Graphics.FillRectangle(blockBrush, b.x, b.y, b.width, b.height);
            }

            // Draws ball
            //foreach (Ball a in balls)
            //{
            //    e.Graphics.FillRectangle(ballBrush, a.x, a.y, a.size, a.size);
            //}

            e.Graphics.FillRectangle(ballBrush, ball.x, ball.y, ball.size, ball.size);

            e.Graphics.FillRectangle(powerUpBrush, power.x, power.y, power.size, power.size);

            
        }
    }
}
