namespace ConsoleApp5;


class QuadraticMatrix<T> : Matrix<T> where T : struct ,  IComparable, IFormattable
{
    public int Size { get; }
    
    
    protected QuadraticMatrix(int size) : base(size,size)
    {
        Size = size;
        matrix = new T[size, size];
    }

    public bool  isDiagonal()
    {
        bool isDiagonal = true;

        for (int row = 0; row < Size ; row++)
        {
            for (int col = 0; col < Size; col++)
            {
                if (col != row)
                {
                    if ( !matrix[row, col].Equals(default(T))  ) // jesli to nie jest zero ( czyli default ) 
                    {
                        isDiagonal = false;
                        break;
                    }
                }
            }
        }
        return isDiagonal;
    }
    
}


class Matrix<T> where T :  struct, IComparable, IFormattable
{

    protected T[,] matrix;
    
    
    public int Rows { get; }
    public int Columns { get;  }
    
    public T GetElement(int row, int column)
    {
        return matrix[row, column];
    }

    public void SetElement(int row, int column, T value)
    {
        matrix[row, column] = value;
    }

    protected Matrix(int rows, int columns)
    {
        if (!IsNumericType(typeof(T)))
        {
            throw new InvalidOperationException("Matrix can only be used with numeric types.");
        }
        else
        {
            this.matrix = new T[rows, columns];
            this.Rows = rows;
            this.Columns = columns;
        }
    }



    protected bool IsNumericType(Type type)
    {
        return type == typeof(int) || type == typeof(float) ||
               type == typeof(double) || type == typeof(decimal) ||
               type == typeof(long) || type == typeof(short) ||
               type == typeof(ulong) || type == typeof(ushort) ||
               type == typeof(byte) || type == typeof(sbyte);
    }




    public static Matrix<T> Multiply(Matrix<T> m1, Matrix<T> m2)
    {
        if (m1.Columns != m2.Rows)
        {
            throw new ArgumentException(
                "Number of columns in the first matrix must match the number of rows in the second matrix.");
        }

        Matrix<T> result = new Matrix<T>(m1.Rows, m2.Columns);

        for (int i = 0; i < m1.Rows; i++)
        {
            for (int j = 0; j < m2.Columns; j++)
            {
                dynamic sum = default(T); 

                for (int k = 0; k < m1.Columns; k++)
                {
                    dynamic val1 = m1.GetElement(i, k);
                    dynamic val2 = m2.GetElement(k, j);
                    sum += val1 * val2;
                }

                result.SetElement(i, j, (T)sum);
            }
        }

        return result;
    }
    
    
    public static Matrix<T> Add(Matrix<T> m1, Matrix<T> m2)
    {
        if (m1.Rows != m2.Rows || m1.Columns != m2.Columns )
        {
            throw new ArgumentException(
                "Cannot add those matrices , incompatible number of rows and columns.");
        }

        Matrix<T> result = new Matrix<T>(m1.Rows, m1.Columns);

        for (int i = 0; i < m1.Rows; i++)
        {
            for (int j = 0; j < m1.Columns; j++)
            {
                // Użycie dynamic, aby umożliwić operację dodawania
                dynamic val1 = m1.GetElement(i, j);
                dynamic val2 = m2.GetElement(i, j);
                dynamic val = val1 + val2;
                T valT = (T) val;
                result.SetElement(i, j, valT );
            }
        }

        return result;

    }
    
    
}