using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matrixMxN
{
    public class Matrix
    {
        #region Exceptions
        public class NegativeSizeException : Exception { };
        public class InvalidIndexException : Exception { };
        public class DifferentSizeException : Exception { };
        public class EntryIsZeroException : Exception { };
        #endregion

        #region Attribute
        private readonly List<int> _matrix = new();
        private readonly int _sizeRow;
        private readonly int _sizeColumn;
        #endregion

        #region Constructors
        public Matrix(int sizeRow, int sizeColumn)//constructor 1
        {
            if (sizeRow <= 0 || sizeColumn <= 0) throw new NegativeSizeException();
            _sizeRow = sizeRow;
            _sizeColumn = sizeColumn;
            _matrix = new List<int>();
            int length;
            if (sizeColumn % 2==0)
            {
                length = (sizeColumn / 2) * sizeRow;
            }
            else
            {
                if (sizeRow % 2 == 0)
                {
                    length = (((sizeColumn / 2) + 1) * (sizeRow / 2)) + (((sizeColumn / 2) * (sizeRow / 2)));
                }
                else
                {
                    length = (((sizeColumn / 2) + 1) * ((sizeRow / 2) + 1)) + (((sizeColumn / 2) * (sizeRow / 2)));
                }
            }
            
            for (int i = 0; i < length; i++)
            {
                _matrix.Add(0);
            }
        }
        public Matrix(Matrix x)//constructor 2
        {
            for (int i = 0; i < x._matrix.Count; ++i)
            {
                _matrix.Add(x._matrix[i]);
            }
        }
        #endregion

        #region IndexConversion
        private int ind(int i, int j)
        {
             
            if (this._sizeColumn % 2 == 0)
            {
                if (i % 2 == 0)
                {
                    return ((i - 1) * (this._sizeColumn / 2)) + (j / 2) - 1;
                }
                else
                {
                    return ((i - 1) * (this._sizeColumn / 2)) + (j / 2);
                }
            }
            else
            {
                if (i % 2 == 0)
                {
                    return ((this._sizeColumn / 2) + 1) * (i / 2)  + (this._sizeColumn / 2) * ((i / 2 ) - 1) + (j / 2) - 1;
                }
                else
                {
                    return ((this._sizeColumn / 2) + 1) * (i / 2) + (this._sizeColumn / 2) * (i / 2) + (j / 2);
                }
            }
        }
        #endregion
       
        #region Getter
        private bool entryIsZero(int i, int j)
        {
            if ((i % 2 == 1) && (j % 2 == 0))
            {
                return true;
            }
            else if ((i % 2 == 0) && (j % 2 == 1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int GetElement(int i, int j)
        {
            if (i <= 0 || j <= 0 || i > this._sizeRow || j > this._sizeColumn)
                throw new InvalidIndexException();
            if (!entryIsZero(i, j))
            {
                return _matrix[ind(i, j)];
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region extra
        public void SetElement(int i, int j, int element)
        {
            _matrix[ind(i, j)] = element;
        }

        public static int Length(int sRow, int sColumn)// Property for getting the length from size of the matrix
        {
            if (sColumn % 2 == 0)
            {
                return (sColumn / 2) * sRow;
            }
            else
            {
                if (sRow % 2 == 0)
                {
                    return ((sColumn / 2) + 1) * (sRow / 2) + (sColumn / 2) * (sRow / 2);
                }
                else
                {
                    return ((sColumn / 2) + 1) * ((sRow / 2) + 1) + (sColumn / 2) * (sRow / 2);
                }
                 
            }
        }
        public void Set(in List<int> x)
        {
            if (Length(this._sizeRow, this._sizeColumn) != x.Count) throw new DifferentSizeException();
            for (int i = 0; i < Length(this._sizeRow, this._sizeColumn); i++)
            {
                this._matrix[i] = x[i];
            }
        }


        public int Length() // Property for getting the length of the vector
        {
            return this._matrix.Count;
        }
        public override int GetHashCode()
        {
            return (base.GetHashCode() << 2);
        }
        public override bool Equals(Object? obj)
        {
            if (obj == null || !(obj is Matrix))
                return false;
            else
            {
                Matrix? mtr = obj as Matrix;
                if (mtr!.Length() != this.Length()) return false;
                for (int i = 0; i < _matrix.Count; i++)
                {
                    if (_matrix[i] != mtr._matrix[i]) return false;
                }
                return true;
            }
        }
        #endregion

        #region Operators
        public static Matrix Add(in Matrix a, in Matrix b)
        {
            if (a._sizeRow != b._sizeRow || a._sizeColumn != b._sizeColumn) throw new DifferentSizeException();
            Matrix c = new(a._sizeRow, a._sizeColumn);
            for (int i = 0; i < c._matrix.Count; i++)
            {
                c._matrix[i] = a._matrix[i] + b._matrix[i];
            }
            return c;
        }




        public static Matrix Multiply(in Matrix a, in Matrix b)
        {
            if (a._sizeColumn != b._sizeRow) throw new DifferentSizeException();
            Matrix c = new(a._sizeRow, b._sizeColumn);
            for (int i = 1; i <= a._sizeRow; i++)
            {
                if (i % 2 == 1)
                {
                    for (int j = 1; j <= b._sizeColumn; j += 2)
                    {
                        for (int p = 1; p <= b._sizeRow; p += 2)//or a.column
                        {
                            c.SetElement(i, j, c.GetElement(i, j) + a.GetElement(i, p) * b.GetElement(p, j));
                        }
                    }
                }
                else
                {
                    for (int j = 2; j <= b._sizeColumn; j += 2)
                    {
                        for (int p = 2; p <= b._sizeRow; p += 2)
                        {
                            c.SetElement(i, j, c.GetElement(i, j) + a.GetElement(i, p) * b.GetElement(p, j));
                        }
                    }
                }
            }
            return c;

        }
        #endregion

        #region Print
        public override string ToString()
        {
            String str = "";
            str += _sizeRow.ToString() + "x" + _sizeColumn.ToString() + "\n";
            for (int i = 1; i <= _sizeRow; i++)
            {
                for (int j = 1; j <= _sizeColumn; j++)
                {
                    if (!entryIsZero(i, j))
                    {
                        str += GetElement(i, j).ToString() + " ";
                    }
                    else
                    {
                        str += 0.ToString() + " ";
                    }
                }
                str += "\n";
            }
            return str;

        }
        #endregion
    }
}
