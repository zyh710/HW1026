using ConsoleApp1.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ConsoleApp1.Repository
{
    class OpenDataRepository
    {
        public string ConnectionString
        {
            get
            {
                return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JonathanHuang\Downloads\1019\ConsoleApp1\ConsoleApp1\AppData\Database.mdf;Integrated Security=True";
            }
            set => throw new NotImplementedException();
        }

        public void Insert(OpenData item)
        {
            var newItem = item;
            var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var command = new SqlCommand("", connection);
            command.CommandText = string.Format(@"INSERT INTO OpenData(名稱,地址,電話,傳真,服務區域)
                                                  VALUES              ('{0}',N'{1}',N'{2}',N'{3}',N'{4})
                                                 ", newItem.名稱, newItem.地址, newItem.電話, newItem.傳真, newItem.服務區域);

            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
