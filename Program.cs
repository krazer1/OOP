using System;
using System.Globalization;


namespace OOP.LAB6.TEST
{
    internal class Program
    {
          static Matrix Sum(Matrix a, Matrix b)
        {
            if (!a.OppSum(b))
            {
                throw new Exception($"Матрицы {a.id} и {b.id} нельзя складывать");
            }
            int Row = a.ROW;
            int Col = a.COL;
            int[,] array = new int[Row, Col];
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    array[i, j] = a[i, j] + b[i, j];
                }
            }
            return new Matrix(array);
        }

        static Matrix Sub(Matrix a, Matrix b)
        {
            if (!a.OppSum(b))
            {
                throw new Exception($"Матрицы {a.id} и {b.id} нельзя вычитать");
            }
            int Row = a.ROW;
            int Col = a.COL;
            int[,] array = new int[Row, Col];
            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    array[i, j] = a[i, j] - b[i, j];
                }
            }
            return new Matrix(array);
        }

        static Matrix Mul(Matrix a, Matrix b)
        {
            if (!a.OppMul(b))
            {
                throw new Exception($"Матрицы {a.id} и {b.id} нельзя перемножать");
            }
            int Row = a.ROW;
            int Col = b.COL;
            int[,] array = new int[Row, Col];
            for (int row1 = 0; row1 < a.ROW; row1++)
            {
                for (int col2 = 0; col2 < b.COL; col2++)
                {
                    for (int i = 0; i < b.ROW; i++)
                    {
                        array[row1, col2] += a[row1, i] * b[i, col2];
                    }
                }
            }
            return new Matrix(array);
        }
// Типы значения и ссылочные типы c#

        static Matrix MulOnSkul(Matrix a, int k)
        {
            int Row = a.ROW;
            int Col = a.COL;
            int[,] array = new int[Row, Col];

            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Col; j++)
                {
                    array[i, j] = a[i, j] * k;
                }
            }
            return new Matrix(array);
        }
        
        public static void Main(string[] args)
        {
            try
            {
                Matrix square = new Matrix(5, 6);
                Console.WriteLine(square);

                Matrix m1 = new Matrix(2, 2, 2);
                Console.WriteLine(m1);
                Matrix m2 = new Matrix(2, 2, 1);
                Console.WriteLine(m2);
                Matrix m3 = m1 * m2;
                Console.WriteLine(m3);
                int x = m1[0, 1];


                Matrix m4 = Mul(m1, m2);
                Console.WriteLine(m4);

                Console.WriteLine("Проверка интерфейса");

                Console.WriteLine("проверка numerable");

                Matrix m5 = new Matrix(3, 3, 9);
                foreach(var i in m5) { Console.Write(" {0}", i); } //проверка numerable
                
                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("проверка Clone");

                m5 = m1.Clone(); //проверка Clone
                Console.WriteLine(m5);

                Console.WriteLine("проверка CompareTo");

                if (m1.CompareTo(m5) < 0) //проверка CompareTo
                    Console.WriteLine("m1 < m5");
                else if (m1.CompareTo(m5) == 0)
                    Console.WriteLine("m1 = m5");
                else
                    Console.WriteLine("m1 > m5");

                Console.WriteLine("проверка formattable");

                Console.WriteLine(m5.ToString("S", CultureInfo.CurrentCulture)); //проверка formattable
                Console.WriteLine();
                Console.WriteLine(m5.ToString("Q", CultureInfo.CurrentCulture));
                Console.WriteLine();

                Console.WriteLine("проверка collection");

                Console.WriteLine(m5.Count); //проверка collection
            }
            catch (Exception mimi)
            {
                Console.WriteLine(mimi);
            }
        }
    }
}
