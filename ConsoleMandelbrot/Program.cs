class Program
{
    static void Main()
    {
        string gradient = " .:!/r(l1Z4H9W8$@";
        Complex c = new Complex(-0.5);
        Complex b = new Complex(0);
        int width = 160;
        int height = 45;
        double scale = 100;
        double charSideCof = 10/3;
        int iterations;
        while (true)
        {
            iterations = int.Parse(Console.ReadLine());
            for (int y = -height; y <= height; y++)
            {
                for (int x = -width; x <= width; x++)
                {
                    Complex a = new Complex(x / scale, y  * charSideCof / scale) + c;
                    Console.Write(CalcColor(a, gradient, iterations));
                }

                Console.WriteLine();
            }
        }
    }

    static char CalcColor(Complex x, string gradient, int iterations)
    {
        Complex xZero = x;
        for (int i = 0; i < iterations; i++)
        {
            if (x.Length() >= 2) return gradient[i];
            else x = x.Square() + xZero;
        }
        return gradient.Last();
    }
}

class Complex
{
    public Complex(double x, double y = 0)
    {
        real = x;
        imaginary = y;
    }
    public double real { get; set; }
    public double imaginary { get; set; }

    public double Length()
    {
        return Math.Sqrt(Math.Pow(real, 2) + Math.Pow(imaginary, 2));
    }

    public Complex Square()
    {
        return new Complex((real + imaginary) * (real - imaginary), 2 * real * imaginary);
    }

    public static Complex operator + (Complex a, Complex b)
    {
        return new Complex(a.real + b.real, a.imaginary + b.imaginary);
    }
    
    public static Complex operator - (Complex a, Complex b)
    {
        return new Complex(a.real - b.real, a.imaginary - b.imaginary);
    }
    
    public static Complex operator * (Complex a, Complex b)
    {
        return new Complex(a.real * b.real - a.imaginary * b.imaginary, a.imaginary * b.real + a.real * b.imaginary);
    }

    public override string ToString()
    {
        return "(" + real + ", " + imaginary + ")";
    }
}