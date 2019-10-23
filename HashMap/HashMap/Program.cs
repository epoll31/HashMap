using System;

namespace HashMap
{
    class Program
    {
        static void Main(string[] args)
        {
            HashMap<int, string> hashMap = new HashMap<int, string>();

            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                int number = random.Next(0, 300);
                if (hashMap.ContainsKey(number))
                {
                    i--; 
                    continue;
                }
                hashMap.Add(number, number.ToString());
            }

        }
    }
}
