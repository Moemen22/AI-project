using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace AI_project
{
    class Snake
    {

        public int size = 5;
        public Color color = Color.Ivory;
        public square sh;
        public int shr;
        public int shc;
    }
    class square
    {
        public Rectangle rectangle;
        public Color color;
        public char Attack = ' ';
    }
    class BigMap
    {

        public square[,] squares;
        public Snake snake = new Snake();
        public List<Blocks> Block = new List<Blocks>();
        public List<Food> foods = new List<Food>();
        public int rows = 15;
        public int cols = 28;
        public int[,] scells;
        Random R = new Random();
        public char direction = ' ';    
        public void SS()
        {
            snake.sh = squares[8, 13];
            snake.shc = 13;
            snake.shr = 8;
            squares[8, 13].Attack = 'S';
            scells = new int[snake.size + 1, 2];
            scells[0, 0] = 8;
            scells[0, 1] = 13;
            for (int i = 0; i < snake.size; i++)
            {
                squares[snake.shr, snake.shc - 1 - i].Attack = 'S';
                scells[i + 1, 0] = snake.shr;
                scells[i + 1, 1] = snake.shc - 1 - i;
            }
        }

        public void CreateFood()
        {
            for (int i = 0; i < foods.Count; i++)
            {
                if (squares[foods[i].row, foods[i].col].Attack != 'F')
                {
                    foods.RemoveAt(i);
                    i--;
                }
            }
            int random1 = R.Next(0, rows);
            int random2 = R.Next(0, cols);
            if (foods.Count() == 0)
            {
                if (squares[random1, random2].Attack == ' ')
                {
                    Food food = new Food();
                    food.row = random1;
                    food.col = random2;
                    foods.Add(food);
                    squares[random1, random2].Attack = 'F';
                }
                else
                {
                    CreateFood();
                }
            }
        }
        public int Snakemotion(node obj)
        {

            char dir = ' ';
            if (obj.r == snake.shr && obj.c > snake.shc)
            {
                dir = 'R';
                MoveSnake(dir);
                return 0;
            }
            if (obj.r == snake.shr && obj.c < snake.shc)
            {
                dir = 'L';
                MoveSnake(dir);
                return 0;
            }
            if (obj.r < snake.shr && obj.c == snake.shc)
            {
                dir = 'U';
                MoveSnake(dir);
                return 0;
            }
            if (obj.r > snake.shr && obj.c == snake.shc)
            {
                dir = 'D';
                MoveSnake(dir);
                return 0;
            }
            return 0;
        }
        public void MoveSnake(char dir)
        {
            for (int i = snake.size; i > 0; i--)
            {
                scells[i, 0] = scells[i - 1, 0];
                scells[i, 1] = scells[i - 1, 1];
            }
            if (dir == 'U')
            {
                if (squares[snake.shr - 1, snake.shc].Attack != ' ')
                {
                    if (squares[snake.shr - 1, snake.shc].Attack == 'F')
                    {
                        GrowSnake();
                    }
                    if (squares[snake.shr - 1, snake.shc].Attack == 'O')
                    {
                        SnakeCollid();
                    }
                }
                scells[0, 0] = snake.shr - 1;
                scells[0, 1] = snake.shc;
            }

            if (dir == 'L')
            {

                if (squares[snake.shr, snake.shc - 1].Attack != ' ')
                {
                    if (squares[snake.shr, snake.shc - 1].Attack == 'F')
                    {
                        GrowSnake();
                    }
                    if (squares[snake.shr, snake.shc - 1].Attack == 'O')
                    {
                        SnakeCollid();
                    }
                }
                scells[0, 0] = snake.shr;
                scells[0, 1] = snake.shc - 1;
            }
            if (dir == 'R')
            {

                if (squares[snake.shr, snake.shc + 1].Attack != ' ')
                {
                    if (squares[snake.shr, snake.shc + 1].Attack == 'F')
                    {
                        GrowSnake();
                    }
                    if (squares[snake.shr, snake.shc + 1].Attack == 'O')
                    {

                        SnakeCollid();
                    }

                }
                scells[0, 0] = snake.shr;
                scells[0, 1] = snake.shc + 1;
            }
            if (dir == 'D')
            {

                if (squares[snake.shr + 1, snake.shc].Attack != ' ')
                {
                    if (squares[snake.shr + 1, snake.shc].Attack == 'F')
                    {
                        GrowSnake();
                    }
                    if (squares[snake.shr + 1, snake.shc].Attack == 'O')
                    {
                        SnakeCollid();
                    }
                }
                scells[0, 0] = snake.shr + 1;
                scells[0, 1] = snake.shc;
            }
            direction = dir;
            updatess();
        }
        public void SnakeCollid()
        {
            MessageBox.Show("Faild");
        }

        public void updatess()
        {
            int count = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int k = 0; k < cols; k++)
                {
                    if (squares[i, k].Attack == 'S')
                    {
                        squares[i, k].Attack = ' ';
                    }
                }
            }

            for (int i = 0; i < snake.size + 1; i++)
            {
                squares[scells[i, 0], scells[i, 1]].Attack = 'S';
                count++;
            }
            snake.shr = scells[0, 0];
            snake.shc = scells[0, 1];
            snake.sh = squares[snake.shr, snake.shc];
        }
        public void GrowSnake()
        {
            snake.size++;
            int[,] temp;
            temp = new int[snake.size, 2];
            for (int i = 0; i < snake.size - 1; i++)
            {
                temp[i, 0] = scells[i, 0];
                temp[i, 1] = scells[i, 1];
            }
            temp[snake.size - 1, 0] = temp[snake.size - 2, 0];
            temp[snake.size - 1, 1] = temp[snake.size - 2, 1] - 1;

            scells = new int[snake.size + 1, 2];
            for (int i = 0; i < snake.size; i++)
            {
                scells[i, 0] = temp[i, 0];
                scells[i, 1] = temp[i, 1];
            }
        }    
        public int CreateBlocks(int n)
        {
            for (int i = 0; i < rows; i++)
            {
                squares[i, 0].Attack = 'O';
                squares[i, cols - 1].Attack = 'O';
            }
            if (n == 0)
            {
                return 0;
            }
            int random1 = R.Next(0, rows);
            int random2 = R.Next(0, cols);
            if (squares[random1, random2].Attack == ' ')
            {
                Blocks obstacle = new Blocks();
                obstacle.row = random1;
                obstacle.col = random2;
                Block.Add(obstacle);

                squares[random1, random2].Attack = 'O';
                n--;
            }
            CreateBlocks(n);
            return 0;
        }
    }
    class Blocks
    {
        public int row, col;
    }
    class node
    {
        public int r, c;
    }
    class Food
    {
        public int row, col;

    }
    interface Behaviour
    {
        List<node> FindPath(BigMap map);
    }
    //////////////////////algorthim////////////////////
    /// 1////
    class DFS : Behaviour
    {

        public List<node> path = new List<node>();
        int x, y;
        square[,] array;
        int[,] vist;

        public DFS(int row, int col, square[,] obj)
        {
            x = row;
            y = col;
            vist = new int[x, y];
            for (int i = 0; i < x; i++)
            {
                for (int k = 0; k < y; k++)
                {
                    vist[i, k] = 0;
                }
            }
            SetArray(obj);
        }
        public void SetArray(square[,] obj)
        {
            array = new square[x, y];

            for (int i = 0; i < x; i++)
            {
                for (int k = 0; k < y; k++)
                {
                    array[i, k] = new square();
                    array[i, k].Attack = obj[i, k].Attack;
                }
            }
        }
        int DFSUtil(int x, int y, int[,] vist)
        {
            if (!isvalid(x, y, vist)) return 0;
            vist[x, y] = 1;
            if (array[x, y].Attack == 'F')
            {
                node n = new node();
                n.r = x;
                n.c = y;
                path.Add(n);
                return 1;
            }
            if (array[x, y].Attack == 'O')
            {
                return -1;
            }
            //right
            if (DFSUtil(x + 1, y, vist) == 1)
            {
                node n = new node();
                n.r = x;
                n.c = y;
                path.Add(n);
                return 1;
            }
            //left
            if (DFSUtil(x - 1, y, vist) == 1)
            {
                node n = new node();
                n.r = x;
                n.c = y;
                path.Add(n);
                return 1;
            }
            //down
            if (DFSUtil(x, y + 1, vist) == 1)
            {
                node n = new node();
                n.r = x;
                n.c = y;
                path.Add(n);
                return 1;
            }
            //up
            int result = DFSUtil(x, y - 1, vist);
            if (result == 1)
            {
                node n = new node();
                n.r = x;
                n.c = y;
                path.Add(n);
                return 1;
            }
            if (array[x, y].Attack == 'S')
            {
                return -1;
            }
            return 0;
        }
        bool isvalid(int x, int y, int[,] vist)
        {
            try
            {
                if (array[x, y].Attack == 'O')
                {
                    return false;
                }
            }
            catch
            {
                // out of bounds    
                return false;
            }

            if (vist[x, y]==1)
            {
                return false;
            }

            return true;
        }
        public void dfs(int r, int c, char dir)
        {
            //code to prevent snake from going backwards
            if (dir == 'U')
            {
                r--;
            }
            if (dir == 'D')
            {
                r++;
            }
            if (dir == 'L')
            {
                c--;
            }
            if (dir == 'R')
            {
                c++;
            }
            DFSUtil(r, c, vist);
        }
        public List<node> FindPath(BigMap map)
        {
            dfs(map.snake.shr, map.snake.shc, map.direction);
            return path;
        }
    }
    ///2///
    class BFS : Behaviour
    {
        public List<node> path = new List<node>();
        int x, y;
        square[,] array;
        int[,] vist;
        node[,] prev;
        public BFS(int r, int c, square[,] obj)
        {
            x = r;
            y = c;
            vist = new int[x, y];
            prev = new node[x, y];
            for (int i = 0; i < x; i++)
            {
                for (int k = 0; k < y; k++)
                {
                    vist[i, k] = 0;
                    prev[i, k] = null;
                }
            }
            SetArray(obj);
        }
        public void SetArray(square[,] obj)
        {
            array = new square[x, y];

            for (int i = 0; i < x; i++)
            {
                for (int k = 0; k < y; k++)
                {
                    array[i, k] = new square();
                    array[i, k].Attack = obj[i, k].Attack;
                }
            }
        }
        int isvalid(int x, int y, int[,] vist)
        {
          
            try
            {
                if (array[x, y].Attack == 'O' || array[x, y].Attack == 'S')
                {
                    return 0;
                }
            }
            catch
            {
                 
                return 0;
            }
            if (vist[x, y]==1)
            {
                return 0;
            }

            return 1;
        }
        public void Perviouspath(int r, int c)
        {
            node pn = new node();
            pn.r = r;
            pn.c = c;
            path.Add(pn);
            for (node ptrav = prev[r, c]; ptrav != null; ptrav = prev[ptrav.r, ptrav.c])
            {
                path.Add(ptrav);
            }
        }
        public void bfs(int x, int y)
        {

            LinkedList<node> queue = new LinkedList<node>();
            vist[x, y] = 1;
            node n = new node();
            n.r = x;
            n.c = y;
            node T = new node();
            queue.AddLast(n);
            while (queue.Any())
            {

                node Current = new node();
                Current = queue.First();
                x = Current.r;
                y = Current.c;
                if (array[Current.r, Current.c].Attack == 'F')
                {
                    Perviouspath(Current.r, Current.c);
                    break;
                }
                queue.RemoveFirst();
                if (isvalid(x + 1, y, vist)==1)
                {
                    vist[x + 1, y] = 1;
                    T = new node();
                    T.r = x + 1;
                    T.c = y;
                    queue.AddLast(T);
                    prev[x + 1, y] = Current;
                }
                if (isvalid(x - 1, y, vist)==1)
                {
                    vist[x - 1, y] = 1;
                    T = new node();
                    T.r = x - 1;
                    T.c = y;
                    queue.AddLast(T);
                    prev[x - 1, y] = Current;

                }
                if (isvalid(x, y + 1, vist)==1)
                {
                    vist[x, y + 1] = 1;

                    T = new node();
                    T.r = x;
                    T.c = y + 1;
                    queue.AddLast(T);
                    prev[x, y + 1] = Current;
                }
                if (isvalid(x, y - 1, vist)==1)
                {
                    vist[x, y - 1] = 1;
                    T = new node();
                    T.r = x;
                    T.c = y - 1;
                    queue.AddLast(T);
                    prev[x, y - 1] = Current;
                }

            }
        }
        public List<node> FindPath(BigMap map)
        {
            bfs(map.snake.shr, map.snake.shc);
            return path;
        }
    }
     //////////////////////////////////////////////////
    static class Program
    {
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
    public partial class Form1 : Form
    {
        public int num;
        bool q= false;
        Bitmap offset;
        BigMap map;
        Behaviour search;
        List<node> Lpath = new List<node>();
         public Random numobsticl = new Random();
        public Form1()
        {
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {       
           
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(e.Graphics);
        }
        public void MakeActors()
        {
            map = new BigMap();
            int X = 0;
            int Y = 0;
            //e///
            int W = 50;
            map.squares = new square[map.rows, map.cols];
            for (int i = 0; i < map.rows; i++)
            {
                for (int k = 0; k < map.cols; k++)
                {
                    map.squares[i, k] = new square();
                    map.squares[i, k].rectangle = new Rectangle();
                    map.squares[i, k].color = Color.Black;
                    map.squares[i, k].rectangle.X = X;
                    map.squares[i, k].rectangle.Y = Y;
                    map.squares[i, k].rectangle.Width = W;
                    map.squares[i, k].rectangle.Height = W;
                    X += W;
                }
                X = 0;
                Y += W;
            }
            map.SS();
            map.CreateBlocks(num);
            map.CreateFood();        
            DrawDubb(CreateGraphics());         
            Start();
        }
        void Start()
        {

            List<node> path = new List<node>();
            //change if you want to BFS
            if (q == false)
            {
                MessageBox.Show("DFS");
                search = new BFS(map.rows, map.cols, map.squares);
                q = true;
            }
            else if (q == true)
            {
                MessageBox.Show("BFS");
                search = new BFS(map.rows, map.cols, map.squares);
                q = false;
            }

            path = search.FindPath(map);
            map.CreateFood();
            DrawDubb(CreateGraphics());
            Thread.Sleep(2000);
            for (int i = path.Count - 1; i > -1; i--)
            {

                map.Snakemotion(path[i]);
                map.squares[path[i].r, path[i].c].color = Color.Black;
                DrawDubb(CreateGraphics());
                Thread.Sleep(10);

            }
            Start();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            num =numobsticl.Next(5,25);
            this.WindowState = FormWindowState.Maximized;
            offset = new Bitmap(this.Width, this.Height);
            MakeActors();

        }
        private void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(offset);
            DrawScene(g2);
            g.DrawImage(offset, 0, 0);
        }
        private void DrawScene(Graphics g)
        {
            int count = 0;
            g.Clear(Color.Black);
            SolidBrush brush;
            for (int i = 0; i < map.rows; i++)
            {
                for (int k = 0; k < map.cols; k++)
                {
                    if (map.squares[i, k].Attack == ' ')
                    {
                        brush = new SolidBrush(map.squares[i, k].color);
                        g.FillEllipse(brush, map.squares[i, k].rectangle);
                    }
                    if (map.squares[i, k].Attack == 'S')
                    {
                        count++;
                        if (map.snake.shc == k && map.snake.shr == i)
                        {
                            int w = map.squares[i, k].rectangle.Width;
                            brush = new SolidBrush(Color.MidnightBlue);
                            g.FillEllipse(brush, map.squares[i, k].rectangle);                         
                        }
                        else
                        {
                            brush = new SolidBrush(Color.Cyan);
                            g.FillEllipse(brush, map.squares[i, k].rectangle);
                        }

                    }
                    if (map.squares[i, k].Attack == 'F')
                    {
                        brush = new SolidBrush(Color.Red);
                        g.FillEllipse(brush, map.squares[i, k].rectangle);
                    }
                    if (map.squares[i, k].Attack == 'O')
                    {
                        brush = new SolidBrush(Color.YellowGreen);
                        g.FillEllipse(brush, map.squares[i, k].rectangle);
                    }
                }
            }
            g.FillRectangle(Brushes.Gray, 0, 0, 50, this.Height);
            g.FillRectangle(Brushes.Gray, this.Width-202, 0, 50, this.Height);
        }
    }
}
