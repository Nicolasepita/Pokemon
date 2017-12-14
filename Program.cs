using System;
using System.Collections.Generic;
using System.Management.Instrumentation;

namespace Pokemon
{
    internal class Program
    {
        private static bool finish = false;
        
        public static void Main(string[] args)
        {
            try
            {
                Connection n = new Connection("127.0.0.1", 65555);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            while (!finish)
            {
                
            }
        }
    }
}