
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOPerations.UpdateBooks
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _dbcontext;
        public int BookId{get;set;}
         public UpdateBookModel Model{get;set;}


        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbcontext=dbContext;
        }

        public void Handle()
        {
            var book=_dbcontext.Books.SingleOrDefault(x=>x.Id==BookId);
            if(book is null)
                throw new InvalidOperationException("Güncelleneck Kitap bulunamadı.");

            book.GenreId=Model.GenreId!= default? Model.GenreId:book.GenreId;
            //book.PageCount=Model.PageCount!=default?Model.PageCount:book.PageCount;
            //book.PublishDate=Model.PublishDate!=default?Model.PublishDate:book.PublishDate;
            book.Title=Model.Title!=default?Model.Title:book.Title;
            _dbcontext.SaveChanges();
            
        }
    }

    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
                
    }
}