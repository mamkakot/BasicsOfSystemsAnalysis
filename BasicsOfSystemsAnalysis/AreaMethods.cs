namespace BasicsOfSystemsAnalysis;

public static class AreaMethods
{
    /// <summary>
    /// Нахождение определённого интеграла от заданной функции методом средних прямоугольников.
    /// </summary>
    /// <param name="start">Начальная точка отсчёта.</param>
    /// <param name="end">Конечная точка отсчёта.</param>
    /// <param name="steps">Количество отрезков, на которые делится площадь под кривой.</param>
    /// <param name="func">Функция, от которой следует найти интеграл.</param>
    /// <param name="dots">Выходной массив значений функций на каждом шаге.</param>
    /// <returns>Площадь под кривой заданной функции.</returns>
    public static double AreaCenter(double start, double end, int steps, Func<double, double> func, out double[,] dots)
    {
        double area = 0;

        dots = new double[steps, 2];
        var x = start;
        var step = (end - start) / steps;
        for (int i = 0; i < steps; i++)
        {
            var y = func(x + step / 2);
            area += y;
            dots[i, 0] = x + step / 2;
            dots[i, 1] = y;
            x += step;
        }

        area *= step;
        return area;
    }

    public static double AreaCenter(double start, double end, int steps, Func<double, double> func1,
        Func<double, Func<double, double>, int, double> func, int k, out double[,] dots)
    {
        double area = 0;

        dots = new double[steps, 2];
        var x = start;
        var step = (end - start) / steps;
        for (int i = 0; i < steps; i++)
        {
            var y = func(x + step / 2, func1, k);
            area += y;
            dots[i, 0] = x + step / 2;
            dots[i, 1] = y;
            x += step;
        }

        area *= step;
        return area;
    }

    /// <summary>
    /// Нахождение определённого интеграла от заданной функции методом трапеций.
    /// </summary>
    /// <param name="start">Начальная точка отсчёта.</param>
    /// <param name="end">Конечная точка отсчёта.</param>
    /// <param name="steps">Количество отрезков, на которые делится площадь под кривой.</param>
    /// <param name="func">Функция, от которой следует найти интеграл.</param>
    /// <param name="dots">Выходной массив значений функций на каждом шаге.</param>
    /// <returns>Площадь под кривой заданной функции.</returns>
    public static double AreaTrapezoid(double start, double end, int steps, Func<double, double> func,
        out double[,] dots)
    {
        var area = (func(start) + func(end)) / 2;
        var step = (end - start) / steps;
        var x = start + step;

        dots = new double[steps + 1, 2];
        dots[0, 0] = start;
        dots[0, 1] = func(start);

        for (int i = 0; i < steps; i++)
        {
            var y = func(x);
            area += y;
            dots[i + 1, 0] = x;
            dots[i + 1, 1] = y;
            x += step;
        }

        area *= step;
        return area;
    }
    public static double AreaTrapezoid(double start, double end, int steps, Func<double, double> func1,
        Func<double, Func<double, double>, int, double> func, int k, out double[,] dots)
    {
        var area = (func(start, func1, k) + func(end, func1, k)) / 2;
        var step = (end - start) / steps;
        var x = start + step;

        dots = new double[steps + 1, 2];
        dots[0, 0] = start;
        dots[0, 1] = func(start, func1, k);

        for (int i = 0; i < steps; i++)
        {
            var y = func(x, func1, k);
            area += y;
            dots[i + 1, 0] = x;
            dots[i + 1, 1] = y;
            x += step;
        }

        area *= step;
        return area;
    }
}