namespace ConsoleApp6;



class QuadraticMatrix<T> : Matrix<T> where T :  ComplexNumber<T>
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


class Matrix<T> where T :  ComplexNumber<T>
{

    protected ComplexNumber<T>[,] matrix;
    protected int rows { get; set; }
    protected int columns { get; set; }
    
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
    public ComplexNumber<T> GetElement(int row, int column)
    {
        return matrix[row, column];
    }

    public void SetElement(int row, int column, ComplexNumber<T> value)
    {
            matrix[row, column] = value;
    }
    
    // Setter, który akceptuje typy proste i konwertuje je do ComplexNumber<T>
    public void SetElement(int row, int column, T value)
    {
        matrix[row, column] = ConvertToComplex(value);
    }

    protected Matrix(int rows, int columns)
    {
        if (!IsNumericType(typeof(T)))
        {
            throw new InvalidOperationException("Matrix can only be used with numeric types.");
        }
        else
        {
            this.matrix = new ComplexNumber<T>[rows, columns];
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
                ComplexNumber<T> sum = new ComplexNumber<T>(default(T), default(T)); // Dla ComplexNumber inicjalizacja domyślna
        
                for (int k = 0; k < m1.columns; k++)
                {
                    ComplexNumber<T> val1 = m1.GetElement(i, k);
                    ComplexNumber<T> val2 = m2.GetElement(k, j);
                    sum += val1 * val2;
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



class ComplexNumber<U> : IComparable, IFormattable where U :   IComparable, IFormattable
{
    public U Real { get; set; }
    public U Imaginary { get; set; }
    
    public ComplexNumber(U real, U imaginary)
    {
        if (!IsNumericType(typeof(U)))
        {
            throw new InvalidOperationException("ComplexNumber can only be used with numeric types.");
        }
        
        Real = real;
        Imaginary = imaginary;
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
    
    protected static T GetZeroValue()
    {
        // Sprawdzamy, czy T jest typem generycznym i czy jego definicją jest ComplexNumber<>
        if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(ComplexNumber<>))
        {
            // Pobieramy typ wewnętrzny U w ComplexNumber<U>
            Type innerType = typeof(T).GetGenericArguments()[0];

            // Tworzymy instancję ComplexNumber<U> z wartościami default(U) dla Real i Imaginary
            var zeroComplex = Activator.CreateInstance(typeof(T), new object[] { Activator.CreateInstance(innerType), Activator.CreateInstance(innerType) });
            return (T)zeroComplex;
        }
        else
        {
            // Dla typów numerycznych zwracamy default(T)
            return default(T);
        }
    }
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
                T sum =  GetZeroValue();
        
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

