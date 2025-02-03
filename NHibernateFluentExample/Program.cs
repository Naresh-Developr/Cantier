using System;
using NHibernate;
using NHibernateFluentExample.Entities;
using NHibernateFluentExample.Helper;
using System.Linq;

namespace NHibernateFluentExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to project 1: [Crud With NHibernate]");
            var operations = new Operations();  // Create instance of Operations class

            while (true)
            {
                Console.WriteLine("----------------------------------------- :(");
                Console.WriteLine("Option available: \n 1.AddEmployee \n 2.List All Employees \n 3.Update \n 4.Delete \n 5.Find By Name \n 6.Exit");
                Console.WriteLine("----------------------------------------- :)");
                string choice = Console.ReadLine() ?? string.Empty;

                if (choice == "1")
                {
                    Console.WriteLine("Selected: Create");
                    Console.WriteLine("Enter Employee Name");
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter Department Name");
                    string department = Console.ReadLine();
                    Console.WriteLine("Enter Salary");
                    decimal salary = Convert.ToDecimal(Console.ReadLine());

                    operations.MyAdd(name, department, salary);  // Use the instance to call MyAdd

                    Console.WriteLine("Employee added successfully");
                }
                if (choice == "2")
                {
                    Console.WriteLine("Selected: Read");
                    operations.MyRead();
                }
                if (choice == "3")
                {
                    Console.WriteLine("Selected: Update");
                    operations.MyRead();
                    Console.WriteLine("Enter Employee ID");
                    int id = Convert.ToInt32(Console.ReadLine());
                    operations.MyUpdate(id);
                }
                if (choice == "4")
                {
                    Console.WriteLine("Selected: Delete");
                    operations.MyRead();
                    Console.WriteLine("Enter Employee ID");
                    int id = Convert.ToInt32(Console.ReadLine());
                    operations.MyDelete(id);
                }
                if (choice == "5")
                {
                    Console.WriteLine("Selected: Find");
                    Console.WriteLine("Enter Employee Name to find:");
                    string name = Console.ReadLine();
                    operations.MyFind(name);
                }
                if (choice == "6")
                {
                    break;
                }
            }
        }
    }

    public class Operations
    {
        public readonly ISessionFactory _sessionFactory;

        public Operations()
        {
            _sessionFactory = NHibernateHelper.SessionFactory;
        }

        // Add Employee
        public void MyAdd(string name, string department, decimal salary)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var employee = new Employee
                    {
                        Name = name,
                        Department = department,
                        Salary = salary
                    };

                    session.Save(employee);
                    transaction.Commit();
                }
            }
        }

        // Read all Employees
        public void MyRead()
        {
            using(var session = _sessionFactory.OpenSession())
            {
                var data = session.Query<Employee>().ToList();

                foreach(var emp in data)
                {
                    Console.WriteLine($"{emp.Id}: {emp.Name} - {emp.Department} - ${emp.Salary}");
                }
            }
        }

        // Update Employee
        public void MyUpdate(int employeeId)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var employee = session.Get<Employee>(employeeId);
                if (employee != null)
                {
                    Console.WriteLine($"Selected Employee: {employee.Name} - {employee.Department}");

                    Console.WriteLine("Enter New Department Name to change: ");
                    string newDepartment = Console.ReadLine();

                    employee.Department = newDepartment;

                    using (var transaction = session.BeginTransaction())
                    {
                        session.Update(employee);
                        transaction.Commit();
                    }

                    Console.WriteLine("Employee updated successfully.");
                }
                else
                {
                    Console.WriteLine("Employee not found");
                }
            }
        }

        // Delete Employee
        public void MyDelete(int employeeId)
        {
            using(var sessions = _sessionFactory.OpenSession())
            {
                var employee = sessions.Get<Employee>(employeeId);
                try
                {
                    using (var transaction = sessions.BeginTransaction())
                    {
                        sessions.Delete(employee);
                        transaction.Commit();
                    }

                    Console.WriteLine("Employee deleted successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception occured" + ex.Message);
                }
          
            }
        }

        // Find Employee by Name
        public void MyFind(string name)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var employees = session.Query<Employee>().Where(e => e.Name == name).ToList();
                if (employees.Any())
                {
                    Console.WriteLine("Employee(s) found:");
                    foreach (var emp in employees)
                    {
                        Console.WriteLine($"{emp.Id}: {emp.Name} - {emp.Department} - ${emp.Salary}");
                    }
                }
                else
                {
                    Console.WriteLine("No employee found with that name.");
                }
            }
        }
    }
}
