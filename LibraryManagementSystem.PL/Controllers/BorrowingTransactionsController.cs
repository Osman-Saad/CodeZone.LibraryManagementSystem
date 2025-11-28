using AutoMapper;
using LibraryManagementSystem.BLL.IServices;
using LibraryManagementSystem.PL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace LibraryManagementSystem.PL.Controllers
{
    public class BorrowingTransactionsController : Controller
    {
        private readonly IBorrowTransactionService _borrowTransactionService;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BorrowingTransactionsController(IBorrowTransactionService borrowTransactionService, IBookService bookService, IMapper mapper)
        {
            this._borrowTransactionService = borrowTransactionService;
            this._bookService = bookService;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllAsync();
            var booksVm = _mapper.Map<IEnumerable<BookViewModel>>(books);
            return View(booksVm);
        }

        [HttpPost]
        public async Task<IActionResult> Borrow(Guid bookId)
        {
            try
            {
                await _borrowTransactionService.BorrowAsync(bookId);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Return(Guid bookId)
        {
            try
            {
                await _borrowTransactionService.ReturnAsync(bookId);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Search(string? status, DateOnly? borrowDate, DateOnly? returnDate)
        {
            var books = await _borrowTransactionService.ListTransactionsAsync(status, borrowDate, returnDate);
            var booksVm = _mapper.Map<IEnumerable<BookViewModel>>(books);
            return View(nameof(Index), booksVm);
        }

        [HttpGet]
        public async Task<IActionResult> Borrow()
        {
            var books = await _bookService.GetForSelectAsync();
            ViewBag.Books = books;
            return View();
        }
    }
}
