
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOPerations.CreateBook;
using WebApi.BookOPerations.DeleteBook;
using WebApi.BookOPerations.GetBookDetail;
using WebApi.BookOPerations.GetBooks;
using WebApi.BookOPerations.UpdateBooks;
using WebApi.DBOperations;

namespace WebApi.AddControlers
{
    [ApiController]
    [Route("[controller]s")]

    public class BookController:ControllerBase
    {
        
        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext context)
        {
            _context=context;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query=new GetBooksQuery(_context);
            var result=query.Handle();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            GetBookDetailQuery query=new GetBookDetailQuery(_context);
            try
            {
                query.BookId=id;
                result =query.Handle();
            }
            catch (Exception ex)
            {                
                return BadRequest(ex.Message);
            }
            
            return Ok(result);

        }

       
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command=new CreateBookCommand(_context);
            try
            {                
            command.Model=newBook;
            command.Handle();              
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
            return Ok();
        }

         [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookCommand command=new UpdateBookCommand(_context);
            try
            {
                command.BookId=id;
                command.Model=updatedBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

            return Ok();

        }

        [HttpDelete("{id}")]

        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command=new DeleteBookCommand(_context);
            try
            {
                command.BookId=id;
                command.Handle();
            }
            catch (Exception ex)
            {                
                return BadRequest(ex.Message);
            }
            return Ok();

        }


         
    }
    
}