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
            pos = new position();
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

        public abstract void Draw(Graphics g, int i, int j);
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

        public override void Draw(Graphics g, int i, int j)
        {
            SolidBrush br = new SolidBrush(Color.Gray);
            Pen p = new Pen(Color.Black,1);

            if (isVisible)
            {
                br.Color = Color.White;
            }
            else if (hasFlag)
            {
                br.Color = Color.Red;
            }

            g.FillRectangle(br, Pos.Col + j * 20, Pos.Row + i * 20, 50, 50);
            g.DrawRectangle(p, Pos.Col + j * 20, Pos.Row + i * 20, 50, 50);
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

        public override void Draw(Graphics g, int i, int j)
        {
            SolidBrush br = new SolidBrush(Color.Gray);
            Pen p = new Pen(Color.Black, 1);
            String num = number.ToString();
            Font font = new Font("Arial", 11 * 1.33f, FontStyle.Bold, GraphicsUnit.Pixel);

            switch (number) 
            {
                case 1:
                    if (IsVisible)
                        br.Color = Color.Blue;
                    break;
                case 2:
                    if (IsVisible)
                        br.Color = Color.Orange;
                    break;
                case 3:
                    if (IsVisible)
                        br.Color = Color.DarkRed;
                    break;
                default:
                    if (IsVisible)
                        br.Color = Color.White;
                    break;
            }

            if (Bomb && IsVisible)
                br.Color = Color.Black;

            g.FillRectangle(br, Pos.Col + j*20, Pos.Row + i*20, 20, 20);
            g.DrawRectangle(p, Pos.Col + j*20, Pos.Row + i*20, 20, 20);
            g.DrawString(num, font, br, new PointF(Pos.Col + j * 20, Pos.Row + i * 20));
            
        }

    }


    [Serializable]

    public class GameGrid
    {
        ActivatedBlock[,] grid;
        int difficulty;
        int size;

        public int Difficulty
        {
            get
            {
                return difficulty;
            }

            set
            {
                difficulty = value;
            }
        }

        public int Size
        {
            get
            {
                return size;
            }

            set
            {
                size = value;
            }
        }

        public ActivatedBlock this[int x,int y]
        {
            get => grid[x, y];
            set => grid[x, y] = value;
        }

        public GameGrid()
            : this(30,20) { }

        public GameGrid(int diff, int s)
        {
            difficulty = diff;
            size = s;

            grid = new ActivatedBlock[size,size];

            int i, j;

            for (i=0;i<size;i++)
            {
                for (j=0;j<size;j++)
                {
                    grid[i, j] = new ActivatedBlock();

                    grid[i, j].Pos.Row = i;
                    grid[i, j].Pos.Col = j;
                }
            }
        }

        public void initGame()
        {
            int rC, rR, i, j;
            Random gen = new Random();

            for (i=0; i< difficulty; i++)
            {
                rR = gen.Next() % size;
                rC = gen.Next() % size;

                grid[rR, rC].Bomb = true;
            }

            

            for (i=0; i<size; i++)
            {
                if (i<size-1 && grid[i + 1, 0].Bomb && !grid[i, 0].Bomb)
                    grid[i, 0].incrementNum();

                if (i < size - 1 && grid[i, 0].Bomb && !grid[i+1,0].Bomb)
                    grid[i + 1, 0].incrementNum();

                for (j=1; j<size; j++)
                {
                    if (!grid[i, j].Bomb)
                    {

                        if (grid[i, j - 1].Bomb)
                            grid[i, j].incrementNum();

                        if (i < size - 1 && grid[i + 1, j - 1].Bomb)
                            grid[i, j].incrementNum();

                        if (i < size - 1 && grid[i + 1, j].Bomb)
                            grid[i, j].incrementNum();

                        if (i > 0 && grid[i - 1, j].Bomb)
                            grid[i, j].incrementNum();

                        if (i > 0 && grid[i - 1, j - 1].Bomb)
                            grid[i, j].incrementNum();

                        if (j < size - 1 && grid[i, j + 1].Bomb)
                            grid[i, j].incrementNum();

                        if (i < size - 1 && j < size - 1 && grid[i + 1, j + 1].Bomb)
                            grid[i, j].incrementNum();

                        if (i > 0 && j < size - 1 && grid[i - 1, j + 1].Bomb)
                            grid[i, j].incrementNum();
                    }

                }
            }
        }

        public void showBlock(int x, int y)
        {
            Console.WriteLine("x="+x+" y="+y);

            if (x >= size || y >= size || x < 0 || y < 0 || grid[x,y].Bomb || grid[x, y].IsVisible)
                return;

            Console.WriteLine("Made grid[" + x + "," + y + "] visible");
            grid[x, y].IsVisible = true;
            

            if (grid[x,y].Number == 0)
            {
                if (x + 1 < size)
                {
                    showBlock(x + 1, y);

                    if (y + 1 < size)
                        showBlock(x + 1, y + 1);
                    if (y - 1 > 0)
                        showBlock(x + 1, y - 1);
                }

                if (x - 1 > 0)
                {
                    showBlock(x - 1, y);

                    if (y + 1 < size)
                        showBlock(x - 1, y + 1);
                    if (y - 1 > 0)
                        showBlock(x - 1, y - 1);
                }


                if (y + 1 < size)
                    showBlock(x, y + 1);

                if (y - 1 > 0)
                    showBlock(x, y - 1);

            }
        }

        public void loseGame()
        {
            int i, j;

            for (i = 0; i < size; i++)
            {
                for (j = 0; j < size; j++)
                {
                    if (grid[i,j].Bomb)
                    {
                        grid[i, j].IsVisible = true;
                    }
                }
            }
        }

        public void DrawGrid(Graphics g)
        {
            int i, j;

            for (i=0;i<size;i++)
            {
                for (j=0; j<size; j++)
                {
                    grid[i, j].Draw(g, i, j);
                }
            }
        }
    }



}
