using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 1;

            while (true)
            {

                if (count == 24 || count == 48 || count == 72)
                {
                    //Operation
                    Console.WriteLine("YOHO");
                }
                count +=1;

                if (count==200)
                {
                    break;
                }
            }
        }
    }
}
