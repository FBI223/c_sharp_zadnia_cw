namespace ConsoleApp5;

class QuadraticMatrix<T> : Matrix<T> where T :   IComparable, IFormattable
{
    
    protected QuadraticMatrix(int size) : base(size, size)
    {
    }

    bool  isDiagonal()
    {
        bool isDiagonal = true;

        for (int row = 0; row < base.rows ; row++)
        {
            for (int col = 0; col < base.rows; col++)
            {
                if (col != row)
                {
                    if ( !base.matrix[row, col].Equals(default(T))  ) // jesli to nie jest zero ( czyli default ) 
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


class Matrix<T> where T :   IComparable, IFormattable
{

    protected T[,] matrix;
    protected int rows { get; set; }
    protected int columns { get; set; }
    
    
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
            this.rows = rows;
            this.columns = columns;
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
        if (m1.columns != m2.rows)
        {
            throw new ArgumentException(
                "Number of columns in the first matrix must match the number of rows in the second matrix.");
        }

        Matrix<T> result = new Matrix<T>(m1.rows, m2.columns);

        for (int i = 0; i < m1.rows; i++)
        {
            for (int j = 0; j < m2.columns; j++)
            {
                dynamic sum = default(T); 

                for (int k = 0; k < m1.columns; k++)
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
        if (m1.rows != m2.rows || m1.columns != m2.columns )
        {
            throw new ArgumentException(
                "Cannot add those matrices , incompatible number of rows and columns.");
        }

        Matrix<T> result = new Matrix<T>(m1.rows, m1.columns);

        for (int i = 0; i < m1.rows; i++)
        {
            for (int j = 0; j < m1.columns; j++)
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