
class ComplexNumber<T> where T :  IComparable, IFormattable
{
    public T Real { get; private set; }
    public T Imaginary { get; private set; }
    
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

    private bool IsNumericType(Type type)
    {
        return type == typeof(int) || type == typeof(float) || 
               type == typeof(double) || type == typeof(decimal) ||
               type == typeof(long) || type == typeof(short) ||
               type == typeof(ulong) || type == typeof(ushort) ||
               type == typeof(byte) || type == typeof(sbyte);
    }
    
    
    
    public T GetRealPart()
    {
        return Real;
    }

    public T GetImaginaryPart()
    {
        return Imaginary;
    }

    public override string ToString()
    {
        return $"{Real} + {Imaginary}i";
    }
    
}