using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToSql
{
    class Program
    {
        static void Main(string[] args)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();

            var booksList = db.Books;

            foreach (Books book in booksList)
            {
                var query = from t in db.BookCategories
                            where t.Id == book.Category
                            select t.Name;
                var categoryName = query.SingleOrDefault();

                var categoryNameLambda = db.BookCategories.SingleOrDefault(t => t.Id == book.Category);
                Console.WriteLine("Book Id = {0} , Title = {1}, Price = {2}, Category = {3}",
                book.Id, book.Title, book.Price, categoryNameLambda.Name);
            }
            //Dodwanie nowej książki - Insert
            Books newBook = new Books();
            newBook.Title = "C# Praktyczny Kurs Programowania";
            newBook.Price = 49;
            newBook.Category = 2;

            db.Books.InsertOnSubmit(newBook);
            db.SubmitChanges();
            Console.WriteLine("*******************NEW BOOK ADDED*******************");
            foreach (Books book in booksList)
            {
                var categoryNameLambda = db.BookCategories.SingleOrDefault(t => t.Id == book.Category);
                Console.WriteLine("Book Id = {0} , Title = {1}, Price = {2}, Category = {3}",
                book.Id, book.Title, book.Price, categoryNameLambda.Name);
            }
            //Edytowanie wartości - Update
            Books changedBook = db.Books.FirstOrDefault(b => b.Title.Equals("C# Praktyczny Kurs Programowania"));
            changedBook.Price = 39;
            db.SubmitChanges();
            Console.WriteLine("*******************NEW BOOK CHANGED*******************");
            foreach (Books book in booksList)
            {
                var categoryNameLambda = db.BookCategories.SingleOrDefault(t => t.Id == book.Category);
                Console.WriteLine("Book Id = {0} , Title = {1}, Price = {2}, Category = {3}",
                book.Id, book.Title, book.Price, categoryNameLambda.Name);
            }
            //Usuwanie wartości - Delete
            Console.WriteLine("*******************BOOKS REMOVED*******************");


            //Books bookToDelete = db.Books.FirstOrDefault(b => b.Category == 5);
            /*while (bookToDelete != null)
            {
                db.Books.DeleteOnSubmit(bookToDelete);
                db.SubmitChanges();
                bookToDelete = db.Books.FirstOrDefault(b => b.Category == 5);
            }*/

            var queryToDelete = db.Books.Where(b => b.Category == 5);
            foreach (Books book in queryToDelete)
            {
                db.Books.DeleteOnSubmit(book);
            }
            db.SubmitChanges();
            foreach (Books book in booksList)
            {
                var categoryNameLambda = db.BookCategories.SingleOrDefault(t => t.Id == book.Category);
                Console.WriteLine("Book Id = {0} , Title = {1}, Price = {2}, Category = {3}",
                book.Id, book.Title, book.Price, categoryNameLambda.Name);
            }


            Console.ReadKey();
        }
    }
}
