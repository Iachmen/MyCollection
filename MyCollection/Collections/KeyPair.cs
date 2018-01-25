using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCollection.Collections
{
    class KeyPair<TKeyFirst, TKeySecond>
    {
        public TKeyFirst ID { get; set; }
        public TKeySecond Name { get; set; }
        public KeyPair(TKeyFirst id, TKeySecond name)
        {
            ID = id;
            Name = name;
        }
        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            KeyPair<TKeyFirst, TKeySecond> temp;
            temp = (KeyPair<TKeyFirst, TKeySecond>)obj;
            
            return ID.Equals(temp.ID) && Name.Equals(temp.Name);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return ID.GetHashCode() ^ Name.GetHashCode();
        }
    }
}
