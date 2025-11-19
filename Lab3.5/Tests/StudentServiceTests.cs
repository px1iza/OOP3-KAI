using Xunit;
using Moq; // Необхідно для створення заглушок
using BLL.Services;
using DAL.Entities;
using DAL.Interfaces;
using System.Collections.Generic;

namespace Tests
{
    public class StudentServiceTests
    {
        [Fact]
        public void GetStudentsWithIdealWeight_ReturnsCorrectStudents()
        {
            // Arrange (Підготовка)
            var mockProvider = new Mock<IDataProvider<Student>>();

            // Створюємо тестові дані
            var testStudents = new List<Student>
    {
        // Ідеальна вага: 180 - 110 = 70. Співпадає.
        new Student("Liza", "Test", 180, 70, "AA123456", null), 
        
        // НЕ ідеальна вага: 170 - 110 = 60. А тут 80.
        new Student("Roman", "Test", 170, 80, "BB123456", null), 
        
        // Ідеальна вага: 165 - 110 = 55. Співпадає.
        new Student("Olena", "Test", 165, 55, "CC123456", null)
    };

            // Налаштовуємо поведінку заглушки
            mockProvider.Setup(p => p.Load(It.IsAny<string>())).Returns(testStudents);

            var service = new StudentService(mockProvider.Object);

            // Act (Дія)
            var result = service.GetStudentsWithIdealWeight("dummy.json");

            // Assert (Перевірка)
            Assert.Equal(2, result.Count); // Очікуємо 2 студента (Ліза та Олена)

            // ВИПРАВЛЕНО ТУТ: перевіряємо наявність тих імен, які ми створили
            Assert.Contains(result, s => s.FirstName == "Liza");
            Assert.Contains(result, s => s.FirstName == "Olena");
        }


        [Fact]
        public void Student_ChangeWeight_FiresEvent_WhenIdealWeightReached()
        {
            // Arrange
            // Студент: Ріст 180, Поточна вага 75. Ідеальна вага = 70.
            var student = new Student("Test", "User", 180, 75, "TT000000", null);
            bool eventFired = false;

            // Підписуємось на подію
            student.IdealWeightReached += (sender, msg) => { eventFired = true; };

            // Act
            // Зменшуємо вагу на 5 (75 - 5 = 70)
            student.ChangeWeight(-5);

            // Assert
            Assert.True(eventFired, "Подія мала спрацювати, оскільки вага стала ідеальною.");
            Assert.Equal(70, student.Weight);
        }

        [Fact]
        public void RegisterStudent_ThrowsException_InvalidID()
        {
            // Arrange
            var mockProvider = new Mock<IDataProvider<Student>>();
            var service = new StudentService(mockProvider.Object);
            var list = new List<Student>();
            var invalidStudent = new Student("Bad", "ID", 180, 80, "INVALID_ID", null);

            // Act & Assert
            Assert.Throws<System.ArgumentException>(() =>
                service.RegisterStudent(list, invalidStudent, "file.txt"));
        }
        // Файл: Lab3.5/Tests/StudentTests.cs

        [Fact]
        public void StudyOnline_ReturnsSuccessMessage_WhenInternetIsAvailable()
        {
            // Arrange (Підготовка)
            var student = new Student("Ivan", "Ivanov", 180, 70, "TE123456", null);

            // Створюємо "фейковий" інтернет
            var mockInternet = new Mock<IInternetService>();

            // Налаштовуємо його: коли спитають IsConnected, поверни true (ТАК)
            mockInternet.Setup(i => i.IsConnected).Returns(true);

            // Act (Дія)
            // Передаємо фейковий інтернет у метод
            string result = student.StudyOnline(mockInternet.Object);

            // Assert (Перевірка)
            Assert.Contains("is studying online", result);
        }

        [Fact]
        public void StudyOnline_ReturnsFailureMessage_WhenNoInternet()
        {
            // Arrange
            var student = new Student("Ivan", "Ivanov", 180, 70, "TE123456", null);

            var mockInternet = new Mock<IInternetService>();

            // Налаштовуємо: коли спитають IsConnected, поверни false (НІ)
            mockInternet.Setup(i => i.IsConnected).Returns(false);

            // Act
            string result = student.StudyOnline(mockInternet.Object);

            // Assert
            Assert.Contains("cannot study", result);
        }
    }
}