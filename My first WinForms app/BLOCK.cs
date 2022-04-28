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
    public abstract class BLOCK
    {
        int row;
        int col;

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

        public abstract void Draw(Graphics g);
    }

    [Serializable]

    public class GameBlock : BLOCK
    {
        bool hasFlag;
        bool hasBomb;
        bool hasNum;
        int number;

        public GameBlock()
            : this(false)
        {}

        public GameBlock(bool bomb)
        {
            hasFlag = false;
            hasBomb = bomb;
            hasNum = false;
            number = 0;
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

        public bool HasBomb
        {
            get
            {
                return hasBomb;
            }
            set
            {
                hasBomb = value;
            }
        }

        public bool HasNum
        {
            get
            {
                return hasNum;
            }
            set
            {
                hasNum = value;
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

        public override void Draw(Graphics g)
        {
            
        }
    }

    [Serializable]

    public class GameGrid
    {
        GameBlock[,] grid;
        int endCol;
        int endRow;

        public GameGrid()
        {
            grid = new GameBlock[5,5];
            endCol = 5;
            endRow = 5;
        }

        public void initGame()
        {
            grid[0, 0].HasBomb = true;
            surroundBomb(0, 0);
        }

        public void surroundBomb(int row, int col)
        {
            if (row != 0)
            {
                grid[row - 1, col].HasNum = true;
                grid[row - 1, col].Number++;

                if (col != 0)
                {
                    grid[row-1,col-1].HasNum = true;
                    grid[row - 1, col - 1].Number++;
                    grid[row, col - 1].HasNum = true;
                    grid[row, col - 1].Number++;
                }

                if (col != endCol)
                {
                    grid[row - 1, col + 1].HasNum = true;
                    grid[row - 1, col + 1].Number++;
                    grid[row, col + 1].HasNum = true;
                    grid[row, col + 1].Number++;
                }

            }

            if (row != endRow)
            {
                grid[row + 1, col].HasNum = true;
                grid[row + 1, col].Number++;

                if (col != 0)
                {
                    grid[row + 1, col - 1].HasNum = true;
                    grid[row + 1, col - 1].Number++;
                    grid[row, col - 1].HasNum = true;
                    grid[row, col - 1].Number++;
                }

                if (col != endCol)
                {
                    grid[row + 1, col + 1].HasNum = true;
                    grid[row + 1, col + 1].Number++;
                    grid[row, col + 1].HasNum = true;
                    grid[row, col + 1].Number++;
                }
            }

        }

        public void DrawGrid(Graphics g)
        {
            SolidBrush br = new SolidBrush(Color.Red);
            Pen pen = new Pen(Color.Cyan, 2);
            g.FillRectangle(br, 5, 5, 50, 50);
            g.DrawRectangle(pen, 5, 5, 50, 50);
        }
    }



}
