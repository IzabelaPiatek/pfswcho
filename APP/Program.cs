using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;

namespace APP
{
    class Program
    {
        static void Main(string[] args)
        {
            DbConnection.OpenConnection(DbConnection.GetConnection());
            DbManagement db = new DbManagement();
            
            db.CreateDb();

            var isAlive = true;

            while (isAlive)
            {
                Console.WriteLine("Biblioteka przeczytanych książek");
                Console.WriteLine("");
				
				Console.WriteLine("***Wybierz opcję***");
                Console.WriteLine("1) Dodaj książkę");
                Console.WriteLine("2) Edytuj książkę");
                Console.WriteLine("3) Wyświetl wszystkie książki");
                Console.WriteLine("4) Usuń książkę");
                Console.WriteLine("5) Zamknij bibliotekę");
				Console.WriteLine("");

                var data = Console.ReadLine();

                if (data == "5")
                    isAlive = false;
                else if (data == "1") //Dodaj
                {
                    Console.WriteLine("Wpisz tytuł książki: ");
                    var title = Console.ReadLine();
                    Console.WriteLine("Wpisz autora książki: ");
                    var author = Console.ReadLine();
                    Console.WriteLine("Wpisz twoją ocenę książki: ");
                    var rate = Int32.Parse(Console.ReadLine());
					Console.WriteLine("");

                    db.InsertValues(title, author, rate);
                }
				else if (data == "2") //Edytuj
                {
                    Console.WriteLine("Podaj id książki: ");

                    var id = Console.ReadLine();

                    Console.WriteLine("Wybierz wartość, która ma być modyfikowana");
                    Console.WriteLine("1) Tytuł");
                    Console.WriteLine("2) Autor");
                    Console.WriteLine("3) Ocena");
					Console.WriteLine("");

                    data = Console.ReadLine();
                    
                    string columnName = "";

                    if (data == "1")
                        columnName = "Title";
                    if (data == "2")
                        columnName = "Author";
                    if (data == "3")
                        columnName = "Rate";

                    Console.WriteLine("Wpisz nowe dane dla " + columnName);

                    string value = Console.ReadLine();

                    db.UpdateValue(columnName, value, "Id", id);

                    Console.WriteLine("Edycja przebiegła pomyślnie");
					Console.WriteLine("");
                }
                else if (data == "3") //Wyświetl
                {
                    List<Book> books = db.SelectValues();

                    foreach (var book in books)
                    {
                        Console.WriteLine("************************");
                        Console.WriteLine("*Id: " + book.Id);
                        Console.WriteLine("*Tytuł: " + book.Title);
                        Console.WriteLine("*Autor: " + book.Author);
                        Console.WriteLine("*Ocena: " + book.Rate);
                        Console.WriteLine("************************");
						Console.WriteLine("");
                    }
                }
                
                else if (data == "4") //Usuń
                {
                    Console.WriteLine("Podaj id: ");
                    int id = Int32.Parse(Console.ReadLine());

                    db.DeleteValue(id);

                    Console.WriteLine("Usunięto pomyślnie");
					Console.WriteLine("");
                }
            }
        }
    }
}
