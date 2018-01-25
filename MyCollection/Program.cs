using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCollection.Collections;

namespace MyCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            MyCollection<UserType, int, string> coll = new MyCollection<UserType, int, string>();
            try
            {
                coll.Add(new UserType(1, "Name1"), 15, "Value1");
                coll.Add(new UserType(2, "Name2"), 15, "Value2");
                coll.Add(new UserType(3, "Name3"), 15, "Value3");
                coll.Add(new UserType(4, "Name4"), 15, "Value4");
                coll[new KeyPair<UserType, int>(new UserType(5, "Name5"), 18)] = "Value5";
                coll[new KeyPair<UserType, int>(new UserType(6, "Name6"), 18)] = "Value6";
                coll[new KeyPair<UserType, int>(new UserType(7, "Name7"), 18)] = "Value7";
                coll[new KeyPair<UserType, int>(new UserType(1, "Name1"), 18)] = "Value7";
				//Поиск значений по ID
				foreach (var item in coll.GetValuesById(new UserType(1, "Name1")))
				{
					Console.WriteLine(item);
				}
				
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
           
            Console.ReadLine();
        }
    }
}
