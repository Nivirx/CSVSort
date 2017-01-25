using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;


namespace CSVSort
{
    /// <summary>
    /// Contains a list of order objects and methods to work with the orders
    /// </summary>
    internal class CSVList
    {
        private static readonly Regex CsvSplit = new Regex(@"(?<!,[^(]+\([^)]+),", RegexOptions.Compiled);
        private string _listName;
        private List<Order> _orderList;
        private SQLOutput _sqlFile;

        public CSVList(string name)
        {
            _listName = name;
            _orderList = new List<Order>();
            _sqlFile = new SQLOutput(_listName);
        }

        /// <summary>
        /// writes the CSV header and writes OrderList to the disk.
        /// </summary>
        public void WriteList()
        {
            if (!(Directory.Exists("./output")))
                Directory.CreateDirectory("./output");

            using (var fs = new FileStream(string.Format("./output/CSVOrder-{0}.csv", _listName), FileMode.CreateNew))
            {
                var sw = new StreamWriter(fs);
                //Header
                //ID,DATE,ITEM,EMAIL,NAME,STREET,CITY,STATE,ZIP,UNITS,SHIPWHT
                sw.WriteLine("ID,DATE,ITEM,EMAIL,NAME,STREET,CITY,STATE,ZIP,UNITS,SHIPWHT");
                foreach (Order o in _orderList)
                    sw.WriteLine(o.BuildListing());

                sw.Flush();
            }
            _sqlFile.WriteSqlFile();
        }

        /// <summary>
        /// Loads orders from disk csv file
        /// </summary>
        /// <param name="csvFile"></param>
        /// <returns> a List of Orders</returns>
        public List<Order> LoadOrders(string csvFile = "CSVOrders.csv")
        {
            var list = new List<Order>();
            try
            {
                using (var fs = new FileStream(csvFile, FileMode.Open))
                {
                    var sr = new StreamReader(fs);

                    List<string> csvSheet = new List<string>();
                    while (!sr.EndOfStream)
                    {
                        csvSheet.Add(sr.ReadLine());
                    }


                    foreach (string order in csvSheet)
                    {
                        string[] splitArray = CsvSplit.Split(order);
                        list.Add(new Order(splitArray));
                    }
                }
                return list;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return list;
            }
        }

        /// <summary>
        /// Counts the number of orders in the list, NOT ITEMS
        /// <seealso cref="GetUnitCount"/>
        /// </summary>
        /// <returns>the number of orders in the list</returns>
        public short GetOrderCount()
        {
            return (short) _orderList.Count();
        }

        /// <summary>
        /// get the total number of ITEMS shipping regardless of orders
        /// </summary>
        /// <returns>the number of items shipping</returns>
        public short GetUnitCount()
        {
            short result = 0;
            foreach (var order in _orderList)
                result += (short)order.Quanity;
            return result;
        }

        /// <summary>
        /// Adds a order to the list
        /// </summary>
        /// <param name="o">a Order object</param>
        public void AddOrder(Order o)
        {
            _orderList.Add(o);
            _sqlFile.AddInsert(o);
        }

        public List<Order> GetOrderList()
        {
            return _orderList;
        }
    }
}