using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static matrixMxN.Matrix;

namespace matrixMxN
{
    internal class Menu
    {
        private List<Matrix> vec = new List<Matrix>();

        public Menu() { }

        public void Run()
        {
            int n;
            do
            {
                PrintMenu();
                try
                {
                    n = int.Parse(Console.ReadLine()!);
                }
                catch (System.FormatException) { n = -1; }
                switch (n)
                {
                    case 1:
                        GetElement();
                        break;
                    case 2:
                        PrintMatrix();
                        break;
                    case 3:
                        AddMatrix();
                        break;
                    case 4:
                        Sum();
                        break;
                    case 5:
                        Mul();
                        break;
                }

            } while (n != 0);

        }

        #region Menu operations
        static private void PrintMenu()
        {
            Console.WriteLine("\n\n 0. - Quit");
            Console.WriteLine(" 1. - Get an element");
            Console.WriteLine(" 2. - Print a matrix");
            Console.WriteLine(" 3. - Set a matrix");
            Console.WriteLine(" 4. - Add matrices");
            Console.WriteLine(" 5. - Multiply matrices");
            Console.Write(" Choose: ");
        }

        private int GetIndex()
        {
            if (vec.Count == 0) return -1;
            int n = 0;
            bool ok;
            do
            {
                Console.Write("Give a matrix index: ");
                ok = false;
                try
                {
                    n = int.Parse(Console.ReadLine()!);
                    ok = true;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Integer is expected!");
                }
                if (n <= 0 || n > vec.Count)
                {
                    ok = false;
                    Console.WriteLine("No such matrix!");
                }
            } while (!ok);
            return n - 1;
        }

        private void GetElement()
        {
            if (vec.Count == 0)
            {
                Console.WriteLine("Set a matrix first!");
                return;
            }
            int ind = GetIndex();
            do
            {
                try
                {
                    Console.WriteLine("Give the index of the row: ");
                    int i = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("Give the index of the column: ");
                    int j = int.Parse(Console.ReadLine()!);
                    Console.WriteLine($"a[{i},{j}]={vec[ind].GetElement(i, j)}");
                    break;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine($"Index must be between 1 and size of matrix");
                }
                catch (InvalidIndexException)
                {
                    Console.WriteLine($"Index must be between 1 and size of matrix");
                }
            } while (true);
        }
        private void PrintMatrix()
        {
            if (vec.Count == 0)
            {
                Console.WriteLine("Set a matrix first!");
                return;
            }
            int ind = GetIndex();
            Console.Write(vec[ind].ToString());
        }


        private void AddMatrix()
        {
            int ind = vec.Count;
            bool ok = false;
            int n = -1;
            int m = -1;

            do
            {
                Console.Write("Enter number of rows: ");
                try
                {
                    n = int.Parse(Console.ReadLine()!);
                    ok = n > 0;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Positive integer is expected!");
                }
            } while (!ok);
            do
            {
                Console.Write("Enter number of columns: ");
                try
                {
                    m = int.Parse(Console.ReadLine()!);
                    ok = m > 0;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Positive integer is expected!");
                }
            } while (!ok);
            Matrix k = new Matrix(n,m);

            ok = true;
            List<int> elements = new List<int>();
            for (int i = 0; i < Matrix.Length(n,m); i++)
            {
                Console.Write("Element: ");
                try
                {
                    int elem = int.Parse(Console.ReadLine()!);
                    if (elem == 0) { throw new EntryIsZeroException(); }
                    elements.Add(elem);
                }
                catch (EntryIsZeroException)
                {
                    Console.WriteLine("Nonzero entry is expected");
                    ok = false;
                    break;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Number is expected!");
                    ok = false;
                    break;
                }
            }

            if (ok)
            {
                k.Set(elements);
                vec.Add(k);
            }
        }
        private void Sum()
        {
            if (vec.Count == 0)
            {
                Console.WriteLine("Set a matrix first!");
                return;
            }
            Console.Write("1st matrix: ");
            int ind1 = GetIndex();
            Console.Write("2nd matrix: ");
            int ind2 = GetIndex();
            try
            {
                Console.Write(Matrix.Add(vec[ind1], vec[ind2]).ToString());
            }
            catch (Matrix.DifferentSizeException)
            {
                Console.WriteLine("Dimension mismatch!");
            }
        }
        private void Mul()
        {
            if (vec.Count == 0)
            {
                Console.WriteLine("Set a matrix first!");
                return;
            }
            Console.Write("1st matrix: ");
            int ind1 = GetIndex();
            Console.Write("2nd matrix: ");
            int ind2 = GetIndex();
            try
            {
                Console.Write(Matrix.Multiply(vec[ind1], vec[ind2]).ToString());
            }
            catch (Matrix.DifferentSizeException)
            {
                Console.WriteLine("Dimension mismatch!");
            }
        }

        #endregion



    }
}
