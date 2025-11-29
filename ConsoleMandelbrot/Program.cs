class Program
{
    static void Main()
    {
        Console.WriteLine("Enter any key or \'j\' for Julia Set");
        bool mod = Console.ReadKey().KeyChar != 'j';
        Complex b =  new Complex(0);
        string gradient = " .:!/r(l1Z4H9W8$@";
        int width = 250;
        int height = 50;
        double charSideCof = 3.4;
        double scale = 100;
        Complex c = new Complex(0);
        int pow = 2;
        int iterations = 100;
        while (true)
        {
            for (int y = height; y >= -height; y--)
            {
                for (int x = -width; x <= width; x++)
                {
                    Complex a = new Complex(x / scale, y  * charSideCof / scale) + c;
                    int color = CalcColor(a, iterations, pow, mod, b); 
                    if (color == 0) Console.Write(' ');
                    else Console.Write(gradient[color * 7 % (gradient.Length - 1) + 1]);
                }

                Console.WriteLine();
            }

            var k = Console.ReadKey().KeyChar;
            switch (k)
            {
                case 'w':
                    c.imaginary += 50 / scale;
                    break;
                case 's':
                    c.imaginary -= 50 / scale;
                    break;
                case 'd':
                    c.real += 50 / scale;
                    break;
                case 'a':
                    c.real -= 50 / scale;
                    break;
                case 'z':
                    scale *= 1.5;
                    break;
                case 'x':
                    scale /= 1.5;
                    break;
                case 'c':
                    Console.WriteLine("c.real: " + c.real);
                    Console.WriteLine("c.imaginary: " + c.imaginary);
                    Console.WriteLine("Scale: " + scale);
                    Console.ReadKey();
                    break;
                case 'v':
                    Console.WriteLine("\nReal:");
                    c.real = double.Parse(Console.ReadLine());
                    Console.WriteLine("Imaginary:");
                    c.imaginary = double.Parse(Console.ReadLine());
                    Console.WriteLine("Scale:");
                    scale = double.Parse(Console.ReadLine());
                    break;
                case 'i':
                    Console.WriteLine("\nIterations: " + iterations);
                    iterations = int.Parse(Console.ReadLine());
                    break;
                case 'p':
                    Console.WriteLine("\nPow: " + pow);
                    pow = int.Parse(Console.ReadLine());
                    break;
                case 'm':
                    Console.WriteLine("\nMandelbrot?: " + mod);
                    mod = bool.Parse(Console.ReadLine());
                    break;
                case 'b':
                    if (!mod)
                    {
                        Console.WriteLine("\nb.real: " + b.real);
                        b.real = double.Parse(Console.ReadLine());
                        Console.WriteLine("\nb.imaginary: " + b.imaginary);
                        b.imaginary = double.Parse(Console.ReadLine());
                    }
                    else Console.WriteLine("Unknown command");
                    break;
                default:
                    Console.WriteLine("Unknown command");
                    break;
            }
        }
    }

    static int CalcColor(Complex x, int iterations, int pow = 2, bool mod = true, Complex b = null)
    {
        Complex xZero = x;
        for (int i = 0; i < iterations; i++)
        {
            if (x.Length() >= 2) return iterations - i;
            x = Complex.Pow(x, pow);
            if (mod)  x += xZero;
            else x += b;
        }
        return 0;
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
    
    public static Complex Pow(Complex x, int pow)
    {
        Complex result = new Complex(1);
        for (int i = 0; i < pow; i++) result *= x;
        return result;
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