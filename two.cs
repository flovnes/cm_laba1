public class Lab1 {
    private const double eps = 0.01;
    public delegate double Function(double x);
    public static double dfx(Function func, double x) => (func(x + 0.01) - func(x)) / 0.01;
    public static List<double> roots = new List<double>();
    public static List<(double, double)> root_intervals = new List<(double, double)>();

    public static void Main() {
        // Function func = x => Math.Cos(Math.Sin(x*x*x)) - 0.7;
        Function func = x => Math.Sin(x * 2) + Math.Cos(x * x * x);
        checkFunc(func, -5, 5, 0);

        Console.WriteLine($"\nFound {roots.Count} unique roots:");
        foreach (var root in roots)
            Console.WriteLine($"x = {root:0.00}");
    }

    public static void checkFunc(Function func, double left, double right, int depth) {
        if (depth > 2) { FindRoot((left, right), func); return; }

        int count = 10;
        double h = Math.Abs(right - left) / count;

        for (int i = 0; i < count; i++) {
            double a = left + i * h;
            double b = left + (i + 1) * h;

            if (Math.Sign(a) != Math.Sign(b)) {
                checkDerivative(func, a, b, depth, true);
            } else {
                checkDerivative(func, a, b, depth, false);
            }
        }
    }

    public static void checkDerivative(Function func, double left, double right, int depth, bool signChanged) {
        double h = Math.Abs(right - left) / 5;
        bool sameSign = true;
        double prevDeriv = dfx(func, left);

        for (int i = 1; i <= 5; i++) {
            double x = left + i * h;
            double derivative = dfx(func, x);

            if (Math.Sign(prevDeriv) != Math.Sign(derivative)) {
                sameSign = false;
                checkFunc(func, left, right, depth + 1);
            }

            prevDeriv = derivative;
        }

        if (sameSign & signChanged) {
            FindRoot((left, right), func);
        }
    }

    public static void FindRoot((double, double) interval, Function func) {
        double x0 = interval.Item1;
        double x1 = interval.Item2;
        double f0 = func(x0);
        double f1 = func(x1);

        if (Math.Abs(f0) < eps) {
            AddRoot(x0, f0);
            return;
        }

        if (Math.Abs(f1) < eps) {
            AddRoot(x1, f1);
            return;
        }

        int count = 0;
        int maxCount = 4;

        do {
            double x2 = x1 - f1 * (x1 - x0) / (f1 - f0);

            if (x2 < Math.Min(x0, x1) || x2 > Math.Max(x0, x1))
                x2 = (x0 + x1) / 2;

            double f2 = func(x2);

            if (Math.Abs(f2) < eps || Math.Abs(x2 - x1) < eps) {
                AddRoot(x2, f2);
                return;
            }

            x0 = x1; f0 = f1;
            x1 = x2; f1 = f2;
        } while (++count < maxCount);
    }

    private static void AddRoot(double root, double funcValue) {
        if (!roots.Any(existingRoot => Math.Abs(existingRoot - root) < eps)) {
            roots.Add(root);
            Console.WriteLine($"Розв'язок: x = {root:0.000}, f(x) = {funcValue:0.000}");
        }
    }
}