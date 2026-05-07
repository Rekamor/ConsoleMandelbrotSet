namespace ConsoleMandelbrot;

public class Complex
{
    public Complex(double x, double y = 0)
    {
        Real = x;
        Imaginary = y;
    }
    public double Real { get; set; }
    public double Imaginary { get; set; }

    public double Length()
    {
        return Math.Sqrt(LengthSquared());
    }

    public double LengthSquared()
    {
        return Real * Real + Imaginary * Imaginary;
    }

    public Complex Conjugate()
    {
        return new Complex(Real, -Imaginary);
    }

    public Complex Squared()
    {
        return new Complex((Real + Imaginary) * (Real - Imaginary), 2 * Real * Imaginary);
    }
    
    public Complex Round()
    {
        return new Complex(Math.Round(Real), Math.Round(Imaginary));
    }
    
    public static Complex Pow(Complex x, int pow)
    {
        if (pow == 0) return new Complex(1);
        if (pow == 1) return x;
        if (pow == 2) return x.Squared();
        return Pow(x, pow / 2).Squared() * Pow(x, pow % 2);
    }
    public static Complex Cos(Complex x)
    {
        return new Complex(Math.Cos(x.Real) * Math.Cosh(x.Imaginary), -Math.Sin(x.Real) * Math.Sinh(x.Imaginary));
    }

    public static Complex operator + (Complex a, Complex b)
    {
        return new Complex(a.Real + b.Real, a.Imaginary + b.Imaginary);
    }
    
    public static Complex operator - (Complex a)
    {
        return new Complex(-a.Real, -a.Imaginary);
    }
    
    public static Complex operator - (Complex a, Complex b)
    {
        return new Complex(a.Real - b.Real, a.Imaginary - b.Imaginary);
    }
    
    public static Complex operator * (Complex a, Complex b)
    {
        return new Complex(a.Real * b.Real - a.Imaginary * b.Imaginary, a.Imaginary * b.Real + a.Real * b.Imaginary);
    }
    
    public static Complex operator * (Complex a, double b)
    {
        return new Complex(a.Real * b, a.Imaginary * b);
    }

    public static Complex Inverse(Complex a)
    {
        double denominator = a.LengthSquared();
        return a.Conjugate() * denominator;
    }

    public static Complex operator /(Complex a, Complex b)
    {
        return a * Inverse(b);
    }
    
    public static Complex operator / (Complex a, double b)
    {
        return new Complex(a.Real / b, a.Imaginary / b);
    }
    
    public override string ToString()
    {
        return "(" + Real + ", " + Imaginary + ")";
    }
}