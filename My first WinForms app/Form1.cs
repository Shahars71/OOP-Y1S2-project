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
            g = e.Graphics;
            game.DrawGrid(g);
        }
    }
}
