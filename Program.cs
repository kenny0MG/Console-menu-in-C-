using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace tolokonnikove
{
    class Data
    {
        /// <summary>
        /// Разрыв строки на подстроки
        /// </summary>
        public static string[] Explode(string separator, string source)
        {
            // Разрыв строки на подстроки (аналог explode на языке php)
            return source.Split(new string[] { separator }, StringSplitOptions.None);
        }
        /// <summary>
        /// Проверка файла на существование
        /// </summary>
        public static bool CheckExistFile(string file, string dir)
        {
            try
            {
                // Если файла не существует, то он не откроется и вылетит ошибка (try ее съест и вернет false)
                using (StreamReader sr = new StreamReader(@"Data\" + dir + "\\" + file + ".dat"))
                {
                    sr.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Считать данные файла построчно
        /// </summary>
        public static string[] ReadFileByString(string file,string dir)
        {
            string dat = "";

            // Считывание данных с файла, с разраничением по ; (этим символом в файлах заканчивается набор данных)
            using (StreamReader sr = new StreamReader(@"Data\"+dir+"\\"+file+".dat"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    dat = dat + line;
                }
                sr.Close();
            }
            return Data.Explode(";", dat);
        }
        /// <summary>
        /// Получить информацию о пользователе
        /// </summary>
        public static string[,] GetUser(string login)
        {
            string[] user = ReadFileByString(login, "Users"); // Получение разбитых данных
            string[] userData = Data.Explode("]", user[0]);  // Получение параметров
            string[,] data = new string[userData.Length,2];
            for(int i = 0;i< userData.Length-1;i++)
            {
                string[] exp = Explode("[", userData[i]); // Разделение на Параметр и значение (в файлах устроено по типу параметр[значение])
                data[i, 0] = exp[0];
                data[i, 1] = exp[1];
            }
            return data;
        }
        /// <summary>
        /// Вывод доступных команд для типа пользователя
        /// </summary>
        public static void GetHelp(string status)
        {
            if(status == "buyer")
            {
                Console.WriteLine("\r\nСписок команд для покупателя\r\n"+new string('_',50));

                var table = new ConsoleTable("Команда", "Параметры", "Описание"); // Заголовок таблицы

                // строки таблицы
                table.AddRow(new string[] { "get_products", "", "Вывести список товаров" });
                table.AddRow(new string[] { "check_chart", "", "Просмотр корзины" });
                table.AddRow(new string[] { "add_chart", "product count", "Добавление товара в корзину" });
                table.AddRow(new string[] { "change_chart", "product count", "Изменение кол-ва товара в корзине" });
                table.AddRow(new string[] { "delete_chart", "product", "Удаление товара из корзины" });

                // вывод таблицы
                table.Write();

                // далее коментировать таблицы не буду
            }
            else if(status == "admin")
            {
                Console.WriteLine("\r\nСписок команд для администратора\r\n" + new string('_', 50));

                var table = new ConsoleTable("Команда", "Параметры", "Описание");

                table.AddRow(new string[] { "delete_user", "login", "Удалить пользовтеля" });
                table.AddRow(new string[] { "change_user", "login", "Изменить пользователя" });
                table.AddRow(new string[] { "add_user", "", "Добавить пользователя" });
                table.AddRow(new string[] { "add_product", "", "Добавить товар" });
                table.AddRow(new string[] { "change_product", "product", "Изменить товар" });
                table.AddRow(new string[] { "delete_product", "product", "Удалить товар" });
                table.AddRow(new string[] { "add_chart", "product count login", "Добавление товара в корзину пользователя" });
                table.AddRow(new string[] { "change_chart", "product count login", "Изменение кол-ва товара в корзине пользователя" });
                table.AddRow(new string[] { "delete_chart", "product login", "Удаление товара из корзины пользователя" });

                table.Write();
            }
            else if (status == "hr")
            {
                Console.WriteLine("\r\nСписок команд для HR\r\n" + new string('_', 50));

                var table = new ConsoleTable("Команда", "Параметры", "Описание");

                table.AddRow(new string[] { "get_users", "", "Вывести список пользователей" });
                table.AddRow(new string[] { "add_worker", "login", "Нанять пользователя" });
                table.AddRow(new string[] { "delete_worker", "login", "Уволить пользователя" });
                table.AddRow(new string[] { "change_worker", "login", "Изменить пользователя" });
                
                table.Write();
            }
            else if (status == "warehouse")
            {
                Console.WriteLine("\r\nСписок команд для кладовщика\r\n" + new string('_', 50));

                var table = new ConsoleTable("Команда", "Параметры", "Описание");

                table.AddRow(new string[] { "get_products", "", "Вывести список товаров" });
                table.AddRow(new string[] { "update_product_count", "product", "Изменить количество товара" });
                table.AddRow(new string[] { "unready_product", "product", "Изменить срок годности товара" });
                table.AddRow(new string[] { "transfer_product", "product", "Изменить склад товара" });
                
                table.Write();
            }
            else if (status == "cassa")
            {
                Console.WriteLine("\r\nСписок команд для кассира\r\n" + new string('_', 50));

                var table = new ConsoleTable("Команда", "Параметры", "Описание");

                table.AddRow(new string[] { "get_chart_list", "", "Просмотр списка корзин" });
                table.AddRow(new string[] { "check_chart", "login", "Просмотр корзины" });
                table.AddRow(new string[] { "complete_order", "login", "Оформление заказа" });

                table.Write();
            }
            else if (status == "finance")
            {
                Console.WriteLine("\r\nСписок команд для бухгалтера\r\n" + new string('_', 50));

                var table = new ConsoleTable("Команда", "Параметры", "Описание");

                table.AddRow(new string[] { "get_order_list", "", "Просмотр списка заказов" });
                table.AddRow(new string[] { "get_order", "period", "Просмотр информации по заказам за период (period - day, month, quoter, year, all)" });
                table.AddRow(new string[] { "check_order", "order", "Просмотр информации по заказу" });
                table.AddRow(new string[] { "send_payment", "", "Выдача зарплат за месяц" });
                table.AddRow(new string[] { "get_payment", "period", "Просмотр информации по зарплатам за период (period - month, quoter, year, all)" });
                table.AddRow(new string[] { "get_budget", "period", "Просмотр информации по бюджету за период (period - month, quoter, year, all)" });
                
                table.Write();
            }
        }
        /// <summary>
        /// Проверка строки на наличие зарезервированных символов (для работы с файлами)
        /// </summary>
        public static bool CheckString(string str)
        {
            // Проверка символов на запрещенные символы
            // для корректной работы, файлы файловой база данных должны быть в формате
            // параметр1[значение]параметр2[значение];
            // параметр1[значение]параметр2[значение];
            if (str != "")
            {
                int err = 0;
                char[] bad = new char[] { ';', '[', ']' };
                for (int i = 0; i < str.Length; i++)
                {
                    if (bad.Contains(str[i]))
                    {
                        err++;
                    }
                }
                if (err > 0)
                {
                    string ch = "";
                    for (int i = 0; i < bad.Length; i++)
                    {
                        ch += " " + bad[i];
                    }
                    Console.WriteLine("Найдены запрещенные символы, исключите следующие символы: " + ch);
                    return false;
                }
                return true;
            }
            else
            {
                Console.WriteLine("Пустая строка недопустима");
                return false;
            }
        }
        /// <summary>
        /// Проверка пароля на минимальные ограничения
        /// </summary>
        public static bool CheckPass(string str)
        {
            if (str.Length >= 8)
            {
                char[] ch = str.ToCharArray();
                var count = ch.Where((n) => n >= '0' && n <= '9').Count(); // Проверка на наличие чисел 0-9
                if (count >= 3)
                {
                    Regex regex = new Regex("([A-Z])|([А-Я])"); // параметры проверки строки (на заглавные)
                    if (regex.Matches(str).Count >= 3)
                    {
                        int err = 0;
                        string c = str;
                        for (int i = 0; i < ch.Length; i++)
                        {
                            if ((c[i] >= 'a' && c[i] <= 'z') || (c[i] >= 'A' && c[i] <= 'Z'))
                            { }
                            else if (c[i] >= '0' && c[i] <= '9')
                            { }
                            else if (c[i] != ' ' && c[i] != '\n')
                            {
                                err++;
                            }
                        }
                        if (err >= 2)
                        {
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Пароль должен содержать минмум 2 спец символа");
                            return false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Пароль должен содержать минмум 3 заглавных символа");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Пароль должен содержать минмум 3 цифры");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Минимальная длинна пароля 8 символов");
                return false;
            }
        }
        /// <summary>
        /// Проверка почты на собаку
        /// </summary>
        public static bool CheckMail(string str)
        {
            bool finded = false;

            // Проход по всей строк посимвольно
            for(int i = 0;i< str.Length;i++)
            {
                if(str[i] == '@')
                {
                    finded = true;
                }
            }

            if(finded)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Электронная почта должна содержать символ @");
                return false;
            }
        }
        /// <summary>
        /// Проверка статуса на поддерживаемость
        /// </summary>
        public static bool CheckStatus(string str)
        {
            string[] status = new string[] { "buyer", "admin", "hr", "warehouse", "cassa", "finance" };
            string n_status = "";
            bool finded = false;

            // Создание строки с перечнем доступных статусов
            for (int i = 0; i < status.Length; i++)
            {
                n_status += " " + status[i];
            }

            // Проверка статуса на наличие в списке
            for(int i = 0;i< status.Length;i++)
            {
                if(status[i] == str)
                {
                    finded = true;
                }
            }

            if (finded)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Введен не поддерживаемый статус");
                Console.WriteLine("Введите один из доступных статусов: " + n_status);
                return false;
            }
        }
        /// <summary>
        /// Получить информацию о товаре
        /// </summary>
        public static string[,] GetProduct(string product)
        {
            // Аналогия с Data.GetUser
            string[] prod = ReadFileByString(product, "Products");
            string[] prodData = Data.Explode("]", prod[0]);
            string[,] data = new string[prodData.Length, 2];
            for (int i = 0; i < prodData.Length - 1; i++)
            {
                string[] exp = Explode("[", prodData[i]);
                data[i, 0] = exp[0];
                data[i, 1] = exp[1];
            }
            return data;
        }
        /// <summary>
        /// Получить информацию о корзине
        /// </summary>
        public static string [][] GetChart(string login)
        {
            // Метод аналогичен с Data.GetUser, но из за того что имеет множественные данные, то возвращается string [][]
            // и происходит дополнительный проход по внутренним записям
            string[] chartList = ReadFileByString(login, "Charts");
            string[][] data = new string[chartList.Length][];
            for (int i = 0; i < chartList.Length - 1; i++)
            {
                // Дополнительный проход по записям, код аналогичен Data.GetUsers
                string[] chartData = Data.Explode("]", chartList[i]);
                string[] chartRes = new string[chartData.Length];
                for(int j = 0;j< chartData.Length-1;j++)
                {
                    string[] exp = Explode("[", chartData[j]);
                    chartRes[j] = exp[1];
                }
                data[i] = chartRes;
            }
            return data;
        }
        /// <summary>
        /// Проверка строки на число
        /// </summary>
        public static bool CheckInt(string integer)
        {
            try
            {
                // Выбьет ошибку при невозможности конвертации
                int num = Convert.ToInt32(integer);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Отправка почтового сообщения
        /// </summary>
        public static bool SendMail(string text, string mailName)
        {
            // Текущий формат почты не поддерживает русские символы (возвращает ? вместо русского симовола, рекомендую исп. английский)
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com"); // Клиент
                mail.From = new MailAddress("zhilcove@gmail.com"); // От кого
                mail.To.Add(mailName); // Кому
                mail.Subject = "Order"; // Тема письма

                // HTML письмо
                mail.Body += " <html>";
                mail.Body += "<body>";
                mail.Body += text;
                mail.Body += "</body>";
                mail.Body += "</html>";

                mail.IsBodyHtml = true; // Для отправки как html

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("zhilcove@gmail.com", "1806326!"); // авторизация
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail); // При не успешной отправке вылетит ошибка, try ее съест и выдаст false

                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Получить информацию о заказе
        /// </summary>
        public static string[][] GetOrder(string login)
        {
            // Получить информацию о заказе, действует аналогично Data.GetChart (т.к. имеет внутреннее множество данных string[][])
            string[] chartList = ReadFileByString(login, "Orders");
            string[][] data = new string[chartList.Length][];
            for (int i = 0; i < chartList.Length - 1; i++)
            {
                string[] chartData = Data.Explode("]", chartList[i]);
                string[] chartRes = new string[chartData.Length];
                for (int j = 0; j < chartData.Length - 1; j++)
                {
                    string[] exp = Explode("[", chartData[j]);
                    chartRes[j] = exp[1];
                }
                data[i] = chartRes;
            }
            return data;
        }
        /// <summary>
        /// Получить информацию о записях за период (заказы, зп)
        /// </summary>
        public static List<string> GetFilesByPeriod(string period, string dir, bool shortname = false)
        {
            // Массив с найденными файлами (лист т.к. не изместно кол-во файлов)
            List<string> res = new List<string>();

            // Получение файлов из дирректории
            string[] files = Directory.GetFiles("Data\\"+dir+"\\");


            foreach (string filename in files)
            {
                string[] finded = Data.Explode("\\", filename);
                finded = Data.Explode(".", finded[finded.Length - 1]); // получение имени файла (без .dat)

                string[] Fulldate;

                // Если записи в формате 12-12-2020 (день-мес-год)
                if (shortname)
                {
                    Fulldate = new string[1] { finded[0] };
                }
                else // Если записи в формате 12-12-2020_12-12-12 (день-мес-год_час-мин-сек) (используется только в заказах для уникализации файлов)
                {
                    Fulldate = Data.Explode("_", finded[0]);
                }

                string[] date = Data.Explode("-", Fulldate[0]); // разбивка даты на подстроки (имя файла)
                string Nowdate = DateTime.Now.ToString();
                string[] exp = Nowdate.Split(new string[] { " " }, StringSplitOptions.None); // Разбитие текущей даты на подстроки
                string[] nowDate = Data.Explode(".", exp[0]); // Разбите текущей даты (день.мес.год) на подстроки (разделяя по .)

                // Проверка на период
                if (period == "day") // Актуально только для заказов
                {
                    if(date[0] == nowDate[0] && date[1] == nowDate[1] && date[2] == nowDate[2])
                    {
                        res.Add(finded[0]);
                    }
                }
                else if(period == "month")
                {
                    if (date[1] == nowDate[1] && date[2] == nowDate[2])
                    {
                        res.Add(finded[0]);
                    }
                }
                else if(period == "qouter")
                {
                    // Поиск текущего квартала
                    int quoterMin = 0;
                    int quoterMax = 0;

                    if(Convert.ToInt32(nowDate[2]) >= 1)
                    {
                        quoterMax = 3;
                        quoterMin = 1;
                    }
                    else if (Convert.ToInt32(nowDate[2]) >= 4)
                    {
                        quoterMax = 6;
                        quoterMin = 4;
                    }
                    else if (Convert.ToInt32(nowDate[2]) >= 7)
                    {
                        quoterMax = 9;
                        quoterMin = 7;
                    }
                    else if (Convert.ToInt32(nowDate[2]) >= 10)
                    {
                        quoterMax = 12;
                        quoterMin = 10;
                    }

                    if ((Convert.ToInt32(date[1]) >= quoterMin || Convert.ToInt32(date[1]) <= quoterMax) && date[2] == nowDate[2])
                    {
                        res.Add(finded[0]);
                    }
                }
                else if(period == "year")
                {
                    if (date[2] == nowDate[2])
                    {
                        res.Add(finded[0]);
                    }
                }
                else if(period == "all")
                {
                    res.Add(finded[0]);
                }
            }

            return res;
        }
        /// <summary>
        /// Получить информацию о зарплате
        /// </summary>
        public static string[][] GetPayment(string login)
        {
            // Получение данных о зп, аналогично  Data.GetChart (имеет внутреннее множество записей string[][])
            string[] chartList = ReadFileByString(login, "Payments");
            string[][] data = new string[chartList.Length][];
            for (int i = 0; i < chartList.Length - 1; i++)
            {
                string[] chartData = Data.Explode("]", chartList[i]);
                string[] chartRes = new string[chartData.Length];
                for (int j = 0; j < chartData.Length - 1; j++)
                {
                    string[] exp = Explode("[", chartData[j]);
                    chartRes[j] = exp[1];
                }
                data[i] = chartRes;
            }
            return data;
        }
    }
    class Admin
    {
        /// <summary>
        /// Обновить файл пользователя (если не найден, создать)
        /// </summary>
        public static void UpdateUser(string login, string pass, string mail,string status, string fio = "-", string education = "-", string exp = "-", string special = "-", string work = "-", string pay = "0.00")
        {
            // Полная перезапись файла пользователей, создает новый файл если его нет (регистрация)
            string writePath = @"Data\Users\"+login+".dat";
            using (StreamWriter sw = new StreamWriter(writePath))
            {
                sw.WriteLine("login["+ login + "]");
                sw.WriteLine("pass[" + pass + "]");
                sw.WriteLine("mail[" + mail + "]");
                sw.WriteLine("status[" + status + "]");
                sw.WriteLine("fio[" + fio + "]");
                sw.WriteLine("education[" + education + "]");
                sw.WriteLine("exp[" + exp + "]");
                sw.WriteLine("special[" + special + "]");
                sw.WriteLine("work[" + work + "]");
                sw.WriteLine("pay[" + pay + "];");
                sw.Close();
            }
        }
        /// <summary>
        /// Удаление пользователя
        /// </summary>
        public static void DeleteUser(string login)
        {
            // Проверка на наличие файла
            if (Data.CheckExistFile(login, "Users"))
            {
                // Удаление файла с концами
                File.Delete("Data\\Users\\" + login + ".dat");
                Console.WriteLine("Пользователь удален");
            }
            else
            {
                Console.WriteLine("Пользователь не найден");
            }
        }
        /// <summary>
        /// Редактирование пользователя
        /// </summary>
        public static void ChangeUser(string login)
        {
            if(Data.CheckExistFile(login,"Users"))
            {
                // Получение текущих данных о пользователе
                string[,] user = Data.GetUser(login);
                string[] dat = new string[10];

                Console.WriteLine(new string('_',50));
                Console.WriteLine("Если пользователь - не сотрудник, значения фио и далее указывайте как '-', зп как 0.00");
                Console.WriteLine("Текущие данные:\r\n");

                // Вывод текущих данных
                for(int i = 0;i < 10;i++)
                {
                    Console.WriteLine(user[i,0]+" : "+ user[i, 1]);
                }
                Console.WriteLine("\r\n");
                for(int i = 0; i < 10;i++)
                {
                    // Ввод параметра (цикл бесконечен, пока параметр не будет введен и не будет соответствовать требованиям))
                    bool access = true;
                    while(access)
                    {
                        Console.Write("Введите новый " + user[i, 0] + ": ");
                        dat[i] = Console.ReadLine();
                        if(Data.CheckString(dat[i]))
                        {
                            if (user[i, 0] == "login")
                            {
                                dat[i] = user[i, 1];
                                access = false;
                                Console.WriteLine("Логин изменить нельзя");
                            }
                            else if(user[i, 0] == "pass")
                            {
                                if(Data.CheckPass(dat[i]))
                                {
                                    access = false;
                                }
                            }
                            else if (user[i, 0] == "mail")
                            {
                                if (Data.CheckMail(dat[i]))
                                {
                                    access = false;
                                }
                            }
                            else if (user[i, 0] == "status")
                            {
                                if (Data.CheckStatus(dat[i]))
                                {
                                    access = false;
                                }
                            }
                            else if (user[i, 0] == "pay")
                            {
                                try
                                {
                                    double n = Convert.ToDouble(dat[i]);
                                    access = false;
                                }
                                catch
                                {
                                    Console.WriteLine("ЗП должно быть дробным числом (5.00, 23.50 ...)");
                                }
                            }
                            else
                            {
                                access = false;
                            }
                        }
                    }
                }
                Console.Write("Перепроверьте данные, для подтверждения операции введите y (eng) , для отмены любой другой символ: ");
                string s = Console.ReadLine();
                if(s == "y")
                {
                    // Перезапись пользователя
                    UpdateUser(dat[0], dat[1], dat[2], dat[3], dat[4], dat[5], dat[6], dat[7], dat[8], dat[9]);
                }
                else
                {
                    Console.WriteLine("Операция отменена");
                }
            }
            else
            {
                Console.WriteLine("Пользователь не найден");
            }
        }
        /// <summary>
        /// Добавление пользователя
        /// </summary>
        public static void AddUser()
        {
            // dat - значения, title - параметры
            string[] dat = new string[10];
            string[] title = new string[] { "login", "pass", "mail", "status", "fio", "education", "exp", "special", "work", "pay" };

            Console.WriteLine(new string('_', 50));
            Console.WriteLine("Если пользователь - не сотрудник, значения фио и далее указывайте как '-', зп как 0.00");
            Console.WriteLine("Добавление пользователя:\r\n");
            Console.WriteLine("\r\n");
            for (int i = 0; i < 10; i++)
            {
                // Заполнение данных, бесконечно пока данные не будут соблюдать условиям
                bool access = true;
                while (access)
                {
                    Console.Write("Введите " + title[i] + ": ");
                    dat[i] = Console.ReadLine();
                    if (Data.CheckString(dat[i]))
                    {
                        if(title[i] == "login")
                        {
                            if(!Data.CheckExistFile(dat[i],"Users"))
                            {
                                access = false;
                            }
                            else
                            {
                                Console.WriteLine("Логин занят");
                            }
                        }
                        else if (title[i] == "pass")
                        {
                            if (Data.CheckPass(dat[i]))
                            {
                                access = false;
                            }
                        }
                        else if (title[i] == "mail")
                        {
                            if (Data.CheckMail(dat[i]))
                            {
                                access = false;
                            }
                        }
                        else if (title[i] == "status")
                        {
                            if (Data.CheckStatus(dat[i]))
                            {
                                access = false;
                            }
                        }
                        else if (title[i] == "pay")
                        {
                            try
                            {
                                double n = Convert.ToDouble(dat[i]);
                                access = false;
                            }
                            catch
                            {
                                Console.WriteLine("ЗП должно быть дробным числом (5.00, 23.50 ...)");
                            }
                        }
                        else
                        {
                            access = false;
                        }
                    }
                }
            }
            Console.Write("Перепроверьте данные, для подтверждения операции введите y (eng) , для отмены любой другой символ: ");
            string s = Console.ReadLine();
            if (s == "y")
            {
                // Перезапись пользователя
                UpdateUser(dat[0], dat[1], dat[2], dat[3], dat[4], dat[5], dat[6], dat[7], dat[8], dat[9]);
            }
            else
            {
                Console.WriteLine("Операция отменена");
            }
        }
        /// <summary>
        /// Перезапись файла товара
        /// </summary>
        public static void UpdateProduct(string shop, string warehouse, string category, string product, string count, string ready, string price)
        {
            // Перезапись файла товара (создание если не существует)
            string writePath = @"Data\Products\" + product + ".dat";
            using (StreamWriter sw = new StreamWriter(writePath))
            {
                sw.WriteLine("shop[" + shop + "]");
                sw.WriteLine("warehouse[" + warehouse + "]");
                sw.WriteLine("category[" + category + "]");
                sw.WriteLine("product[" + product + "]");
                sw.WriteLine("count[" + count + "]");
                sw.WriteLine("ready[" + ready + "]");
                sw.WriteLine("price[" + price + "];");
                sw.Close();
            }
        }
        /// <summary>
        /// Добавление товара
        /// </summary>
        public static void AddProduct()
        {
            // Метод аналогичен добавлению, обновлению пользователей, но только проверки сделанны на несколько другие параметры (цена, кол-во, дата)
            string[] dat = new string[7];
            string[] title = new string[] { "shop", "warehouse", "category", "product", "count", "ready", "price"};

            Console.WriteLine(new string('_', 50));
            Console.WriteLine("Добавление товара:\r\n");
            for (int i = 0; i < dat.Length; i++)
            {
                bool access = true;
                while (access)
                {
                    Console.Write("Введите " + title[i] + ": ");
                    dat[i] = Console.ReadLine();
                    if (Data.CheckString(dat[i]))
                    {
                        if (title[i] == "product")
                        {
                            if (!Data.CheckExistFile(dat[i], "Products"))
                            {
                                access = false;
                            }
                            else
                            {
                                Console.WriteLine("Товар уже существует");
                            }
                        }
                        else if (title[i] == "count")
                        {
                            try
                            {
                                int n = Convert.ToInt32(dat[i]);
                                access = false;
                            }
                            catch
                            {
                                Console.WriteLine("Количество должно быть целочисленным");
                            }
                        }
                        else if (title[i] == "price")
                        {
                            try
                            {
                                double n = Convert.ToDouble(dat[i]);
                                access = false;
                            }
                            catch
                            {
                                Console.WriteLine("Цена должна быть дробным числом (5,00 , 23,50 ...)");
                            }
                        }
                        else if (title[i] == "ready")
                        {
                            DateTime dt;
                            bool parse = DateTime.TryParse(dat[i], out dt); // вернет true если dat[i] - дата
                            if (parse)
                            {
                                access = false;
                            }
                            else
                            {
                                Console.WriteLine("Срок годности должен быть в виде даты (25.12.2020)");
                            }
                        }
                        else
                        {
                            access = false;
                        }
                    }
                }
            }
            Console.Write("Перепроверьте данные, для подтверждения операции введите y (eng) , для отмены любой другой символ: ");
            string s = Console.ReadLine();
            if (s == "y")
            {
                Admin.UpdateProduct(dat[0], dat[1], dat[2], dat[3], dat[4], dat[5], dat[6]);
            }
            else
            {
                Console.WriteLine("Операция отменена");
            }
        }
        /// <summary>
        /// Изменение товара
        /// </summary>
        public static void ChangeProduct(string product)
        {
            if (Data.CheckExistFile(product, "Products"))
            {
                // Обновление товара по аналогии с пользователем
                string[,] prod = Data.GetProduct(product);
                string[] dat = new string[7];

                Console.WriteLine(new string('_', 50));
                Console.WriteLine("Текущие данные:\r\n");

                for (int i = 0; i < dat.Length; i++)
                {
                    Console.WriteLine(prod[i, 0] + " : " + prod[i, 1]);
                }
                Console.WriteLine("\r\n");
                for (int i = 0; i < dat.Length; i++)
                {
                    bool access = true;
                    while (access)
                    {
                        Console.Write("Введите новый " + prod[i, 0] + ": ");
                        dat[i] = Console.ReadLine();
                        if (Data.CheckString(dat[i]))
                        {
                            if (prod[i, 0] == "product")
                            {
                                dat[i] = prod[i, 1];
                                Console.WriteLine("Имя товара изменить нельзя");
                                access = false;
                            }
                            else if(prod[i,0] == "count")
                            {
                                try
                                {
                                    int n = Convert.ToInt32(dat[i]);
                                    access = false;
                                }
                                catch
                                {
                                    Console.WriteLine("Количество должно быть целочисленным");
                                }
                            }
                            else if(prod[i,0] == "price")
                            {
                                try
                                {
                                    double n = Convert.ToDouble(dat[i]);
                                    access = false;
                                }
                                catch
                                {
                                    Console.WriteLine("Цена должна быть дробным числом (5,00  23,50 ...)");
                                }
                            }
                            else if (prod[i, 0] == "ready")
                            {
                                DateTime dt;
                                bool parse = DateTime.TryParse(dat[i], out dt);
                                if (parse)
                                {
                                    access = false;
                                }
                                else
                                {
                                    Console.WriteLine("Срок годности должен быть в виде даты (25.12.2020)");
                                }
                            }
                            else 
                            { 
                                access = false; 
                            }
                        }
                    }
                }
                Console.Write("Перепроверьте данные, для подтверждения операции введите y (eng) , для отмены любой другой символ: ");
                string s = Console.ReadLine();
                if (s == "y")
                {
                    UpdateProduct(dat[0], dat[1], dat[2], dat[3], dat[4], dat[5], dat[6]);
                }
                else
                {
                    Console.WriteLine("Операция отменена");
                }
            }
            else
            {
                Console.WriteLine("Товар не найден");
            }
        }
        /// <summary>
        /// Удаление товара
        /// </summary>
        public static void DeleteProduct(string product)
        {
            // Удаление товара по аналогии с пользователем
            if (Data.CheckExistFile(product, "Products"))
            {
                File.Delete("Data\\Products\\" + product + ".dat");
                Console.WriteLine("Товар удален");
            }
            else
            {
                Console.WriteLine("Товар не найден");
            }
        }
    }
    class Buyer
    {
        /// <summary>
        /// Перезапись файла корзины
        /// </summary>
        public static void UpdateChart(string login, string data)
        {
            // Перезапись файла корзины, если нет - создаст (по аналогии с пользователем)
            string writePath = @"Data\Charts\" + login + ".dat";
            string[] exp = Data.Explode("]",data);
            using (StreamWriter sw = new StreamWriter(writePath))
            {
                for(int i = 0;i< exp.Length-1;i = i + 2)
                {
                    sw.WriteLine(exp[i]+"]");
                    sw.WriteLine(exp[i+1]+"];");
                }
                sw.Close();
            }
            Console.WriteLine("Корзина обновлена");
        }
        /// <summary>
        /// Вывод корзины пользователя
        /// </summary>
        public static void CheckChart(string login)
        {
            // Проверка корзины на существование
            if(Data.CheckExistFile(login,"Charts"))
            {
                // Получение данных о корзине
                string[][] data = Data.GetChart(login);

                // Вывод таблицы
                var table = new ConsoleTable("Товар", "Количество");

                for (int i = 0; i < data.Length-1; i++)
                {
                    table.AddRow(new string[] { data[i][0], data[i][1] });
                }

                table.Write();
            }
            else
            {
                Console.WriteLine("Корзины еще нет");
            }
        }
        /// <summary>
        /// Добавление товара в корзину
        /// </summary>
        public static void AddChart(string login, string product, string count)
        {
            // Добавление товара в корзину (если корзины нет, она создастся)
            bool access = true;
            string res = "";

            if (Data.CheckExistFile(product, "Products"))
            {
                if(Data.CheckInt(count))
                {
                    if (Data.CheckExistFile(login, "Charts"))
                    {
                        // Получение корзины (если есть)
                        string[][] data = Data.GetChart(login);
                        // Проход по корзине
                        for (int i = 0; i < data.Length - 1; i++)
                        {
                            // Если добавляемый товар уже есть в корзине, его количество увеличится
                            if (product == data[i][0])
                            {
                                ChangeChart(login, product, count, true);
                                access = false;
                            }
                            res += "product[" + data[i][0] + "]";
                            res += "count[" + data[i][1] + "]";
                        }
                    }
                    else
                    {
                        string[][] data = new string[1][];
                    }

                    // еслитовара раньше в корзине не было, то создастся новая позиция в корзине
                    if (access)
                    {
                        res += "product[" + product + "]";
                        res += "count[" + count + "]";
                        UpdateChart(login, res);
                    }
                }
                else
                {
                    Console.WriteLine("Количество должно быть целочисленным");
                }
            }
            else
            {
                Console.WriteLine("Указанного товара не существует");
            }
        }
        /// <summary>
        /// Изменить количество товара в корзине
        /// </summary>
        public static void ChangeChart(string login, string product, string count, bool plus = false)
        {
            // Обновление количества товаров в корзине
            if (Data.CheckExistFile(product, "Products"))
            {
                if (Data.CheckInt(count))
                {
                    if (Data.CheckExistFile(login, "Charts"))
                    {
                        string res = "";
                        string[][] data = Data.GetChart(login);
                        for (int i = 0; i < data.Length - 1; i++)
                        {
                            if (product == data[i][0])
                            {
                                // если требуется нарастить ( см Buyer.AddChart() )
                                if(plus)
                                {
                                    res += "product[" + data[i][0] + "]";
                                    res += "count[" + (Convert.ToInt32(data[i][1]) + Convert.ToInt32(count)).ToString() + "]";
                                }
                                else // В иных случаях количество устанавливается на введеннное
                                {
                                    res += "product[" + data[i][0] + "]";
                                    res += "count[" + count + "]";
                                }
                            }
                            else // Если товар не тот, который нужно изменить, его не трогаем
                            {
                                res += "product[" + data[i][0] + "]";
                                res += "count[" + data[i][1] + "]";
                            }
                        }
                        UpdateChart(login, res); // перезапись файла корзины
                    }
                }
                else
                {
                    Console.WriteLine("Количество должно быть целочисленным");
                }
            }
            else
            {
                Console.WriteLine("Указанного товара не существует");
            }
        }
        /// <summary>
        /// Удалить товар из корзины
        /// </summary>
        public static void DeleteChart(string login, string product)
        {
            // Удаление товара из корзины
            if (Data.CheckExistFile(product, "Products"))
            {
                if (Data.CheckExistFile(login, "Charts"))
                {
                    string res = "";
                    string[][] data = Data.GetChart(login);
                    for (int i = 0; i < data.Length - 1; i++)
                    {
                        if (product != data[i][0]) // в строку добавляются только записи, отличные от той, что нужно удалить
                        {
                            res += "product[" + data[i][0] + "]";
                            res += "count[" + data[i][1] + "]";
                        }
                    }
                    if(res == "") // если товар единственный в корзине, то удаляется файл
                    {
                        File.Delete("Data\\Charts\\" + login + ".dat");
                        Console.WriteLine("Корзина удалена");
                    }
                    else
                    {
                        UpdateChart(login, res); // если есть другие товары в корзине, то он не удаляется
                    }
                }
            }
            else
            {
                Console.WriteLine("Указанного товара не существует");
            }
        }
    }
    class HR
    {
        /// <summary>
        /// Вывод списка пользователей
        /// </summary>
        public static void GetUsers(string status)
        {
            string[] files = Directory.GetFiles(@"Data\Users\");
            int n = 0;

            // заголовок
            var table = new ConsoleTable("Логин", "Пароль", "Почта", "Статус", "ФИО", "Образование", "Опыт", "Должность", "Место работы", "ЗП");

            // вырисовка строк таблицы
            foreach (string filename in files)
            {
                n++;
                string[] finded = Data.Explode("\\", filename);
                finded = Data.Explode(".", finded[finded.Length - 1]);
                string[,] user = Data.GetUser(finded[0]);
                string[] userDat = new string[10];
                for (int i = 0; i < 10; i++)
                {
                    if(status == "hr" && i == 1) // если это hr, то пароль заменяется звездочками
                    {
                        userDat[i] = new string('*', user[i, 1].Length);
                    }
                    else
                    {
                        userDat[i] = user[i, 1];
                    }
                }
                table.AddRow(userDat);
            }
            table.Write();
        }
        /// <summary>
        /// Редактирование пользователя по типу
        /// </summary>
        public static void ChangeUserByType(string login, string type)
        {
            if (Data.CheckExistFile(login, "Users"))
            {
                string[,] user = Data.GetUser(login);
                string[] dat = new string[10];

                Console.WriteLine(new string('_', 50));

                // Увольнениие пользователей, перевод в покупателей
                if(type == "delete")
                {
                    dat[3] = "buyer"; // обнуление данных
                    for (int i = 0; i < 4; i++)
                    {
                        dat[i] = user[i, 1];
                    }
                    for (int i = 4; i < 9; i++)
                    {
                        dat[i] = "-"; // обнуление данных
                    }
                    dat[9] = "0.00"; // обнуление данных

                    // Перезапись пользователя
                    Admin.UpdateUser(dat[0], dat[1], dat[2], dat[3], dat[4], dat[5], dat[6], dat[7], dat[8], dat[9]);

                    Console.WriteLine("Пользователь уволен");
                }
                else // Рекрутинг пользователя
                {
                    Console.WriteLine("Текущие данные:\r\n");

                    for (int i = 0; i < 10; i++)
                    {
                        if(i == 1) // Замена пароля звездочками
                        {
                            Console.WriteLine(user[i, 0] + " : "+new string('*', user[i, 1].Length));
                        }
                        else
                        {
                            Console.WriteLine(user[i, 0] + " : " + user[i, 1]);
                        }
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        dat[i] = user[i, 1];
                    }

                    // Ввод новых данных (после статуса (фио, зп, должность и т.д.))
                    Console.WriteLine("\r\n");
                    for (int i = 4; i < 10; i++)
                    {
                        bool access = true;
                        while (access)
                        {
                            Console.Write("Введите новый " + user[i, 0] + ": ");
                            dat[i] = Console.ReadLine();
                            if (Data.CheckString(dat[i]))
                            {
                                if (user[i, 0] == "pay")
                                {
                                    try
                                    {
                                        double n = Convert.ToDouble(dat[i]);
                                        access = false;
                                    }
                                    catch
                                    {
                                        Console.WriteLine("ЗП должно быть дробным числом (5.00, 23.50 ...)");
                                    }
                                }
                                else
                                {
                                    access = false;
                                }
                            }
                        }
                    }
                    Console.Write("Перепроверьте данные, для подтверждения операции введите y (eng) , для отмены любой другой символ: ");
                    string s = Console.ReadLine();
                    if (s == "y")
                    {
                        Admin.UpdateUser(dat[0], dat[1], dat[2], dat[3], dat[4], dat[5], dat[6], dat[7], dat[8], dat[9]);
                    }
                    else
                    {
                        Console.WriteLine("Операция отменена");
                    }
                }
            }
            else
            {
                Console.WriteLine("Пользователь не найден");
            }
        }
    }
    class Warehouse
    {
        /// <summary>
        /// Вывод списка товара
        /// </summary>
        public static void GetProducts()
        {
            string[] files = Directory.GetFiles(@"Data\Products\");
            int n = 0;
            var table = new ConsoleTable("Магазин", "Склад", "Категория", "Товар", "Кол-во", "Годен до", "Цена");
            List<string> categ = new List<string>();
            List < string[][] > dat = new List<string[][]>(); 

            // Прохождение по товарам
            foreach (string filename in files)
            {
                n++;
                string[] finded = Data.Explode("\\", filename);
                finded = Data.Explode(".", finded[finded.Length - 1]);
                string[,] prod = Data.GetProduct(finded[0]);
                string[] prodDat = new string[7];
                string cat = "";
                for (int i = 0; i < 7; i++)
                {
                    prodDat[i] = prod[i, 1];

                    if (prod[i,0] == "category")
                    {
                        cat = prodDat[i];
                        if(!categ.Contains(cat)) // Проверка на найденные категории
                        {
                            categ.Add(cat); // Добавление новой категории
                        }
                    }
                }
                string[][] res = new string[][] { new string[] { n.ToString() }, prodDat };
                dat.Add(res);

            }
            // Проход по категориям (для того что бы была сортировка по категориям при выводе)
            for(int i = 0;i < categ.Count;i++)
            {
                // проход по товарам
                for(int  j = 0; j< dat.Count;j++)
                {
                    string[] str = dat[j][1];
                    if (str[2] == categ[i]) // нужная категория
                    {
                        if (DateTime.Now > DateTime.Parse(str[5])) // проверка на просрочку
                        {
                            str[6] = (Convert.ToDouble(str[6])*0.5).ToString()+" (-50%)";
                        }
                        table.AddRow(str);
                    }
                }
            }
            table.Write();
        }
        /// <summary>
        /// Изменение товара (один параметр - waregouse, category, count)
        /// </summary>
        public static void UpdateProductByParam(string type, string product)
        {
            if (Data.CheckExistFile(product, "Products"))
            {
                string[,] prod = Data.GetProduct(product);
                string[] dat = new string[7];

                Console.WriteLine(new string('_', 50));
                Console.WriteLine("Текущие данные:\r\n");

                for (int i = 0; i < dat.Length; i++)
                {
                    Console.WriteLine(prod[i, 0] + " : " + prod[i, 1]);
                }
                Console.WriteLine("\r\n");
                for (int i = 0; i < dat.Length; i++)
                {
                    // Проверка параметра на нужный
                    if(prod[i,0] == type)
                    {
                        // Ввод нового занчения до соответствия требованиям
                        bool access = true;
                        while (access)
                        {
                            Console.Write("Введите новый " + prod[i, 0] + ": ");
                            dat[i] = Console.ReadLine();
                            if (Data.CheckString(dat[i]))
                            {
                                if (prod[i, 0] == "count")
                                {
                                    try
                                    {
                                        int n = Convert.ToInt32(dat[i]);
                                        access = false;
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Количество должно быть целочисленным");
                                    }
                                }
                                else if (prod[i, 0] == "ready")
                                {
                                    DateTime dt;
                                    bool parse = DateTime.TryParse(dat[i], out dt);
                                    if (parse)
                                    {
                                        access = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Срок годности должен быть в виде даты (25.12.2020)");
                                    }
                                }
                                else
                                {
                                    access = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        dat[i] = prod[i, 1];
                    }
                }
                Console.Write("Перепроверьте данные, для подтверждения операции введите y (eng) , для отмены любой другой символ: ");
                string s = Console.ReadLine();
                if (s == "y")
                {
                    Admin.UpdateProduct(dat[0], dat[1], dat[2], dat[3], dat[4], dat[5], dat[6]); // Перезапись товара
                }
                else
                {
                    Console.WriteLine("Операция отменена");
                }
            }
            else
            {
                Console.WriteLine("Товар не найден");
            }
        }
    }
    class Cassa
    {
        /// <summary>
        /// Вывод списка корзин
        /// </summary>
        public static void GetChartList()
        {
            string[] files = Directory.GetFiles(@"Data\Charts\");
            int n = 0;
            var table = new ConsoleTable("Пользователь");

            foreach (string filename in files)
            {
                n++;
                string[] finded = Data.Explode("\\", filename);
                finded = Data.Explode(".", finded[finded.Length - 1]); // убираем .dat
                table.AddRow(finded[0]);
            }
            table.Write();
        }
        /// <summary>
        /// Оформление заказа
        /// </summary>
        public static void CompleteOrder(string order)
        {
            // Проверка на наличие
            if (Data.CheckExistFile(order, "Charts"))
            {
                string[][] data = Data.GetChart(order);
                string[] price = new string[data.Length-1];
                string[] price_one = new string[data.Length - 1]; // цена одного товара
                bool access = true;

                // Проход по корзине
                for(int i = 0; i < data.Length-1;i++)
                {
                    string[,] prod = Data.GetProduct(data[i][0]);

                    // Проход по товару
                    for(int j = 0; j < prod.Length/2-1;j++)
                    {
                        if(prod[j,0] == "count") // Проверка на доступность количества товара
                        {
                            if(Convert.ToInt32(prod[j,1]) < Convert.ToInt32(data[i][1]))
                            {
                                Console.WriteLine("Недостаточно "+(Convert.ToInt32(data[i][1]) - Convert.ToInt32(prod[j, 1])) +" ед. продукции "+data[i][0]);
                                access = false;
                            }
                        }
                        else if (prod[j, 0] == "price") // Проверка цены на скидки
                        {
                            price[i] = (Convert.ToDouble(prod[j, 1]) * Convert.ToInt32(data[i][1])).ToString();
                            price_one[i] = prod[j, 1];
                            if (DateTime.Now > DateTime.Parse(prod[5, 1]))
                            {
                                price[i] = (Convert.ToDouble(price[i]) * 0.5).ToString();
                                price_one[i] = (Convert.ToDouble(price[i]) * 0.5).ToString()+ " (-50%)";
                            }
                        }
                    }

                }

                // Если всех нужных товаров достаточно
                if(access)
                {
                    string date = "";
                    string time = DateTime.Now.ToString();

                    // Текущая дата в формате 12-12-2020_12-12-12 (д-м-г_ч-м-с для уникализации)
                    for (int i = 0; i < time.Length;i++)
                    {
                        if(time[i] == ' ') { date += "_"; }
                        else if (time[i] == '.') { date += "-"; }
                        else if (time[i] == ':') { date += "-"; }
                        else { date += time[i]; }
                    }

                    string[,] user = Data.GetUser(order); // получение пользователя

                    // текст эмейла (рекомендую исп. английский)
                    string text = "Thanks. Your order:<br>";

                    for (int i = 0; i < data.Length - 1; i++)
                    {
                        text += "<hr>";
                        text += "Product - " + data[i][0] + "<br>";
                        text += "Count - " + data[i][1] + "<br>";
                        text += "Summary cost - " + price[i] + "<br>";
                        text += "Cost per unit - " + price_one[i] + "<br>";
                    }

                    text += "<hr>";
                    text += "<br>Date - " + DateTime.Now + "<br>";

                    // если сообщение успешно отправилось, то сохраняем файл завершенного заказа
                    if (Data.SendMail(text, user[2, 1]))
                    {
                        string writePath = @"Data\Orders\" + date + ".dat";
                        using (StreamWriter sw = new StreamWriter(writePath))
                        {
                            for (int i = 0; i < data.Length - 1; i++)
                            {
                                sw.WriteLine("user[" + order + "]");
                                sw.WriteLine("product[" + data[i][0] + "]");
                                sw.WriteLine("count[" + data[i][1] + "]");
                                sw.WriteLine("price[" + price[i] + "]");
                                sw.WriteLine("price_one[" + price_one[i] + "]");
                                sw.WriteLine("date[" + date + "];");
                            }
                            sw.Close();
                        }

                        // Минусование количества товара который был в корзине
                        for (int j = 0; j < data.Length-1; j++)
                        {
                            string[,] prod = Data.GetProduct(data[j][0]);
                            string[] dat = new string[7];

                            for (int i = 0; i < dat.Length; i++)
                            {
                                if (prod[i, 0] == "count")
                                {
                                    dat[i] = (Convert.ToInt32(prod[i, 1]) - Convert.ToInt32(data[j][1])).ToString();
                                }
                                else
                                {
                                    dat[i] = prod[i, 1];
                                }
                            }
                            Admin.UpdateProduct(dat[0], dat[1], dat[2], dat[3], dat[4], dat[5], dat[6]); // перезапись товара
                        }
                        File.Delete("Data\\Charts\\" + order + ".dat"); // удаление файла корзины
                        Console.WriteLine("Заказ оформлен");
                    }
                    else
                    {
                        Console.WriteLine("Не получилось отправить электронное сообщение");
                    }
                }
            }
            else
            {
                Console.WriteLine("Заказ не найден");
            }
        }
    }
    class Finance
    {
        /// <summary>
        /// Вывести информации по заказам за период
        /// </summary>
        public static void GetOrderByPeriod(string period)
        {
            Console.Clear();
            List<string> data = Data.GetFilesByPeriod(period, "Orders"); // получение файлов, соответсвующих периоду
            if(data.Count > 0)
            {
                double res = 0;
                // отрисовка таблицы
                var table = new ConsoleTable("Пользователь", "Товар", "Количество", "Цена", "Дата");

                // проход по заказам
                for (int j = 0;j < data.Count; j++)
                {
                    string[][] dat = Data.GetOrder(data[j]); // получение заказа

                    // проход по товарам в заказе
                    for (int i = 0; i < dat.Length - 1; i++)
                    {
                        table.AddRow(new string[] { dat[i][0], dat[i][1], dat[i][2], dat[i][3], dat[i][4] });
                        res += Convert.ToDouble(dat[i][3]);
                    }
                }
                table.Write();
                Console.WriteLine("Общий доход: "+res);
            }
            else
            {
                Console.WriteLine("Записей за период не найдено");
            }
        }
        /// <summary>
        /// просмотреть информацию по заказу
        /// </summary>
        public static void CheckOrder(string order)
        {
            if (Data.CheckExistFile(order, "Orders"))
            {
                string[][] data = Data.GetOrder(order); // получение заказа

                var table = new ConsoleTable("Пользователь", "Товар","Количество","Цена","Дата");

                // проход по товарам в заказе, вырисовка их
                for (int i = 0; i < data.Length - 1; i++)
                {
                    table.AddRow(new string[] { data[i][0], data[i][1], data[i][2], data[i][3], data[i][4] });
                }

                table.Write();
            }
            else
            {
                Console.WriteLine("Заказ не найден");
            }
        }
        /// <summary>
        /// Вывод списка заказов
        /// </summary>
        public static void GetOrderList()
        {
            string[] files = Directory.GetFiles(@"Data\Orders\");
            int n = 0;
            var table = new ConsoleTable("Заказ");

            // Отрисовка списка заказов
            foreach (string filename in files)
            {
                n++;
                string[] finded = Data.Explode("\\", filename);
                finded = Data.Explode(".", finded[finded.Length - 1]);
                table.AddRow(finded[0]);
            }
            table.Write();
        }
        /// <summary>
        /// Произвести выплаты зп
        /// </summary>
        public static void SendPayments()
        {
            string[] files = Directory.GetFiles(@"Data\Users\");

            string date = "";
            string[] Ntime = Data.Explode(" ", DateTime.Now.ToString());
            string time = Ntime[0];

            // Изменение сегодняшней даты в нужный формат
            for (int i = 0; i < time.Length; i++)
            {
                if (time[i] == '.') { date += "-"; }
                else { date += time[i]; }
            }

            // Проверка на наличие выдачи зп за текущий месяц
            if(!Data.CheckExistFile(date,"Payments"))
            {
                string writePath = @"Data\Payments\" + date + ".dat"; // создание нового файла зп
                using (StreamWriter sw = new StreamWriter(writePath))
                {
                    // проход по папке пользователей
                    foreach (string filename in files)
                    {
                        string[] finded = Data.Explode("\\", filename);
                        finded = Data.Explode(".", finded[finded.Length - 1]);
                        string[,] user = Data.GetUser(finded[0]); // поулчение данных пользователя
                        string[] userDat = new string[10];
                        if (user[9, 1] != "") // если зп не пустое
                        {
                            if (Data.CheckInt(user[9,1])) // если зп числовое, то начислить зп
                            {
                                sw.WriteLine("user["+ user[0, 1] + "]");
                                sw.WriteLine("payment[" + user[9, 1] + "];");
                            }
                        }
                    }
                    sw.Close();
                }
                Console.WriteLine("Зарплаты выплачены");
            }
            else
            {
                Console.WriteLine("В этом месяце зарпалы уже выплачивались");
            }
        }
        /// <summary>
        /// Вывод списка выплаченной зп за период
        /// </summary>
        public static void GetPaymentsByPeriod(string period)
        {
            Console.Clear();
            List<string> data = Data.GetFilesByPeriod(period, "Payments");
            if (data.Count > 0) //  если найдены файлы за необходимый период
            {
                double res = 0;
                var table = new ConsoleTable("Пользователь", "Зарплата","Дата");

                // проход по файлам зп
                for (int j = 0; j < data.Count; j++)
                {
                    string[][] dat = Data.GetPayment(data[j]);

                    // проход по сотрудникам, получившим зп, отрисовка
                    for (int i = 0; i < dat.Length - 1; i++)
                    {
                        table.AddRow(new string[] { dat[i][0], dat[i][1], data[j] });
                        res += Convert.ToDouble(dat[i][1]);
                    }
                }
                table.Write();
                Console.WriteLine("Всего выплачено: " + res);
            }
            else
            {
                Console.WriteLine("Записей за период не найдено");
            }
        }
        /// <summary>
        /// Вывод бюджета компании за период
        /// </summary>
        public static void GetBudgetByPeriod(string period)
        {
            Console.Clear();

            double outcome = 0;
            double income = 0;

            Console.WriteLine("Продажи за период: \r\n"+new string('_',50)+"\r\n");

            // Проход по заказам за период (так же как и в GetOrderByPeriod , только помимо отрисовки считается общий доход)
            List<string> data = Data.GetFilesByPeriod(period, "Orders");
            if (data.Count > 0)
            {
                var table = new ConsoleTable("Пользователь", "Товар", "Количество", "Цена", "Дата");

                for (int j = 0; j < data.Count; j++)
                {
                    string[][] dat = Data.GetOrder(data[j]);

                    for (int i = 0; i < dat.Length - 1; i++)
                    {
                        table.AddRow(new string[] { dat[i][0], dat[i][1], dat[i][2], dat[i][3], dat[i][4] });
                        income += Convert.ToDouble(dat[i][3]);
                    }
                }
                table.Write();
                income = income * 0.9398;
                Console.WriteLine("Общий доход учетом налогов: " + income);
            }
            else
            {
                Console.WriteLine("Записей за период не найдено");
            }

            data.Clear();

            Console.WriteLine("\r\n\r\nВыплаты ЗП за период: \r\n" + new string('_', 50)+"\r\n");

            // Проход по зп за период (так же как и в GetPaymentsByPeriod , только помимо отрисовки считается общий расход)
            data = Data.GetFilesByPeriod(period, "Payments");
            if (data.Count > 0)
            {
                var table = new ConsoleTable("Пользователь", "Зарплата", "Дата");

                for (int j = 0; j < data.Count; j++)
                {
                    string[][] dat = Data.GetPayment(data[j]);

                    for (int i = 0; i < dat.Length - 1; i++)
                    {
                        table.AddRow(new string[] { dat[i][0], dat[i][1], data[j] });
                        outcome += Convert.ToDouble(dat[i][1]);
                    }
                }
                table.Write();
                Console.WriteLine("Всего выплачено: " + outcome);
            }
            else
            {
                Console.WriteLine("Записей за период не найдено");
            }
            // вывод баланса (доход - расход) (учитывая налоги)
            Console.WriteLine("Состояние бюджета за период: "+(income-outcome));
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // базовые переменные
            string login = "";
            string status = "";
            string statusName = "";
            bool work = true;
            bool log_in = true;

            Console.WriteLine("Здравствуйте, Введите log для авторизации, reg для регистрации: ");

            // авторизация, регистрация, цикл бесконечен пока не произойдет авторизация или регистрация
            while (log_in)
            {
                string res = Console.ReadLine();
                // авторизация
                if (res == "log")
                {
                    Console.Write("Введите логин и пароль через пробел: ");
                    string[] dat = Data.Explode(" ", Console.ReadLine());

                    // проверка на наличие логина и пароля
                    if(dat.Length >= 2)
                    {
                        // проверка на наличие логина в списке пользоателей
                        if (Data.CheckExistFile(dat[0], "Users"))
                        {
                            string[,] Finduser = Data.GetUser(dat[0]); // получение пользователя
                            if (dat[0] == Finduser[0, 1]) // проверка логина
                            {
                                if (dat[1] == Finduser[1, 1]) // проверка пароля
                                {
                                    log_in = false;
                                    login = Finduser[0, 1];
                                    status = Finduser[3, 1];
                                    switch (Finduser[3, 1]) // присвоение статуса (исходя из данных пользователя)
                                    {
                                        case "buyer": statusName = "покупатель"; break;
                                        case "admin": statusName = "администратор"; break;
                                        case "hr": statusName = "HR"; break;
                                        case "warehouse": statusName = "кладовщик"; break;
                                        case "cassa": statusName = "кассир-продавец"; break;
                                        case "finance": statusName = "бухгалтер"; break;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Пароль не верный");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Пользователь не найден");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Пользователь не найден");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Не все параметры переданы") ;
                    }
                }
                else if(res == "reg") // регистрация
                {
                    Console.Write("Введите логин, пароль и email через пробел: ");
                    string[] dat = Data.Explode(" ", Console.ReadLine());
                    if(dat.Length >= 3) // проверка на логин, пароль и почту (наличие)
                    {
                        if(Data.CheckString(dat[0]) && Data.CheckString(dat[1]) && Data.CheckString(dat[2])) // проверка на недопустимые символы
                        {
                            if (!Data.CheckExistFile(dat[0], "Users")) // проверка на занятость логина
                            {
                                if(Data.CheckPass(dat[1])) // проверка пароля на необходимые компоненты
                                {
                                    if(Data.CheckMail(dat[2])) // проверка почты на собаку
                                    {
                                        Admin.UpdateUser(dat[0], dat[1], dat[2], "buyer"); // создание пользователя
                                        login = dat[0];
                                        status = "buyer";
                                        statusName = "покупатель";
                                        log_in = false;
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Логин занят");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Не все параметры указаны");
                    }
                }
                else
                {
                    Console.WriteLine("Команда не опознана");
                }
            }

            Console.Clear();
            Console.WriteLine("Здравствуйте " + statusName + " " + login);

            // цикл бесконечный пока работает программа
            while (work)
            {
                string comand = "";
                Console.WriteLine("\r\nВведите команду. Узнать список доступных команд можно введя команду help.");
                comand = Console.ReadLine();

                // введенная команда
                string[] dat = Data.Explode(" ", comand);

                /*
                    Список команд
                    В них действует след структура:
                    1) Проверка на соответствие статусу
                    2) Проверка на наличие требуемых параметров (если они прдусмотрены)
                    3) Запуск необходимого метода
                */
                if (dat[0] == "help")
                {
                    Console.WriteLine("Команды указанны в следующем порядке - Команда Аргумент1 Аргумент2 АргументN - описание");
                    Console.WriteLine("\r\nСписок общих команд\r\n" + new string('_', 50));
                    var table = new ConsoleTable("Команда", "Параметры", "Описание");

                    table.AddRow(new string[] { "help", "", "Вывести список доступных команд" });
                    table.AddRow(new string[] { "exit", "", "Выход" });
                    table.AddRow(new string[] { "clear", "", "Очистить консоль" });

                    table.Write();

                    if (status == "admin")
                    {
                        string[] usersN = new string[] { "buyer", "admin",  "hr", "warehouse", "cassa", "finance" };
                        for(int i = 0; i < usersN.Length;i++)
                        {
                            Data.GetHelp(usersN[i]);
                        }
                    }
                    else
                    {
                        Data.GetHelp(status);
                    }
                }
                else if(dat[0] == "exit")
                {
                    work = false;
                }
                else if(dat[0] == "clear")
                {
                    Console.Clear();
                }
                else if(dat[0] == "delete_user")
                {
                    if (dat.Length >= 2)
                    {
                        if (status == "admin")
                        {
                            Admin.DeleteUser(dat[1]);
                        }
                        else
                        {
                            Console.WriteLine("Доступ заблокирован");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Не все параметры указаны");
                    }
                }
                else if(dat[0] == "change_user")
                {
                    if (dat.Length >= 2)
                    {
                        if (status == "admin")
                        {
                            Admin.ChangeUser(dat[1]);
                        }
                        else
                        {
                            Console.WriteLine("Доступ заблокирован");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Не все параметры указаны");
                    }
                }
                else if(dat[0] == "add_user")
                {
                    if (status == "admin")
                    {
                        Admin.AddUser();
                    }
                    else
                    {
                        Console.WriteLine("Доступ заблокирован");
                    }
                }
                else if(dat[0] == "add_product")
                {
                    if (status == "admin")
                    {
                        Admin.AddProduct();
                    }
                    else
                    {
                        Console.WriteLine("Доступ заблокирован");
                    }
                }
                else if(dat[0] == "change_product")
                {
                    if (dat.Length >= 2)
                    {
                        if (status == "admin")
                        {
                            Admin.ChangeProduct(dat[1]);
                        }
                        else
                        {
                            Console.WriteLine("Доступ заблокирован");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Не все параметры указаны");
                    }
                }
                else if(dat[0] == "delete_product")
                {
                    if(dat.Length >= 2)
                    {
                        if (status == "admin")
                        {
                            Admin.DeleteProduct(dat[1]);
                        }
                        else
                        {
                            Console.WriteLine("Доступ заблокирован");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Не все параметры указаны");
                    }
                }
                else if(dat[0] == "get_products")
                {
                    if (status == "admin" || status == "warehouse" || status == "buyer")
                    {
                        Warehouse.GetProducts();
                    }
                    else
                    {
                        Console.WriteLine("Доступ заблокирован");
                    }
                }
                else if(dat[0] == "update_product_count")
                {
                    if (dat.Length >= 2)
                    {
                        if (status == "admin" || status == "warehouse")
                        {
                            Warehouse.UpdateProductByParam("count", dat[1]);
                        }
                        else
                        {
                            Console.WriteLine("Доступ заблокирован");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Не все параметры указаны");
                    }
                }
                else if(dat[0] == "unready_product")
                {
                    if (dat.Length >= 2)
                    {
                        if (status == "admin" || status == "warehouse")
                        {
                            Warehouse.UpdateProductByParam("ready",dat[1]);
                        }
                        else
                        {
                            Console.WriteLine("Доступ заблокирован");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Не все параметры указаны");
                    }
                }
                else if(dat[0] == "transfer_product")
                {
                    if (dat.Length >= 2)
                    {
                        if (status == "admin" || status == "warehouse")
                        {
                            Warehouse.UpdateProductByParam("warehouse", dat[1]);
                        }
                        else
                        {
                            Console.WriteLine("Доступ заблокирован");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Не все параметры указаны");
                    }
                }
                else if(dat[0] == "get_users")
                {
                    if (status == "admin" || status == "hr")
                    {
                        HR.GetUsers(status);
                    }
                    else
                    {
                        Console.WriteLine("Доступ заблокирован");
                    }
                }
                else if(dat[0] == "add_worker")
                {
                    if (dat.Length >= 2)
                    {
                        if (status == "admin" || status == "hr")
                        {
                            HR.ChangeUserByType(dat[1],"add");
                        }
                        else
                        {
                            Console.WriteLine("Доступ заблокирован");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Не все параметры указаны");
                    }
                }
                else if(dat[0] == "delete_worker")
                {
                    if (dat.Length >= 2)
                    {
                        if (status == "admin" || status == "hr")
                        {
                            HR.ChangeUserByType(dat[1], "delete");
                        }
                        else
                        {
                            Console.WriteLine("Доступ заблокирован");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Не все параметры указаны");
                    }
                }
                else if(dat[0] == "change_worker")
                {
                    if (dat.Length >= 2)
                    {
                        if (status == "admin" || status == "hr")
                        {
                            HR.ChangeUserByType(dat[1], "change");
                        }
                        else
                        {
                            Console.WriteLine("Доступ заблокирован");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Не все параметры указаны");
                    }
                }
                else if(dat[0] == "check_chart")
                {
                    if (status == "admin" || status == "cassa")
                    {
                        if (dat.Length >= 2)
                        {
                            Buyer.CheckChart(dat[1]);
                        }
                        else
                        {
                            Console.WriteLine("Не все параметры указаны");
                        }
                    }
                    else if(status == "buyer")
                    {
                        Buyer.CheckChart(login);
                    }
                    else
                    {
                        Console.WriteLine("Доступ заблокирован");
                    }
                }
                else if(dat[0] == "add_chart")
                {
                    if (status == "admin" || status == "buyer")
                    {
                        if (dat.Length >= 3)
                        {
                            if(status == "admin")
                            {
                                if (dat.Length >= 4)
                                {
                                    Buyer.AddChart(dat[3], dat[1], dat[2]);
                                }
                                else
                                {
                                    Console.WriteLine("Не указан логин");
                                }
                            }
                            else if(status == "buyer")
                            {
                                Buyer.AddChart(login, dat[1], dat[2]);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Не все параметры указаны");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Доступ заблокирован");
                    }
                }
                else if(dat[0] == "change_chart")
                {
                    if (status == "admin" || status == "buyer")
                    {
                        if (dat.Length >= 3)
                        {
                            if (status == "admin")
                            {
                                if (dat.Length >= 4)
                                {
                                    Buyer.ChangeChart(dat[3], dat[1], dat[2]);
                                }
                                else
                                {
                                    Console.WriteLine("Не указан логин");
                                }
                            }
                            else if (status == "buyer")
                            {
                                Buyer.ChangeChart(login, dat[1], dat[2]);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Не все параметры указаны");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Доступ заблокирован");
                    }
                }
                else if(dat[0] == "delete_chart")
                {
                    if (status == "admin" || status == "buyer")
                    {
                        if (dat.Length >= 2)
                        {
                            if (status == "admin")
                            {
                                if (dat.Length >= 3)
                                {
                                    Buyer.DeleteChart(dat[2], dat[1]);
                                }
                                else
                                {
                                    Console.WriteLine("Не указан логин");
                                }
                            }
                            else if (status == "buyer")
                            {
                                Buyer.DeleteChart(login, dat[1]);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Не все параметры указаны");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Доступ заблокирован");
                    }
                }
                else if(dat[0] == "get_chart_list")
                {
                    if (status == "admin" || status == "cassa")
                    {
                        Cassa.GetChartList();
                    }
                    else
                    {
                        Console.WriteLine("Доступ заблокирован");
                    }
                }
                else if(dat[0] == "complete_order")
                {
                    if (status == "admin" || status == "cassa")
                    {
                        if (dat.Length >= 2)
                        {
                            Cassa.CompleteOrder(dat[1]);
                        }
                        else
                        {
                            Console.WriteLine("Не все параметры указаны");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Доступ заблокирован");
                    }
                }
                else if(dat[0] == "get_order_list")
                {
                    if (status == "admin" || status == "finance")
                    {
                        Finance.GetOrderList();
                    }
                    else
                    {
                        Console.WriteLine("Доступ заблокирован");
                    }
                }
                else if(dat[0] == "get_order")
                {
                    if (status == "admin" || status == "finance")
                    {
                        if (dat.Length >= 2)
                        {
                            Finance.GetOrderByPeriod(dat[1]);
                        }
                        else
                        {
                            Console.WriteLine("Не указан период");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Доступ заблокирован");
                    }
                }
                else if(dat[0] == "check_order")
                {
                    if (status == "admin" || status == "finance")
                    {
                        if (dat.Length >= 2)
                        {
                            Finance.CheckOrder(dat[1]);
                        }
                        else
                        {
                            Console.WriteLine("Не все параметры указаны");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Доступ заблокирован");
                    }
                }
                else if(dat[0] == "send_payment")
                {
                    if (status == "admin" || status == "finance")
                    {
                        Finance.SendPayments();
                    }
                    else
                    {
                        Console.WriteLine("Доступ заблокирован");
                    }
                }
                else if(dat[0] == "get_payment")
                {
                    if (status == "admin" || status == "finance")
                    {
                        if (dat.Length >= 2)
                        {
                            Finance.GetPaymentsByPeriod(dat[1]);
                        }
                        else
                        {
                            Console.WriteLine("Не указан период");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Доступ заблокирован");
                    }
                }
                else if(dat[0] == "get_budget")
                {
                    if (status == "admin" || status == "finance")
                    {
                        if (dat.Length >= 2)
                        {
                            Finance.GetBudgetByPeriod(dat[1]);
                        }
                        else
                        {
                            Console.WriteLine("Не указан период");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Доступ заблокирован");
                    }
                }
                else
                {
                    Console.WriteLine("Команда не распознана");
                }
            }
        }
    }
}
