using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MyOrganization
{
    internal abstract class Organization
    {
        private Position root;
        private int uniqueIdentifier = 0;
        public Dictionary<Position, List<Employee>> HiredPersonnel = new Dictionary<Position, List<Employee>>();
        public Organization()
        {
            root = CreateOrganization();
        }

        protected abstract Position CreateOrganization();

        /**
         * hire the given person as an employee in the position that has that title
         * 
         * @param person
         * @param title
         * @return the newly filled position or empty if no position has that title
         */
        public Position? GetPosition(string title) //BFS, worst case O(n)
        {
            Queue<Position> queue = new Queue<Position>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                Position current_position = queue.Dequeue();
                if (current_position.GetTitle() == title)
                {
                    return current_position;
                }
                foreach (Position subordinate in current_position.GetDirectReports())
                {
                    queue.Enqueue(subordinate);
                }
        

            }
            return null;
        }
        public Position? Hire(Name person, string title)
        {
            //your code here
            //I'm not enforcing an isFilled condition because multiple people may be hired for the same position. I would add a headcount property to a position but I cannot modify MyOrganization.cs.
            //If this code is doing too much, it can easily be shrunk so only one position can take one employee
            //but because of the loose requirements and free direction given, I've opted to use lists as a property in the Position class. The code can also be extended to add a headcount property to deny hiring
            //if headcount is reached, (would require editing MyOrganization.cs to have a headcount argument on a Position instantiate, so I will refrain).
            
            
            Position? PositionToHireFor = GetPosition(title);

            if (PositionToHireFor != null)
            {
                PositionToHireFor.AddEmployee(new Employee(uniqueIdentifier, person));
                uniqueIdentifier = uniqueIdentifier + 1;

            }
            
            

            return null;

        }


        override public string ToString()
        {
            return PrintOrganization(root, "");
        }

        private string PrintOrganization(Position pos, string prefix)
        {
            StringBuilder sb = new StringBuilder();
            Queue<Position> queue = new Queue<Position>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                Position current_position = queue.Dequeue();
                sb.Append(current_position.GetTitle() + ": [");
                if (current_position.GetEmployees().Count > 0) {
                    foreach (Employee employee in current_position.GetEmployees())
                    {
                        
                        sb.Append(prefix + employee.GetName() + ",");
                    }
                    sb.Remove(sb.Length - 1, 1);
                }
                
                sb.Append("]");
                sb.Append('\n');
                foreach (Position subordinate in current_position.GetDirectReports())
                {
                    queue.Enqueue(subordinate);
                }


            }
         
            return sb.ToString();
        }
    }
}
