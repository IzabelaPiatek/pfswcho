using System.Collections.Generic;
using System.IO;
using System.Text;
using MySql.Data.MySqlClient;

namespace APP
{
    public class DbManagement
    {
        public void CreateDb()
        {
            string sqlCreate = "CREATE DATABASE IF NOT EXISTS pfswcho;\r\nCREATE TABLE IF NOT EXISTS pfswcho.books\r\n(\r\n    id MEDIUMINT NOT NULL AUTO_INCREMENT,\r\n    title VARCHAR(30),\r\n    author VARCHAR(30),\r\n    rate INT(3),\r\n    PRIMARY KEY (id)\r\n)";

            using (var command = new MySqlCommand(sqlCreate, DbConnection.GetConnection()))
            {
                command.ExecuteNonQuery();
            }
        }

        public void InsertValues(string title, string author, int rate)
        {
            using (var command = new MySqlCommand($"INSERT INTO pfswcho.books(title, author, rate)VALUES(\"{ title }\", \"{ author }\", \"{ rate }\")", DbConnection.GetConnection()))
            {
                command.ExecuteNonQuery();
            }
        }

        public void UpdateValue(string columnName, string value, string whereColumnName, string whereValue)
        {
            using (var command = new MySqlCommand($"UPDATE pfswcho.books SET {columnName}=\"{value}\" WHERE {whereColumnName}=\"{whereValue}\"", DbConnection.GetConnection()))
            {
                command.ExecuteNonQuery();
            }
        }

        public List<Book> SelectValues()
        {
            var books = new List<Book>();

            using (var command = new MySqlCommand("SELECT id, title, author, rate FROM pfswcho.books", DbConnection.GetConnection()))
            {
                using (MySqlDataReader rdr = command.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        books.Add(new Book
                        {
                            Id = int.Parse(rdr["Id"].ToString()),
                            Name = rdr["Title"].ToString(),
                            Surname = rdr["Author"].ToString(),
                            Age = int.Parse(rdr["Rate"].ToString())
                        });
                    }
                }
            }

            return books;
        }

        public void DeleteValue(int id)
        {
            using (var command = new MySqlCommand($"DELETE FROM pfswcho.books WHERE id = {id}", DbConnection.GetConnection()))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}