using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;


namespace ConsoleApp8
{
    class Menu //Основное меню приложения
    {
        private int SelectedIndex; // Определяет преффикс при выводе пункта меню на экран
        private string Prompt; // сообщение выводимое программой

        private string[] Options; //действия ,доступные пользователю
        public Menu(string[] options, string prompt)
        {

            Prompt = prompt;

            Options = options;
            SelectedIndex = 0;

        }
        private void DisplayOptions()
        {
            ForegroundColor = ConsoleColor.Red;
            WriteLine(Prompt);
            for (int i = 0; i < Options.Length; i++)
            {
                string currentOption = Options[i];
                string prefix = "*";
                if (i == SelectedIndex)
                {

                    prefix = " * ";
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.Green;

                }
                else
                {
                    prefix = "  ";
                    ForegroundColor = ConsoleColor.Green;
                    BackgroundColor = ConsoleColor.Black;

                }
                WriteLine($"<<{currentOption}>>");
            }

            ResetColor();
        }
        public int Run() //работа клавиш
        {
            ConsoleKey keyPressed;// при нажатие клавиши регистр либо прибавляет значение либо вычетает
            do
            {
                Clear();
                DisplayOptions();
                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;
                //перемещает индекс
                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0;
                    }
                }
            } while (keyPressed != ConsoleKey.Enter);
            return SelectedIndex;

        }
    }


    class System//само приложение
    {

        static void Main(string[] args)
        {






            string prompt = @"█░░░█ █▀▀ █░░ ▄▀ ▄▀▄ █▄░▄█ █▀▀ 
█░█░█ █▀▀ █░▄ █░ █░█ █░█░█ █▀▀ 
░▀░▀░ ▀▀▀ ▀▀▀ ░▀ ░▀░ ▀░░░▀ ▀▀▀ 
Добро пожаловать в систему!!!";


            string[] options = { "Войти", "Зарегистрироваться", "Выйти" };//массив значений в options

            Menu MainMenu = new Menu(options, prompt);
            int selectedIndex = MainMenu.Run();


            switch (selectedIndex)
            {
                case 0:
                    User();
                    break;
                case 1:
                    NewUser();
                    break;
                case 2:
                    ExitSystem();
                    break;

            }

            void ExitSystem()//система выхода
            {
                WriteLine("Если хотите вернуться в меню нажмите клавишу esc");
                WriteLine("\nНажмите дважды на любую кнопку для выхода");
                ConsoleKey keyPressed;
                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;
                if (keyPressed == ConsoleKey.Escape)
                {
                    Main(args);
                }
                ReadKey(true);
                Environment.Exit(0);
            }
            void NewUser()//система регистрации с возможностью возвращения в меню
            {

                Clear();
                WriteLine("Если хотите вернуться в меню нажмите ecs");
                WriteLine("\nЧто бы зарегестрироваться введите логин и пароль:");

                ConsoleKey keyPressed;
                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;
                if (keyPressed == ConsoleKey.Escape)
                {
                    Main(args);
                }
                ReadKey();

            }
            void User( )//вход в систему уже зарегестривовшегося пользователя
            {
            m:
                WriteLine("Если хотите вернуться в меню нажмите esc");
                
                string a,b,c,d,e,login;//присваиваю значение логинов
                a = "Admin";
                b = "HR";
                c = "Sklad";
                d = "Buhgalter";
                e = "Manager";
                
                WriteLine("Введите логин");
                ConsoleKey keyPressed;
                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;
                if (keyPressed == ConsoleKey.Escape)
                {
                    Main(args);
                }
               

                login = ReadLine();
                if (login == a)//проверка на логин
                {
                    string password = "Kenny0MG4", inpt = string.Empty;
                    while (password != inpt)//проверка на пароль
                    {
                        Console.Write("Введите пароль для работы с файлами Windows: ");
                        inpt = string.Empty;
                        while (true)
                        {
                            var key = Console.ReadKey(true);//не отображаем клавишу - true

                            if (key.Key == ConsoleKey.Enter) break; //enter - выходим из цикла

                            Console.Write("*");//рисуем звезду вместо нее
                            inpt += key.KeyChar; //копим в пароль символы
                        }
                        Console.WriteLine();
                    }
                    Console.Write("Допуск получен!");
                    Admin();
                    Console.ReadKey();

                } //поиск по логину и проверка пароля           
                if (login == b)
                {
                    string password = "qwerty123", inpt = string.Empty;
                    while (password != inpt)
                    {
                        Console.Write("Введите пароль для работы с файлами Windows: ");
                        inpt = string.Empty;
                        while (true)
                        {
                            var key = Console.ReadKey(true);//не отображаем клавишу - true

                            if (key.Key == ConsoleKey.Enter) break; //enter - выходим из цикла

                            Console.Write("*");//рисуем звезду вместо нее
                            inpt += key.KeyChar; //копим в пароль символы
                        }
                        Console.WriteLine();
                    }
                    Console.Write("Допуск получен!");
                    HRMenedj();
                    Console.ReadKey();

                }
                if (login == c)
                {
                    string password = "Salam11", inpt = string.Empty;
                    while (password != inpt)
                    {
                        Console.Write("Введите пароль для работы с файлами Windows: ");
                        inpt = string.Empty;
                        while (true)
                        {
                            var key = Console.ReadKey(true);//не отображаем клавишу - true

                            if (key.Key == ConsoleKey.Enter) break; //enter - выходим из цикла

                            Console.Write("*");//рисуем звезду вместо нее
                            inpt += key.KeyChar; //копим в пароль символы
                        }
                        Console.WriteLine();
                    }
                    Console.Write("Допуск получен!");
                    Sklad();
                    Console.ReadKey();

                }
                if (login == d)
                {
                    string password = "228666Ster", inpt = string.Empty;
                    while (password != inpt)
                    {
                        Console.Write("Введите пароль для работы с файлами Windows: ");
                        inpt = string.Empty;
                        while (true)
                        {
                            var key = Console.ReadKey(true);//не отображаем клавишу - true

                            if (key.Key == ConsoleKey.Enter) break; //enter - выходим из цикла

                            Console.Write("*");//рисуем звезду вместо нее
                            inpt += key.KeyChar; //копим в пароль символы
                        }
                        Console.WriteLine();
                    }
                    Console.Write("Допуск получен!");
                    Buhgalter();
                    Console.ReadKey();

                }
                if (login == e)
                {
                    string password = "1488Dima", inpt = string.Empty;
                    while (password != inpt)
                    {
                        Console.Write("Введите пароль для работы с файлами Windows: ");
                        inpt = string.Empty;
                        while (true)
                        {
                            var key = Console.ReadKey(true);//не отображаем клавишу - true

                            if (key.Key == ConsoleKey.Enter) break; //enter - выходим из цикла

                            Console.Write("*");//рисуем звезду вместо нее
                            inpt += key.KeyChar; //копим в пароль символы
                        }
                        Console.WriteLine();
                    }
                    Console.Write("Допуск получен!");
                    Manager();
                    Console.ReadKey();

                }



                else
                {
                    WriteLine("Повторите логин");
                    goto m;
                }
            }
            void Admin()//панель каждого рабочего
            {
                Clear();
                ForegroundColor = ConsoleColor.Yellow;
                WriteLine(@"╔═══╗╔═══╗╔═╗╔═╗╔══╗╔═╗─╔╗
║╔═╗║╚╗╔╗║║║╚╝║║╚╣─╝║║╚╗║║
║║─║║─║║║║║╔╗╔╗║─║║─║╔╗╚╝║
║╚═╝║─║║║║║║║║║║─║║─║║╚╗║║
║╔═╗║╔╝╚╝║║║║║║║╔╣─╗║║─║║║
╚╝─╚╝╚═══╝╚╝╚╝╚╝╚══╝╚╝─╚═╝
<<ВЫ АДМИН>>если хотите вернуться в меню нажмите esc");
                ConsoleKey keyPressed;
                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;
                if (keyPressed == ConsoleKey.Escape)
                {
                    Main(args);
                }
                ResetColor();
                ReadKey(true);
            }//рабочая панель каждого рабочего
            void HRMenedj()
            {
                Clear();
                ForegroundColor = ConsoleColor.Blue;
                WriteLine(@"╔╗─╔╗╔═══╗
║║─║║║╔═╗║
║╚═╝║║╚═╝║
║╔═╗║║╔╗╔╝
║║─║║║║║╚╗
╚╝─╚╝╚╝╚═╝
<<ВЫ HR-МЕНЕДЖЕР>>если хотите вернуться в меню нажмите esc");
                ConsoleKey keyPressed;
                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;
                if (keyPressed == ConsoleKey.Escape)
                {
                    Main(args);
                }
                ResetColor();
                ReadKey(true);
            }
            void Sklad()
            {
                Clear();
                ForegroundColor = ConsoleColor.Cyan;
                WriteLine(@"╔═══╗╔═══╗╔═══╗╔═══╗╔═══╗╔════╗╔═══╗╔═══╗     ╔═══╗╔╗╔═╗╔╗───╔═══╗╔═══╗╔═══╗
║╔═╗║║╔═╗║║╔══╝║╔═╗║║╔═╗║║╔╗╔╗║║╔═╗║║╔═╗║     ║╔═╗║║║║╔╝║║───║╔═╗║╚╗╔╗║║╔═╗║
║║─║║║╚═╝║║╚══╗║╚═╝║║║─║║╚╝║║╚╝║║─║║║╚═╝║     ║╚══╗║╚╝╝─║║───║║─║║─║║║║║║─║║
║║─║║║╔══╝║╔══╝║╔╗╔╝║╚═╝║──║║──║║─║║║╔╗╔╝     ╚══╗║║╔╗║─║║─╔╗║╚═╝║─║║║║║╚═╝║
║╚═╝║║║───║╚══╗║║║╚╗║╔═╗║──║║──║╚═╝║║║║╚╗     ║╚═╝║║║║╚╗║╚═╝║║╔═╗║╔╝╚╝║║╔═╗║
╚═══╝╚╝───╚═══╝╚╝╚═╝╚╝─╚╝──╚╝──╚═══╝╚╝╚═╝     ╚═══╝╚╝╚═╝╚═══╝╚╝─╚╝╚═══╝╚╝─╚╝
<<ВЫ ОПЕРАТОР СКЛАДА>>если хотите вернуться в меню нажмите esc");
                ConsoleKey keyPressed;
                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;
                if (keyPressed == ConsoleKey.Escape)
                {
                    Main(args);
                }
                ResetColor();
                ReadKey(true);
            }
            void Buhgalter()
            {
                Clear();
                ForegroundColor = ConsoleColor.Magenta;
                WriteLine(@"╔══╗─╔╗─╔╗╔╗─╔╗╔═══╗╔═══╗╔╗───╔════╗╔═══╗╔═══╗
║╔╗║─║║─║║║║─║║║╔═╗║║╔═╗║║║───║╔╗╔╗║║╔══╝║╔═╗║
║╚╝╚╗║║─║║║╚═╝║║║─╚╝║║─║║║║───╚╝║║╚╝║╚══╗║╚═╝║
║╔═╗║║║─║║║╔═╗║║║╔═╗║╚═╝║║║─╔╗──║║──║╔══╝║╔╗╔╝
║╚═╝║║╚═╝║║║─║║║╚╩═║║╔═╗║║╚═╝║──║║──║╚══╗║║║╚╗
╚═══╝╚═══╝╚╝─╚╝╚═══╝╚╝─╚╝╚═══╝──╚╝──╚═══╝╚╝╚═╝
<<ВЫ БУХГАЛТЕР>>если хотите вернуться в меню нажмите esc");
                ConsoleKey keyPressed;
                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;
                if (keyPressed == ConsoleKey.Escape)
                {
                    Main(args);
                }
                ResetColor();
                ReadKey(true);
            }
            void Manager()
            {
                Clear();
                ForegroundColor = ConsoleColor.DarkYellow;
                WriteLine(@"╔═╗╔═╗╔═══╗╔═╗─╔╗╔═══╗╔═══╗╔═══╗╔═══╗
║║╚╝║║║╔═╗║║║╚╗║║║╔═╗║║╔═╗║║╔══╝║╔═╗║
║╔╗╔╗║║║─║║║╔╗╚╝║║║─║║║║─╚╝║╚══╗║╚═╝║
║║║║║║║╚═╝║║║╚╗║║║╚═╝║║║╔═╗║╔══╝║╔╗╔╝
║║║║║║║╔═╗║║║─║║║║╔═╗║║╚╩═║║╚══╗║║║╚╗
╚╝╚╝╚╝╚╝─╚╝╚╝─╚═╝╚╝─╚╝╚═══╝╚═══╝╚╝╚═╝
<<ВЫ МЕНЕДЖЕР>>если хотите вернуться в меню нажмите esc");
                ConsoleKey keyPressed;
                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;
                if (keyPressed == ConsoleKey.Escape)
                {
                    Main(args);
                }
                ResetColor();
                ReadKey(true);

            }


        }



    }
    
        

}


