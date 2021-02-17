using System;
using System.Collections.Generic;
using System.Reflection;

namespace HomeworkRelectionLesson
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь до .dll или .exe файла.");
            string path = Console.ReadLine();

            Dictionary<string, string> typeDateDotNetVsSql = new Dictionary<string, string>
            {
                {"System.String", "NVARCHAR" },
                {"System.Int32", "INT"},                
                {"System.Double", "FLOAT"},
                {"System.Boolean", "BIT"},
                {"System.Byte", "TINYINT"},
                {"System.Char", "NVARCHAR"},
                {"System.Object", "SQL_VARIANT"},
                {"System.Int64", "BIGINT"},
                {"System.Decimal", "DECIMAL"},
                {"System.DateTime", "DATETIME"},
                {"System.Int16", "SMALLINT"},
                {"System.TimeSpan", "TIME"}               

            };
            Assembly myLibrary = Assembly.LoadFile(path);
            
            foreach(var type in myLibrary.GetTypes())
            {
                
                if (type.IsClass && !type.IsAbstract && !type.IsInterface && !type.IsEnum)
                {
                    if(type.Name == "Program")
                    {
                        continue;
                    }               
                    
                    Console.WriteLine($"CREATE TABLE {type.Name} (");
                    
                    foreach(var member in type.GetMembers())
                    {
                        if(member is PropertyInfo)
                        {                            
                            var propertyInfo = member as PropertyInfo;
                            foreach(var item in typeDateDotNetVsSql)
                            {
                                if(item.Key == propertyInfo.PropertyType.ToString())
                                {                                 
                                    Console.WriteLine($"{propertyInfo.Name}  {item.Value}");                                        
                                }
                                
                            }
                        }
                    }
                }
                Console.WriteLine(")");
               
            }
            
        }
    }
}
