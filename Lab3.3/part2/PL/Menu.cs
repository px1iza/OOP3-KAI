using System;
using System.Collections.Generic;
using DAL.Entities;
using DAL.DataProviders;
using DAL.Interfaces;
using BLL;
using BLL.Exceptions;
using DAL;

namespace PL
{
    public class Menu
    {
        public void MainMenu()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("СИСТЕМА УПРАВЛІННЯ СТУДЕНТАМИ\n");
                Console.WriteLine("Оберіть тип серіалізації:");
                Console.WriteLine("1. Binary (.bin)");
                Console.WriteLine("2. XML (.xml)");
                Console.WriteLine("3. JSON (.json)");
                Console.WriteLine("4. Custom (.txt)");
                Console.WriteLine("0. Вихід");
                Console.Write("\nВаш вибір: ");

                string choice = Console.ReadLine();

                if (choice == "0")
                {
                    Console.WriteLine("Вихід із програми...");
                    break;
                }

                string provider;
                string extension;

                switch (choice)
                {
                    case "1":
                        provider = "binary";
                        extension = ".bin";
                        break;
                    case "2":
                        provider = "xml";
                        extension = ".xml";
                        break;
                    case "3":
                        provider = "json";
                        extension = ".json";
                        break;
                    case "4":
                        provider = "custom";
                        extension = ".txt";
                        break;
                    default:
                        Console.WriteLine("Невірний вибір. Натисніть будь-яку клавішу, щоб спробувати ще раз...");
                        Console.ReadKey();
                        continue;
                }

                var service = new EntityService(provider, $"students{extension}");

                RunDatabaseMenu(service);
            }
        }

        private static void RunDatabaseMenu(EntityService service)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- МЕНЮ ---");
                Console.WriteLine("1. Додати студента");
                Console.WriteLine("2. Показати всіх студентів");
                Console.WriteLine("3. Видалити студента");
                Console.WriteLine("4. Знайти студента за ID");
                Console.WriteLine("5. Оновити дані студента");
                Console.WriteLine("0. Назад (до вибору серіалізації)");
                Console.Write("\nВаш вибір: ");

                string option = Console.ReadLine();
                if (option == "0") break;

                try
                {
                    switch (option)
                    {
                        case "1": AddStudent(service); break;
                        case "2": ShowAll(service); break;
                        case "3": DeleteStudent(service); break;
                        case "4": FindStudent(service); break;
                        case "5": UpdateStudent(service); break;
                        default:
                            Console.WriteLine("Невірний вибір!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}");
                }

                Console.WriteLine("\nНатисніть будь-яку клавішу, щоб продовжити...");
                Console.ReadKey();
            }
        }

        private static void AddStudent(EntityService service)
        {
            Console.Write("Ім’я: ");
            string fn = Console.ReadLine();
            Console.Write("Прізвище: ");
            string ln = Console.ReadLine();
            Console.Write("Зріст (см): ");
            int height = int.Parse(Console.ReadLine());
            Console.Write("Вага (кг): ");
            int weight = int.Parse(Console.ReadLine());
            Console.Write("Student ID (формат: AA123456): ");
            string id = Console.ReadLine();
            Console.Write("Серія паспорта: ");
            string ps = Console.ReadLine();
            Console.Write("Номер паспорта: ");
            int pn = int.Parse(Console.ReadLine());

            var student = new Student(fn, ln, height, weight, id, new Passport(ps, pn));
            service.AddStudent(student);
            Console.WriteLine("*** Студента додано! ***");
        }

        private static void ShowAll(EntityService service)
        {
            var list = service.GetAllStudents();
            if (list.Count == 0)
            {
                Console.WriteLine("База порожня.");
                return;
            }

            foreach (var s in list)
                Console.WriteLine(s);
        }

        private static void DeleteStudent(EntityService service)
        {
            Console.Write("Введіть ID для видалення: ");
            string id = Console.ReadLine();
            service.DeleteStudent(id);
            Console.WriteLine(" Видалено (якщо існував) ");
        }

        private static void FindStudent(EntityService service)
        {
            Console.Write("Введіть ID: ");
            string id = Console.ReadLine();
            var all = service.GetAllStudents();
            var student = all.Find(s => s.StudentID == id);
            if (student != null)
                Console.WriteLine(student);
            else
                Console.WriteLine("Не знайдено!");
        }

        private static void UpdateStudent(EntityService service)
        {
            Console.Write("Введіть ID для оновлення: ");
            string id = Console.ReadLine();
            service.DeleteStudent(id);
            Console.WriteLine("Введіть нові дані:");
            AddStudent(service);
        }
    }
}