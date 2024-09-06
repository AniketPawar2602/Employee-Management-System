using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EmployeeManagementSystem
{
    class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public string Department { get; set; }

        public override string ToString()
        {
            return $"{Id} | {Name} | {Position} | {Salary} | {Department}";
        }
    }

    class EmployeeManager
    {
        private List<Employee> employees = new List<Employee>();
        private const string FilePath = "employees.txt";

        public void LoadFromFile()
        {
            if (File.Exists(FilePath))
            {
                var lines = File.ReadAllLines(FilePath);
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    employees.Add(new Employee
                    {
                        Id = int.Parse(parts[0]),
                        Name = parts[1],
                        Position = parts[2],
                        Salary = decimal.Parse(parts[3]),
                        Department = parts[4]
                    });
                }
            }
        }

        public void SaveToFile()
        {
            var lines = employees.Select(e => $"{e.Id}|{e.Name}|{e.Position}|{e.Salary}|{e.Department}");
            File.WriteAllLines(FilePath, lines);
        }

        public void AddEmployee(Employee employee)
        {
            employees.Add(employee);
            SaveToFile();
            Console.WriteLine("__ Employee added successfully __");
        }

        public void ViewEmployees()
        {
            if (employees.Count == 0)
            {
                Console.WriteLine("No employees to display.");
            }
            else
            {
                foreach (var emp in employees)
                {
                    Console.WriteLine(emp);
                }
            }
            Console.WriteLine("__ End of Employee List __");
        }

        public Employee SearchEmployee(int id)
        {
            return employees.FirstOrDefault(e => e.Id == id);
        }

        public void UpdateEmployee(Employee updatedEmployee)
        {
            var index = employees.FindIndex(e => e.Id == updatedEmployee.Id);
            if (index != -1)
            {
                employees[index] = updatedEmployee;
                SaveToFile();
                Console.WriteLine("__ Employee updated successfully __");
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }

        public void DeleteEmployee(int id)
        {
            var employee = SearchEmployee(id);
            if (employee != null)
            {
                employees.Remove(employee);
                SaveToFile();
                Console.WriteLine("__ Employee deleted successfully __");
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var manager = new EmployeeManager();
            manager.LoadFromFile();

            while (true)
            {
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. View Employees");
                Console.WriteLine("3. Search Employee");
                Console.WriteLine("4. Update Employee");
                Console.WriteLine("5. Delete Employee");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        var emp = new Employee();
                        Console.Write("Enter ID: ");
                        emp.Id = int.Parse(Console.ReadLine());
                        Console.Write("Enter Name: ");
                        emp.Name = Console.ReadLine();
                        Console.Write("Enter Position: ");
                        emp.Position = Console.ReadLine();
                        Console.Write("Enter Salary: ");
                        emp.Salary = decimal.Parse(Console.ReadLine());
                        Console.Write("Enter Department: ");
                        emp.Department = Console.ReadLine();
                        manager.AddEmployee(emp);
                        break;
                    case "2":
                        manager.ViewEmployees();
                        break;
                    case "3":
                        Console.Write("Enter ID to search: ");
                        var searchId = int.Parse(Console.ReadLine());
                        var employee = manager.SearchEmployee(searchId);
                        if (employee != null)
                        {
                            Console.WriteLine(employee);
                        }
                        else
                        {
                            Console.WriteLine("Employee not found.");
                        }
                        Console.WriteLine("__ End of Search __");
                        break;
                    case "4":
                        Console.Write("Enter ID to update: ");
                        var updateId = int.Parse(Console.ReadLine());
                        var existingEmployee = manager.SearchEmployee(updateId);
                        if (existingEmployee != null)
                        {
                            Console.Write("Enter new Name: ");
                            existingEmployee.Name = Console.ReadLine();
                            Console.Write("Enter new Position: ");
                            existingEmployee.Position = Console.ReadLine();
                            Console.Write("Enter new Salary: ");
                            existingEmployee.Salary = decimal.Parse(Console.ReadLine());
                            Console.Write("Enter new Department: ");
                            existingEmployee.Department = Console.ReadLine();
                            manager.UpdateEmployee(existingEmployee);
                        }
                        else
                        {
                            Console.WriteLine("Employee not found.");
                        }
                        break;
                    case "5":
                        Console.Write("Enter ID to delete: ");
                        var deleteId = int.Parse(Console.ReadLine());
                        manager.DeleteEmployee(deleteId);
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
                Console.WriteLine("__ Operation Complete __");
            }
        }
    }
}
