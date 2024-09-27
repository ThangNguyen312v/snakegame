using System.Security.Cryptography.X509Certificates;

namespace snakeGame{
     public class Point
    {
        public int X { set; get; }
        public int Y { set; get; }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    class snakeCTR
    {
        private Point food = new Point(8, 8);
        private bool foodExist = false;
        private int speed = 500;
        private int row = 20;
        private int col = 40;
        private string direction = "Right";
        private int score;
        private string[,] broad;
        private Point[] body = new Point[1]
        {
            new Point(4,4)
        };
        private Point head = new Point(10, 10);

        public Point Food { get => food; set => food = value; }
        public bool FoodExist { get => foodExist; set => foodExist = value; }
        public int Speed { get => speed; set => speed = value; }
        public int Row { get => row; set => row = value; }
        public int Col { get => col; set => col = value; }
        public string Direction { get => direction; set => direction = value; }
        public int Score { get => score; set => score = value; }
        public string[,] Broad { get => broad; set => broad = value; }
        public Point[] Body { get => body; set => body = value; }
        public Point Head { get => head; set => head = value; }

        public snakeCTR()
        {
            string[,] broad = new string[Row, Col];
        }

        public snakeCTR(Point food, bool foodExist, int speed, int row, int col, string direction, int score, Point _head)
        {
            this.Food = food;
            this.FoodExist = foodExist;
            this.Speed = speed;
            this.Row = row;
            this.Col = col;
            this.Direction = direction;
            this.Score = score;
            this.Head = _head;
            Broad = new string[Row, Col];
        }

        public void Drawboard()
        {
        
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    if (i == 0 || i == Row - 1 || j == 0 || j == Col - 1)
                    {
                        Broad[i, j] = "#";
                    }
                    else if (i == Head.X && j == Head.Y)
                    {
                        Broad[i, j] = "*";
                    }
                    else
                    {
                        bool isBodyPart = false;
                        for (int count = 0; count < Body.Length; count++)
                        {
                            if (i == Body[count].X && j == Body[count].Y)
                            {
                                Broad[i, j] = "+";
                                isBodyPart = true;
                                break;
                            }
                        }
                        if (!isBodyPart)
                        {
                            if (i == Food.X && j == Food.Y)
                            {
                                Broad[i, j] = "@";
                            }
                            else
                            {
                                Broad[i, j] = " ";
                            }
                        }
                    }
                }
            }
        }
         public void setUpBoard()
        {
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    Console.Write(Broad[i, j]);
                }
                Console.WriteLine();
            }
        }
        public void MoveHead()
        {
            switch(Direction)
            {
                case"Right":
                    Head.Y += 1;
                    if(Head.Y == -1)
                        {
                        Head.Y = 1;
                        }
                break;
                case "Left":
                    Head.Y -= 1;
                    if (Head.Y == 0)
                    {
                        Head.Y = Col - 1;
                    }
                    break;
                case "Up":
                    Head.X -= 1;
                    if (Head.X == 0)
                    {
                        Head.X = Row - 1;
                    }
                    break;
                case "Down":
                    Head.X += 1;
                    if (Head.X == Row - 1)
                    {
                        Head.X = 1;
                    }
                    break;
            }
        }
          public void ListenKey()
        {
            while (true)
            {
                ConsoleKeyInfo keyinfo = Console.ReadKey();
                switch (keyinfo.Key)
                {
                    case ConsoleKey.RightArrow:
                        if (Direction == "Up" || Direction == "Down" || Direction == "Left")
                        {
                            Direction = "Right";
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (Direction == "Up" || Direction == "Down" || Direction == "Right")
                        {
                            Direction = "Left";
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (Direction == "Left" || Direction == "Right" || Direction == "Down")
                        {
                            Direction = "Up";
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (Direction == "Left" || Direction == "Right" || Direction == "Up")
                        {
                            Direction = "Down";
                        }
                        break;
                }
            }
        }
        public void EatFood()
        {
             
             Point[] newbody = Body;
            if(Head.X == Food.X && Head.Y == Food.Y )
            {
                Score += 1;
                Array.Resize(ref newbody, newbody.Length + 1);
                newbody[newbody.Length -1] = new Point(-1,-1);
                Body = newbody;
                Speed -= 20;
                FoodExist = false;
            }
        }
        public void SpawnBody()
        {
            for(int i = Body.Length -1; i > 0; i--)
            {
                Body[i].X = Body[i-1].X;
                Body[i].Y = Body[i-1].Y;
            }
            if(Body.Length > 0)
            {
                Body[0].X = Head.X;
                Body[0].Y = Head.Y;
            }
        }
        public void PopupFood()
        {
            Random random = new Random();
            int x = random.Next(1, Row -1);
            int y = random.Next(1, Col - 1);
            if(x != Head.X && y != Head.Y)
            {
                if(FoodExist == false)
                {
                     Food.X = x;
                    Food.Y = y;
                    FoodExist = true;

                }
            }
        }
        public bool checkDead(bool dead)
        {
             if(Head.Y == Col - 1 || Head.Y <= 0 || Head.X <= 0 || Head.X == Row - 1 )
             {
                 Console.WriteLine("Game Over");
                return true;
             }
             return false;
        }
        public bool Stopgame()
        {
         ConsoleKeyInfo keyinfo = Console.ReadKey();
         
         switch(keyinfo.Key)
         {
            case ConsoleKey.Escape:
            return true;
         }
         return false;
        }
        public void ShowPoint()
        {
            Console.WriteLine($"score: {Score}");
        }

        public void savePoint()
        {
            string saveP = @"E:\hoctap\snake\Point.csv";
            using(StreamWriter save = new StreamWriter(saveP))
            {
                save.Write($"so diem trong vong truoc la: {Score}");
            }
        }
        public int LoadScore()
        {
            string saveP = @"E:\hoctap\snake\Point.csv";
            int diem;
            if(File.Exists(saveP))
            {
                using(StreamReader readscore = new StreamReader(saveP))
                {
                    if(int.TryParse(readscore.ReadLine(),out diem))
                    {
                        return diem;
                    }
                }
            }
            return  0;
        }
    }
}