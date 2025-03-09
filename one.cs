// public class Lab1 {
//     private const double eps = 0.01;
//     public delegate double Function(double x);
//     public static double dfx(Function func, double x, double h) => (func(x+h) - func(x))/h;

//     public static List<double> roots = new List<double>();
//     public static List<(double, double)> root_intervals = new List<(double, double)>();

//     public static void Main() {
//         Function func = x => Math.Cos(Math.Sin(x*x*x)) - 0.7;
//         double a = -3.14;
//         double b = 3.14;
        
//         Solve(a, b, func);
       
//         foreach (var root in roots)
//             Console.WriteLine($"x = {root:0.000}\n");
//     }
   
//     public static void Solve(double a, double b, Function func) {
//         divideInterval(a, b, func);

//         foreach (var interval in root_intervals) {
//             findRoot(interval, func);
//         }
//     }

//     public static void divideInterval(double a, double b, Function func) {
//         int count = 100;
//         double h = (b - a) / count;
//         double[] x_values = new double[count+1];
//         double[] fx_values = new double[count+1];

//         x_values[0] = a;
//         fx_values[0] = func(a);

//         bool foundRoots = false;

//         for (int i = 1; i <= count; ++i) {
//             x_values[i] = a + h*i;
//             fx_values[i] = func(x_values[i]);
//             Console.WriteLine($"f({x_values[i]:0.00}) = {fx_values[i]:0.00}\n");

//             if (Math.Sign(fx_values[i]) != Math.Sign(fx_values[i-1])) {
//                 foundRoots = true;
//                 root_intervals.Add((x_values[i-1], x_values[i]));
//             }
//         }

//         if (!foundRoots) {
//             Console.WriteLine($"no roots on the interval [{a}; {b}]");
//         }
//     }
   
//     public static void findRoot((double, double) interval, Function func) {
//         double x0 = interval.Item1;
//         double x1 = interval.Item2;

//         double x2;
//         double f0 = func(x0);
//         double f1 = func(x1);
       
//         int maxIter = 100;
//         int iterCount = 0;
       
//         do {
//             x2 = x1 - f1 * (x1 - x0) / (f1 - f0);
            
//             double f2 = func(x2);
            
//             if (Math.Abs(f2) < eps || Math.Abs(x2 - x1) < eps) {
//                 roots.Add(x2);
//                 return;
//             }
           
//             x0 = x1;
//             f0 = f1;
//             x1 = x2;
//             f1 = f2;
           
//             iterCount++;
//         } while (iterCount < maxIter);
       
//         roots.Add(x1);
//     }
// }