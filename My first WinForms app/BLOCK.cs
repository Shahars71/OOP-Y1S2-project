using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace My_first_WinForms_app
{
    using System;
    using System.Collections;


    [Serializable]
    public class position
    {
        int row;
        int col;

        public position()
                :this(0, 0)
            {}

        public position(int r, int c)
        {
            row = r;
            col = c;
        }

        public int Row
        {
            get
            {
                return row;
            }
            set
            {
                row = value;
            }
        }

        public int Col
        {
            get
            {
                return col;
            }
            set
            {
                col = value;
            }
        }
    }



    [Serializable]
    public abstract class BLOCK
    {
        position pos;

        public BLOCK()
            :this(0,0)
        { }

        public BLOCK(int r, int c)
        {
            pos.Row = r;
            pos.Col = c;
        }

        public position Pos
        {
            get
            {
                return pos;
            }

            set
            {
                pos = value;
            }
            
        }

        public abstract void Draw(Graphics g);
    }

    [Serializable]

    public class GameBlock : BLOCK
    {
        bool hasFlag;
        bool isVisible;

        public GameBlock()
        {
            hasFlag = false;
            isVisible = false;
        }

        public bool HasFlag
        {
            get
            {
                return hasFlag;
            }
            set
            {
                hasFlag = value;
            }
        }

        public bool IsVisible
        {
            get
            {
                return isVisible;
            }
            set
            {
                isVisible = value;
            }
        }

        public override void Draw(Graphics g)
        {
            SolidBrush br = new SolidBrush(Color.Gray);
            Pen p = new Pen(Color.Black,5);

            if (isVisible)
            {
                br.Color = Color.White;
            }
            else if (hasFlag)
            {
                br.Color = Color.Red;
            }

            g.FillRectangle(br, Pos.Col, Pos.Row, 5, 5);
            g.DrawRectangle(p, Pos.Col, Pos.Row, 5, 5);
        }
    }

    [Serializable]

    public class ActivatedBlock : GameBlock
    {
        bool bomb;
        int number;

        public ActivatedBlock()
            : this(false, 0)
        {}

        public ActivatedBlock(bool b, int n)
        {
            bomb = b;
            number = n;
        }

        public bool Bomb
        {
            get
            {
                return bomb;
            }

            set
            {
                bomb = value;
            }
        }

        public int Number
        {
            get
            {
                return number;
            }

            set
            {
                number = value;
            }
        }


        public void incrementNum()
        {
            if (!bomb)
                number++;
        }

        public override void Draw(Graphics g)
        {
            SolidBrush br = new SolidBrush(Color.Gray);
            Pen p = new Pen(Color.Black, 5);

            switch (number) 
            {
                case 1:
                    br.Color = Color.Blue;
                    break;
                case 2:
                    br.Color = Color.Orange;
                    break;
                case 3:
                    br.Color = Color.DarkRed;
                    break;
                default:
                    break;
            }

            g.FillRectangle(br, Pos.Col, Pos.Row, 5, 5);
            g.DrawRectangle(p, Pos.Col, Pos.Row, 5, 5);
        }

    }


    [Serializable]

    public class GameGrid
    {
        ActivatedBlock[,] grid;
        int difficulty;

        public GameGrid()
            : this(5) { }

        public GameGrid(int diff)
        {
            difficulty = diff;

            grid = new ActivatedBlock[50,50];

            int i, j;

            for (i=0;i<50;i++)
            {
                for (j=0;j<50;j++)
                {
                    grid[i, j].Pos.Row = i;
                    grid[i, j].Pos.Col = j;
                }
            }
        }

        public void initGame()
        {
            int rC, rR, i, j;
            Random gen = new Random();

            for (i=0; i<= difficulty; i++)
            {
                rR = gen.Next() % 50;
                rC = gen.Next() % 50;

                grid[rR, rC].Bomb = true;
            }

            if (grid[1, 0].Bomb && !grid[0,0].Bomb)
                grid[0, 0].incrementNum();

            for (i=0; i<50; i++)
            {
                for (j=1; j<50; j++)
                {
                    if (grid[i, j - 1].Bomb)
                        grid[i, j].incrementNum();

                    if (grid[i + 1, j - 1].Bomb)
                        grid[i, j].incrementNum();

                    if (grid[i + 1, j].Bomb)
                        grid[i, j].incrementNum();
                }
            }
        }



        public void DrawGrid(Graphics g)
        {
            int i, j;

            for (i=0;i<50;i++)
            {
                for (j=0;j<50;j++)
                {
                    grid[i, j].Draw(g);
                }
            }
        }
    }



}
