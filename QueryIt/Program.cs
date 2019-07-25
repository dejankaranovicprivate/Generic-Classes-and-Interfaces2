using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryIt
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<EmployeeDb>());

            using (IRepository<Employee> empolyeeRepository = new SqlRepository<Employee>(new EmployeeDb()))
            {
                AddEmployees(empolyeeRepository);
                AddManagers(empolyeeRepository);
                CountEmployees(empolyeeRepository);
                QueryEmployees(empolyeeRepository);
                DumpPeople(empolyeeRepository);

                IEnumerable<Person> temp = empolyeeRepository.FindAll();
            }

            Console.ReadLine();
        }

        private static void AddManagers(IWriteOnlyRepository<Manager> empolyeeRepository)
        {
            empolyeeRepository.Add(new Manager { Name = "Alex" });
            empolyeeRepository.Commit();
        }

        private static void DumpPeople(IReadOnlyRepository<Person> empolyeeRepository)
        {
            var employees = empolyeeRepository.FindAll();
            foreach(var employee in employees)
            {
                Console.WriteLine(employee.Name);
            }
        }

        private static void QueryEmployees(IRepository<Employee> empolyeeRepository)
        {
            var employee = empolyeeRepository.FindById(1);
            Console.WriteLine(employee.Name);
        }

        private static void CountEmployees(IRepository<Employee> empolyeeRepository)
        {
            Console.WriteLine(empolyeeRepository.FindAll().Count());
        }

        private static void AddEmployees(IRepository<Employee> empolyeeRepository)
        {
            empolyeeRepository.Add(new Employee { Name = "Scott" });
            empolyeeRepository.Add(new Employee { Name = "Chris" });
            empolyeeRepository.Commit();
        }
    }
}
