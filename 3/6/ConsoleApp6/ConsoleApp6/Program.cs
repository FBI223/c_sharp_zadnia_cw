

public class Program
{
    public static void Main()
    {
        // Test 1: Matrix of int - AddMatrix
        var intMatrix1 = new Matrix<int>(2, 2);
        var intMatrix2 = new Matrix<int>(2, 2);

        intMatrix1[0, 0] = 1;
        intMatrix1[0, 1] = 2;
        intMatrix1[1, 0] = 3;
        intMatrix1[1, 1] = 4;

        intMatrix2[0, 0] = 5;
        intMatrix2[0, 1] = 6;
        intMatrix2[1, 0] = 7;
        intMatrix2[1, 1] = 8;

        Console.WriteLine("Matrix<int> Add:");
        var intAddResult = intMatrix1.AddMatrix(intMatrix2);
        intAddResult.PrintMatrix();

        // Test 2: Matrix of int - MultiplyMatrix
        Console.WriteLine("\nMatrix<int> Multiply:");
        var intMultiplyResult = intMatrix1.MultiplyMatrix(intMatrix2);
        intMultiplyResult.PrintMatrix();

        // Test 3: Matrix of Complex<int> - AddMatrix
        var complexMatrix1 = new Matrix<Complex<int>>(2, 2);
        var complexMatrix2 = new Matrix<Complex<int>>(2, 2);

        complexMatrix1[0, 0] = new Complex<int>(1, 2);
        complexMatrix1[0, 1] = new Complex<int>(3, 4);
        complexMatrix1[1, 0] = new Complex<int>(5, 6);
        complexMatrix1[1, 1] = new Complex<int>(7, 8);

        complexMatrix2[0, 0] = new Complex<int>(1, 1);
        complexMatrix2[0, 1] = new Complex<int>(2, 2);
        complexMatrix2[1, 0] = new Complex<int>(3, 3);
        complexMatrix2[1, 1] = new Complex<int>(4, 4);

        Console.WriteLine("\nMatrix<Complex<int>> Add:");
        var complexAddResult = complexMatrix1.AddMatrix(complexMatrix2);
        complexAddResult.PrintMatrix();

        // Test 4: Matrix of Complex<int> - MultiplyMatrix
        Console.WriteLine("\nMatrix<Complex<int>> Multiply:");
        var complexMultiplyResult = complexMatrix1.MultiplyMatrix(complexMatrix2);
        complexMultiplyResult.PrintMatrix();
    }


}

public class Complex<U> : IComparable, IFormattable where U : struct, IComparable, IFormattable
{
    public U Real { get; private set; }
    public U Imaginary { get; private set; }

    public Complex(U real, U imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    public int CompareTo(object obj)
    {
        if (obj is Complex<U> other)
        {
            return Real.CompareTo(other.Real);
        }
        throw new ArgumentException("Object is not a Complex number");
    }

    public override string ToString() => $"({Real},{Imaginary})";

    public string ToString(string format, IFormatProvider formatProvider)
    {
        return $"( {Real.ToString(format, formatProvider)} , {Imaginary.ToString(format, formatProvider)}i )";
    }

    public static Complex<U> operator +(Complex<U> a, Complex<U> b)
    {
        dynamic realA = a.Real;
        dynamic realB = b.Real;
        dynamic imagA = a.Imaginary;
        dynamic imagB = b.Imaginary;

        return new Complex<U>(realA + realB, imagA + imagB);
    }

    public static Complex<U> operator *(Complex<U> a, Complex<U> b)
    {
        dynamic realA = a.Real;
        dynamic realB = b.Real;
        dynamic imagA = a.Imaginary;
        dynamic imagB = b.Imaginary;

        var realPart = realA * realB - imagA * imagB;
        var imagPart = realA * imagB + imagA * realB;

        return new Complex<U>(realPart, imagPart);
    }
}

public class Matrix<T> where T : IComparable, IFormattable
{
    private T[,] elements;

    public int Rows { get; }
    public int Columns { get; }

    public Matrix(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
        elements = new T[rows, columns];
    }

    public T this[int row, int column]
    {
        get => elements[row, column];
        set => elements[row, column] = value;
    }

    public Matrix<T> AddMatrix(Matrix<T> other)
    {
        if (Rows != other.Rows || Columns != other.Columns)
            throw new ArgumentException("Matrices must have the same dimensions.");

        var result = new Matrix<T>(Rows, Columns);
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                dynamic a = (dynamic)this[i, j];
                dynamic b = (dynamic)other[i, j];
                result[i, j] = a + b;
            }
        }
        return result;
    }

    public Matrix<T> MultiplyMatrix(Matrix<T> other) 
    {
        if (Columns != other.Rows)
            throw new ArgumentException("Matrix dimensions are not compatible for multiplication.");

        var result = new Matrix<T>(Rows, other.Columns);

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < other.Columns; j++)
            {
                dynamic first = (dynamic)this.elements[i, 0] * (dynamic)other[0, j];
                for (int k = 1; k < Columns; k++)
                {
                    dynamic a = this[i, k];
                    dynamic b = other[k, j];
                    first += a * b;
                }
                result[i, j] = first;
            }
        }
        return result;
    }
    
    
    public override string ToString()
    {
        String result = "\n";
        for (int i = 0; i < Rows; i++)
        {
            result += '\n';
            for (int j = 0; j < Columns; j++)
            {
                 result += (elements[i, j].ToString() + "\t");
            }
            
        }
        return result;
    }


    public void PrintMatrix()
    {
        Console.Write(this.ToString()) ;
    }
}
