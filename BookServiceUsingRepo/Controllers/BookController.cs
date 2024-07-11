using BookServiceUsingRepo.IRepo;
using BookServiceUsingRepo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookServiceUsingRepo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        IBookRepo _repo;
       
        public BookController(IBookRepo repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var _log4net = log4net.LogManager.GetLogger(typeof(BookController));
            _log4net.Info("Info from Log4net");
            _log4net.Error("eeee");
            //throw new NullReferenceException("nullllllll");
            return Ok(_repo.GetBooks());
        }


        [HttpGet("{id}")]
        public IActionResult GetBook(int id)
        {
            return Ok(_repo.GetBook(id));
        }

        [HttpPost]
        //[Authorize("Admin")]
        public IActionResult AddBook(Book book)
        {
            _repo.AddBook(book);
            return Created("ok", book);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book book)
        {
            _repo.EditBook(id, book);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            _repo.DeleteBook(id);
            return Ok();
        }
        }

    }

