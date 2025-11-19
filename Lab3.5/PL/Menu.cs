using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Services;
using DAL.Entities;        // Це нормально — сутності можна використовувати в PL
using PL.Services;
using DAL.Interfaces;

namespace PL
{
    public class Menu
    {
        private readonly StudentService _studentService;
        private readonly IInternetService _internetService;

        private List<Student> _students;

        // ✔ Конструктор нічого не знає про DAL
        public Menu()
        {
            _studentService = ServiceFactory.CreateStudentService();
            _internetService = new ConsoleInternetService();

            _students = _studentService.GetAllStudents();
        }

        public void Run() => Show();

        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Система Управління Студентами ===");
                Console.WriteLine("1. Показати всіх студентів");
                Console.WriteLine("2. Додати нового студента");
                Console.WriteLine("3. Знайти студентів з ідеальною вагою");
                Console.WriteLine("4. Змінити вагу студента (подія)");
                Console.WriteLine("5. Вчитися онлайн (DI)");
                Console.WriteLine("6. Зберегти та вийти");
                Console.Write("Оберіть опцію: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        PrintStudents(_students);
                        break;

                    case "2":
                        AddStudent();
                        break;

                    case "3":
                        var ideal = _studentService.GetStudentsWithIdealWeight();
                        Console.WriteLine("\n--- Студенти з ідеальною вагою ---");
                        PrintStudents(ideal);
                        break;

                    case "4":
                        ChangeWeightMenu();
                        break;

                    case "5":
                        StudyOnlineMenu();
                        break;

                    case "6":
                        _studentService.SaveStudents(_students);
                        return;

                    default:
                        Console.WriteLine("Невірна опція.");
                        break;
                }

                Console.WriteLine("\nНатисніть будь-яку клавішу...");
                Console.ReadKey();
            }
        }

        private void PrintStudents(List<Student> list)
        {
            if (!list.Any())
            {
                Console.WriteLine("Список порожній.");
                return;
            }

            int i = 0;
            foreach (var s in list)
            {
                Console.WriteLine($"{i++}. {s}");
            }
        }

        private void AddStudent()
        {
            Console.Write("Ім'я: ");
            string fn = Console.ReadLine();

            Console.Write("Прізвище: ");
            string ln = Console.ReadLine();

            Console.Write("Зріст (см): ");
            int h = int.Parse(Console.ReadLine());

            Console.Write("Вага (кг): ");
            int w = int.Parse(Console.ReadLine());

            Console.Write("Студентський квиток (XX000000): ");
            string id = Console.ReadLine();

            var student = new Student(fn, ln, h, w, id, new Passport("AB", 123456));

            try
            {
                _studentService.RegisterStudent(student);
                _students = _studentService.GetAllStudents();
                Console.WriteLine("Студента успішно додано.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }

        private void ChangeWeightMenu()
        {
            Console.WriteLine("Оберіть номер студента:");
            PrintStudents(_students);

            if (int.TryParse(Console.ReadLine(), out int index) &&
                index >= 0 && index < _students.Count)
            {
                var student = _students[index];

                EventHandler<string> handler = (sender, msg) =>
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n[СПОВІЩЕННЯ] {msg}");
                    Console.ResetColor();
                };

                student.IdealWeightReached += handler;

                Console.Write($"Поточна вага: {student.Weight}. Введіть зміну (-5 або 5): ");

                if (int.TryParse(Console.ReadLine(), out int amount))
                {
                    student.ChangeWeight(amount);
                    Console.WriteLine($"Нова вага: {student.Weight}");
                }
                else
                {
                    Console.WriteLine("Некоректне число.");
                }

                student.IdealWeightReached -= handler;
            }
            else
            {
                Console.WriteLine("Невірний індекс.");
            }
        }

        private void StudyOnlineMenu()
        {
            Console.WriteLine("Оберіть студента:");
            PrintStudents(_students);

            if (int.TryParse(Console.ReadLine(), out int index) &&
                index >= 0 && index < _students.Count)
            {
                var student = _students[index];
                string result = student.StudyOnline(_internetService);
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine("Невірний індекс.");
            }
        }
    }
}