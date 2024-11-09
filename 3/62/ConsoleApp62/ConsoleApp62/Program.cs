



 using System.Globalization;
 

 class Program
 {
    static void Main(string[] args)
    {
        // Test dla MatrixPrimitive<int> (suma i mnożenie)
        var intMatrix1 = new MatrixPrimitive<int>(2, 2);
        intMatrix1.SetElement(0, 0, 1);
        intMatrix1.SetElement(0, 1, 2);
        intMatrix1.SetElement(1, 0, 3);
        intMatrix1.SetElement(1, 1, 4);

        var intMatrix2 = new MatrixPrimitive<int>(2, 2);
        intMatrix2.SetElement(0, 0, 5);
        intMatrix2.SetElement(0, 1, 6);
        intMatrix2.SetElement(1, 0, 7);
        intMatrix2.SetElement(1, 1, 8);

        var sumIntMatrix = intMatrix1.Add(intMatrix2);
        var productIntMatrix = intMatrix1.Multiply(intMatrix2);

        Console.WriteLine("Suma macierzy int:");
        sumIntMatrix.PrintMatrix();
        Console.WriteLine("\nMnożenie macierzy int:");
        productIntMatrix.PrintMatrix();

        
        
        // Test dla MatrixPrimitive<double> (suma i mnożenie)
        var doubleMatrix1 = new MatrixPrimitive<double>(2, 2);
        doubleMatrix1.SetElement(0, 0, 1.5);
        doubleMatrix1.SetElement(0, 1, 2.5);
        doubleMatrix1.SetElement(1, 0, 3.5);
        doubleMatrix1.SetElement(1, 1, 4.5);

        var doubleMatrix2 = new MatrixPrimitive<double>(2, 2);
        doubleMatrix2.SetElement(0, 0, 5.5);
        doubleMatrix2.SetElement(0, 1, 6.5);
        doubleMatrix2.SetElement(1, 0, 7.5);
        doubleMatrix2.SetElement(1, 1, 8.5);

        var sumDoubleMatrix = doubleMatrix1.Add(doubleMatrix2);
        var productDoubleMatrix = doubleMatrix1.Multiply(doubleMatrix2);

        Console.WriteLine("\nSuma macierzy double:");
        
        sumDoubleMatrix.PrintMatrix();
        Console.WriteLine("\nMnożenie macierzy double:");
        productDoubleMatrix.PrintMatrix();

        
        
        
        // Test dla MatrixComplex<Complex<int>> (suma i mnożenie)
        var complexIntMatrix1 = new MatrixComplex<Complex<int>>(2, 2);
        complexIntMatrix1.SetElement(0, 0, new Complex<int>(2, 3));
        complexIntMatrix1.SetElement(0, 1, new Complex<int>(4, -2));
        complexIntMatrix1.SetElement(1, 0, new Complex<int>(1, -1));
        complexIntMatrix1.SetElement(1, 1, new Complex<int>(3, 5));

        var complexIntMatrix2 = new MatrixComplex<Complex<int>>(2, 2);
        complexIntMatrix2.SetElement(0, 0, new Complex<int>(0, 1));
        complexIntMatrix2.SetElement(0, 1, new Complex<int>(-1, 4));
        complexIntMatrix2.SetElement(1, 0, new Complex<int>(5, -3));
        complexIntMatrix2.SetElement(1, 1, new Complex<int>(2, 0));

        var sumComplexIntMatrix = complexIntMatrix1.Add(complexIntMatrix2);
        var productComplexIntMatrix = complexIntMatrix1.Multiply(complexIntMatrix2);

        Console.WriteLine("\nSuma macierzy Complex<int>:");
        sumComplexIntMatrix.PrintMatrix();
        Console.WriteLine("\nMnożenie macierzy Complex<int>:");
        productComplexIntMatrix.PrintMatrix();
        
        
        

        // Test dla MatrixComplex<Complex<double>> (suma i mnożenie)
        var complexDoubleMatrix1 = new MatrixComplex<Complex<double>>(2, 2);
        complexDoubleMatrix1.SetElement(0, 0, new Complex<double>(1.5, 2.5));
        complexDoubleMatrix1.SetElement(0, 1, new Complex<double>(3.5, -1.5));
        complexDoubleMatrix1.SetElement(1, 0, new Complex<double>(0.5, 4.0));
        complexDoubleMatrix1.SetElement(1, 1, new Complex<double>(2.0, 3.0));

        var complexDoubleMatrix2 = new MatrixComplex<Complex<double>>(2, 2);
        complexDoubleMatrix2.SetElement(0, 0, new Complex<double>(2.5, -0.5));
        complexDoubleMatrix2.SetElement(0, 1, new Complex<double>(-1.0, 2.0));
        complexDoubleMatrix2.SetElement(1, 0, new Complex<double>(3.0, -2.5));
        complexDoubleMatrix2.SetElement(1, 1, new Complex<double>(1.5, 1.5));

        var sumComplexDoubleMatrix = complexDoubleMatrix1.Add(complexDoubleMatrix2);
        var productComplexDoubleMatrix = complexDoubleMatrix1.Multiply(complexDoubleMatrix2);

        Console.WriteLine("\nSuma macierzy Complex<double>:");
        sumComplexDoubleMatrix.PrintMatrix();
        Console.WriteLine("\nMnożenie macierzy Complex<double>:");
        productComplexDoubleMatrix.PrintMatrix();
        
        
        
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        
        
        var complexNumber = new Complex<double>(1.2345, 6.789);

        // Wywołanie z formatem "F2" i kulturą en-US
        string formattedString = complexNumber.ToString("F8", CultureInfo.InvariantCulture);
        Console.WriteLine(formattedString);  // Oczekiwane: "1.23 + 6.79i"

        // Wywołanie z innym formatem, np. "N1", i kulturą pl-PL
        string formattedStringPL = complexNumber.ToString("N1", new CultureInfo("pl-PL"));
        Console.WriteLine(formattedStringPL); // Oczekiwane: "1,2 + 6,8i"
        
    }

    
 }

 
 
 
 public abstract class Matrix<T> where T : IComparable, IFormattable
 {
     protected T[,] elements;
     public int Rows { get; }
     public int Columns { get; }

     protected Matrix(int rows, int columns) 
     {
         Rows = rows;
         Columns = columns;
         elements = new T[rows, columns];
     }

     public T GetElement(int row, int column)
     {
         return elements[row, column];
     }

     public void SetElement(int row, int column, T value)
     {
         elements[row, column] = value;
     }


     public void PrintMatrix()
     {
         Console.WriteLine();
         for (int i = 0; i < Rows; i++)
         {
             for (int j = 0; j < Columns; j++)
             {
                 Console.Write(GetElement(i, j).ToString() + "\t");
             }
             Console.WriteLine();
         }
     }
     
     
     public abstract Matrix<T> Multiply(Matrix<T> other);
     public abstract Matrix<T> Add(Matrix<T> other);



 }
 

 
 
 
 
 
 
 
 public class MatrixPrimitive<T> : Matrix<T> where T : struct, IComparable, IFormattable
 {
     public MatrixPrimitive(int rows, int columns) : base(rows, columns)
     {
         for (int i = 0; i < Rows; i++)
         {
             for (int j = 0; j < Columns; j++)
             {
                 elements[i, j] = default(T);
             }
         }
     }
     

     public override MatrixPrimitive<T> Add(Matrix<T> other)
     {
         if (Rows != other.Rows || Columns != other.Columns)
             throw new InvalidOperationException("Macierze muszą mieć te same wymiary.");

         var result = new MatrixPrimitive<T>(Rows, Columns);

         for (int i = 0; i < Rows; i++)
         {
             for (int j = 0; j < Columns; j++)
             {
                 result.SetElement(i, j, (dynamic)this.GetElement(i, j) + (dynamic)other.GetElement(i, j));
             }
         }

         return result;
     }

     public override MatrixPrimitive<T> Multiply(Matrix<T> other)
     {
         if (Columns != other.Rows)
             throw new InvalidOperationException("Liczba kolumn pierwszej macierzy musi być równa liczbie wierszy drugiej macierzy.");

         var result = new MatrixPrimitive<T>(Rows, other.Columns);

         for (int i = 0; i < Rows; i++)
         {
             for (int j = 0; j < other.Columns; j++)
             {
                 dynamic sum = default(T);
                 for (int k = 0; k < Columns; k++)
                 {
                     sum += (dynamic)this.GetElement(i, k) * (dynamic)other.GetElement(k, j);
                 }
                 result.SetElement(i, j, sum);
             }
         }

         return result;
     }
 }
 
 
 
 
 
 
 
 
 
 
 
 
 
 public class MatrixComplex<T> : Matrix<T> where T : IComparable, IFormattable, new()
 {
     public MatrixComplex(int rows, int columns) : base(rows, columns)
     {
         for (int i = 0; i < Rows; i++)
         {
             for (int j = 0; j < Columns; j++)
             {
                 elements[i, j] = new T(); // Inicjalizacja jako Complex(0, 0)
             }
         }
     }
     
     

     public override MatrixComplex<T> Add(Matrix<T> other)
     {
         if (Rows != other.Rows || Columns != other.Columns)
             throw new InvalidOperationException("Macierze muszą mieć te same wymiary.");

         var result = new MatrixComplex<T>(Rows, Columns);

         for (int i = 0; i < Rows; i++)
         {
             for (int j = 0; j < Columns; j++)
             {
                 result.SetElement(i, j, (dynamic)this.GetElement(i, j) + (dynamic)other.GetElement(i, j));
             }
         }

         return result;
     }

     public override MatrixComplex<T> Multiply(Matrix<T> other)
     {
         if (Columns != other.Rows)
             throw new InvalidOperationException("Liczba kolumn pierwszej macierzy musi być równa liczbie wierszy drugiej macierzy.");

         var result = new MatrixComplex<T>(Rows, other.Columns);

         for (int i = 0; i < Rows; i++)
         {
             for (int j = 0; j < other.Columns; j++)
             {
                 dynamic sum = new T(); // Inicjalizacja sum jako Complex<U>(0, 0)
                 for (int k = 0; k < Columns; k++)
                 {
                     sum += (dynamic)this.GetElement(i, k) * (dynamic)other.GetElement(k, j);
                 }
                 result.SetElement(i, j, sum);
             }
         }

         return result;
     }
 }
 
 
 
 
 
 
 
 
 
 
 
 public class Complex<U> : IComparable, IFormattable where U : struct, IComparable, IFormattable
 {
     public U Real { get; }
     public U Imaginary { get; }

     public Complex(U real, U imaginary)
     {
         Real = real;
         Imaginary = imaginary;
     }

     // Konstruktor domyślny (ustawia 0 jako wartość domyślną)
     public Complex() : this(default(U), default(U)) { }

     public static Complex<U> operator +(Complex<U> a, Complex<U> b)
     {
         return new Complex<U>((dynamic)a.Real + (dynamic)b.Real, (dynamic)a.Imaginary + (dynamic)b.Imaginary);
     }

    
     public static Complex<U> operator *(Complex<U> a, Complex<U> b)
     {
         // Obliczamy część rzeczywistą i urojoną zgodnie z zasadami mnożenia liczb zespolonych
         dynamic realPart = (dynamic)a.Real * (dynamic)b.Real - (dynamic)a.Imaginary * (dynamic)b.Imaginary;
         dynamic imaginaryPart = (dynamic)a.Real * (dynamic)b.Imaginary + (dynamic)a.Imaginary * (dynamic)b.Real;
         return new Complex<U>(realPart, imaginaryPart);
     }
     
         // Metoda klonująca
    public Complex<U> Clone()
    {
        return new Complex<U>(Real, Imaginary);
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
     
     public override string ToString()
     {
         return $"{Real} + {Imaginary}i";
     }
     
 }

 
 
 
 