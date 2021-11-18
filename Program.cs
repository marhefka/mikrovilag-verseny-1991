using System;
using System.Collections.Generic;

namespace mikrovilag_1991_verseny_golyo_feladat
{
    class Program
    {// proba branch
        static void Main(string[] args)
        {
            Console.Write("Melyik golyot? (x1 y1): ");  // console-ra irunk
          
            string s = Console.ReadLine();      // new comment
            string[] ss = s.Split(" ");
            int x1 = Int32.Parse(ss[0]);
            int y1 = Int32.Parse(ss[1]);

            Console.Write("Melyik poziciora? (x2 y2): ");
            string s2 = Console.ReadLine();
            string[] ss2 = s2.Split(" ");
            int x2 = Int32.Parse(ss2[0]);
            int y2 = Int32.Parse(ss2[1]);

            int ux = 9, uy = 9;

            PrintState(x1, y1, x2, y2, ux, uy);

            while (!(x1 == x2 && y1 == y2))
            {

                int x3;
                int y3;
                if (y1 == y2)
                {
                    y3 = y1;
                    x3 = x1 + Math.Sign(x2 - x1);
                }
                else
                {
                    x3 = x1;
                    y3 = y1 + Math.Sign(y2 - y1);
                }

                MoveHoleWithoutMovingBall(x1, y1, x2, y2, ux, uy, x3, y3);
                ux = x3;
                uy = y3;

                Swap(ref x1, ref ux);
                Swap(ref y1, ref uy);
                PrintState(x1, y1, x2, y2, ux, uy);
            }
        }

        class COOR
        {
            public int x, y;
        }

        static void MoveHoleWithoutMovingBall(int x1, int y1, int x2, int y2, int ux, int uy, int x3, int y3)
        {
            int[,] m = new int[11,11];
            COOR[,] c = new COOR[11, 11];

            for(int i = 1; i <= 9; i++)
            {
                for(int j = 1; j<= 9; j++)
                {
                    m[i, j] = Int32.MaxValue;
                }
            }

            m[x1, y1] = -1;
            for(int i = 0; i <= 10; i++)
            {
                m[0, i] = -1;
                m[10, i] = -1;
                m[i, 0] = -1;
                m[i, 10] = -1;
            }

            MoveHoleWithoutMovingBall(x3, y3, ux, uy, m, c, 0, null);

            List<COOR> path = new List<COOR>();
            COOR q = c[x3, y3];
            while (q != null)
            {
                path.Add(q);
                q = c[q.x, q.y];
            }

            for (int i = path.Count - 2; i >= 0; i--)
            {
                PrintState(x1, y1, x2, y2, path[i].x, path[i].y);
            }
            PrintState(x1, y1, x2, y2, x3, y3);
        }

        static void MoveHoleWithoutMovingBall(int x3, int y3, int ux, int uy, int[,] m, COOR[,] c, int step, COOR prevCoor)
        {
            m[ux, uy] = step;
            c[ux, uy] = prevCoor;

            COOR actCoor = new COOR();
            actCoor.x = ux;
            actCoor.y = uy;

            if (m[ux + 1, uy] > step + 1)
            {
                MoveHoleWithoutMovingBall(x3, y3, ux + 1, uy, m, c, step + 1, actCoor);
            }

            if (m[ux - 1, uy] > step +1)
            {
                MoveHoleWithoutMovingBall(x3, y3, ux - 1, uy, m, c, step + 1, actCoor);

            }

            if (m[ux, uy + 1] > step + 1)
            {
                MoveHoleWithoutMovingBall(x3, y3, ux, uy + 1, m, c, step + 1, actCoor);

            }

            if (m[ux, uy - 1] > step + 1)
            {
                MoveHoleWithoutMovingBall(x3, y3, ux, uy - 1, m, c, step + 1, actCoor);
            }
        }


        static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp = lhs;
            lhs = rhs;
            rhs = temp;
        }

        static void PrintState(int x1, int y1, int x2, int y2, int ux, int uy)
        {
            for (int y = 1; y <= 9; y++)
            {
                for (int x = 1; x<= 9; x++)
                {
                    if (x == ux && y == uy)
                    {
                        Console.Write(" ");
                        continue;
                    }

                    if (x == x1 && y == y1)
                    {
                        Console.Write("O");
                        continue;
                    }

                    if (x == x2 && y == y2)
                    {
                        Console.Write("X");
                        continue;
                    }

                    Console.Write(".");
                }

                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
