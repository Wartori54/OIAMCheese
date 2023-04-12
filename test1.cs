using System;
using System.Reflection;
using System.Reflection.Emit;

// Interop.Sys
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;



namespace Test1 {
    // struct Timespec {
    //     public long tv_sec;  /* Seconds.  */
    //     public long tv_nsec;  /* Nanoseconds.  */
    // };
    class Program {
        // [DllImport("libSystem.Native", EntryPoint = "SystemNative_GetSystemTimeAsTicks")]
        // // [SuppressGCTransition]
        // public static extern long GetSystemTimeAsTicks();

        // [DllImport("./ldintercept.so")]
        // private static unsafe extern int clock_gettime(
        // int clockid, 
        // Timespec* tp);

        static int CheckSeed(int seed, int tries) {
            Random rand = new Random(seed);
            for (int i = 0; i < tries; i++) {
                bool restart = false;
                // 50% -> 6
                // 20% -> 6
                for (int j = 0; j < 6; j++) {
                    int num = rand.Next() % 100;
                    if (num < 50) {
                        restart = true;
                        break;
                    }
                }
                if (restart) continue;
                for (int j = 0; j < 6; j++) {
                    int num = rand.Next() % 100;
                    if (num < 80) {
                        restart = true;
                        break;
                    }
                }
                if (restart) continue;
                else {
                    return i;
                }
            }
            return -1;
        }

        static int CheckSeedBillion(int seed, int tries) {
            Random rand = new Random(seed);
            for (int i = 0; i < tries; i++) {
                bool restart = false;
                // 50% -> 6
                // 20% -> 6
                for (int j = 0; j < 6; j++) {
                    int num = rand.Next() % 100;
                    if (num < 50) {
                        restart = true;
                        break;
                    }
                }
                if (restart) continue;
                for (int j = 0; j < 6; j++) {
                    int num = rand.Next() % 100;
                    if (num < 80) {
                        restart = true;
                        break;
                    }
                }
                if (restart) continue;

                // 50% -> 6
                // 20% -> 6
                for (int j = 0; j < 6; j++) {
                    int num = rand.Next() % 100;
                    if (num < 50) {
                        restart = true;
                        break;
                    }
                }
                if (restart) continue;
                for (int j = 0; j < 6; j++) {
                    int num = rand.Next() % 100;
                    if (num < 80) {
                        restart = true;
                        break;
                    }
                }
                if (restart) continue;
                else {
                    return i;
                }
            }
            return -1;
        }

        static void CrackSeed() {
            long start_val = 504911268000000000L;
            long incr = 10;
            long curr_val = 0;
            int res = -1;
            while (res == -1) {
                int sd = (int)((start_val+incr*curr_val) & 0x7FFFFFFF);
                res = CheckSeed(sd, 1);
                curr_val++;
                if (curr_val % 1000 == 0) {
                    Console.WriteLine("seeds checked: {0}", curr_val);
                }
            }
            Console.WriteLine("seed found: {0} in {1} attempts", start_val+incr*(curr_val-1), res);
            Console.WriteLine("curr_val: {0}", curr_val-1);
        }

        static void CrackSeedBillion() {
            long start_val = 504911268000000000L;
            long incr = 10;
            long curr_val = 0;
            int res = -1;
            while (res == -1) {
                int sd = (int)((start_val+incr*curr_val) & 0x7FFFFFFF);
                res = CheckSeedBillion(sd, 50);
                curr_val++;
                if (curr_val % 1000 == 0) {
                    Console.WriteLine("seeds checked: {0}", curr_val);
                }
            }
            Console.WriteLine("seed found: {0} in {1} attempts", start_val+incr*(curr_val-1), res);
            Console.WriteLine("curr_val: {0}", curr_val-1);
        }

        static void Main(string[] args) {
            // System.TimeZoneInfo.Local = 
            Console.WriteLine("Before calling");
            Console.WriteLine("dtnow.Ticks: {0}", DateTime.Now.Ticks);
            Console.WriteLine("dtnow.Ticks & 0x7FFFFFFF: {0}", DateTime.Now.Ticks & 0x7FFFFFFF);
            // int sd = 71599592;
            // Console.WriteLine("seed: {0}, will get 1/1000000 in {1}", sd, CheckSeedBillion(sd, 1000000000));
            // CrackSeed();
            // CrackSeedBillion();
            // Console.WriteLine("dtnow.Ticks & 0x7FFFFFFF: {0}", dtnow.Ticks & 0x7FFFFFFF);
            // Console.WriteLine("DateTime.UtcNow.Ticks: {0}", DateTime.UtcNow.Ticks); // Interop.Sys.GetSystemTimeAsTicks
            // Console.WriteLine("DateTime.UtcNow.Ticks & 0x7FFFFFFF: {0}", DateTime.UtcNow.Ticks & 0x7FFFFFFF); // Interop.Sys.GetSystemTimeAsTicks
            // // Console.WriteLine("GetSystemTimeAsTicks(): {0}", GetSystemTimeAsTicks()); // Interop.Sys.GetSystemTimeAsTicks
            // // System.Collections.ObjectModel.ReadOnlyCollection<System.TimeZoneInfo> zones = System.TimeZoneInfo.GetSystemTimeZones();
            // // foreach (System.TimeZoneInfo zne in zones) {
            // //     System.Console.WriteLine(zne.ToString());
            // // }
            // Console.WriteLine(TimeZoneInfo.Utc.BaseUtcOffset.Ticks);
            // // System.Console.WriteLine(System.TimeZoneInfo.Local);
            // Type tzitype = (typeof(TimeZoneInfo));
            // MethodInfo[] tzimethods = tzitype.GetMethods(BindingFlags.Instance|BindingFlags.Static|BindingFlags.NonPublic);
            // MethodInfo ourmeth = null;
            // foreach (MethodInfo meth in tzimethods) {
            //     // Console.WriteLine(meth.Name);
            //     if (meth.Name == "GetDateTimeNowUtcOffsetFromUtc") {
            //         ourmeth = meth;
            //     }
            // }
            // if (ourmeth != null) {
            //     Console.WriteLine(ourmeth.Name);
            //     bool isblabla = true;
            //     TimeSpan ticks = (TimeSpan) ourmeth.Invoke(null, new object[]{DateTime.UtcNow, isblabla});
            //     Console.WriteLine(ticks.Ticks);
            //     Console.WriteLine(isblabla);
            //     Console.WriteLine(DateTime.UtcNow.Ticks);
            // }

            // for (int i = 0; i < 100; i++) {
            //     Console.WriteLine("DateTime.Now.Ticks: {0}", DateTime.Now.Ticks); // Interop.Sys.GetSystemTimeAsTicks
            //     System.Threading.Thread.Sleep(10);
            // }
            // Timespec tp;
            // tp.tv_nsec = 0;
            // tp.tv_sec = 0;
            // unsafe {
            //     clock_gettime(298, &tp);
            //     Console.WriteLine("clock_gettime(298): " + tp.tv_sec +  ", " + tp.tv_nsec);
            // }
        }
    }
}