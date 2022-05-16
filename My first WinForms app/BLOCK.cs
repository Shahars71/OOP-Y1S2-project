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
        bool isVisible;
        Image img;
        public BLOCK()
            :this(0,0)
        { }

        public BLOCK(int r, int c)
        {
            pos = new position();
            pos.Row = r;
            pos.Col = c;
            isVisible = false;
            img = Image.FromFile("Sprites\\block.png");
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

        public Image Img 
        {

            get
            {
                return img;
            }

            set
            {
                img = value;
            }
        }

        public abstract void Draw(Graphics g, int i, int j);
        public abstract bool IsBomb();
        public abstract int giveNum();
        public abstract void changeFlag(bool cond);
    }

    [Serializable]

    public class VisBlock : BLOCK
    {
        bool lostGame;

        public VisBlock()
            :this(false) { }

        public VisBlock(bool cond)
        {
            Pos = new position();
            Pos.Row = 0;
            Pos.Col = 0;
            IsVisible = false;
            Img = Image.FromFile("Sprites\\happySmile.png");
        }
            

        public bool LostGame
        {
            get
            {
                return lostGame;
            }

            set
            {
                lostGame = value;
            }
        }

        public override void Draw(Graphics g, int i, int j)
        {
            if (lostGame)
            {
                Img = Image.FromFile("Sprites\\deadSmile.png");
            }

            if (!lostGame)
                Img = Image.FromFile("Sprites\\happySmile.png");

            g.DrawImage(this.Img, 0, 0);
        }

        public override int giveNum()
        {
            return -1;
        }

        public override bool IsBomb()
        {
            return false;
        }

        public override void changeFlag(bool cond) { }
    }


    [Serializable]

    public class GameBlock : BLOCK
    {
        bool hasFlag;

        public GameBlock()
        {
            hasFlag = false;
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

        

        public override void Draw(Graphics g, int i, int j)
        {
            if (IsVisible)
            {
                this.Img = Image.FromFile("Sprites\\blockvis.png");
            }
            else if (hasFlag)
            {
                this.Img = Image.FromFile("Sprites\\flagGreen.png");
            }
            else this.Img = Image.FromFile("Sprites\\block.png");

            g.DrawImage(this.Img, Pos.Col + j * 20, Pos.Row + i * 20);
        }
        public override bool IsBomb()
        {
            return false;
        }

        public override int giveNum()
        {
            return 0;
        }

        public override void changeFlag(bool cond) 
        {
            hasFlag = cond;
        }
    }

    //[Serializable]

    //public class ActivatedBlock : GameBlock
    //{
    //    bool bomb;
    //    int number;

    //    public ActivatedBlock()
    //        : this(false, 0)
    //    {}

    //    public ActivatedBlock(bool b, int n)
    //    {
    //        bomb = b;
    //        number = n;
    //    }

    //    public bool Bomb
    //    {
    //        get
    //        {
    //            return bomb;
    //        }

    //        set
    //        {
    //            bomb = value;
    //        }
    //    }

    //    public int Number
    //    {
    //        get
    //        {
    //            return number;
    //        }

    //        set
    //        {
    //            number = value;
    //        }
    //    }


    //    public void incrementNum()
    //    {
    //        if (!bomb)
    //            number++;
    //    } 

    //    public override void Draw(Graphics g, int i, int j)
    //    {
    //        SolidBrush br = new SolidBrush(Color.Gray);
    //        Pen p = new Pen(Color.Black, 1);
    //        //String num = number.ToString();
    //        //Font font = new Font("Arial", 11 * 1.33f, FontStyle.Bold, GraphicsUnit.Pixel);

    //        switch (number) 
    //        {
    //            case 1:
    //                if (IsVisible)
    //                    br.Color = Color.Blue;
    //                break;
    //            case 2:
    //                if (IsVisible)
    //                    br.Color = Color.Orange;
    //                break;
    //            case 3:
    //                if (IsVisible)
    //                    br.Color = Color.DarkRed;
    //                break;
    //            default:
    //                if (IsVisible)
    //                    br.Color = Color.White;
    //                break;
    //        }

    //        if (Bomb && IsVisible)
    //            br.Color = Color.Black;

    //        g.FillRectangle(br, Pos.Col + j*20, Pos.Row + i*20, 20, 20);
    //        g.DrawRectangle(p, Pos.Col + j*20, Pos.Row + i*20, 20, 20);
    //        //g.DrawString(num, font, br, new PointF(Pos.Col + j * 20, Pos.Row + i * 20));

    //    }

    //}

    [Serializable]
    public class BombBlock : GameBlock
    {
        bool bomb;
        public BombBlock()
        {
            bomb = true;
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

        public override bool IsBomb()
        {
            return true;
        }

        public override int giveNum()
        {
            return -1;
        }

        public override void Draw(Graphics g, int i, int j)
        {

            if (IsVisible)
                this.Img = Image.FromFile("Sprites\\blockBomb.png");
            else if (HasFlag)
                this.Img = Image.FromFile("Sprites\\flagRed.png");
            else this.Img = Image.FromFile("Sprites\\block.png");

            g.DrawImage(this.Img, Pos.Col + j * 20, Pos.Row + i * 20);
        }

    }

    [Serializable]
    public class NumBlock : GameBlock
    {
        int num;

        public NumBlock()
            : this(0) {}

        public NumBlock(int n)
        {
            num = n;
        }

        public int Num
        {
            get
            {
                return num;
            }

            set
            {
                num = value;
            }

        }

        public void IncrementNum()
        {
            if (!IsBomb())
                num++;
        }

        public override bool IsBomb()
        {
            return false;
        }

        public override int giveNum()
        {
            return num;
        }

        public override void Draw(Graphics g, int i, int j)
        {

            switch (num)
            {
                case 1:
                    if (IsVisible)
                        this.Img = Image.FromFile("Sprites\\1.png");
                    break;
                case 2:
                    if (IsVisible)
                        this.Img = Image.FromFile("Sprites\\2.png");
                    break;
                case 3:
                    if (IsVisible)
                        this.Img = Image.FromFile("Sprites\\3.png");
                    break;

                case 4:
                    if (IsVisible)
                        this.Img = Image.FromFile("Sprites\\4.png");
                    break;

                case 5:
                    if (IsVisible)
                        this.Img = Image.FromFile("Sprites\\5.png");
                    break;

                case 6:
                    if (IsVisible)
                        this.Img = Image.FromFile("Sprites\\6.png");
                    break;

                case 7:
                    if (IsVisible)
                        this.Img = Image.FromFile("Sprites\\7.png");
                    break;

                case 8:
                    if (IsVisible)
                        this.Img = Image.FromFile("Sprites\\8.png");
                    break;

                default:
                    if (IsVisible)
                        this.Img = Image.FromFile("Sprites\\blockvis.png");
                    break;
            }

            if (HasFlag && !IsVisible)
                this.Img = Image.FromFile("Sprites\\flagGreen.png");
            else if (!HasFlag && !IsVisible)
                this.Img = Image.FromFile("Sprites\\block.png");

            g.DrawImage(this.Img, Pos.Col + j * 20, Pos.Row + i * 20);
        }
    }

    [Serializable]

    public class GameGrid
    {
        BLOCK [,] grid;
        int difficulty;
        int size;   
        position[] bombs;

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


        public BLOCK this[int x,int y]
        {
            get => grid[x, y];
            set => grid[x, y] = value;
        }

        public GameGrid()
            : this(40,21) { }

        public GameGrid(int diff)
            : this(diff, 21) { }

        public GameGrid(int diff, int s)
        {
            difficulty = diff;
            size = s;
            bombs = new position[diff];

            grid = new GameBlock[size,size];

            int i, j;

            for (i=0;i<size;i++)
            {
                for (j=0;j<size;j++)
                {
                    grid[i, j] = new GameBlock();
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
                
                grid[rR, rC] = new BombBlock();

                bombs[i] = new position();

                bombs[i].Col = rC;
                bombs[i].Row = rR;
            }

            
                for (i = 0; i < size; i++)
                {

                    for (j = 0; j < size; j++)
                    {
                        if (grid[i, j].IsBomb())
                        {
                            if (j > 0)
                            {
                            if (!grid[i, j - 1].IsBomb()) 
                                grid[i, j - 1] = new NumBlock();
                            }

                            if (j < size - 1)
                            {
                            if (!grid[i, j + 1].IsBomb()) 
                                grid[i, j + 1] = new NumBlock();
                            }

                            if (i > 0)
                            {
                            if (!grid[i - 1, j].IsBomb()) 
                                grid[i - 1, j] = new NumBlock();

                                if (j > 0)
                                {
                                if (!grid[i - 1, j - 1].IsBomb()) 
                                    grid[i - 1, j - 1] = new NumBlock();
                                }

                                if (j < size - 1)
                                {
                                if (!grid[i - 1, j + 1].IsBomb()) 
                                    grid[i - 1, j + 1] = new NumBlock();
                                }
                            }

                            if (i < size - 1)
                            {
                            if (!grid[i + 1, j].IsBomb()) 
                                grid[i + 1, j] = new NumBlock();

                                if (j > 0)
                                {
                                if (!grid[i + 1, j - 1].IsBomb()) 
                                    grid[i + 1, j - 1] = new NumBlock();
                                }

                                if (j < size - 1)
                                {
                                if (!grid[i + 1, j + 1].IsBomb()) 
                                    grid[i + 1, j + 1] = new NumBlock();
                                }
                            }
                        }
                    }
                }


            for (i=0; i<size; i++)
            {

                for (j=0; j<size; j++)
                {
                    if (grid[i,j].IsBomb())
                    {
                        if (j > 0)
                        {
                            if (!grid[i, j - 1].IsBomb()) 
                                ((NumBlock)grid[i, j - 1]).IncrementNum();
                        }

                        if (j < size - 1)
                        {
                            if (!grid[i, j + 1].IsBomb()) 
                                ((NumBlock)grid[i, j + 1]).IncrementNum();
                        }

                        if (i > 0)
                        {
                            if (!grid[i - 1, j].IsBomb()) 
                                ((NumBlock)grid[i - 1, j]).IncrementNum();

                            if (j > 0)
                            {
                                if (!grid[i - 1, j - 1].IsBomb()) 
                                    ((NumBlock)grid[i - 1, j - 1]).IncrementNum();
                            }

                            if (j < size - 1)
                            {
                                if (!grid[i - 1, j + 1].IsBomb()) 
                                    ((NumBlock)grid[i - 1, j + 1]).IncrementNum();
                            }
                        }

                        if (i < size - 1)
                        {
                            if (!grid[i + 1, j].IsBomb()) 
                                ((NumBlock)grid[i + 1, j]).IncrementNum();

                            if (j > 0)
                            {
                                if (!grid[i + 1, j - 1].IsBomb()) 
                                    ((NumBlock)grid[i + 1, j - 1]).IncrementNum();
                            }

                            if (j < size - 1)
                            {
                                if (!grid[i + 1, j + 1].IsBomb()) 
                                    ((NumBlock)grid[i + 1, j + 1]).IncrementNum();
                            }
                        }
                    }
                }
            }
        }

        public void redoGame()
        {
            int i, j;
            bombs = new position[difficulty];

            for (i = 0; i < size; i++) 
            {
                for (j = 0; j < size; j++) 
                {
                    grid[i,j] = new GameBlock();
                }
            }
        }

        public void showBlock(int x, int y)
        {
            //Console.WriteLine("x="+x+" y="+y);

            if (x >= size || y >= size || x < 0 || y < 0 || grid[x,y].IsBomb() || grid[x, y].IsVisible)
                return;

            //Console.WriteLine("Made grid[" + x + "," + y + "] visible");
            (grid[x, y]).IsVisible = true;
            

            if (grid[x,y].giveNum() == 0)
            {
                if (y > 0 && !grid[x,y-1].IsVisible)
                {
                    showBlock(x, y - 1);
                }

                if (y < size - 1 && !grid[x, y + 1].IsVisible)
                {
                    showBlock(x, y + 1);
                }

                if (x > 0)
                {
                    if (!grid[x - 1, y].IsVisible)
                    {
                        showBlock(x - 1, y);
                    }

                    //if (y > 0 && !grid[x-1, y - 1].IsVisible)
                    //{
                    //    showBlock(x - 1, y - 1);
                    //}

                    //if (y < size - 1 && !grid[x - 1, y + 1].IsVisible)
                    //{
                    //    showBlock(x - 1, y + 1);
                    //}
                }

                if (x < size - 1)
                {

                    if (!grid[x + 1, y].IsVisible)
                    {
                        showBlock(x + 1, y);
                    }

                    //if (y > 0 && !grid[x + 1, y - 1].IsVisible)
                    //{
                    //    showBlock(x + 1, y - 1);
                    //}

                    //if (y < size - 1 && !grid[x + 1, y + 1].IsVisible)
                    //{
                    //    showBlock(x + 1, y + 1);
                    //}
                }
            }
        }

        public void loseGame()
        {
            int i, j;

            for (i = 0; i < size; i++)
            {
                for (j = 0; j < size; j++)
                {
                    if (grid[i,j].IsBomb())
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

        public void Flag(int i, int j, bool cond)
        {
            ((GameBlock)grid[i, j]).changeFlag(cond);

            //if (((GameBlock)grid[i, j]).HasFlag)
            //{
            //    if (((GameBlock)grid[i, j]).IsBomb())
            //        flagCount--;
            //}
            //else   if (((GameBlock)grid[i, j]).IsBomb()) flagCount++;
        }

        public bool winGame()
        {
            int i;

            for (i=0;i < difficulty; i++)
            {
                //Console.WriteLine("Bomb [" + i + "] = [" + bombs[i].Row + "," + bombs[i].Col + "]");
                if (!((GameBlock)grid[bombs[i].Row,bombs[i].Col]).HasFlag)
                    return false;
            }

            return true;
        }
    }



}
