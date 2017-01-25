using System;
using System.Collections.Generic;

namespace CSVSort
{
    class Order
    {
        public bool ValidEntry = true;
        public string ID;
        public string Created;
        public string ItemName;
        public string Email;
        //NAME,STREET,CITY,STATE,ZIP,PACKED,SHIPPED
        public int Quanity;
        public string Packed;
        public string Shipped;
        public string Name;

        public struct MailingAddress
        {
            public string Street;
            public string City;
            public string State;
            public string Zip;
        }

        public MailingAddress mailingAddress;

        public Order(string[] stringArray)
        {
            List<string> processedArray = new List<string>();
            foreach (string s in stringArray)
            {
                processedArray.Add(item: s.Replace("\"", "").Trim());
            }

            if (processedArray.Count == 12)
            {

                ID = processedArray[0];
                Created = processedArray[1];
                ItemName = processedArray[2];
                Email = processedArray[3];
                Name = processedArray[4];
                mailingAddress.Street = processedArray[5];
                mailingAddress.City = processedArray[6];
                mailingAddress.State = processedArray[7];
                mailingAddress.Zip = processedArray[8];
                Quanity = Convert.ToInt32(processedArray[9]);
                Packed = processedArray[10];
                Shipped = processedArray[11];

            }
            else if (stringArray.Length == 8)
            {
                Console.WriteLine("Header is {0}", processedArray);
            }
            else
            {
                ValidEntry = false;
                Console.WriteLine("Order ID: {0} has a improper address and will not be included", ID);
            }
        }

        public string BuildListing()
        {
            //ID,DATE,ITEM,EMAIL,NAME,STREET,CITY,STATE,ZIP,UNITS,SHIPWHT
            return String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}",
                ID,
                DateTime.Today.Date,
                ItemName,
                Email,
                Name,
                mailingAddress.Street,
                mailingAddress.City,
                mailingAddress.State,
                mailingAddress.Zip,
                Quanity,
                (Quanity*9));
        }
    }
}
