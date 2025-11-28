using AutoMapper;
using LibraryManagementSystem.BLL.IServices;
using LibraryManagementSystem.DAL.Enums;
using LibraryManagementSystem.DAL.Models;
using LibraryManagementSystem.PL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace LibraryManagementSystem.PL.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public BookController(IBookService bookService, IAuthorService authorService, IMapper mapper)
        {
            this._bookService = bookService;
            this._authorService = authorService;
            this._mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            var bookCount = await _bookService.GetBookCount();
            var pageCount = (int)Math.Ceiling(bookCount / 5.0);
            var books = await _bookService.GetAllAsync(pageNumber);
            var booksVm = _mapper.Map<IEnumerable<BookViewModel>>(books);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.PageCount = pageCount;
            return View(booksVm);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
                return NotFound();
            var bookVm = _mapper.Map<BookViewModel>(book);
            return View(bookVm);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var authors = await _authorService.GetForSelectAsync();
            ViewBag.Authors = new SelectList(authors, "Id", "FullName");
            ViewBag.Genre = new SelectList(Enum.GetNames(typeof(Genre)));
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BookViewModel bookVm)
        {
            if (!ModelState.IsValid)
                return View(bookVm);
            var book = _mapper.Map<Book>(bookVm);
            await _bookService.AddAsync(book);
            var result = await _bookService.CompleteAsync();
            if (result > 0)
                return RedirectToAction(nameof(Index));
            ModelState.AddModelError(string.Empty, "Something went wrong.");
            return View(bookVm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id is null)
                return BadRequest();
            var book = await _bookService.GetByIdAsync(id.Value);
            if (book == null) return NotFound();
            var authors = await _authorService.GetForSelectAsync();
            ViewBag.Authors = new SelectList(authors, "Id", "FullName");
            ViewBag.Genre = new SelectList(Enum.GetNames(typeof(Genre)));
            var bookVm = _mapper.Map<BookViewModel>(book);
            return View(bookVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, BookViewModel bookVm)
        {
            if (id != bookVm.Id)
                return BadRequest();
            if (!ModelState.IsValid)
                return View(bookVm);
            var book = await _bookService.GetByIdAsync(id);
            if (book == null) return NotFound();
            book.AuthorId = bookVm.AuthorId;
            book.Description = bookVm.Description;
            book.Title = book.Title;
            book.Genre = (Genre)Enum.Parse(typeof(Genre), bookVm.Genre);
            _bookService.UpdateAsync(book);
            var result = await _bookService.CompleteAsync();
            if (result > 0)
                return RedirectToAction(nameof(Index));
            ModelState.AddModelError(string.Empty, "Something went wrong.");
            return View(bookVm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id is null)
                return BadRequest();
            var book = await _bookService.GetByIdAsync(id.Value);
            if (book == null)
                return NotFound();
            var bookVm = _mapper.Map<BookViewModel>(book);
            return View(bookVm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id,BookViewModel bookVm)
        {
            if (id != bookVm.Id)
                return BadRequest();
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
                return NotFound();
            _bookService.DeleteAsync(book);
            var result = await _bookService.CompleteAsync();
            if (result > 0)
                return RedirectToAction(nameof(Index));
            ModelState.AddModelError(string.Empty, "Something went wrong.");
            return View(bookVm);
        }
    }
}
