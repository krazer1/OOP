using System;
using System.Text;
using System.Globalization;
using System.Collections;
using System.Linq;

namespace OOP.LAB6.TEST
{
    class Matrix : IEnumerator, IEnumerable, ICloneable, IComparable, IFormattable, ICollection
    {
        protected int row, col;
        protected int[] arr;
        static int idx = 1;
        public int id;

        #region Интерфейсы
        /*
         * IEnumerable служит для перечисления элементов матрицы, 
         * интерфейс ICloneable реализует клонирование матрицы, 
         * интерфейс IComparable содержит метод CompareTo для сравнения матриц, 
         * интерфейс IFormattable содержит метод ToString для форматирования вывода матрицы
        */

        private int position = -1;

        public delegate double Del(uint i, uint j);
        /*
         * Делегаты используются для передачи методов в качестве параметров к другим методам.
         */

        //IEnumerable
        public object Current
        {                     //переменная ссылочного типа object может ссылаться на объект любого другого типа
            get { return arr[position]; }  // Кроме того, переменная типа object может ссылаться на любой массив, 
        }                                    // поскольку в C# массивы реализуются как объекты.


        public IEnumerator GetEnumerator() // пронумеровывает каждый эл-т, нужен для IEnumerable, foreach
        {
            return (IEnumerator)this;
        }

        public bool MoveNext() // переводит курсор на следующую позицию в матрице и возвращает true, 
        {                      // если курсор не достиг конца матрицы (по сути проверка не вышли ли за границы)
            position++;
            return (position < arr.Length);
        }

        public void Reset() //сбрасывает курсор в начало матрицы
        {
            position = -1;
        }
        //end of numerable

        //clonable
        object ICloneable.Clone() // объявляет явную реализацию интерфейса 
        {                         // ICloneable для создания клонов объектов
            return Clone();
        }
        /*
         * Этот метод создает копию объекта и возвращает ссылку на эту копию.
         * Отметим, что при этом выполняется неглубокое копирование, т.е. копируются все типы значений в классе.
         * Если же класс включает в себя члены ссылочных типов, то копируются только ссылки, а не объекты,
         * на которые они указывают. 
        */

        public Matrix Clone() //создает клон объекта Matrix и возвращает его как результат
        {
            var matr = new Matrix(this);
            return matr;
        }
        //end of clonable

        public int CompareTo(object? obj)
        {
            if (obj == null)
                return 1;  // возвращает 1 (указывая, что текущий объект больше null объекта)

            Matrix? otherMatr = obj as Matrix;
            int otherSize = (int)otherMatr.row * (int)otherMatr.col;
            int objSize = (int)this.row * (int)this.col;
            if (otherSize != null)                  // сравнение размеров матриц. Если количество элементов другой матрицы не равно null, то используется метод CompareTo() для сравнения количества элементов текущей матрицы с количеством элементов другой матрицы
                return objSize.CompareTo(otherSize); // и возвращение соответствующего значения.
            else
                throw new ArgumentException("Error");
        }
        //end of compare

        //formatte

        //public string ToString(string? format, IFormatProvider? formatProvider)
        //{
        //    if (String.IsNullOrEmpty(format)) format = "Q";
        //    if (formatProvider == null) formatProvider = CultureInfo.CurrentCulture; //  если провайдер формата равен null, то ему присваивается текущая культура

        //    switch (format.ToUpperInvariant())
        //    {
        //        case "Q": //как матрица 
        //            return this.ToString();
        //        case "S": //в одну строку
        //            {
        //                string matrixString = "";
        //                for (int i = 0; i < row; i++)
        //                {
        //                    for (int j = 0; j < col; j++)
        //                        matrixString += arr[i * col + j].ToString() + '\t';
        //                }
        //                return matrixString;
        //            }

        //        default:
        //            throw new FormatException(String.Format("{0} формат не поддерживается", format));
        //    }
        //}

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            if (String.IsNullOrEmpty(format)) format = "Q";
            if (formatProvider == null) formatProvider = CultureInfo.CurrentCulture;

            switch (format.ToUpperInvariant())
            {
                case "Q": //как матрица
                    {
                        var sb = new StringBuilder();

                        int count = 0;
                        for (int i = 0; i < row; i++)
                        {
                            for (int j = 0; j < col; j++, count++)
                            {
                                sb.Append(arr[count].ToString());
                                if (j != col - 1)
                                {
                                    sb.Append('\t');
                                }
                            }
                            sb.AppendLine();
                        }
                        return sb.ToString();
                    }
                case "S": //в одну строку
                    {
                        string matrixString = "";
                        for (int i = 0; i < row; i++)
                        {
                            for (int j = 0; j < col; j++)
                            {
                                matrixString += arr[i * col + j].ToString();
                                if (j != col - 1)
                                {
                                    matrixString += '\t';
                                }
                            }
                        }
                        return matrixString;
                    }

                default:
                    throw new FormatException(String.Format("{0} формат не поддерживается", format));
            }
        }

        //end of formatte

        //collection
        public int Count => ((ICollection)arr).Count;
        public bool IsSynchronized => arr.IsSynchronized;
        public object SyncRoot => arr.SyncRoot;
        public void CopyTo(Array array, int index) // копирует элементы матрицы arr в другой массив, начиная с указанного индекса
        {
            arr.CopyTo(array, index);
        }

        //end of collection
        #endregion

        #region Конструкторы и финализатор
        //По умолчанию
        public Matrix() : this(1, 3, 0)
        {
            Console.WriteLine($"Создана матрица {id}.\n");
        }

        public Matrix(int Row, int Col, int num) : this(Row, Col, new int[Row * Col].Select(x => num).ToArray()) { }

        public Matrix(int Razmernost, int num) : this(Razmernost, Razmernost, new int[Razmernost * Razmernost].Select(x => num).ToArray())
        {
            Console.WriteLine($"Создана квадратная матрица {id}.\n");
        }

        public Matrix(int Row, int Col, int[] array)
        {
            if (Row == 0 && Col == 0)
            {
                throw new Exception("Неверные параметры матрицы!");
            }
            row = Row; col = Col;
            arr = new int[row * col];
            for (int i = 0; i < row * col; i++)
            {
                arr[i] = array[i];
            }
            id = idx;
            idx++;
            Console.WriteLine($"Создана матрица {id} с заданным размером.\n");
        }

        //Копирование
        public Matrix(Matrix mat)
        {
            row = mat.ROW;
            col = mat.COL;
            arr = new int[row * col];
            Array.Copy(mat.arr, arr, row * col);
            Console.WriteLine($"Создана матрица(копия) {id}.\n");
            id = idx;
            idx++;
        }

        //Копирование по массиву
        public Matrix(int[,] array)
        {
            row = array.GetLength(0);
            col = array.GetLength(1);
            arr = new int[row * col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    arr[i * col + j] = array[i, j];
                }
            }
            id = idx;
            idx++;
            Console.WriteLine($"Создана матрица {id} с заданным размером.\n");
        }

        public Matrix(int Row, int Col, in Func<int, int, int> func)
        {
            if (Row == 0 && Col == 0)
            {
                throw new Exception("Неверные параметры матрицы!");
            }
            row = Row;
            col = Col;
            arr = new int[row * col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    arr[i * col + j] = func(i,j);
                }
            }
            id = idx;
            idx++;
            Console.WriteLine($"Создана матрица {id} по заданной функции.\n");
        }

        ~Matrix()
        {
            Console.WriteLine($"матрица {id} удалена.");
        }
        #endregion

        #region Свойства
        public int ROW => row;

        public int COL
        {
            get { return col; }
        }

        public int this[int Row, int Col]
        {
            get
            {
                if(Row >= row || Row < 0 || Col >= col || Col < 0)
                {
                    throw new IndexOutOfRangeException("Выход за пределы массва!");
                }
                return arr[Row * col + Col];
            }
            set
            {
                if (Row >= row || Row < 0 || Col >= col || Col < 0)
                {
                    throw new IndexOutOfRangeException("Выход за пределы массва!");
                }
                arr[Row * col + Col] = value;
            }
        }
        #endregion

        #region Функции
        public bool OppSum(Matrix a)
        {
            return a.row == row && a.col == col;
        }

        public bool OppMul(Matrix a)
        {
            return a.col == row;
        }

        public void Output()
        {
            int count = 0;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++, count++)
                {
                    Console.Write($"{arr[count]} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public int Max()
        {
            int max = int.MinValue;
            for(int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > max)
                    max = arr[i];
            }
            return max;
        }

        public int Min()
        {
            int min = int.MaxValue;
            for (int i = 0; i < arr.Length; i++)
            {
                if (min > arr[i])
                    min = arr[i];
            }
            return min;
        }

        //public override string ToString()
        //{
        //    var sb = new StringBuilder();

        //    int count = 0;
        //    for(int i = 0; i < row; i++)
        //    {
        //        for(int j = 0; j < col; j++, count++)
        //        {
        //            sb.Append(arr[count].ToString());
        //        }
        //        sb.AppendLine();
        //    }
        //    return sb.ToString();
        //}
        #endregion

        #region Операторы
        // A+B
        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.OppSum(b) == false)
            {
                throw new Exception($"Матрицы {a.id} и {b.id} нельзя складывать");
            }

            int Row = a.ROW;
            int Col = a.COL;
            int[] array = new int[Row * Col];
            for (int i = 0; i < Row * Col; i++)
            {
                array[i] = a.arr[i] + b.arr[i];
            }

            return new Matrix(Row, Col, array);
        }

        // A-B
        public static Matrix operator -(Matrix a, Matrix b)
        {
            if(a.OppSum(b) == true)
            {
                int Row = a.ROW;
                int Col = a.COL;
                int[] array = new int[Row * Col];
                for (int i = 0; i < Row * Col; i++)
                {
                    array[i] = a.arr[i] - b.arr[i];
                }

                return new Matrix(Row, Col, array);
            }
            else { throw new Exception($"Матрицы {a.id} и {b.id} нельзя вычитать"); }
        }

        // A*B
        public static Matrix operator *(Matrix a, Matrix b)
        {

            if(a.OppMul(b) == true)
            {
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
            else { throw new Exception($"Матрицы {a.id} и {b.id} нельзя перемножать"); }
        }

        // A*k
        public static Matrix operator *(Matrix a, int x)
        {
            int Row = a.ROW;
            int Col = a.COL;
            int[] array = new int[Row * Col];
            for (int i = 0; i < Row * Col; i++)
            {
                array[i] = a.arr[i] * x;
            }

            return new Matrix(Row, Col, array);
        }
        #endregion
    }
}
