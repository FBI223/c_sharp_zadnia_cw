// kompozycja plus interfejs zeby byl liskov

/*
class Rectangle
{
    public virtual int Height { get; set; }
    public virtual int Width { get; set; }
    public int GetArea()
    {
        return Width * Height;
    }
}
class Square : Rectangle
{
    public override int Height
    {
        get => base.Height;
        set => base.Height = base.Width = value;
    }
    public override int Width
    {
        get => base.Width;
        set => base.Width = base.Height = value;
    }
    
    public void Method(Rectangle r)
    {
        r.Width = 5;
        r.Height = 10;
        int area = r.GetArea();
    }

}
*/

public interface IShape
{
    int GetArea();
}

public class Rectangle : IShape
{
    public int Height { get; set; }
    public int Width { get; set; }

    public int GetArea()
    {
        return Width * Height;
    }
}

// kompozycja plus interfejs zeby byl liskov

public class Square : IShape
{
    private Rectangle _rectangle;

    public Square(int side)
    {
        _rectangle = new Rectangle { Width = side, Height = side };
    }

    public int Side
    {
        get => _rectangle.Width;
        set
        {
            _rectangle.Width = value;
            _rectangle.Height = value;
        }
    }

    public int GetArea()
    {
        return _rectangle.GetArea();
    }
}



// interfejs sam
/*
public class Square : IShape
{
    public int Side { get; set; }

    public int GetArea()
    {
        return Side * Side;
    }
}
*/




// odwrocenie dziedziczenia
/*
public class Square
{
    public int Side { get; set; }

    public Square(int side)
    {
        Side = side;
    }

    public int GetArea()
    {
        return Side * Side;
    }
}

public class Rectangle : Square
{
    public int Width { get; set; }

    public Rectangle(int height, int width) : base(height)
    {
        Width = width;
    }

    public new int GetArea()
    {
        return Side * Width;
    }
}

*/