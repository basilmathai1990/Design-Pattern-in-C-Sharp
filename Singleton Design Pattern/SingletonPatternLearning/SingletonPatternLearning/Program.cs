using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SingletonPatternLearning
{
    class Program
    {
        static void Main(string[] args)
        {
            // Task.WaitAll(Task.Run(() => WebCache.Instance), Task.Run(() => WebCache.Instance));
            // WebCache cache = new WebCache(); is inaccessible due to its protection level .
            Singleton s = Singleton.Instance;

            WebCache cache1 = WebCache.Instance;
            string t1 = $" Airport list first value : { cache1.GetAirpotList[0]}";
            Console.WriteLine(t1);
            Console.WriteLine(t1);
            WebCache cache2 = WebCache.Instance;
            t1 = $" Airport list first value :  { cache2.GetAirpotList[0]}";
            Console.WriteLine(t1);
            Console.WriteLine(t1);
            cache2.GetAirpotList = null;
            Console.WriteLine("After assigning new values");
            t1 = $" Airport list first value :  { cache2.GetAirpotList[0]}";
            Console.WriteLine(t1);


            Console.ReadKey();
        }
    }

    // First version - not thread-safe

    //public sealed class WebCache
    //{
    //    private static WebCache instance = null;

    //    private List<string> _getList = null;

    //    private WebCache()
    //    {
    //        Console.WriteLine("inside private constructor");
    //    }

    //    public static WebCache Instance
    //    {
    //        get
    //        {
    //            if (instance == null)
    //            {
    //                instance = new WebCache();
    //            }
    //            return instance;
    //        }
    //    }

    //    public List<string> GetAirpotList
    //    {
    //        get
    //        {
    //            if (_getList == null)
    //            {
    //                Console.WriteLine("Airpot List Reintialized");
    //                _getList = new List<string> { "COK", "DXB" };
    //            }

    //            return _getList;
    //        }

    //        set { _getList = value; }
    //    }
    //}

    // Third version - attempted thread-safety using double-check locking
    public sealed class WebCache
    {

        private static WebCache _instance = null;
        private List<string> _getList = null;
        private static volatile object _padlock = new object();

        private WebCache()
        {
            Console.WriteLine("Inside private constructor .");
        }

        public static WebCache Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_padlock)
                    {
                        if (_instance == null)
                        {
                            Console.WriteLine("Cache intialized .");
                            _instance = new WebCache();
                        }
                    }
                }

                return _instance;
            }

            set
            {
                _instance = value;
            }

        }


        public List<string> GetAirpotList
        {
            get
            {
                if (_getList == null)
                {
                    Console.WriteLine("Airpot list reintialized .");
                    _getList = new List<string> { "COK", "DXB" };
                }

                return _getList;
            }

            set { _getList = value; }
        }
    }


    //Fourth  Static Initialization

    public sealed class Singleton
    {

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Singleton()
        {
            Console.WriteLine("static");
        }

        private Singleton()
        {
            Console.WriteLine("private");
        }

        public static Singleton Instance { get; } = new Singleton();
    }

    //Fifth version - fully lazy instantiation
    //public sealed class Singleton
    //{
    //    private Singleton()
    //    {
    //    }

    //    public static Singleton Instance { get { return Nested.instance; } }

    //    private class Nested
    //    {
    //        // Explicit static constructor to tell C# compiler
    //        // not to mark type as beforefieldinit
    //        static Nested()
    //        {
    //        }

    //        internal static readonly Singleton instance = new Singleton();
    //    }
    //}


    // Sixth lazy implementation
    //public sealed class Singleton
    //{
    //    private static readonly Lazy<Singleton> lazy =
    //        new Lazy<Singleton>(() => new Singleton());

    //    public static Singleton Instance { get { return lazy.Value; } }

    //    private Singleton()
    //    {
    //    }
    //}

   
}
