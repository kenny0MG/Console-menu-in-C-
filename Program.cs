using System;
using System.IO;

namespace file
{
    class Program
    {
        static void Main(string[] args)
        {
            int a;
            string Cheki = "F:\\Cheki\\";
            Directory.CreateDirectory(Cheki);
            File.Create(Cheki + "Cheki.txt");
            Console.WriteLine("выберете товар: что бы выбрать раздел укажите нужную цифру: 1 - Телефоны; 2 - Компьютеры");
            a = Convert.ToInt32(Console.ReadLine());
            if(a==1)
            {
                int b,c;
                Console.WriteLine("Выберете модель с кнопка - 1; без кнопок -2");
                b = Convert.ToInt32(Console.ReadLine());
                switch(b)
                {
                    case 1:
                        Console.WriteLine("цена 100$, экран 2 дюйма размер 10х10");
                        break;
                    case 2:
                        Console.WriteLine("цена 1000000$, экран 9 дюймов размер 10х10");
                        break;
                  
                }
                Console.WriteLine("какую модель хотите приобрести");
                c = Convert.ToInt32(Console.ReadLine());

                if (c == 1)
                {
                    Console.WriteLine("Поздравляю с покупкой");
                    using (StreamWriter sw = new StreamWriter(Cheki + "Cheki.txt"))
                    {
                        sw.Write("цена 100$, экран 2 дюйма размер 10х10");
                    }
                    Console.ReadKey();
                }

            }

        }
    }
}
