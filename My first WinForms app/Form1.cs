using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace My_first_WinForms_app
{
    public partial class Form1 : Form
    {
        GameGrid game;
        Graphics g;
        public Form1()
        {
            InitializeComponent();

            game = new GameGrid();
            game.initGame();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            game.DrawGrid(g);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //game.initGame();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            game.Difficulty = 10;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            game.Difficulty = 20;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            game.Difficulty = 30;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int mX = e.X / 20;
            int mY = e.Y / 20;

            if (mY == game.Size)
                mY--;
            if (mX == game.Size)
                mX--;

            if (mY < game.Size && mY >= 0 && mX < game.Size && mX >= 0 && game[mY,mX].Bomb)
                game.loseGame();


            game.showBlock(mY,mX);

            Console.WriteLine("Clicked on x=" + mX + "  y=" + mY);

            pictureBox1.Invalidate();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
