namespace ConsoleMandelbrot;

class Program
{
    static void Main()
    {
        string gradient = " .:!/r(l1Z4H9W8$@";
        Console.WriteLine("1 - Mandelbrot Set\n2 - The burning ship\n3 - 3n+1");
        
        int mod = int.Parse(Console.ReadLine());
        
        int colorMod = 1;
        int step = 1;
        
        int iterations = 100;
        int pow = 2;
        
        Console.CursorVisible = false;
        int width = Console.WindowWidth/2-1;
        int height = Console.WindowHeight/2;
        double zoom = (double)width / 2;
        double charSideCof = 3.4;
        
        Complex c = new Complex(0);
        Complex a = new Complex(0);
        
        while (true)
        {
            for (int y = height; y >= -height; y--)
            {
                Console.SetCursorPosition(0, height - y);
                for (int x = -width; x <= width; x++)
                {
                    Complex z = new Complex(x / zoom, y  * charSideCof / zoom) + c;
                    int color = CalcColor(z, a, pow, mod, iterations, step); 
                    if (color == iterations) Console.Write(' ');
                    else Console.Write(gradient[(iterations - color) * colorMod % (gradient.Length - 1) + 1]);
                }
            }
            
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    c.Imaginary += 25 / zoom;
                    break;
                case ConsoleKey.DownArrow:
                    c.Imaginary -= 25 / zoom;
                    break;
                case ConsoleKey.RightArrow:
                    c.Real += 25 / zoom;
                    break;
                case ConsoleKey.LeftArrow:
                    c.Real -= 25 / zoom;
                    break;
                case ConsoleKey.Z:
                    zoom *= 1.5;
                    break;
                case ConsoleKey.X:
                    zoom /= 1.5;
                    break;
                case ConsoleKey.R:
                    int w = width;
                    width = Console.WindowWidth/2 - 1;
                    height = Console.WindowHeight/2;
                    zoom *= (double)width / w;
                    break;
                case ConsoleKey.C:
                    Console.WriteLine("\nc.real: " + c.Real);
                    Console.WriteLine("c.imaginary: " + c.Imaginary);
                    Console.WriteLine("Scale: " + zoom);
                    Console.ReadKey();
                    break;
                case ConsoleKey.V:
                    Console.WriteLine("\nReal:");
                    c.Real = double.Parse(Console.ReadLine());
                    Console.WriteLine("Imaginary:");
                    c.Imaginary = double.Parse(Console.ReadLine());
                    Console.WriteLine("Zoom:");
                    zoom = double.Parse(Console.ReadLine());
                    break;
                case ConsoleKey.A:
                    Console.WriteLine("\nReal:");
                    a.Real = double.Parse(Console.ReadLine());
                    Console.WriteLine("Imaginary:");
                    a.Imaginary = double.Parse(Console.ReadLine());
                    break;
                case ConsoleKey.I:
                    Console.WriteLine("\nIterations: " + iterations);
                    iterations = int.Parse(Console.ReadLine());
                    break;
                case ConsoleKey.S:
                    Console.WriteLine("\nSteps: " + step);
                    step = int.Parse(Console.ReadLine());
                    break;
                case ConsoleKey.G:
                    Console.WriteLine("\nColorMod: " + colorMod);
                    colorMod = int.Parse(Console.ReadLine());
                    break;
                case ConsoleKey.P:
                    Console.WriteLine("\nPow: " + pow);
                    pow = int.Parse(Console.ReadLine());
                    break;
                case ConsoleKey.M:
                    Console.WriteLine("\nMandelbrot?: " + mod);
                    mod = int.Parse(Console.ReadLine());
                    break;
                default:
                    Console.WriteLine("Unknown command");
                    Console.Beep();
                    break;
            }
        }
    }

    static int CalcColor(Complex xZero, Complex a, int pow, int mod, int iterations, int step = 1)
    {
        Complex x = xZero;
        for (int i = 0; i < iterations; i++)
        {
            bool exit = false;
            for (int j = 0; j < step; j++)
            {
                exit = Fuction(ref x, xZero, a, pow, mod);
            }
            if (exit) return i;
        }
        return iterations;
    }

    static bool Fuction(ref Complex x, Complex xZero, Complex a, int pow, int mod)
    {
        bool exit = false;
        switch (mod)
        {
            case 0:
                x = Complex.Pow(x, pow);
                x += a;
                exit = x.Length() > 2;
                break;
            case 1:
                x = Complex.Pow(x, pow);
                x += xZero;
                exit = x.Length() > 2;
                break;
            case 2:
                x = Complex.Pow(new Complex(Math.Abs(x.Real), -Math.Abs(x.Imaginary)), pow);
                x += xZero;
                exit = x.Length() > 2;
                break;
            case 3:
                x = x * 7 + a - Complex.Pow(Complex.Cos(x * Math.PI), pow) * (x * 5 + new Complex(2));
                x /= 4;
                exit = x.Length() >= 100000000;
                break;
            case 4:
                x = Complex.Cos(x) * Complex.Cos(xZero);
                exit = x.Length() > 10;
                break;
        }
        return exit;
    }
}