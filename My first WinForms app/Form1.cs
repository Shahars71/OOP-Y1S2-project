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
            game.initGame();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            game.setDifficulty(10);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            game.setDifficulty(20);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            game.setDifficulty(30);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            double mX = e.X;
            double mY = e.Y;

            game.showBlock((int)mX, (int)mY);      
        }
    }
}
