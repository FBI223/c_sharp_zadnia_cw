using System.Numerics;

namespace ConsoleApp5;

class Matrix<T> where T : struct, IComparable, IFormattable, INumber<T>
{
    protected T[,] matrix;

    public int Rows { get; }
    public int Columns { get; }

    public Matrix(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
        matrix = new T[rows, columns];

        for (int i = 0; i < Rows; i++)
            for (int j = 0; j < Columns; j++)
                matrix[i, j] = T.AdditiveIdentity;
    }

    public T GetElement(int row, int column) => matrix[row, column];

    public void SetElement(int row, int column, T value) => matrix[row, column] = value;

    public static Matrix<T> Multiply(Matrix<T> m1, Matrix<T> m2)
    {
        if (m1.Columns != m2.Rows)
            throw new ArgumentException(
                "Number of columns in the first matrix must match the number of rows in the second matrix.");

        var result = new Matrix<T>(m1.Rows, m2.Columns);

        for (int i = 0; i < m1.Rows; i++)
        {
            for (int j = 0; j < m2.Columns; j++)
            {
                T sum = T.AdditiveIdentity;

                for (int k = 0; k < m1.Columns; k++)
                {
                    sum += m1.GetElement(i, k) * m2.GetElement(k, j);
                }

                result.SetElement(i, j, sum);
            }
        }

        return result;
    }

    public static Matrix<T> Add(Matrix<T> m1, Matrix<T> m2)
    {
        if (m1.Rows != m2.Rows || m1.Columns != m2.Columns)
            throw new ArgumentException("Cannot add matrices with different dimensions.");

        var result = new Matrix<T>(m1.Rows, m1.Columns);

        for (int i = 0; i < m1.Rows; i++)
        {
            for (int j = 0; j < m1.Columns; j++)
            {
                result.SetElement(i, j, m1.GetElement(i, j) + m2.GetElement(i, j));
            }
        }

        return result;
    }

    public virtual bool IsDiagonal()
    {
        if (Rows != Columns)
            return false;

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                if (i != j && !matrix[i, j].Equals(T.AdditiveIdentity)) //; czyli zero
                    return false;
            }
        }

        return true;
    }
}

class QuadraticMatrix<T> : Matrix<T> where T : struct, IComparable, IFormattable, INumber<T>
{
    public int Size { get; }

    public QuadraticMatrix(int size) : base(size, size)
    {
        Size = size;
    }

}
