using AutoMapper;
using LibraryManagementSystem.BLL.IServices;
using LibraryManagementSystem.DAL.Models;
using LibraryManagementSystem.PL.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.PL.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorService authorService, IMapper mapper)
        {
            this._authorService = authorService;
            this._mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int pageNumber =1)
        {
            var authorCount = await _authorService.GetAuthorCount();
            var pageCount = (int)Math.Ceiling(authorCount / 5.0);
            var authors = await _authorService.GetAllAsync(pageNumber);
            var authorsVm = _mapper.Map<IEnumerable<AuthorViewModel>>(authors);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.PageCount = pageCount;
            return View(authorsVm);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if (author == null)
                return NotFound();
            var authorVm = _mapper.Map<AuthorViewModel>(author);
            return View(authorVm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthorViewModel authorVm)
        {
            if (!ModelState.IsValid)
                return View(authorVm);
            if (await _authorService.FullNameIsExist(null,authorVm.FullName))
            {
                ModelState.AddModelError(nameof(authorVm.FullName), "Full name is already exist.");
                return View(authorVm);
            }
            if (await _authorService.EmailIsExist(null,authorVm.Email))
            {
                ModelState.AddModelError(nameof(authorVm.Email), "Email is already exist.");
                return View(authorVm);
            }
            var author = _mapper.Map<Author>(authorVm);
            await _authorService.AddAsync(author);
            var result = await _authorService.CompleteAsync();
            if (result > 0)
                return RedirectToAction(nameof(Index));
            ModelState.AddModelError(string.Empty, "Something went wrong.");
            return View(authorVm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id is null)
                return BadRequest();
            var author = await _authorService.GetByIdAsync(id.Value);
            if (author == null)
                return NotFound();
            var authorVm = _mapper.Map<AuthorViewModel>(author);
            return View(authorVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, AuthorViewModel authorVm)
        {
            if (id != authorVm.Id)
                return BadRequest();
            if (!ModelState.IsValid)
                return View(authorVm);
            if (await _authorService.FullNameIsExist(id,authorVm.FullName))
            {
                ModelState.AddModelError(nameof(authorVm.FullName), "Full name is already exist.");
                return View(authorVm);
            }
            if (await _authorService.EmailIsExist(id,authorVm.Email))
            {
                ModelState.AddModelError(nameof(authorVm.Email), "Email is already exist.");
                return View(authorVm);
            }
            var author = await _authorService.GetByIdAsync(id);
            if (author == null)
                return NotFound();
            author.Email = authorVm.Email;
            author.FullName = authorVm.FullName;
            author.Bio = authorVm.Bio;
            author.Website = authorVm.Website;
            _authorService.UpdateAsync(author);
            var result = await _authorService.CompleteAsync();
            if (result > 0)
                return RedirectToAction(nameof(Index));
            ModelState.AddModelError(string.Empty, "Something went wrong.");
            return View(authorVm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id is null)
                return BadRequest();
            var author = await _authorService.GetByIdAsync(id.Value);
            if (author == null)
                return NotFound();
            var authorVm = _mapper.Map<AuthorViewModel>(author);
            return View(authorVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id,Author authorVm)
        {
            if(id != authorVm.Id)
                return BadRequest();
            var author = await _authorService.GetByIdAsync(id);
            if (author == null)
                return NotFound();
            _authorService.DeleteAsync(author);
            var result = await _authorService.CompleteAsync();
            if (result > 0)
                return RedirectToAction(nameof(Index));
            ModelState.AddModelError(string.Empty, "Something went wrong.");
            return View(authorVm);
        }
    }
}
