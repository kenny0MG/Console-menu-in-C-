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
                string prefix;
                if (i == SelectedIndex)
                {
                    
                    prefix = " = ";
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
            ForegroundColor = ConsoleColor.Red;
            string prompt="Добро пожаловать!!!";


            string[] options = { "Войти", "Зарегистрироваться", "Выйти" };//массив значений в options

            Menu MainMenu = new Menu(options, prompt);
            int selectedIndex = MainMenu.Run();

            //WriteLine("Нажмите на любую кнопку для выхода...");
            // ReadKey(true);

            ResetColor();
        }

    }
}
