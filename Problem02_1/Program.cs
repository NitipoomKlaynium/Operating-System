using System;
using System.Diagnostics;
using System.Threading;

// namespace Ex00 {
//     class Program {
        
//         private static int sum = 0;
//         static void plus() {
//             int i;
//             for (i = 1 ; i < 10000001 ; i++) {
//                 sum += i;
//             }
//         }

//         static void minus() {
//             int i;
//             for (i = 0 ; i < 10000000 ; i++) {
//                 sum -= i;
//             }
//         }
        
//         static void Main(string[] args) {
//             Stopwatch sw = new Stopwatch();
//             Console.WriteLine("Start...");
//             sw.Start();
//             plus();
//             minus();
//             sw.Stop();
//             Console.WriteLine("sum = {0}", sum);
//             Console.WriteLine("Time used: " + sw.ElapsedMilliseconds.ToString() + "ms");
//         }
//     }
// }

// namespace Ex01 {
//     class Program {
//         private static int sum = 0;

//         static void plus() {
//             int i;
//             int inSum = 0;
//             for (i = 1 ; i < 10000001 ; i++) {
//                 inSum += i;
//             }
//             sum += inSum;
//         }

//         static void minus() {
//             int i;
//             int inSum = 0;
//             for (i = 0 ; i < 10000000 ; i++) {
//                 sum -= i;
//             }
//             sum += inSum;
//         }

//         static void Main(string[] args) {
//             Thread P = new Thread(new ThreadStart(plus));
//             Thread M = new Thread(new ThreadStart(minus));

//             Stopwatch sw = new Stopwatch();
//             Console.WriteLine("Start...");
//             sw.Start();

//             P.Start();
//             M.Start();

//             P.Join();
//             M.Join();

//             sw.Stop();
//             Console.WriteLine("sum = {0}", sum);
//             Console.WriteLine("Time used: " + sw.ElapsedMilliseconds.ToString() + "ms");
//         }
//     }
// }

// namespace Ex02 {
//     class Program {
//         private static int sum = 0;
//         private static object _Lock = new object();
//         static void plus() {
//             int i;
//             for (i = 1 ; i < 10000001 ; i++) {
//                 lock (_Lock) {
//                     sum += i;
//                 }
//             }
//         }

//         static void minus() {
//             int i;
//             for (i = 0 ; i < 10000000 ; i++) {
//                 lock (_Lock) {
//                     sum -= i;
//                 }
//             }
//         }

//         static void Main(string[] args) {
//             Thread P = new Thread(new ThreadStart(plus));
//             Thread M = new Thread(new ThreadStart(minus));

//             Stopwatch sw = new Stopwatch();
//             Console.WriteLine("Start...");
//             sw.Start();

//             P.Start();
//             M.Start();

//             P.Join();
//             M.Join();
//             // while(P.IsAlive && M.IsAlive) {}

//             sw.Stop();
//             Console.WriteLine("sum = {0}", sum);
//             Console.WriteLine("Time used: " + sw.ElapsedMilliseconds.ToString() + "ms");
//         }
//     }
// }

// namespace Ex03 {
//     class Program {
//         private static int sum = 0;
//         private static object _Lock = new object();
//         static void plus() {
//             int i;
//             for (i = 1 ; i < 10000001 ; i++) {
//                 lock (_Lock) {
//                     sum += i;
//                 }
//             }
//         }

//         static void minus() {
//             int i;
//             lock (_Lock) {
//                 for (i = 0 ; i < 10000000 ; i++) {
//                     sum -= i;
//                 }
//             }
//         }

//         static void Main(string[] args) {
//             Thread P = new Thread(new ThreadStart(plus));
//             Thread M = new Thread(new ThreadStart(minus));

//             Stopwatch sw = new Stopwatch();
//             Console.WriteLine("Start...");
//             sw.Start();

//             P.Start();
//             M.Start();

//             P.Join();
//             M.Join();
//             // while(P.IsAlive && M.IsAlive) {}

//             sw.Stop();
//             Console.WriteLine("sum = {0}", sum);
//             Console.WriteLine("Time used: " + sw.ElapsedMilliseconds.ToString() + "ms");
//         }
//     }
// }

// namespace Ex04 {
//     class Program {
//         private static string x = "";
//         private static int exitflag = 0;

//         static void ThReadX() {
//             string xx;
//             while(exitflag==0) {
//                 if (x == "") {
//                     Console.Write("Input: ");
//                     xx = Console.ReadLine();
//                     x = xx;
//                 }
//             }
//         }

//         static void ThWriteX() {           
//             while (exitflag == 0) {
//                 if (x != "") {
//                     if (x == "exit") {
//                         Console.WriteLine("X = {0}", x);
//                         exitflag = 1;
//                     }
//                     else {
//                         Console.WriteLine("X = {0}", x);
//                         x = "";
//                     }
//                 }
//             }
//         }

//         static void Main(string[] args) {
//             Thread A = new Thread(ThReadX);
//             Thread B = new Thread(ThWriteX);

//             A.Start();
//             B.Start();
//         }
//     }
// }

namespace Ex05 {
    class Program {
        private static string x = "";
        private static int exitflag = 0;
        private static int updateFlag = 0;
        private static object _Lock = new object();

        static void ThReadX() {
            string xx;
            while (exitflag == 0) {
                lock (_Lock) {
                    Console.Write("Input: ");
                    xx = Console.ReadLine();
                    if (xx == "exit") {
                        exitflag = 1;
                    }
                    x = xx;
                }
            }     
        }

        static void ThWriteX(object i) {
            while(exitflag == 0) {
                lock (_Lock) {
                    if (x != "exit" && x != "") {
                        Console.WriteLine("***Thread {0} : x = {1}***", i, x);
                        x = "";
                    }
                }
            }
            Console.WriteLine("---Thread {0} exit---", i);
        }

        static void Main(string[] args) {
            Thread A = new Thread(ThReadX);
            Thread B = new Thread(ThWriteX);
            Thread C = new Thread(ThWriteX);
            Thread D = new Thread(ThWriteX);

            A.Start();
            B.Start(1);
            C.Start(2);
            D.Start(3);
        }
    }
}