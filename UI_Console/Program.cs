using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace UI_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UI(true);
        }

        static List<string> TypeOfFuel = new List<string>();
        static bool state;

        static void UI(bool connected)
        {
            Console.Title = "Диагностика и управление заправочными терминалами";
            if (connected) 
            {
                string userInput = "";
                ConsoleWriteLine(ConsoleColor.Green, "Подключение успешно завершено!", true);
                RandomFuel();
                RandomState();

                ConsoleWrite(ConsoleColor.Green, "Для завершения работы введите команду ", true);
                ConsoleWrite(ConsoleColor.Blue, "exit", false);
                ConsoleWrite(ConsoleColor.Green, ", для получении информации о доступных командах введите ", false);
                ConsoleWrite(ConsoleColor.Blue, "help\n", false);
                userInput = Trimmer(ConsoleReadLine());

                while (!userInput.Equals("exit"))
                {
                    switch (userInput)
                    {
                        case "help":
                            HelpCommand();
                            break;
                        case "help launch":
                            LaunchHelp();
                            break;
                        case "help change":
                            ChangeHelp();
                            break;
                        case "help state":
                            StateHelp();
                            break;
                        case "launch":
                            Launch();
                            break;
                        case "change":
                            Change();
                            break;
                        case "change add 92":
                            ChangeAdd("92 бензин");
                            break;
                        case "change add 95":
                            ChangeAdd("95 бензин");
                            break;
                        case "change add 100":
                            ChangeAdd("100 бензин");
                            break;
                        case "change add dizel":
                            ChangeAdd("Дизельное топливо");
                            break;
                        case "change delete 92":
                            ChangeDelete("92 бензин");
                            break;
                        case "change delete 95":
                            ChangeDelete("95 бензин");
                            break;
                        case "change delete 100":
                            ChangeDelete("100 бензин");
                            break;
                        case "change delete dizel":
                            ChangeDelete("Дизельное топливо");
                            break;
                        case "state":
                            State();
                            break;
                        case "state on":
                            StateOn();
                            break;
                        case "state off":
                            StateOff();
                            break;
                        default:
                            Error(userInput);
                            break;
                    }
                    userInput = Trimmer(ConsoleReadLine());
                }
            } 
            else 
            {
                ConsoleWriteLine(ConsoleColor.Red, "Ошибка подключения!", true);
                ConsoleWriteLine(ConsoleColor.White, "Нажмите любую кнопку для закрытия...", true);
                Console.ReadKey();
            }
        }

        static string Trimmer(string userInput)
        {
            string result = Regex.Replace(userInput, @"\s{2,}", " ");
            return result.TrimEnd().TrimStart();
        }

        public static void Error(string text)
        {
            ConsoleWrite(ConsoleColor.Red, "Ошибка! ", true);
            ConsoleWrite(ConsoleColor.Green, "Команды '", false);
            ConsoleWrite(ConsoleColor.Red, text, false);
            ConsoleWrite(ConsoleColor.Green, "' не существует! ", false);

            ConsoleWrite(ConsoleColor.Green, "Для получении информации о доступных командах введите ", false);
            ConsoleWrite(ConsoleColor.Blue, "help\n", false);
        }

        public static void StateOn()
        {
            if (!state)
            {
                state = true;
                ConsoleWriteLine(ConsoleColor.Green, "Заправочный терминал был успешно подключен к сети!", true);
            }
            else ConsoleWriteLine(ConsoleColor.Red, "Ошибка! Заправочный терминал уже подключен к сети!", true);
        }

        public static void StateOff()
        {
            if (state)
            {
                state = false;
                ConsoleWriteLine(ConsoleColor.Green, "Заправочный терминал был успешно отключен от сети!", true);
            }
            else ConsoleWriteLine(ConsoleColor.Red, "Ошибка! Заправочный терминал уже отключен от сети!", true);
        }

        public static void State()
        {
            if (state)
            {
                ConsoleWrite(ConsoleColor.Green, "Заправочный терминал подключен к сети ", true);
                ConsoleWrite(ConsoleColor.Blue, "(online)\n", false);
            }
            else
            {
                ConsoleWrite(ConsoleColor.Green, "Заправочный терминал отключен от сети ", true);
                ConsoleWrite(ConsoleColor.Red, "(offline)\n", false);
            }
        }

        public static void RandomState()
        {
            Random rnd = new Random();
            if (rnd.Next(0, 2) == 1) state = true;
            else state = false;
        }

        public static void ChangeDelete(string typeOfFuel)
        {
            foreach (var fuel in TypeOfFuel)
            {
                if (fuel.Equals(typeOfFuel))
                {
                    TypeOfFuel.Remove(typeOfFuel);
                    ConsoleWriteLine(ConsoleColor.Green, "Вид топлива успешно удален!", true);
                    return;
                }
            }
            ConsoleWriteLine(ConsoleColor.Red, "Данного вида топлива нет на заправочном терминале!", true);
        }

        public static void ChangeAdd(string typeOfFuel)
        {
            foreach (var fuel in TypeOfFuel)
            {
                if (fuel.Equals(typeOfFuel))
                {
                    ConsoleWriteLine(ConsoleColor.Red, "Такой вид топлива уже есть на заправочном терминале!", true);
                    return;
                }
            }
            TypeOfFuel.Add(typeOfFuel); 
            ConsoleWriteLine(ConsoleColor.Green, "Новый вид топлива успешно добавлен!", true);
        }

        public static void Change()
        {
            ConsoleWrite(ConsoleColor.Green, "Доступные виды топлива на заправочном терминале: ", true);
            foreach (var fuel in TypeOfFuel)
            {
                ConsoleWrite(ConsoleColor.Red, fuel + " ", false);
            }
            ConsoleWrite(ConsoleColor.Green, "\n", false);
        }

        static void Launch()
        {
            Random rnd = new Random();
            int percents = 0;
            string text = "";

            ConsoleWriteLine(ConsoleColor.Green, "Запуск диагностики обородувания...", true);

            Console.CursorVisible = false;

            //50 квадратов
            while (percents != 100)
            {
                percents++;

                if (percents % 2 == 0) text = text + "■";

                Thread.Sleep(rnd.Next(25, 200));

                ConsoleWrite(ConsoleColor.Green, text, true);
                ConsoleSpace(text, 50);
                ConsoleWrite(ConsoleColor.Green, "  - " + percents.ToString() + "% \n", false);

                Console.SetCursorPosition(0, Console.CursorTop - 1);
            }
            Console.SetCursorPosition(0, Console.CursorTop + 1);

            ConsoleWriteLine(ConsoleColor.Green, "Ошибок не выявлено!", true);

            Console.CursorVisible = true;
        }

        static void RandomFuel()
        {
            Random rnd = new Random();
            string[] Fuels = { "92 бензин", "95 бензин", "100 бензин", "Дизельное топливо" };
            int count = rnd.Next(1, Fuels.Length + 1);

            while (TypeOfFuel.Count < count)
            {
                int randomFuelIndex = rnd.Next(Fuels.Length);
                string randomFuel = Fuels[randomFuelIndex];

                if (!string.IsNullOrEmpty(randomFuel))
                {
                    TypeOfFuel.Add(randomFuel);
                    Fuels[randomFuelIndex] = "";
                }
            }
        }

        static void StateHelp()
        {
            ConsoleWrite(ConsoleColor.Green, "Команда ", true);
            ConsoleWrite(ConsoleColor.Blue, "state ", false);
            ConsoleWrite(ConsoleColor.Green, "используется для включения/выключения заправочного терминала\n", false);
            ConsoleWriteLine(ConsoleColor.Green, "", false);

            ConsoleWrite(ConsoleColor.Green, "  Для того, чтобы посмотреть текущее состояния заправочного терминала " +
                                             "введите ", false);
            ConsoleWrite(ConsoleColor.Blue, "state\n", false);

            ConsoleWrite(ConsoleColor.Green, "  Для того, чтобы включить заправочный терминал введите ", false);
            ConsoleWrite(ConsoleColor.Blue, "state on\n", false);

            ConsoleWrite(ConsoleColor.Green, "  Для того, чтобы выключить заправочный терминал введите ", false);
            ConsoleWrite(ConsoleColor.Blue, "state off\n", false);
        }

        static void ChangeHelp()
        {
            ConsoleWrite(ConsoleColor.Green, "Команда ", true);
            ConsoleWrite(ConsoleColor.Blue, "change ", false);
            ConsoleWrite(ConsoleColor.Green, "используется для изменения параметров оборудования\n", false);
            ConsoleWriteLine(ConsoleColor.Green, "", false);

            ConsoleWrite(ConsoleColor.Green, "  Для того, чтобы вывести виды топлива доступные на " +
                                                 "заправочном терминале введите ", false);
            ConsoleWrite(ConsoleColor.Blue, "change\n", false);

            ConsoleWrite(ConsoleColor.Green, "  Для того, чтобы добавить новый вид топлива введите ", false);
            ConsoleWrite(ConsoleColor.Blue, "change add <вид топлива> \n", false);

            ConsoleWrite(ConsoleColor.Green, "  Для того, чтобы убрать вид топлива введите ", false);
            ConsoleWrite(ConsoleColor.Blue, "change delete <вид топлива> \n", false);

            ConsoleWriteLine(ConsoleColor.Green, "", false);
            ConsoleWrite(ConsoleColor.Green, "  Виды топлива: ", false);
            ConsoleWrite(ConsoleColor.Red, "92 95 100 dizel \n", false);
        }

        static void LaunchHelp()
        {
            ConsoleWrite(ConsoleColor.Green, "Команда ", true);
            ConsoleWrite(ConsoleColor.Blue, "launch ", false);
            ConsoleWrite(ConsoleColor.Green, "используется для запуска диагностики оборудования\n", false);
            ConsoleWriteLine(ConsoleColor.Green, "", false);
            ConsoleWriteLine(ConsoleColor.Red, "  Обратите внимание!", false);
            ConsoleWriteLine(ConsoleColor.Green, "  Данный процесс может занять некоторое время, запрещено прерывать " +
                                                 "данный процесс во избежание поломки оборудования.", false);
        }

        static void HelpCommand()
        {
            ConsoleWrite(ConsoleColor.Green, "Для получения сведений об определенной команде " +
                                                 "наберите ", true);
            ConsoleWrite(ConsoleColor.Blue, "help <имя команды>\n", false);
            ConsoleWriteLine(ConsoleColor.Green, "Список доступных команд:", true);

            ConsoleWrite(ConsoleColor.Blue, "launch", true);
            ConsoleSpace("launch", 20);
            ConsoleWrite(ConsoleColor.Green, "Запуск диагностики оборудования\n", false);

            ConsoleWrite(ConsoleColor.Blue, "change", true);
            ConsoleSpace("change", 20);
            ConsoleWrite(ConsoleColor.Green, "Изменить параметры оборудования\n", false);

            ConsoleWrite(ConsoleColor.Blue, "state", true);
            ConsoleSpace("state", 20);
            ConsoleWrite(ConsoleColor.Green, "Включить/выключить оборудование\n", false);
        }

        static void ConsoleSpace(string text, int amount)
        {
            int destinion = amount - text.Length;
            string spaces = "";
            for (int i = 0; i < destinion; i++)
            {
                spaces = spaces + " ";
            }
            ConsoleWrite(ConsoleColor.Green, spaces, false);
        }

        static string ConsoleReadLine()
        {
            Console.Write("- "); 
            return Console.ReadLine().ToLower();

        }

        static void ConsoleWriteLine(ConsoleColor color, string text, bool dashMark)
        {
            if (dashMark) ConsoleWrite(ConsoleColor.Green, "- ", false);
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        static void ConsoleWrite(ConsoleColor color, string text, bool dashMark)
        {
            if (dashMark) ConsoleWrite(ConsoleColor.Green, "- ", false);
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }
    }
}
