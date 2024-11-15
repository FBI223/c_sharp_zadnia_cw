
using System.Numerics;

struct ComplexNumber<T> where T :  IComparable, IFormattable , INumber<T>
{
    public T Real { get; set; }
    public T Imaginary { get; set; }
    
    public ComplexNumber(T real, T imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }
    
    
    // Implementacja IComparable<Complex<U>>
    public int CompareTo(ComplexNumber<T> other)
    {
        
        // Porównanie według modułu liczby zespolonej
        var thisMagnitude = Real * Real + Imaginary * Imaginary;
        var otherMagnitude = other.Real * other.Real + other.Imaginary * other.Imaginary;
        return thisMagnitude.CompareTo(otherMagnitude);
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