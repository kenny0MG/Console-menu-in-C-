


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;


namespace ConsoleApp8
{
    class Menu //Основное меню приложения
    {
        private int SelectedIndex; // Определяет преффикс при выводе пункта меню на экран
        private string Prompt; // сообщение выводимое программой
        private string Prompt1;
        private string Prompt2;

        private string[] Options; //действия ,доступные пользователю
        private int SelectedIndex1; // Определяет преффикс при выводе пункта меню на экран


        private string[] Options1; //действия ,доступные пользователю
        public void Menu1(string[] options, string prompt1, string prompt2)
        {
            Prompt2 = prompt2;
            Prompt1 = prompt1;
            Options1 = options;

            SelectedIndex1 = 0;

        }
        private void DisplayOptions1()
        {
            WriteLine(Prompt2);
            WriteLine(Prompt1);
            ForegroundColor = ConsoleColor.Yellow;

            for (int i = 0; i < Options1.Length; i++)
            {
                string currentOption1 = Options1[i];
                string prefix = " ";
                if (i == SelectedIndex1)
                {

                    prefix = "*";
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.Green;

                }
                else
                {
                    prefix = "  ";
                    ForegroundColor = ConsoleColor.Green;
                    BackgroundColor = ConsoleColor.Black;

                }
                WriteLine($"<<{currentOption1}>>");
            }

            ResetColor();
        }
        public Menu(string[] options, string prompt)
        {

            Prompt = prompt;


            Options = options;

            SelectedIndex = 0;

        }
        private void DisplayOptions()
        {
            ForegroundColor = ConsoleColor.Cyan;
            WriteLine(Prompt);
            for (int i = 0; i < Options.Length; i++)
            {
                string currentOption = Options[i];
                string prefix = " ";
                if (i == SelectedIndex)
                {

                    prefix = "*";
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
        private int SelectedIndex1; // Определяет преффикс при выводе пункта меню на экран


        private string[] Options1; //действия ,доступные пользователю
        public void Menu1(string[] options)
        {

            Options1 = options;

            SelectedIndex1 = 0;

        }
        private void DisplayOptions()
        {
            ForegroundColor = ConsoleColor.Cyan;

            for (int i = 0; i < Options1.Length; i++)
            {
                string currentOption = Options1[i];
                string prefix = " ";
                if (i == SelectedIndex1)
                {

                    prefix = "*";
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


        static void Main(string[] args)
        {



            Title = "Metadon";



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
                ForegroundColor = ConsoleColor.Red;
                WriteLine("Если хотите вернуться в меню нажмите клавишу esc");

                WriteLine("\nНажмите дважды на любую кнопку для выхода");
                ResetColor();
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

                string Name, Number, Pochta, Log, Passwd, a;
                int y, x, l, w, c, z, mn;
                y = 0;
                x = 0;
                l = 0;
                w = 0;
                c = 0;
                z = 0;


                Regex reg = new Regex("@[!@#$%^&*]");//Спец символы для проверки
                Regex tel = new Regex("@[+]");




                Clear();



                ForegroundColor = ConsoleColor.Red;
                WriteLine("<<Если хотите вернуться в меню нажмите ecs>>");
                ResetColor();
                ForegroundColor = ConsoleColor.Yellow;


                WriteLine(" \n<<Если хотите начать регистрацию нажмите Enter>> ");
                ResetColor();
                ConsoleKey keyPressed;
                ConsoleKeyInfo keyInfo = ReadKey(true);
                keyPressed = keyInfo.Key;
                if (keyPressed == ConsoleKey.Escape)
                {
                    Main(args);
                }

                else if (keyPressed == ConsoleKey.Enter)
                {
                    goto d;
                }
                ReadKey(true);



                Clear();
            d:
                ForegroundColor = ConsoleColor.Yellow;
                WriteLine("\nЧто бы зарегестрироваться введите :");
                ResetColor();

                ForegroundColor = ConsoleColor.Green;
                WriteLine("\n<<ФИО:>>");

                ResetColor();

                Name = ReadLine();

            h:
                ForegroundColor = ConsoleColor.Green;
                WriteLine("<<Введите номер телефона:>>");
                ResetColor();
                Number = ReadLine();
                Match s = tel.Match(Number);



                if (Number.Length == 12)
                {

                    ForegroundColor = ConsoleColor.Green;
                    WriteLine("<<Введите почту:>>");
                    ResetColor();
                    Pochta = ReadLine();

                    ForegroundColor = ConsoleColor.Green;
                    WriteLine("<<Введите логин:>>");
                    ResetColor();
                    Log = ReadLine();


                p:
                    ForegroundColor = ConsoleColor.Green;
                    WriteLine("<<Введите пароль(три цифры,три буквы и три спец символа):>>");
                    ResetColor();
                    Passwd = ReadLine();

                    Match m = reg.Match(Passwd);//Проверка пароля на спец символы
                    foreach (char t in Passwd.Where(char.IsNumber))
                        y = y + 1;
                    foreach (char f in Passwd.Where(char.IsNumber))
                        x = x + 1;
                    foreach (char d in Passwd.Where(char.IsNumber))
                        l = l + 1;
                    foreach (char p in Passwd.Where(char.IsUpper))//Проверка на прописные буквы
                        w = w + 1;
                    foreach (char o in Passwd.Where(char.IsUpper))
                        c = c + 1;
                    foreach (char i in Passwd.Where(char.IsUpper))
                        z = z + 1;
                    if (Passwd.Length == 8 && y > 0 && x > 0 && l > 0 && w > 0 && c > 0 && z > 0)
                    {

                    g:
                        var rep = new StreamWriter(@"C:\Users\Пользователь\source\repos\Metadon1.1\Metadon1.1\bin\Debug\клиенты1.txt", true);//система сохранения новых пользователей
                        ForegroundColor = ConsoleColor.Green;
                        WriteLine("<<Введите пароль снова:>>");
                        ResetColor();// закрыть файл для записи
                        a = ReadLine();
                        if (a == Passwd)
                        {
                            Clear();
                            ForegroundColor = ConsoleColor.Yellow;
                            WriteLine("<<Регистрация прошла успешно!>>");
                            ResetColor();
                            rep.Write(Passwd, true);
                            rep.Write(Log, true);
                            rep.Write(Number, true);
                            rep.Write(Pochta, true);
                            rep.Write(Name, true);
                            rep.Write("\n");
                            rep.Close();

                            WriteLine("Пользователей зарегеcтрировано:");

                            ForegroundColor = ConsoleColor.Red;
                            WriteLine("\nНажмите esc что бы вернуться в меню...");
                            ResetColor();
                            ConsoleKey keyPressed1;
                            ConsoleKeyInfo keyInfo1 = ReadKey(true);
                            keyPressed1 = keyInfo1.Key;
                            if (keyPressed1 == ConsoleKey.Escape)
                            {
                                Main(args);
                            }
                            ReadKey(true);
                        }
                        else
                        {
                            ForegroundColor = ConsoleColor.Red;
                            WriteLine("\nПароль не совпадает");
                            WriteLine("\nНажмите esc что бы вернуться в меню...");
                            WriteLine("\nНажмите Enter что бы повторить пароль еще раз...");
                            ResetColor();
                            ConsoleKey keyPressed2;
                            ConsoleKeyInfo keyInfo2 = ReadKey(true);
                            keyPressed2 = keyInfo2.Key;
                            if (keyPressed2 == ConsoleKey.Escape)
                            {
                                Main(args);
                            }
                            ReadKey(true);
                            ConsoleKey keyPressed3;
                            ConsoleKeyInfo keyInfo3 = ReadKey(true);
                            keyPressed3 = keyInfo3.Key;
                            if (keyPressed3 == ConsoleKey.Enter)



                            {
                                goto g;
                            }
                            ReadKey(true);


                        }
                    }


                    else
                    {
                        ForegroundColor = ConsoleColor.Red;
                        WriteLine("\n<<НЕ ДОСТАТОЧНО СИМВОЛОВ , НУЖНО 8 ЗНАКОВ>>");
                        WriteLine("\nНажмите esc что бы вернуться в меню...");
                        ResetColor();
                        ForegroundColor = ConsoleColor.Yellow;
                        WriteLine("\nНажмите Enter что бы продолжить регистрацию...");
                        ResetColor();
                        ConsoleKey keyPressed2;
                        ConsoleKeyInfo keyInfo2 = ReadKey(true);
                        keyPressed2 = keyInfo2.Key;
                        if (keyPressed2 == ConsoleKey.Escape)
                        {
                            Main(args);
                        }

                        else if (keyPressed2 == ConsoleKey.Enter)
                        {
                            goto p;
                        }
                        ReadKey(true);
                    }


                }


                else
                {

                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("\n<<НЕ ДОСТАТОЧНО СИМВОЛОВ ИЛИ ВЫ ПРОПУСТИЛИ +(НУЖНО 12 ЗНАКОВ)>>");
                    WriteLine("\nНажмите esc что бы вернуться в меню...");

                    ResetColor();
                    ForegroundColor = ConsoleColor.Yellow;
                    WriteLine("\nНажмите Enter что бы продолжить регистрацию...");
                    ResetColor();
                    ConsoleKey keyPressed2;
                    ConsoleKeyInfo keyInfo2 = ReadKey(true);
                    keyPressed2 = keyInfo2.Key;
                    if (keyPressed2 == ConsoleKey.Escape)
                    {
                        Main(args);
                    }

                    else if (keyPressed2 == ConsoleKey.Enter)
                    {
                        goto h;
                    }
                    ReadKey(true);

                }





            }
            void User()//вход в систему уже зарегестривовшегося пользователя
            {
                string DefaultPath = "C\\";



                ForegroundColor = ConsoleColor.Red;

                WriteLine("Если хотите вернуться в меню нажмите esc");
                ResetColor();
                Excel.Application ObjWorkExcel = new Excel.Application();
                Excel.Workbook ObjWorkBook =  ObjWorkExcel.Workbooks.Open(DefaultPath);
                Excel.Worksheet ObjWorkSheet = (Excel.Worksheet)ObjWorkBook.Sheets[1];
                var lastCell = ObjWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell);
                WriteLine("Авторизация...");
                Write("Введите логин:");
                string login;
                login = ReadLine();
                while (true)
                {
                    for (int i = 1; i < lastCell.Row; i++)
                    {
                        if (ObjWorkSheet.Cells[i, 1].Text.Tostring() == login)
                        {
                            WriteLine("Пользователь найден");
                            string password = "ваш пароль", inpt = string.Empty;
                            while (password != inpt)
                            {
                                Console.Write("Введите пароль: ");

                                while (true)
                                {

                                    var key = Console.ReadKey(true);//не отображаем клавишу - true
                                    if (key.Key == ConsoleKey.Enter) break; //enter - выходим из цикла
                                    if (key.Key == ConsoleKey.Backspace)
                                    {

                                        Console.Clear();
                                        Console.Write("Введите пароль: ");
                                        for (int p = 0; p < inpt.Length - 1; p++)
                                        {
                                            Console.Write("");//рисуем звезду вместо нее
                                        }
                                        inpt = inpt.TrimEnd(inpt[inpt.Length - 1]);
                                    }
                                    else
                                    {
                                        Console.Write("");//рисуем звезду вместо нее
                                        inpt += key.KeyChar; //копим в пароль символы
                                    }

                                }
                                WriteLine("Пользователь не найден");

                            }
                            WriteLine("Допуск получен");
                            switch(ObjWorkSheet.Cells[i, 4].Text.ToString())
                            {
                                case "1":
                                    {
                                        Admin();
                                        break;
                                    }
                                case "2":
                                    {
                                        Manager();
                                        break;
                                    }
                            }


                        }
                    }
                }
            }


            void NewUser1()
            {

            }
            



                void Admin()//панель каждого рабочего
                {
                    void Delete()
                    {
                        WriteLine("<3");
                    }

                    string prompt1 = (@"╔═══╗╔═══╗╔═╗╔═╗╔══╗╔═╗─╔╗
║╔═╗║╚╗╔╗║║║╚╝║║╚╣─╝║║╚╗║║
║║─║║─║║║║║╔╗╔╗║─║║─║╔╗╚╝║
║╚═╝║─║║║║║║║║║║─║║─║║╚╗║║
║╔═╗║╔╝╚╝║║║║║║║╔╣─╗║║─║║║
╚╝─╚╝╚═══╝╚╝╚╝╚╝╚══╝╚╝─╚═╝
<<ВЫ АДМИН>>что бы выйти нажмите esc");
                    string[] options1 = { "Редакировать пользователей", "Создать пользователя", "Удалить пользователя", "Выход" };//массив значений в options для админа

                    Menu MainMenu1 = new Menu(options1, prompt1);
                    int selectedIndex1 = MainMenu1.Run();
                    Clear();
                    switch (selectedIndex1)
                    {

                        case 0:
                            User();

                            break;
                        case 1:

                            NewUser1();
                            break;
                        case 2:
                            Delete();
                            break;
                        case 3:
                            ExitSystem();
                            break;
                    }



                    Clear();



                }//рабочая панель каждого рабочего
                void HRMenedj()
                {
                    Clear();
                    ForegroundColor = ConsoleColor.Blue;
                    string prompt2 = (@"╔╗─╔╗╔═══╗
║║─║║║╔═╗║
║╚═╝║║╚═╝║
║╔═╗║║╔╗╔╝
║║─║║║║║╚╗
╚╝─╚╝╚╝╚═╝
<<ВЫ HR-МЕНЕДЖЕР>>если хотите вернуться в меню нажмите esc");
                    string[] options1 = { "1", "2", "3" };//массив значений в options

                    Menu MainMenu1 = new Menu(options1, prompt2);
                    int selectedIndex1 = MainMenu1.Run();


                    switch (selectedIndex1)
                    {
                        case 0:
                            Clear();
                            WriteLine("<3");
                            ReadKey(true);
                            break;
                        case 1:
                            NewUser();
                            break;
                        case 2:
                            Main(args);
                            break;

                    }
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
