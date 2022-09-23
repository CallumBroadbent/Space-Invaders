using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Space_Invader
{
    public partial class Form1 : Form
    {
//all important variables are placed outside of all functions for public use
        bool goLeft;
        bool goRight;
        bool isPressed;

        int speed = 5;
        int score = 0;
        int totalEnemies = 26;
        int playerSpeed = 6;

//end of public variables

        public Form1()
        {
            InitializeComponent();
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
//controls what happens when key is pressed down
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Space && !isPressed)
            {
                isPressed = true;

                makeBullet();
            }
        }
//end of what happens when key is pressed down
        private void keyisup(object sender, KeyEventArgs e)
        {
//controls what happens when left and right arrow key is no longer pressed
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (isPressed)
            {
                isPressed = false;
            }
        }
//end of key is up function

        private void timer1_Tick(object sender, EventArgs e)
        {
//controls players speed going left and right
            if (goLeft)
            {
                Player.Left -= playerSpeed;
            }else if (goRight)
            {
                Player.Left += playerSpeed;
            }
//end of players speed control

//controls enemy movement
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "Invaders")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(Player.Bounds))
                    {
                        gameOver();

                    }
                    ((PictureBox)x).Left += speed;

                    if (((PictureBox)x).Left > 720)
                    {
                        ((PictureBox)x).Top += ((PictureBox)x).Height + 10;
                        ((PictureBox)x).Left = -50;
                    }
                }
            }
//end of enemy movement controls


//animating the bullets and removing them when they are no longer on the screen
            foreach (Control y in this.Controls)
            {
                if (y is PictureBox && y.Tag == "bullets")
                {

                    y.Top -= speed;

                    if (((PictureBox)y).Top < this.Height - 490)
                    {
                        this.Controls.Remove(y);
                    }
                }
            }
//end of animating the bullet and removing it

//This controles what happens when a bullet hits an enemy
            foreach (Control i in this .Controls)
            {
                foreach (Control j in this.Controls)
                {
                    if (i is PictureBox && i.Tag == "Invaders")
                    {
                        if (j is PictureBox && j.Tag == "bullets")
                        {
                            if (i.Bounds.IntersectsWith(j.Bounds))
                            {
                                score++;
                                this.Controls.Remove(i);
                                this.Controls.Remove(j);
                            }
                        }
                    }
                }
            }
            //end of what happens when an enemy is hit.
            label1.Text = "Score : " + score;

            if (score > totalEnemies - 1)
            {
                gameOver();
                MessageBox.Show("You Saved Earth from the invaders!!!");
            }

        }

        public void makeBullet()
        {
//this creates a new picture box for when the space bar is pressed creating the bullet
            PictureBox bullet = new PictureBox();

            bullet.Image = Properties.Resources.bullet1;
            bullet.Size = new Size(5, 20);
            bullet.Tag = "bullets";
            bullet.Left = Player.Left + Player.Width / 2;
            bullet.Top = Player.Top - 20;
            this.Controls.Add(bullet);
            bullet.BringToFront();

        }
//end of bullet creation
        private void gameOver()
        {
//this is what controls stopping the game
            timer1.Stop();
            label2.Text = "!!!Game Over!!!";
        }

    }
}
