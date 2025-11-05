using System;
using System.Collections.Generic;
using DAL;
using DAL.Entities;
using BLL.Exceptions;
using DAL.Interfaces;
using DAL.DataProviders;

namespace BLL
{
    public class EntityService
    {
        private readonly EntityContext<Student> _context;

        public EntityService(string provider, string filePath)
        {
            IDataProvider<Student> dataProvider = provider switch
            {
                "binary" => new MemoryPackProvider<Student>(),
                "xml" => new XmlProvider<Student>(),
                "json" => new JSONProvider<Student>(),
                "custom" => new CustomProvider<Student>(),
                _ => throw new ArgumentException("Невідомий тип провайдера даних.")
            };
            _context = new EntityContext<Student>(dataProvider, filePath);

        }

        public void AddStudent(Student s)
        {
            if (string.IsNullOrWhiteSpace(s.FirstName) || string.IsNullOrWhiteSpace(s.LastName))
                throw new InvalidStudentDataException("Ім’я або прізвище не може бути порожнім!");

            if (!s.IsValidStudentID())
                throw new InvalidStudentDataException("Невірний формат Student ID!");

            List<Student> all = _context.Load();
            foreach (Student st in all)
            {
                if (st.StudentID == s.StudentID)
                    throw new InvalidStudentDataException($"Студент з ID {s.StudentID} вже існує!");
            }

            all.Add(s);
            _context.Save(all);
        }

        public void DeleteStudent(string id)
        {
            List<Student> all = _context.Load();
            bool found = false;

            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].StudentID == id)
                {
                    all.RemoveAt(i);
                    found = true;
                    break;
                }
            }

            if (!found)
                throw new StudentNotFoundException($"Студента з ID {id} не знайдено!");

            _context.Save(all);
        }

        public Student FindStudent(string id)
        {
            List<Student> all = _context.Load();
            foreach (Student s in all)
            {
                if (s.StudentID == id)
                    return s;
            }
            throw new StudentNotFoundException($"Студента з ID {id} не знайдено!");
        }

        public List<Student> GetAllStudents() => _context.Load();

        public void UpdateStudent(string id, Student updated)
        {
            List<Student> all = _context.Load();
            bool found = false;

            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].StudentID == id)
                {
                    all[i] = updated;
                    found = true;
                    break;
                }
            }

            if (!found)
                throw new StudentNotFoundException($"Студента з ID {id} не знайдено!");

            _context.Save(all);
        }
    }
}