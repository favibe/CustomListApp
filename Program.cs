using System;
using System.Linq;

namespace CustomListApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomList<int> list = new CustomList<int>();

            for (int i = 0; i < 1000; i++)
            {
                list.Add(i);
            }

            list.Remove(1);
            list.Insert(1, 90);
            list.RemoveAt(0);

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            var linqTest = list.Where(x => x % 2 == 0);

            foreach (var item in linqTest)
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }
    }
}
