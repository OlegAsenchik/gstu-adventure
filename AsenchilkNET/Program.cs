using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Создаем настройки DbContext
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        optionsBuilder.UseSqlServer("Server=db8629.public.databaseasp.net; Database=db8629; User Id=db8629; Password=o-7XJ4n=_F2m; Encrypt=False; MultipleActiveResultSets=True;");

        // Задание 3.2.1 - Выборка всех данных из таблицы на стороне "один" (например, из таблицы Courses)
        using (var context = new ApplicationContext(optionsBuilder.Options))
        {
            var courses = context.Courses.ToList();
            Console.WriteLine("All Courses:");
            foreach (var course in courses)
            {
                Console.WriteLine($"CourseID: {course.CourseID}, CourseName: {course.CourseName}, Price: {course.Price}");
            }
        }

        // Задание 3.2.2 - Выборка данных с фильтрацией по условию (например, для Listeners с ID от 100 до 125)
        using (var context = new ApplicationContext(optionsBuilder.Options))
        {
            var filteredListeners = context.Listeners
                .Where(l => l.ListenerID >= 100 && l.ListenerID <= 125)
                .ToList();
            Console.WriteLine("\nFiltered Listeners (ID from 100 to 125):");
            foreach (var listener in filteredListeners)
            {
                Console.WriteLine($"ListenerID: {listener.ListenerID}, FullName: {listener.FullName}");
            }
        }


        // Задание 3.2.3 - Сгруппировать данные по Intensity в таблице Courses и подсчитать количество каждого уровня интенсивности
        using (var context = new ApplicationContext(optionsBuilder.Options))
        {
            var groupedCourses = context.Courses
                .GroupBy(c => c.Intensity)
                .Select(g => new
                {
                    Intensity = g.Key,
                    Count = g.Count()
                }).ToList();
            Console.WriteLine("\nGrouped Courses by Intensity:");
            foreach (var group in groupedCourses)
            {
                Console.WriteLine($"Intensity: {group.Intensity}, Count: {group.Count}");
            }
        }

        // Задание 3.2.4 - Выборка данных из двух таблиц, связанных отношением "один-ко-многим" (например, Listeners и Debtors)
        using (var context = new ApplicationContext(optionsBuilder.Options))
        {
            var listenersWithDebtors = context.Listeners
                .Where(l => l.Debtor != null && l.Debtor.DebtAmount >= 3000 && l.Debtor.DebtAmount <= 4500)
                .Select(l => new
                {
                    ListenerName = l.FullName,
                    DebtAmount = l.Debtor.DebtAmount
                }).ToList();

            Console.WriteLine("\nListeners with Debtors (Debt Amount between 3000 and 4500):");
            foreach (var item in listenersWithDebtors)
            {
                Console.WriteLine($"Listener: {item.ListenerName}, Debt Amount: {item.DebtAmount}");
            }
        }


        // Задание 3.2.5 - Выборка данных из двух таблиц с фильтрацией (например, Listeners и Payments)
        using (var context = new ApplicationContext(optionsBuilder.Options))
        {
            var listenersWithPayments = context.Listeners
                .Where(l => l.Payments.Any(p => p.Amount > 9900)) // Фильтрация по условию для Payments
                .Select(l => new
                {
                    ListenerName = l.FullName,
                    Payments = l.Payments.Where(p => p.Amount > 9900)
                }).ToList();
            Console.WriteLine("\nListeners with Payments > 9900:");
            foreach (var listener in listenersWithPayments)
            {
                Console.WriteLine($"Listener: {listener.ListenerName}");
                foreach (var payment in listener.Payments)
                {
                    Console.WriteLine($"  Payment Date: {payment.PaymentDate}, Amount: {payment.Amount}");
                }
            }
        }

        // Задание 3.2.6 - Вставка данных в таблицу на стороне "Один" (например, в таблицу Debtors)
        using (var context = new ApplicationContext(optionsBuilder.Options))
        {
            Random rand = new Random();

            var newDebtor = new Debtor
            {
                ListenerID = rand.Next(1, 10001), // Случайный ListenerID (от 1 до 10000)
                DebtAmount = rand.Next(1000, 5000), // Случайная сумма долга (от 1000 до 5000)
                LastPaymentDate = DateTime.Now.AddDays(-rand.Next(0, 730)) // Случайная дата последнего платежа (от 0 до 2 лет назад)
            };

            context.Debtors.Add(newDebtor);
            context.SaveChanges();
            Console.WriteLine($"\nNew Debtor inserted: ListenerID = {newDebtor.ListenerID}, DebtAmount = {newDebtor.DebtAmount}, LastPaymentDate = {newDebtor.LastPaymentDate}");
        }

        // Задание 3.2.7 - Вставка данных в таблицу на стороне "Многие" (например, в таблицу Payments)
        using (var context = new ApplicationContext(optionsBuilder.Options))
        {
            Random rand = new Random();

            var newPayment = new Payment
            {
                ListenerID = rand.Next(1, 10001), // Случайный ListenerID (от 1 до 10000)
                PaymentDate = DateTime.Now.AddDays(-rand.Next(0, 365)), // Случайная дата платежа (от 0 до 1 года назад)
                PaymentPurpose = "Course Fee", // Можно расширить случайными данными, если нужно
                Amount = rand.Next(500, 2000) // Случайная сумма платежа (от 500 до 2000)
            };

            context.Payments.Add(newPayment);
            context.SaveChanges();
            Console.WriteLine($"\nNew Payment inserted: ListenerID = {newPayment.ListenerID}, PaymentDate = {newPayment.PaymentDate}, Amount = {newPayment.Amount}");
        }


        // Задание 3.2.8 - Удаление данных из таблицы на стороне "Один" (например, из таблицы Listeners)
        using (var context = new ApplicationContext(optionsBuilder.Options))
        {
            var listenerToDelete = context.Listeners.FirstOrDefault(); // Получаем первого доступного слушателя
            if (listenerToDelete != null)
            {
                context.Listeners.Remove(listenerToDelete);
                context.SaveChanges();
                Console.WriteLine($"\nListener with ID {listenerToDelete.ListenerID} deleted.");
            }
        }

        // Задание 3.2.9 - Удаление данных из таблицы на стороне "Многие" (например, из таблицы Payments)
        using (var context = new ApplicationContext(optionsBuilder.Options))
        {
            var paymentToDelete = context.Payments.FirstOrDefault(); // Получаем первый доступный платеж
            if (paymentToDelete != null)
            {
                context.Payments.Remove(paymentToDelete);
                context.SaveChanges();
                Console.WriteLine($"\nPayment with ID {paymentToDelete.PaymentID} deleted.");
            }
        }

        // Задание 3.2.10 - Обновление данных по условию (например, обновление суммы долга у Debtors)
        using (var context = new ApplicationContext(optionsBuilder.Options))
        {
            Random rand = new Random();

            // Выбираем случайного должника
            var debtorToUpdate = context.Debtors.OrderBy(d => rand.Next()).FirstOrDefault();

            if (debtorToUpdate != null)
            {
                // Сохраняем старую сумму долга для вывода
                var oldDebtAmount = debtorToUpdate.DebtAmount;

                // Обновляем сумму долга на случайное значение
                debtorToUpdate.DebtAmount = rand.Next(1000, 5000); // Случайная сумма долга от 1000 до 5000

                // Сохраняем изменения в базе данных
                context.SaveChanges();

                // Выводим информацию о должнике до и после обновления
                Console.WriteLine($"\nDebtor updated: ListenerID = {debtorToUpdate.ListenerID}");
                Console.WriteLine($"Old Debt Amount: {oldDebtAmount}");
                Console.WriteLine($"New Debt Amount: {debtorToUpdate.DebtAmount}");
            }
            else
            {
                Console.WriteLine("\nNo debtor found to update.");
            }
        }

    }
}
