using System;
using System.Collections.Generic;

namespace Pokemon
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Network_manager n = new Network_manager("127.0.0.1", 65555);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}