using System;



public class Program
{
    public static void Main()
    {
        // Test 1: Matrix of Complex<double>
        var complexMatrix1 = new Matrix<Complex<double>>(2, 2);
        var complexMatrix2 = new Matrix<Complex<double>>(2, 2);

        complexMatrix1[0, 0] = new Complex<double>(1.5, 2.5);
        complexMatrix1[0, 1] = new Complex<double>(3.0, 4.0);
        complexMatrix1[1, 0] = new Complex<double>(5.0, 6.0);
        complexMatrix1[1, 1] = new Complex<double>(7.0, 8.0);

        complexMatrix2[0, 0] = new Complex<double>(1.5, 1.5);
        complexMatrix2[0, 1] = new Complex<double>(2.0, 2.0);
        complexMatrix2[1, 0] = new Complex<double>(3.0, 3.0);
        complexMatrix2[1, 1] = new Complex<double>(4.0, 4.0);

        var complexResult = complexMatrix1.AddMatrix(complexMatrix2);
        Console.WriteLine("Complex<double> Matrix Addition Result:");
        PrintMatrix(complexResult);

        // Test 2: Matrix of Complex<int>
        var complexIntMatrix1 = new Matrix<Complex<int>>(2, 2);
        var complexIntMatrix2 = new Matrix<Complex<int>>(2, 2);

        complexIntMatrix1[0, 0] = new Complex<int>(1, 2);
        complexIntMatrix1[0, 1] = new Complex<int>(3, 4);
        complexIntMatrix1[1, 0] = new Complex<int>(5, 6);
        complexIntMatrix1[1, 1] = new Complex<int>(7, 8);

        complexIntMatrix2[0, 0] = new Complex<int>(1, 1);
        complexIntMatrix2[0, 1] = new Complex<int>(2, 2);
        complexIntMatrix2[1, 0] = new Complex<int>(3, 3);
        complexIntMatrix2[1, 1] = new Complex<int>(4, 4);

        var complexIntResult = complexIntMatrix1.AddMatrix(complexIntMatrix2);
        Console.WriteLine("\nComplex<int> Matrix Addition Result:");
        PrintMatrix(complexIntResult);

        // Test 3: Matrix of int
        var intMatrix1 = new Matrix<int>(2, 2);
        var intMatrix2 = new Matrix<int>(2, 2);

        intMatrix1[0, 0] = 1;
        intMatrix1[0, 1] = 2;
        intMatrix1[1, 0] = 3;
        intMatrix1[1, 1] = 4;

        intMatrix2[0, 0] = 1;
        intMatrix2[0, 1] = 2;
        intMatrix2[1, 0] = 3;
        intMatrix2[1, 1] = 4;

        var intResult = intMatrix1.AddMatrix(intMatrix2);
        Console.WriteLine("\nInt Matrix Addition Result:");
        PrintMatrix(intResult);

        // Test 4: Matrix of double
        var doubleMatrix1 = new Matrix<double>(2, 2);
        var doubleMatrix2 = new Matrix<double>(2, 2);

        doubleMatrix1[0, 0] = 1.5;
        doubleMatrix1[0, 1] = 2.5;
        doubleMatrix1[1, 0] = 3.5;
        doubleMatrix1[1, 1] = 4.5;

        doubleMatrix2[0, 0] = 1.5;
        doubleMatrix2[0, 1] = 2.5;
        doubleMatrix2[1, 0] = 3.5;
        doubleMatrix2[1, 1] = 4.5;

        var doubleResult = doubleMatrix1.AddMatrix(doubleMatrix2);
        Console.WriteLine("\nDouble Matrix Addition Result:");
        PrintMatrix(doubleResult);
    }

    private static void PrintMatrix<T>(Matrix<T> matrix) where T : IComparable, IFormattable
    {
        for (int i = 0; i < matrix.Rows; i++)
        {
            for (int j = 0; j < matrix.Columns; j++)
            {
                Console.Write(matrix[i, j].ToString() + "\t"); // Wywołanie ToString()
            }
            Console.WriteLine();
        }
    }
    
}

public class Complex<U> : IComparable, IFormattable where U : struct, IComparable, IFormattable
{
    public U Real { get; set; }
    public U Imaginary { get; set; }

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

    public string ToString(string format, IFormatProvider formatProvider)
    {
        return $"{Real.ToString(format, formatProvider)} + {Imaginary.ToString(format, formatProvider)}i";
    }
    
    public override string ToString() => $"{Real} + {Imaginary}i";


    
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

    private T AddValues(T a, T b)
    {
        if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(Complex<>))
        {
            dynamic ca = a;
            dynamic cb = b;
        
            // Determine the type parameter of Complex<>
            Type innerType = typeof(T).GetGenericArguments()[0];
        
            // Create a new Complex instance with the correct type for Real and Imaginary parts
            var real = ca.Real + cb.Real;
            var imaginary = ca.Imaginary + cb.Imaginary;
        
            // Use reflection to instantiate the appropriate Complex<U> type
            return (T)Activator.CreateInstance(typeof(Complex<>).MakeGenericType(innerType), real, imaginary);
        }
        else
        {
            dynamic da = a;
            dynamic db = b;
            return (T)(da + db);
        }
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
                result[i, j] = AddValues(this[i, j], other[i, j]);
            }
        }
        return result;
    }
    
    
}
