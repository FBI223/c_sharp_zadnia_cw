namespace ConsoleApp6;

class Program
{
    public static void Main(string[] args)
    {
        /*
        // Tworzymy dwie macierze 2x2 typu `ComplexNumber<int>`
        Matrix<int> m1 = new Matrix<int>(2, 2);
        Matrix<int> m2 = new Matrix<int>(2, 2);

        // Ustawiamy wartości w macierzy m1
        m1.SetElement(0, 0, new ComplexNumber<int>(2, 3));
        m1.SetElement(0, 1, new ComplexNumber<int>(4, -1));
        m1.SetElement(1, 0, new ComplexNumber<int>(-5, 2));
        m1.SetElement(1, 1, new ComplexNumber<int>(6, -3));

        // Ustawiamy wartości w macierzy m2
        m2.SetElement(0, 0, new ComplexNumber<int>(1, -1));
        m2.SetElement(0, 1, new ComplexNumber<int>(3, 2));
        m2.SetElement(1, 0, new ComplexNumber<int>(4, 1));
        m2.SetElement(1, 1, new ComplexNumber<int>(2, 2));

        // Dodawanie macierzy
        Matrix<int> resultAdd = Matrix<int>.Add(m1, m2);
        Console.WriteLine("Result of Adding m1 and m2:");
        resultAdd.PrintMatrix();

        // Mnożenie macierzy
        Matrix<int> resultMul = Matrix<int>.Multiply(m1, m2);
        Console.WriteLine("Result of Multiplying m1 and m2:");
        resultMul.PrintMatrix();

        // Tworzymy macierz kwadratową i sprawdzamy, czy jest diagonalna
        QuadraticMatrix<int> diagMatrix = new QuadraticMatrix<int>(3);
        diagMatrix.SetElement(0, 0, 5);
        diagMatrix.SetElement(1, 1, 3);
        diagMatrix.SetElement(2, 2, 7);

        Console.WriteLine($"Is the matrix diagonal? {diagMatrix.isDiagonal()}");
        ComplexNumber<int>.TestComplexOperators();
        
        */
        
        // Tworzenie liczby zespolonej dla testu
        ComplexNumber<int> complex1 = new ComplexNumber<int>(3, 4);
        ComplexNumber<int> complex2 = new ComplexNumber<int>(1, 2);

        // Tworzenie zwykłej macierzy 2x2 z liczbami zespolonymi
        Matrix<int> matrix1 = new Matrix<int>(2, 2);
        matrix1.SetElement(0, 0, complex1);
        matrix1.SetElement(0, 1, complex2);
        matrix1.SetElement(1, 0, complex2);
        matrix1.SetElement(1, 1, complex1);

        // Tworzenie drugiej macierzy 2x2 do operacji
        Matrix<int> matrix2 = new Matrix<int>(2, 2);
        matrix2.SetElement(0, 0, new ComplexNumber<int>(5, 6));
        matrix2.SetElement(0, 1, new ComplexNumber<int>(7, 8));
        matrix2.SetElement(1, 0, new ComplexNumber<int>(9, 10));
        matrix2.SetElement(1, 1, new ComplexNumber<int>(11, 12));

        // Dodawanie macierzy
        Matrix<int> sumMatrix = Matrix<int>.Add(matrix1, matrix2);
        Console.WriteLine("Suma macierzy:");
        sumMatrix.PrintMatrix();

        // Mnożenie macierzy
        Matrix<int> productMatrix = Matrix<int>.Multiply(matrix1, matrix2);
        Console.WriteLine("Iloczyn macierzy:");
        productMatrix.PrintMatrix();

        // Tworzenie macierzy kwadratowej i sprawdzanie, czy jest diagonalna
        QuadraticMatrix<int> diagMatrix = new QuadraticMatrix<int>(2);
        diagMatrix.SetElement(0, 0, new ComplexNumber<int>(1, 0));
        diagMatrix.SetElement(1, 1, new ComplexNumber<int>(1, 0));
        bool isDiagonal = diagMatrix.isDiagonal();
        Console.WriteLine($"Czy macierz jest diagonalna? {isDiagonal}");
        
        
    }

}



class QuadraticMatrix<T> : Matrix<T> where T : struct, IComparable, IFormattable
{
    
    public QuadraticMatrix(int size) : base(size, size)
    {
    }

    public bool  isDiagonal()
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


class Matrix<T> where T :  struct, IComparable, IFormattable
{
    protected dynamic def_val = default(T);

    protected ComplexNumber<T>[,] matrix;
    protected int rows { get; set; }
    protected int columns { get; set; }
    
    protected static ComplexNumber<T> ConvertToComplex(T value)
    {
        // Tworzymy Complex z wartością rzeczywistą i zerową częścią urojoną
        return new ComplexNumber<T>(value);
    }
    public ComplexNumber<T> GetElement(int row, int column)
    {
        return matrix[row, column];
    }

    
    public void PrintMatrix()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Console.Write($"{matrix[i, j]} ");
            }
            Console.WriteLine();
        }
    }
    
    public void SetElement(int row, int column, ComplexNumber<T> value)
    {
            //matrix[row, column].Imaginary = value.Imaginary;
            //matrix[row, column].Real = value.Real;
            matrix[row, column] = value;
    }
    
    // Setter, który akceptuje typy proste i konwertuje je do ComplexNumber<T>
    public void SetElement(int row, int column, T value)
    {
        ComplexNumber<T> temp = ConvertToComplex(value);
        //matrix[row, column].Imaginary = temp.Imaginary ;
        //matrix[row, column].Real = temp.Real ;
        matrix[row, column] = temp ;
    }

    public Matrix(int rows, int columns)
    {
        this.rows = rows;
        this.columns = columns;
        this.matrix = new ComplexNumber<T>[rows, columns];
        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                matrix[i, j] = new ComplexNumber<T>() ; 
            }
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
                // Inicjalizujemy sum jako zero
                ComplexNumber<T> sum = new ComplexNumber<T>(default(T), default(T));
    
                for (int k = 0; k < m1.columns; k++)
                {
                    ComplexNumber<T> val1 = m1.GetElement(i, k);
                    ComplexNumber<T> val2 = m2.GetElement(k, j);
                    sum = sum + (val1 * val2);  // Dodajemy wynik do sumy
                }

                result.SetElement(i, j, sum);
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

                ComplexNumber<T> val1 = m1.GetElement(i, j);
                ComplexNumber<T> val2 = m2.GetElement(i, j);
                ComplexNumber<T> sum = new ComplexNumber<T>(default(T), default(T));
                sum =   val1 + val2 ;

                result.SetElement(i, j, sum );
            }
        }

        return result;

    }
    
    
}



class ComplexNumber<U> : IComparable, IFormattable where U : struct,  IComparable, IFormattable
{
    public U Real { get; set; }
    public U Imaginary { get; set; }
    
    public ComplexNumber(U real, U imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    public ComplexNumber()
    {
        Real = default(U);
        Imaginary = default(U);
    }
    
    public static ComplexNumber<U> Zero()
    {
        return new ComplexNumber<U>(default(U), default(U));
    }
    // Konstruktor dla wartości rzeczywistej (część urojona ustawiona na zero)
    public ComplexNumber(U real) : this(real, default(U)) {}
    
    
    // Dodawanie liczb zespolonych
    public static ComplexNumber<U> operator +(ComplexNumber<U> a, ComplexNumber<U> b)
    {
        dynamic real = (dynamic)a.Real + b.Real;
        dynamic imaginary = (dynamic)a.Imaginary + b.Imaginary;
        return new ComplexNumber<U>((U)real, (U)imaginary);
    }
    
    // Mnożenie liczb zespolonych
    public static ComplexNumber<U> operator *(ComplexNumber<U> a, ComplexNumber<U> b)
    {
        dynamic real = (dynamic)a.Real * b.Real - (dynamic)a.Imaginary * b.Imaginary;
        dynamic imaginary = (dynamic)a.Real * b.Imaginary + (dynamic)a.Imaginary * b.Real;
        return new ComplexNumber<U>((U)real, (U)imaginary);
    }
    

    private bool IsNumericType(Type type)
    {
        return type == typeof(int) || type == typeof(float) || 
               type == typeof(double) || type == typeof(decimal) ||
               type == typeof(long) || type == typeof(short) ||
               type == typeof(ulong) || type == typeof(ushort) ||
               type == typeof(byte) || type == typeof(sbyte);
    }


    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        FormattableString formattable = $"{nameof(Real)}: {Real}, {nameof(Imaginary)}: {Imaginary}";
        return formattable.ToString(formatProvider);
    }

    public override string ToString()
    {
        return $"{Real} + {Imaginary}i";
    }

// Implementacja IComparable
    public int CompareTo(object? obj)
    {
        if (obj == null) return 1;

        if (obj is ComplexNumber<U> other)
        {
            // Przykładowe porównanie na podstawie modułu liczby zespolonej
            double magnitudeThis = Math.Sqrt(Math.Pow(Convert.ToDouble(Real), 2) + Math.Pow(Convert.ToDouble(Imaginary), 2));
            double magnitudeOther = Math.Sqrt(Math.Pow(Convert.ToDouble(other.Real), 2) + Math.Pow(Convert.ToDouble(other.Imaginary), 2));
            return magnitudeThis.CompareTo(magnitudeOther);
        }
        else
        {
            throw new ArgumentException("Object is not a Complex<T>");
        }
    }
    
    
    public static void TestComplexOperators()
    {
        ComplexNumber<int> a = new ComplexNumber<int>(2, 3);
        ComplexNumber<int> b = new ComplexNumber<int>(1, -1);

        // Test dodawania
        var sum = a + b;
        Console.WriteLine($"Sum: {sum}");  // Oczekiwane: 3 + 2i

        // Test mnożenia
        var product = a * b;
        Console.WriteLine($"Product: {product}");  // Oczekiwane: 5 + i
    }
    
}


































/*



class QuadraticMatrix<T> : Matrix<T> where T :  IComparable, IFormattable
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


class Matrix<T> where T :  IComparable, IFormattable
{

    protected T[,] matrix;
    protected int rows { get; set; }
    protected int columns { get; set; }
    
    public T GetElement(int row, int column)
    {
        return matrix[row, column];
    }

    protected static ComplexNumber<T> ConvertToComplex(T value)
    {
        if (value is ComplexNumber<T> complexValue)
        {
            return complexValue;
        }
        else
        {
            // Tworzymy Complex z wartością rzeczywistą i zerową częścią urojoną
            return new ComplexNumber<T>(value);
        }
    }
    
    public void SetElement(int row, int column, T value)
    {
        if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(ComplexNumber<>) )
        {
            matrix[row, column] = value;
        }
        else
        {
            matrix[row, column] = ;
        }
 
    }
    


    protected Matrix(int rows, int columns)
    {
        if (!IsNumericType(typeof(T)) && typeof(T) != typeof(ComplexNumber<T>))
        {
            throw new InvalidOperationException("Matrix can only be used with numeric types or ComplexNumber.");
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
                T sum = default (T);
        
                for (int k = 0; k < m1.columns; k++)
                {
                    dynamic val1 = m1.GetElement(i, k);
                    dynamic val2 = m2.GetElement(k, j);
                    
                    dynamic temp = val1 * val2;
                    sum = sum + temp;
                }

                result.SetElement(i, j, sum );
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

                dynamic val1 = m1.GetElement(i, j);
                dynamic val2 = m2.GetElement(i, j);

                dynamic sum =   val1 + val2 ;

                result.SetElement(i, j, sum );
            }
        }
        return result;
    }
}



class ComplexNumber<T> : IComparable, IFormattable where T :   IComparable, IFormattable
{
    public T Real { get; set; }
    public T Imaginary { get; set; }
    
    public ComplexNumber(T real, T imaginary)
    {
        if (!IsNumericType(typeof(T)))
        {
            throw new InvalidOperationException("ComplexNumber can only be used with numeric types.");
        }
        
        Real = real;
        Imaginary = imaginary;
    }
    
    
    // Konstruktor dla wartości rzeczywistej (część urojona ustawiona na zero)
    public ComplexNumber(T real) : this(real, default(T)) {}
    
    // Metoda fabryczna zwracająca ComplexNumber z real i imaginary ustawionymi na 0
    public static ComplexNumber<T> Zero()
    {
        return new ComplexNumber<T>(default(T), default(T));
    }
    
    // Dodawanie liczb zespolonych
    public static ComplexNumber<T> operator +(ComplexNumber<T> a, ComplexNumber<T> b)
    {
        dynamic real = (dynamic)a.Real + b.Real;
        dynamic imaginary = (dynamic)a.Imaginary + b.Imaginary;
        return new ComplexNumber<T>((T)real, (T)imaginary);
    }

    // Mnożenie liczb zespolonych
    public static ComplexNumber<T> operator *(ComplexNumber<T> a, ComplexNumber<T> b)
    {
        dynamic real = (dynamic)a.Real * b.Real - (dynamic)a.Imaginary * b.Imaginary;
        dynamic imaginary = (dynamic)a.Real * b.Imaginary + (dynamic)a.Imaginary * b.Real;
        return new ComplexNumber<T>((T)real, (T)imaginary);
    }
    
    private bool IsNumericType(Type type)
    {
        return type == typeof(int) || type == typeof(float) || 
               type == typeof(double) || type == typeof(decimal) ||
               type == typeof(long) || type == typeof(short) ||
               type == typeof(ulong) || type == typeof(ushort) ||
               type == typeof(byte) || type == typeof(sbyte);
    }


    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        FormattableString formattable = $"{nameof(Real)}: {Real}, {nameof(Imaginary)}: {Imaginary}";
        return formattable.ToString(formatProvider);
    }

    public override string ToString()
    {
        return $"{Real} + {Imaginary}i";
    }

// Implementacja IComparable
    public int CompareTo(object? obj)
    {
        if (obj == null) return 1;

        if (obj is ComplexNumber<T> other)
        {
            // Przykładowe porównanie na podstawie modułu liczby zespolonej
            double magnitudeThis = Math.Sqrt(Math.Pow(Convert.ToDouble(Real), 2) + Math.Pow(Convert.ToDouble(Imaginary), 2));
            double magnitudeOther = Math.Sqrt(Math.Pow(Convert.ToDouble(other.Real), 2) + Math.Pow(Convert.ToDouble(other.Imaginary), 2));
            return magnitudeThis.CompareTo(magnitudeOther);
        }
        else
        {
            throw new ArgumentException("Object is not a Complex<T>");
        }
    }
}

*/

