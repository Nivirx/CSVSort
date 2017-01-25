using System;
using System.Collections.Generic;
using System.IO;



//DROP TABLE IF EXISTS <table>;
//CREATE TABLE <table>(
//  ID INTEGER(12) NOT NULL PRIMARY KEY 
//, DATE VARCHAR(14)
//, ITEM VARCHAR(16)
//, EMAIL VARCHAR(48)
//, NAME VARCHAR(48)
//, STREET VARCHAR(48)
//, CITY VARCHAR(20)
//, STATE VARCHAR(4)
//, ZIP VARCHAR(16)
//, UNITS INTEGER(8)
//, SHIPWHT INTEGER(3)
//);

//INSERT INTO <table>(ID,DATE,ITEM,EMAIL,NAME,STREET,CITY,STATE,ZIP,UNITS,SHIPWHT) VALUES (id,date,item,email,name,street,city,state,zip,units,shipwht);
//VARCHAR format as '<String>'


namespace CSVSort
{
    class SQLOutput
    {
        private string _tableName;
        private List<string> _sqlFile;

        public SQLOutput(string table)
        {
            _tableName = table;
            _sqlFile = new List<string>()
            {
                String.Format("DROP TABLE IF EXISTS {0};",_tableName),
                String.Format("CREATE TABLE {0}(", _tableName),
                "  ID INTEGER(12) NOT NULL PRIMARY KEY",
                ", DATE VARCHAR(14)",
                ", ITEM VARCHAR(16)",
                ", EMAIL VARCHAR(48)",
                ", NAME VARCHAR(48)",
                ", STREET VARCHAR(48)",
                ", CITY VARCHAR(48)",
                ", STATE VARCHAR(20)",
                ", ZIP VARCHAR(16)",
                ", UNITS INTEGER(8)",
                ", SHIPWHT INTEGER(8)",
                ");",
                ""
            };
        }

        public void AddInsert(Order o)
        {
            string sqlInsert = String.Format("INSERT INTO {0}(ID,DATE,ITEM,EMAIL,NAME,STREET,CITY,STATE,ZIP,UNITS,SHIPWHT) VALUES ({1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},{11});",
                _tableName,
                o.ID,
                o.Created,
                o.ItemName,
                o.Email,
                o.Name,
                o.mailingAddress.Street,
                o.mailingAddress.City,
                o.mailingAddress.State,
                o.mailingAddress.Zip,
                o.Quanity,
                o.Quanity * 9);

            _sqlFile.Add(sqlInsert);
        }

        public void WriteSqlFile()
        {
            if (!(Directory.Exists("./output")))
                Directory.CreateDirectory("./output");

                using (var fs = new FileStream("./output/gearhog-" + _tableName + ".sql", FileMode.CreateNew))
                {
                    var sw = new StreamWriter(fs);

                    foreach (string s in _sqlFile)
                    {
                        sw.WriteLine(s);
                    }
                    sw.Flush();
                }
            
        }
    }
}
