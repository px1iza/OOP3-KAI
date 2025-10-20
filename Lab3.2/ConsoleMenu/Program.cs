using System;
using System.Collections;
using System.Collections.Generic;
using VectorLibrary;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // ========== ЗАВДАННЯ 2: GENERIC КОЛЕКЦІЯ (List<T>) ==========
            Console.WriteLine("1. УЗАГАЛЬНЕНА КОЛЕКЦІЯ (List<Vector>)");
            List<Vector> list = new List<Vector>();

            // Додавання
            list.Add(new Vector(3, 4));
            list.Add(new Vector(1, 2));
            list.Add(new Vector(5, 12));

            Console.WriteLine("Після додавання:");
            Display(list);

            // Оновлення
            list[0] = new Vector(6, 8);
            Console.WriteLine("Після оновлення (індекс 0):");
            Display(list);

            // Пошук
            var found = list.Find(v => v.Length > 10);
            if (found != null)
                Console.WriteLine("Пошук (довжина > 10): " + found.Output() + "\n");

            // Видалення
            list.RemoveAt(0);
            Console.WriteLine("Після видалення (індекс 0):");
            Display(list);

            // ========== ЗАВДАННЯ 2: МАСИВ ==========
            Console.WriteLine("\n2. МАСИВ (Vector[])");
            Vector[] arr = new Vector[3];
            arr[0] = new Vector(3, 4);
            arr[1] = new Vector(1, 2);
            arr[2] = new Vector(5, 12);

            Console.WriteLine("Після створення:");
            Display(arr);

            // Оновлення
            arr[0] = new Vector(6, 8);
            Console.WriteLine("Після оновлення (індекс 0):");
            Display(arr);

            // Пошук
            Console.Write("Пошук (довжина > 10): ");
            foreach (Vector v in arr)
            {
                if (v != null && v.Length > 10)
                {
                    Console.WriteLine(v.Output() + "\n");
                    break;
                }
            }

            // Видалення (встановлення null)
            arr[2] = null;
            Console.WriteLine("Після видалення (індекс 2):");
            Display(arr);

            // ===== ЗАВДАННЯ 2: NON-GENERIC КОЛЕКЦІЯ (ArrayList) ==========
            Console.WriteLine("\n3. НЕ-УЗАГАЛЬНЕНА КОЛЕКЦІЯ (ArrayList)");
            ArrayList nonGenericList = new ArrayList();

            // Додавання
            nonGenericList.Add(new Vector(3, 4));
            nonGenericList.Add(new Vector(1, 2));
            nonGenericList.Add(new Vector(5, 12));

            Console.WriteLine("Після додавання:");
            Display(nonGenericList);

            // Оновлення
            nonGenericList[0] = new Vector(6, 8);
            Console.WriteLine("Після оновлення (індекс 0):");
            Display(nonGenericList);

            // Пошук
            Console.Write("Пошук (довжина > 10): ");
            Vector foundArrayList = null;
            foreach (object item in nonGenericList)
            {
                if (item is Vector vec && vec.Length > 10)
                {
                    foundArrayList = vec;
                    break;
                }
            }
            Console.WriteLine(foundArrayList?.Output() ?? "Не знайдено");
            Console.WriteLine();

            // Видалення
            nonGenericList.RemoveAt(2);
            Console.WriteLine("Після видалення (індекс 2):");
            Display(nonGenericList);

            // ========== ЗАВДАННЯ 3-4: БІНАРНЕ ДЕРЕВО ==========
            Console.WriteLine("\n4. БІНАРНЕ ДЕРЕВО (Сортування за довжиною - IComparable)");
            BinaryTree<Vector> lengthTree = new BinaryTree<Vector>();

            lengthTree.Add(new Vector(5, 12));   // довжина 13
            lengthTree.Add(new Vector(3, 4));    // довжина 5
            lengthTree.Add(new Vector(8, 15));   // довжина 17
            lengthTree.Add(new Vector(1, 1));    // довжина 1.41

            Console.WriteLine("Кореневий елемент: " + new Vector(5, 12).Output());
            Console.WriteLine("\nПрямий обхід (Preorder) - сортування за довжиною:");
            foreach (var item in lengthTree)
            {
                Console.WriteLine(item.Output());
            }
            Console.WriteLine();

            Console.WriteLine("\n 5. БІНАРНЕ ДЕРЕВО (Сортування за X - IComparer) ");
            BinaryTree<Vector> xTree = new BinaryTree<Vector>(new VectorComparer());

            xTree.Add(new Vector(5, 12));
            xTree.Add(new Vector(2, 8));
            xTree.Add(new Vector(7, 3));
            xTree.Add(new Vector(1, 5));

            Console.WriteLine("Прямий обхід (Preorder) - сортування за координатою X:");
            foreach (var item in xTree)
            {
                Console.WriteLine(item.Output());
            }
            Console.WriteLine();

            // ========== ДЕМОНСТРАЦІЯ МЕТОДІВ ВЕКТОРА ==========
            Console.WriteLine("\n 6. ДЕМОНСТРАЦІЯ МЕТОДІВ КЛАСУ VECTOR ");
            Vector testVector = new Vector(3, 4);
            Console.WriteLine("Початковий вектор: " + testVector.Output());
            Console.WriteLine("Довжина: " + testVector.Length);

            testVector.Increase(2);
            Console.WriteLine("\nПісля збільшення у 2 рази: " + testVector.Output());
            Console.WriteLine("Нова довжина: " + testVector.Length);

        }

        // Методи для виведення колекцій
        static void Display(List<Vector> list)
        {
            foreach (Vector v in list)
                Console.WriteLine(v.Output());
            Console.WriteLine();
        }

        static void Display(Vector[] arr)
        {
            foreach (Vector v in arr)
            {
                if (v != null)
                    Console.WriteLine(v.Output());
                else
                    Console.WriteLine("(null)");
            }
            Console.WriteLine();
        }

        static void Display(ArrayList list)
        {
            foreach (object item in list)
            {
                if (item is Vector v)
                    Console.WriteLine(v.Output());
                else
                    Console.WriteLine($"[Не Vector: {item.GetType().Name}]");
            }
            Console.WriteLine();
        }
    }
}