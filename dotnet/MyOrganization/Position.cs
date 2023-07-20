using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOrganization
{
    internal class Position
    {
        private string title;
        private List<Employee> employees;
        private HashSet<Position> directReports;
        


        public Position(string title)
        {
            this.title = title;
            employees = new List<Employee>();
            directReports = new HashSet<Position>();

        }

        public Position(String title, Employee employee) : this(title)
        {
            if (employee != null)
                AddEmployee(employee);
        }

        public String GetTitle()
        {
            return title;
        }

        public void AddEmployee(Employee employee)
        {
            this.employees.Add(employee);      
        }

        public List<Employee> GetEmployees()
        {
            return employees;
        }

        public Boolean NotEmpty()
        {
            return employees.Count() > 0;
        }


        public Boolean AddDirectReport(Position position)
        {
            if (position == null)
                throw new Exception("position cannot be null");
            return directReports.Add(position);
        }

        public Boolean RemovePosition(Position position)
        {
            return directReports.Remove(position);
        }

        public ImmutableList<Position> GetDirectReports()
        {
            return directReports.ToImmutableList();
        }

        override public string ToString()
        {
            return title + (employees != null ? ": " + employees.ToString() : "");
        }
    }
}
