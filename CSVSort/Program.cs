using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSVSort
{
    class Program
    {
        static void Main(string[] args)
        {

           int numberOfOrders = 0;

           Console.WriteLine("Starting File Load");
           CSVList selectedOrders = new CSVList("single");
           CSVList selectedOrdersGt2 = new CSVList("GT2");
           CSVList selectedOrdersGt5 = new CSVList("GT5");

           CSVList listing = new CSVList("ALL");

           Console.WriteLine("Sorting");
           foreach (Order o in listing.LoadOrders())
           {
               listing.AddOrder(o);
           }

           Console.WriteLine("Found {0} listings", listing.GetOrderCount());

           foreach (Order o in listing.GetOrderList())
           {
               if (o.Quanity == 1)
               {
                   selectedOrders.AddOrder(o);
               }
               if ((o.Quanity >= 2) && (o.Quanity < 5) ) 
               {
                   selectedOrdersGt2.AddOrder(o);
               }
               if (o.Quanity >= 5)
               {
                   selectedOrdersGt5.AddOrder(o);
               }
               numberOfOrders += o.Quanity;
           }

           Console.WriteLine("Flushing {0} orders to disk",numberOfOrders);
           listing.WriteList();
           selectedOrders.WriteList();
           selectedOrdersGt2.WriteList();
           selectedOrdersGt5.WriteList();

           
        }

    }
}
