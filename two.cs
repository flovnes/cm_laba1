// public class Lab1 {
//     private const double eps = 0.01;
//     public delegate double Function(double x);
//     public static double dfx(Function func, double x, double h) => (func(x+h) - func(x))/h;

//     public static List<double> roots = [];
//     public static List<(double, double)> root_intervals = [];

//     public static void Main() {
//         // Function func = x => Math.Cos(Math.Sin(x*x*x)) - 0.7;
//         Function func = x => Math.Sin(x*2) + Math.Cos(x*x*x);
//         Solve(-5, 5, func);
       
//         Console.WriteLine($"\nfound {roots.Count} roots:");
//         foreach (var root in roots)
//             Console.WriteLine($"x = {root:0.00}\n");
//     }
   
//     public static void Solve(double a, double b, Function func) {
//         divideInterval(a, b, func);

//         foreach (var interval in root_intervals) {
//             Console.WriteLine($"({interval.Item1:0.00}, {interval.Item2:0.00}): ");
            
//             var intervals_temp = new List<(double, double)>();
//             divideRecursive(interval.Item1, interval.Item2, func, intervals_temp, 0);
            
//             foreach (var root_interval in intervals_temp)
//                 findRoot(root_interval, func);
//         }
//     }

//     public static void divideInterval(double a, double b, Function func) {
//         int count = 10;
//         double h = (b - a) / count;
//         double[] x_values = new double[count+1];
//         double[] fx_values = new double[count+1];

//         x_values[0] = a;
//         fx_values[0] = func(a);
//         bool flag = false;

//         for (int i = 1; i <= count; ++i) {
//             x_values[i] = a + h*i;
//             fx_values[i] = func(x_values[i]);

//             if (Math.Sign(fx_values[i]) != Math.Sign(fx_values[i-1])) {
//                 flag = true;
//                 root_intervals.Add((x_values[i-1], x_values[i]));
//                 Console.WriteLine($"Saved -> ({x_values[i-1]:0.00}, {x_values[i]:0.00})");
//             }
//         }

//         if (!flag)
//             Console.WriteLine($"no roots in [{a}; {b}]");
//     }
    
//     public static void divideRecursive(double a, double b, Function func, List<(double, double)> intervals, int depth) {
//         if (depth > 2 || Math.Abs(b - a) < eps) {
//             intervals.Add((a, b));
//             return;
//         }
        
//         int count = 5;
//         double h = (b - a) / count;
//         double dx = h / 10;
        
//         double[] x_values = new double[count+1];
//         double[] dfx_values = new double[count+1];
        
//         x_values[0] = a;
//         dfx_values[0] = dfx(func, a, dx);
//         bool flag = false;
        
//         for (int i = 1; i <= count; ++i) {
//             x_values[i] = a + h*i;
//             dfx_values[i] = dfx(func, x_values[i], dx);
            
//             if (Math.Sign(dfx_values[i]) != Math.Sign(dfx_values[i-1])) {
//                 flag = true;
//                 divideRecursive(x_values[i-1], x_values[i], func, intervals, ++depth);
//             }
//         }
        
//         if (!flag)
//             intervals.Add((a, b));
//     }
   
//     public static void findRoot((double, double) interval, Function func) {
//         double x0 = interval.Item1;
//         double x1 = interval.Item2;
//         double f0 = func(x0);
//         double f1 = func(x1);
        
//         if (Math.Abs(f0) < eps) {
//             roots.Add(x0);
//             return;
//         }
        
//         if (Math.Abs(f1) < eps) {
//             roots.Add(x1);
//             return;
//         }
       
//         int count = 0;
//         int maxCount = 2;
       
//         do {
//             double x2 = x1 - f1 * (x1 - x0) / (f1 - f0);
            
//             if (x2 < Math.Min(x0, x1) || x2 > Math.Max(x0, x1))
//                 x2 = (x0 + x1) / 2;
            
//             double f2 = func(x2);
            
//             if (Math.Abs(f2) < eps || Math.Abs(x2 - x1) < eps) {
//                 if (!roots.Any(existingRoot => Math.Abs(existingRoot - x2) < eps)) {
//                     roots.Add(x2);
//                     Console.WriteLine($"root found: {x2:0.000}, f(x) = {f2:0.000}");
//                 }
//                 return;
//             }
           
//             x0 = x1; f0 = f1;
//             x1 = x2; f1 = f2;
//         } while (++count < maxCount);
//     }
// }