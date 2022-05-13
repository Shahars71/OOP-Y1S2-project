using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace My_first_WinForms_app
{
    public partial class Form1 : Form
    {
        GameGrid game;
        Graphics g;
        int diff;
        bool gameOver;
        VisBlock Smiley;
        bool getClicks;
        public Form1()
        {
            InitializeComponent();

            diff = 20;
            gameOver = false;
            getClicks = true;

            Smiley = new VisBlock();
            game = new GameGrid(diff);
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
            button1.Text = "Started!";
            gameOver = false;
            getClicks = true;
            game.redoGame();
            Smiley.LostGame = false;
            pictureBox2.Invalidate();
            game.initGame();
            pictureBox1.Invalidate();
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            game.Difficulty = 20;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            game.Difficulty = 40;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            game.Difficulty = 80;
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

            if (mY < game.Size && mY >= 0 && mX < game.Size && mX >= 0 && game[mY, mX].IsBomb() && e.Button.ToString() == "Left" && !gameOver && getClicks)
            {
                game.loseGame();
                gameOver = true;
                Smiley.LostGame = true;
                button1.Text = "Start Game";
            }

            if (!gameOver && e.Button.ToString() == "Left" && getClicks)
                game.showBlock(mY,mX);
            if (!gameOver && e.Button.ToString() == "Right" && !((GameBlock)game[mY, mX]).HasFlag && !game[mY, mX].IsVisible && getClicks)
            {
                game.Flag(mY, mX, true);
                if (game.winGame())
                {
                    gameOver = true;
                    button1.Text = "Start Game";
                }
            }
            else if (!gameOver && e.Button.ToString() == "Right" && ((GameBlock)game[mY, mX]).HasFlag && getClicks)
                game.Flag(mY, mX, false);

            //Console.WriteLine("Clicked on x=" + mX + "  y=" + mY);

            pictureBox1.Invalidate();
            pictureBox2.Invalidate();

            if (gameOver && Smiley.LostGame && getClicks)
            {
                MessageBox.Show("You Lose!");
                getClicks = false;
            }
            if (gameOver && !Smiley.LostGame && getClicks)
            {
                MessageBox.Show("You Win!");
                getClicks = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            Smiley.Draw(g, 0, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();// + "..\\myModels";
            saveFileDialog1.Filter = "minesweeper files (*.msw)|*.msw|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                IFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    //!!!!
                    formatter.Serialize(stream, game);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();// + "..\\myModels";
            openFileDialog1.Filter = "minesweep files (*.msw)|*.msw|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Stream stream = File.Open(openFileDialog1.FileName, FileMode.Open);
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                //!!!!
                game = (GameGrid)binaryFormatter.Deserialize(stream);
                pictureBox1.Invalidate();
            }
        }
    }
}
