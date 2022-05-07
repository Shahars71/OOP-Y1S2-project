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

        public void DrawSmiley(Graphics g, int x, int y, int radius)
        {
            Brush br = new SolidBrush(Color.Yellow);
            Brush brb = new SolidBrush(Color.Black);
            Brush bl = new SolidBrush(Color.DeepPink);

            Pen p = new Pen(Color.Black, radius / 50);
            g.FillEllipse(br, new Rectangle(x, y, radius, radius));
            //eyes
            g.FillEllipse(brb, new Rectangle(x + radius / 5, y + radius / 4, radius / 10, radius / 10));
            g.FillEllipse(brb, new Rectangle(x + radius / 2 + radius / 4, y + radius / 4, radius / 10, radius / 10));
            //mouth
            g.DrawArc(new Pen(Color.Black), new Rectangle(x + radius / 2, y + radius / 3, radius / 10, radius / 10), 0, 180);
            //blush
            g.FillEllipse(bl, new Rectangle(x + radius / 3 - radius / 5, y + radius / 3 + radius / 20, radius / 10, radius / 10));
            g.FillEllipse(bl, new Rectangle(x + radius / 2 + radius / 3, y + radius / 3 + radius / 20, radius / 10, radius / 10));


            //outline
            g.DrawEllipse(p, new Rectangle(x, y, radius, radius));
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

            if (mY < game.Size && mY >= 0 && mX < game.Size && mX >= 0 && game[mY,mX].IsBomb())
                game.loseGame();


            game.showBlock(mY,mX);

            Console.WriteLine("Clicked on x=" + mX + "  y=" + mY);

            pictureBox1.Invalidate();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            DrawSmiley(e.Graphics, 0, 0, pictureBox2.Width);
        }
    }
}
