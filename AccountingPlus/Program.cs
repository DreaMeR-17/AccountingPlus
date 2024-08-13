using System;
using System.Collections.Generic;

namespace AccountingPlus
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandAddNewAccounting = "1";
            const string CommandShowAllAccounting = "2";
            const string CommandDeleteAccounting = "3";
            const string CommandSearchByName = "4";
            const string CommandExit = "5";

            Dictionary<string, List<string>> dossiers = CreateDossiers();

            string userInput;

            bool isWork = true;

            while (isWork)
            {
                Console.Clear();

                Console.WriteLine("Кадровый Учет.\n\nВыберите команду: ");
                Console.WriteLine($"{CommandAddNewAccounting} Добавить досье.");
                Console.WriteLine($"{CommandShowAllAccounting} Показать все досье.");
                Console.WriteLine($"{CommandDeleteAccounting} Удалить досье.");
                Console.WriteLine($"{CommandSearchByName} Поиск по имени.");
                Console.WriteLine($"{CommandExit} Выход из программы.");

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandAddNewAccounting:
                        AddNewAccounting(dossiers);
                        break;

                    case CommandShowAllAccounting:
                        ShowAllAccounting(dossiers);
                        WriteAnyKey();
                        break;

                    case CommandDeleteAccounting:
                        DeleteAccounting(dossiers);
                        WriteAnyKey();
                        break;

                    case CommandSearchByName:
                        SearchByName(dossiers);
                        break;

                    case CommandExit:
                        Console.Clear();
                        Console.WriteLine("Прощайте!");
                        isWork = false;
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Введена неверная команда.");
                        WriteAnyKey();
                        break;
                }
            }
        }

        static Dictionary<string, List<string>> CreateDossiers()
        {
            Dictionary<string, List<string>> dossier = new Dictionary<string, List<string>>();

            List<string> petNames = new List<string>();
            List<string> drunkerNames = new List<string>();
            List<string> winnerNames = new List<string>();

            string jobPet = "Питомец";
            string jobDrunker = "Пьяница";
            string jobWinner = "Победитель по жизни";

            petNames.Add("Азатот");
            petNames.Add("Цербер");

            drunkerNames.Add("Медведь");
            drunkerNames.Add("Робеспьер");

            winnerNames.Add("Андрей");

            dossier.Add(jobPet, petNames);
            dossier.Add(jobDrunker, drunkerNames);
            dossier.Add(jobWinner, winnerNames);

            return dossier;
        }

        static void AddNewAccounting(Dictionary<string, List<string>> dossiers)
        {
            Console.Clear();

            string newName = ReadNewString(info: "имя");
            string newJob = ReadNewString(info: "должность");

            List<string> tempName = new List<string>();

            Console.WriteLine($"Добавлен новый работник {newName} в должности {newJob}");

            if (dossiers.ContainsKey(newJob) == false)
            {
                dossiers.Add(newJob, new List<string>());
            }
            else
            {
                dossiers[newJob].Add(newName);
            }
        }

        static void ShowAllAccounting(Dictionary<string, List<string>> dossiers)
        {
            Console.Clear();

            Console.WriteLine("Ваши работники(Для дальнейшего вывода нажмите любую клавишу): ");

            string separator = "-";
            string colon = ":";

            foreach (var dossier in dossiers)
            {
                Console.WriteLine($"{dossier.Key}{colon}");

                foreach (var name in dossier.Value)
                {
                    Console.WriteLine($"{separator}{name}");
                }

                Console.ReadKey();
            }
        }

        static void DeleteAccounting(Dictionary<string, List<string>> dossiers)
        {
            ShowAllAccounting(dossiers);

            List<string> value;

            string jobName = ReadJobName(dossiers, out value);
            string fullName = ReadFullName(value);

            value.Remove(fullName);

            if (value.Count == 0)
            {
                dossiers.Remove(jobName);
            }
        }

        static void SearchByName(Dictionary<string, List<string>> dossiers)
        {
            Console.Clear();

            Console.WriteLine("Введите имя: ");
            string userInput = Console.ReadLine();

            bool isFound = false;

            char separator = ' ';

            foreach (KeyValuePair<string, List<string>> item in dossiers)
            {
                string key = item.Key;

                List<string> fullNames = item.Value;

                foreach (var fullName in fullNames)
                {
                    string[] lastName = fullName.Split(separator);

                    if (userInput == lastName[0])
                    {
                        Console.WriteLine($"\nПо вашему запросу найдено:\n" +
                            $"{item.Key} - {fullName}\n");

                        isFound = true;

                        WriteAnyKey();
                    }
                }
            }

            if (isFound == false)
            {
                Console.WriteLine("\nВо вашему запросу ничего не найдено.");

                WriteAnyKey();
            }
        }

        static string ReadNewString(string info)
        {
            Console.WriteLine($"Введите {info}");

            return Console.ReadLine();
        }

        static string ReadFullName(List<string> value)
        {
            string fullNameDelete = "";

            bool isWork = true;
            bool isFound = false;

            while (isWork)
            {
                foreach (string fullName in value)
                {
                    Console.WriteLine($"{fullName}");
                }

                Console.WriteLine("Введите имя работника:");
                string userInput = Console.ReadLine();
               
                if (value.Contains(userInput))
                {
                    fullNameDelete = userInput;
                    isFound = true;
                }

                if (isFound)
                {
                    isWork = false;
                }
                else
                {
                    Console.WriteLine("Такой фамилии нет.");
                }
            }

            return fullNameDelete;
        }

        static string ReadJobName(Dictionary<string, List<string>> dossiers, out List<string> value)
        {
            bool isWork = true;

            value = new List<string>();

            string jobName = "";

            while (isWork)
            {
                Console.WriteLine("\nВведите должность: ");
                string userInput = Console.ReadLine();

                if (dossiers.TryGetValue(userInput, out value))
                {
                    jobName = userInput;

                    isWork = false;
                }
                else
                {
                    Console.WriteLine("Должность не найдена.");
                }
            }

            return jobName;
        }

        static void WriteAnyKey()
        {
            Console.WriteLine("\nДля возврата в меню нажмите любую клавишу.");
            Console.ReadKey();
        }
    }
}
