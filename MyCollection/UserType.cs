using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCollection
{
    class UserType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserType(int id, string name)
        {
            Id = id;
            Name = name;
        }
        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            UserType temp = (UserType)obj;
            return Id.Equals(temp.Id) && Name.Equals(temp.Name);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {   
            return Id.GetHashCode()^Name.GetHashCode();
        }
        public override string ToString()
        {
            return String.Format("Id ={0}, Name = {1}", Id, Name);
        }
    }
}
