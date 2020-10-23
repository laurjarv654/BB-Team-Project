﻿/*  Created by: 
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
using System.Threading;
using System.Xml;

namespace BrickBreaker
{
    public partial class GameScreen : UserControl
    {
        #region global values

        Random random = new Random();

        //variables for powerup
        int speedX, sizeX, bottomX;
        int speedY = 0;
        int sizeY = 0;
        int bottomY = 0;
        int powerXSpeed = 4;
        int powerYSpeed = 4;
        int powerUpSize = 20;
        int paddleWidth = 80;

        //player1 button control keys - DO NOT CHANGE
        Boolean ballStart = false;

        public static Boolean leftArrowDown, rightArrowDown, escapeKeyDown, pause, gameStart, spaceDown;

        string level = "02";

        // Game values
        int lives;

        int xSpeed = 8;
        int ySpeed = 6;
        int score;

        // Paddle and Ball objects
        Paddle paddle;
        Ball ball;
        PowerUp speed, size, bottom;


        // list of all blocks for current level
        List<Block> blocks = new List<Block>();
        List<PowerUp> powerUps = new List<PowerUp>();

        //list of high scores
        List<HighScore> highScore = new List<HighScore>(); 


        // Brushes
        SolidBrush paddleBrush = new SolidBrush(Color.White);
        SolidBrush ballBrush = new SolidBrush(Color.White);
        SolidBrush blockBrush = new SolidBrush(Color.Red);
        SolidBrush sizeBrush = new SolidBrush(Color.Aquamarine);
        SolidBrush speedBrush = new SolidBrush(Color.Yellow);
        SolidBrush sheildBrush = new SolidBrush(Color.MediumBlue);

        //bool for sheild spawn
        bool sheildSpawn = false;
        bool bigPaddle = false;
        int sheildHits = 0;

        int ballX, ballY;
        int ballSize = 20;

        #endregion
        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        }

        public void BreannaPowerUp()
        {

            Rectangle ballRec = new Rectangle(ball.x, ball.y, ball.size, ball.size);
            Rectangle paddleRec = new Rectangle(paddle.x, paddle.y, paddle.width, paddle.height);
            Rectangle sizeRec = new Rectangle(size.x, size.y, size.size, size.size);
            Rectangle speedRec = new Rectangle(speed.x, speed.y, speed.size, speed.size);
            Rectangle bottomRec = new Rectangle(bottom.x, bottom.y, bottom.size, bottom.size);
            Rectangle sheild = new Rectangle(0, this.Height - 30, this.Width, 20);
            
            //changes size of paddle
            if (sizeRec.IntersectsWith(paddleRec))
            {
                bigPaddle = true;

                if (paddleWidth == 80)
                {
                    paddle.width = 160;
                }
            }

            if (speedRec.IntersectsWith(paddleRec))
            {
                if (ball.xSpeed >= 2 && ball.ySpeed >= 2)
                {
                    ball.xSpeed--;
                    ball.ySpeed--;
                }
            }

            if (bottomRec.IntersectsWith(paddleRec))
            {
                sheildSpawn = true;
            }

            foreach (PowerUp power in powerUps)
            {
                Rectangle powerUpRec = new Rectangle(power.x, power.y, power.size, power.size);

                if (power.x < 0 || power.x > this.Width)
                {
                    power.XCollision();

                    if (power.y < 0 || power.y > this.Height)
                    {
                        power.YCollision();
                    }

                }
                else if (power.y < 0 || power.y > this.Height)
                {
                    power.YCollision();

                    if (power.x < 0 || power.x > this.Width)
                    {
                        power.XCollision();
                    }
                }

                if (powerUpRec.IntersectsWith(paddleRec))
                {
                    power.Collision();
                }


                if (sheildSpawn == true)
                {
                    //Rectangle sheild = new Rectangle(0, this.Height - 30, this.Width, 20);

                    if (powerUpRec.IntersectsWith(sheild))
                    {
                        power.Collision();
                    }
                }

                if (speed.x == speedX)
                {
                    speed.Move();
                    speedX = speed.x;
                    speedY = speed.y;
                }
                else if (speed.y == speedY)
                {
                    speed.Move();
                    speedX = speed.x;
                    speedY = speed.y;
                }

                if (size.x == sizeX)
                {
                    size.Move();
                    sizeX = size.x;
                    sizeY = size.y;
                }
                else if (size.y == sizeY)
                {
                    size.Move();
                    sizeX = size.x;
                    sizeY = size.y;
                }

                if (bottom.x == bottomX)
                {
                    bottom.Move();
                    bottomX = bottom.x;
                    bottomY = bottom.y;
                }
                else if (bottom.y == bottomY)
                {
                    bottom.Move();
                    bottomX = bottom.x;
                    bottomY = bottom.y;
                }

                if (sheildSpawn == true)
                {
                    if (ballRec.IntersectsWith(sheild))
                    {
                        sheildHits++;

                        if (sheildHits == 5)
                        {
                            sheildSpawn = false;
                            sheildHits = 0;
                        }
                        else
                        {
                            ball.SheildCollistion();
                        }
                    }
                }
            }
        }
        public void OnStart()
        {

            speedX = random.Next(30, this.Width - 29);
            sizeX = random.Next(30, this.Width - 29);
            bottomX = random.Next(30, this.Width - 29);
            speed = new PowerUp(speedX, speedY, powerXSpeed, powerYSpeed, powerUpSize);
            size = new PowerUp(sizeX, sizeY, powerXSpeed, powerYSpeed, powerUpSize);
            bottom = new PowerUp(bottomX, bottomY, powerXSpeed, powerYSpeed, powerUpSize);
            powerUps.Add(size);
            powerUps.Add(speed);
            powerUps.Add(bottom);


            gameStart = true;
            //set life counter
            lives = 3;

            int score = 0;

            //set all button presses to false.
            leftArrowDown = rightArrowDown = false;

            // setup starting paddle values and create paddle object

            int paddleHeight = 20;
            int paddleX = ((this.Width / 2) - (paddleWidth / 2));
            int paddleY = (this.Height - paddleHeight) - 60;
            int paddleSpeed = 8;
            paddle = new Paddle(paddleX, paddleY, paddleWidth, paddleHeight, paddleSpeed, Color.White);

            // setup starting ball values
            ballX = this.Width / 2 - 10;
            ballY = this.Height - paddle.height - 80;

            //balls.Add(ball);


            // Creates a new ball
            int ballSize = 20;
            ball = new Ball(ballX, ballY, xSpeed, ySpeed, ballSize);

            //clears blocks
            blocks.Clear();
            
            //loads levels
            LoadLevel();

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
                case Keys.Space:
                    spaceDown = true;
                    ballStart = true;
                    ball.xSpeed = 6;
                    break;
                case Keys.Escape:
                    escapeKeyDown = true;
                    Pause();
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
                case Keys.Space:
                    spaceDown = false;
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

                if(ballStart == false)
                ball.MoveWithPaddle("left", paddle);

            }
            if (rightArrowDown && paddle.x < (this.Width - paddle.width))
            {
                paddle.Move("right");

                if(ballStart == false)
                ball.MoveWithPaddle("right", paddle);

            }

            // Move ball
            if (ballStart)
            {
                ball.Move();
            }


            // Check for collision with top and side walls
            ball.WallCollision(this);

            // Check for ball hitting bottom of screen
            if (ball.BottomCollision(this))
            {
                lives--;
                if (paddle.width != 80)
                {
                    bigPaddle = false;
                    paddle.width = 80;
                }

                if (ball.xSpeed != 6 && ball.ySpeed != 6)
                {
                    ball.xSpeed = 6;
                    ball.ySpeed = 6;
                }

                // Moves the ball back to origin
                paddle.x = (this.Width / 2);

                ball.x = ((paddle.x - (ball.size / 2)) + (paddle.width / 2));
                ball.y = (this.Height - paddle.height) - 85;

                Refresh();
                Thread.Sleep(1500);


                if (lives == 0)
                {
                    gameTimer.Enabled = false;
                    OnEnd();
                }
            }
            // Check for collision of ball with paddle, (incl. paddle movement)
            ball.PaddleCollision(paddle);
            // Check if ball has collided with any blocks
            foreach (Block b in blocks)
            {
                if (ball.BlockCollision(b, blocks))
                {
                    b.hp--;
                    score += 100;
                    if(b.hp == 0)
                    {
                        blocks.Remove(b);
                    }
                    if (blocks.Count == 0)
                    {
                        gameTimer.Enabled = false;
                        Win();
                    }
                    break;
                }

            }

            //redraw the screen
            Refresh();
            if (gameStart)
            {
                Thread.Sleep(1500);
                gameStart = false;
            }
        }
        public void LoadLevel()
        {
            //XmlReader reader = XmlReader.Create("Resources/level01.xml");
            XmlTextReader reader = new XmlTextReader("Resources/level" + level +".xml");
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Text)
                {
                    int x = Convert.ToInt16(reader.ReadString());

                    reader.ReadToNextSibling("y");
                    int y = Convert.ToInt32(reader.ReadString());

                    reader.ReadToNextSibling("hp");
                    int hp = Convert.ToInt32(reader.ReadString());

                    Block newBlock = new Block(x, y, hp);
                    blocks.Add(newBlock);
                }
            }
        }
        public void Pause()
        {
            if (gameTimer.Enabled == true)
            {

                gameTimer.Enabled = false;

                DialogResult dr = PauseForm.Show();

                if (dr == DialogResult.Cancel)
                {
                    gameTimer.Enabled = true;
                }
                else if (dr == DialogResult.Abort)
                {
                    Form form = this.FindForm();
                    MenuScreen ms = new MenuScreen();

                    ms.Location = new Point((form.Width - ms.Width) / 2, (form.Height - ms.Height) / 2);

                    form.Controls.Add(ms);
                    form.Controls.Remove(this);
                }
            }
        }
        public void Win()
        {
            Form form = this.FindForm();

            WinnerScreen ws = new WinnerScreen();

            ws.Location = new Point((form.Width - ws.Width) / 2, (form.Height - ws.Height) / 2);

            form.Controls.Add(ws);
            form.Controls.Remove(this);
        }
        public void OnEnd()
        {
            // Goes to the game over screen
            Form form = this.FindForm();

            GameOverScreen gos = new GameOverScreen();

            gos.Location = new Point((form.Width - gos.Width) / 2, (form.Height - gos.Height) / 2);

            form.Controls.Add(gos);
            form.Controls.Remove(this);
        }

        public void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            //ball
            e.Graphics.DrawImage(Properties.Resources.ball, ball.x, ball.y, ball.size, ball.size);

            //foreach (PowerUp power in powerUps)
            //{
            //    e.Graphics.FillRectangle(powerUpBrush, power.x, power.y, power.size, power.size);
            //}

            //Power ups
            e.Graphics.DrawImage(Properties.Resources.paddlePowerBall, size.x, size.y, size.size, size.size);
            e.Graphics.DrawImage(Properties.Resources.ballPowerBall, speed.x, speed.y, speed.size, speed.size);
            e.Graphics.DrawImage(Properties.Resources.shieldPowerBall, bottom.x, bottom.y, bottom.size, bottom.size);

            // Draws paddle
            if (bigPaddle == true)
            {
                e.Graphics.DrawImage(Properties.Resources.longPaddle, paddle.x, paddle.y, paddle.width, paddle.height);
            }
            else
            {
                e.Graphics.DrawImage(Properties.Resources.Paddle, paddle.x, paddle.y, paddle.width, paddle.height);
            }
            
            // Draws blocks
            foreach (Block b in blocks)
            {
                if (b.hp == 1)
                    e.Graphics.DrawImage(Properties.Resources.greenBrick, b.x, b.y, b.width, b.height);
                if (b.hp == 2)
                    e.Graphics.DrawImage(Properties.Resources.blueBrick, b.x, b.y, b.width, b.height);
                if (b.hp == 3)
                    e.Graphics.DrawImage(Properties.Resources.redBrick, b.x, b.y, b.width, b.height);
            }

            //shield
            if (sheildSpawn == true)
            {
                e.Graphics.DrawImage(Properties.Resources.shieldPowerUp, 0, this.Height - 30, this.Width, 20);
            }
        }
    }
}
     
