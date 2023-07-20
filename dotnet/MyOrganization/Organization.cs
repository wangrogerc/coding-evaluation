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
        public Dictionary<Position, List<Name>> hired_personnel = new Dictionary<Position, List<Name>>();
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
        public Position? GetPosition(string title)
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
        public string PluralCheck(int count)
        {
            if (count == 1)
            {
                return "person";
            }
            else
            {
                return "people";
            }
        }
        public Position? Hire(Name person, string title)
        {
            //your code here
            Position? PositionToHireFor = GetPosition(title);
            if (PositionToHireFor != null && !hired_personnel.ContainsKey(PositionToHireFor))
            {
                hired_personnel.Add(PositionToHireFor, new List<Name>());
                hired_personnel[PositionToHireFor].Add(person);
                
 
                Console.WriteLine("Hired" + " " + person.GetFirst() + " " + person.GetLast() + " for " + PositionToHireFor + ". " + "That makes for " + hired_personnel[PositionToHireFor].Count + " " + PluralCheck(hired_personnel[PositionToHireFor].Count) + " in the position.");
                return PositionToHireFor;
            }
            else if (PositionToHireFor != null && hired_personnel.ContainsKey(PositionToHireFor))
            {
                hired_personnel[PositionToHireFor].Add(person);
                Console.WriteLine("Hired" + " " + person.GetFirst() + " " + person.GetLast() + " for " + PositionToHireFor + " ." + "That makes for " + hired_personnel[PositionToHireFor].Count + PluralCheck(hired_personnel[PositionToHireFor].Count) + "in the position.");
                return PositionToHireFor;
            }

            return null;

        }


        override public string ToString()
        {
            return PrintOrganization(root, "");
        }

        private string PrintOrganization(Position pos, string prefix)
        {
            StringBuilder sb = new StringBuilder(prefix + "+-" + pos.ToString() + "\n");
            foreach (Position p in pos.GetDirectReports())
            {
                sb.Append(PrintOrganization(p, prefix + "  "));
            }
            return sb.ToString();
        }
    }
}
