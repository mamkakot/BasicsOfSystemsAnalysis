// See https://aka.ms/new-console-template for more information


using System.Globalization;
using BasicsOfSystemsAnalysis;

double Function1(double x)
{
    return Math.Sin(x + Math.PI / 4);
}

double Function2(double x)
{
    return Math.Sin(x);
}

double Function3(double x)
{
    return Math.Cos(x);
}

double Function4(double x)
{
    return x > 0 ? x : -x;
}

double Function5(double x)
{
    return Math.Sign(x);
}

double Function6(double x)
{
    return Math.Pow(x, 3) + 3 * x + 5;
}

double Function7(double x)
{
    return 1 / x;
}

double Function8(double x)
{
    var p = Math.Pow(x, 2);
    return Math.Pow(Math.E, p);
}

double Function9(double x)
{
    return 5 * x - 3;
}

double Cos(double x, Func<double, double> func, int k)
{
    return func(x) * Math.Cos(k * x);
}

double Sin(double x, Func<double, double> func, int k)
{
    return func(x) * Math.Sin(k * x);
}

var functions = new Func<double, double>[11]
{
    Function1,
    Function2,
    Function3,
    Function4,
    Function5,
    Function6,
    Function7,
    Function7,
    Function7,
    Function8,
    Function9,
};

var bounds = new double[11, 2]
{
    { -Math.PI, Math.PI },
    { -Math.PI, Math.PI },
    { -Math.PI, Math.PI },
    { -Math.PI, Math.PI },
    { -Math.PI, Math.PI },
    { -Math.PI, Math.PI },
    { -Math.PI, Math.PI },
    { -Math.PI, Math.PI },
    { -Math.PI, Math.PI },
    { -Math.PI, Math.PI },
    { -Math.PI, Math.PI },
};
const string basePath = "C:\\Users\\Dmitry\\RiderProjects\\BasicsOfSystemsAnalysis\\";

for (int i = 0; i < bounds.Length / 2; i++)
{
    using var writerSquares = new StreamWriter(basePath + $"output_squares\\output_squares{i}.txt");
    using var writerTrapezoids = new StreamWriter(basePath + $"output_trapezoids\\output_trapezoids{i}.txt");
    for (int steps = 10; steps <= 10000; steps *= 10)
    {
        using var writerSquareDots =
            new StreamWriter(basePath + $"output_squares\\dots\\dots_squares{i}_{steps}.txt");
        using var writerTrapezoidDots =
            new StreamWriter(basePath + $"output_trapezoids\\dots\\dots_trapezoids{i}_{steps}.txt");
        var coefficientsCosSquares = new double[20];
        var coefficientsSinSquares = new double[20];
        var coefficientsCosTrapezoids = new double[20];
        var coefficientsSinTrapezoids = new double[20];

        for (int j = 0; j < 20; j++)
        {
            coefficientsCosSquares[j] = AreaMethods.AreaCenter(bounds[i, 0], bounds[i, 1], steps, functions[i],
                Cos, j + 1, out var squareCosDots);
            coefficientsSinSquares[j] = AreaMethods.AreaCenter(bounds[i, 0], bounds[i, 1], steps, functions[i],
                Sin, j + 1, out var squareSinDots);
            coefficientsCosTrapezoids[j] = AreaMethods.AreaTrapezoid(bounds[i, 0], bounds[i, 1], steps, functions[i],
                Cos, j + 1, out var _);
            coefficientsSinTrapezoids[j] = AreaMethods.AreaTrapezoid(bounds[i, 0], bounds[i, 1], steps, functions[i],
                Sin, j + 1, out var _);
        }

        writerSquares.WriteLine(
            $"{steps}\n" +
            $"{AreaMethods.AreaCenter(bounds[i, 0], bounds[i, 1], steps, functions[i], out var squareDots).ToString(CultureInfo.InvariantCulture)}\n" +
            $"{string.Join(" ", coefficientsCosSquares.Select(d => d.ToString(CultureInfo.InvariantCulture)))}\n" +
            $"{string.Join(" ", coefficientsSinSquares.Select(d => d.ToString(CultureInfo.InvariantCulture)))}"
        );

        for (int k = 0; k < squareDots.GetLength(0); k++)
        {
            writerSquareDots.WriteLine(
                $"{squareDots[k, 0].ToString(CultureInfo.InvariantCulture)} " +
                $"{squareDots[k, 1].ToString(CultureInfo.InvariantCulture)}"
            );
        }


        writerTrapezoids.WriteLine(
            $"{steps}\n" +
            $"{AreaMethods.AreaTrapezoid(bounds[i, 0], bounds[i, 1], steps, functions[i], out var trapezoidDots).ToString(CultureInfo.InvariantCulture)}\n" +
            $"{string.Join(" ", coefficientsCosTrapezoids.Select(d => d.ToString(CultureInfo.InvariantCulture)))}\n" +
            $"{string.Join(" ", coefficientsSinTrapezoids.Select(d => d.ToString(CultureInfo.InvariantCulture)))}"
        );

        for (int k = 0; k < trapezoidDots.GetLength(0); k++)
        {
            writerTrapezoidDots.WriteLine(
                $"{trapezoidDots[k, 0].ToString(CultureInfo.InvariantCulture)} " +
                $"{trapezoidDots[k, 1].ToString(CultureInfo.InvariantCulture)}"
            );
        }
    }
}