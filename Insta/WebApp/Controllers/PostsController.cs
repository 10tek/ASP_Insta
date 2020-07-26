using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Insta.DataAccess;
using Insta.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Insta.DataAccess.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using WebApp.Models.Post;
using System.IO;

namespace WebApp.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _map;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _environment;

        public PostsController(IUnitOfWork uow, IMapper map, UserManager<User> userManager, IWebHostEnvironment environment)
        {
            _uow = uow;
            _map = map;
            _userManager = userManager;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _uow.PostRepository.GetAllAsync();
            var viewModel = _map.Map<IList<PostIndexViewModel>>(posts);
            return View(viewModel);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var post = await _uow.PostRepository.GetAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            var viewModel = _map.Map<PostDetailViewModel>(post);
            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = _map.Map<Post>(viewModel);
                model.UserId = _userManager.GetUserId(HttpContext.User);

                if (viewModel.Photo != null)
                {
                    var path = "/Files/" + viewModel.Photo.FileName;

                    using (var fileStream = new FileStream(_environment.WebRootPath + path, FileMode.Create))
                    {
                        viewModel.Photo.CopyTo(fileStream);
                    }

                    model.PhotoPath = path;
                }

                var createdPost = await _uow.PostRepository.CreateAsync(model);
                await _uow.Save();

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _uow.PostRepository.GetAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            var viewModel = _map.Map<PostEditViewModel>(post);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PostEditViewModel postEditViewModel)
        {
            if (id != postEditViewModel.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var post = _map.Map<Post>(postEditViewModel);
                    await _uow.PostRepository.EditAsync(post);
                    await _uow.Save();
                }
                catch (DbUpdateConcurrencyException)

                {
                    if (!PostExists(postEditViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(postEditViewModel);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var patient = await _uow.PostRepository.GetAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            var viewModel = _map.Map<PostDeleteViewModel>(patient);
            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var post = await _uow.PostRepository.GetAsync(id);
            _uow.PostRepository.Remove(post);
            await _uow.Save();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<Post> AddLike(Guid id)
        {
            var post = await _uow.PostRepository.GetAsync(id);
            post.Likes.Add(new Like()
            {
                UserId = _userManager.GetUserId(HttpContext.User),
                PostId = id
            });

            await _uow.Save();
            return post;
        }

        public async Task<IActionResult> GetPosts()
        {
            var posts = await _uow.PostRepository.GetAllAsync();
            var viewModel = _map.Map<IList<PostIndexViewModel>>(posts);

            var user = _userManager.Users.FirstOrDefault(u => u.Email == HttpContext.User.Identity.Name);
            ViewBag.UserId = user?.Id;

            return PartialView(viewModel);
        }

        private bool PostExists(Guid id)
        {
            return _uow.PostRepository.Exist(id);
        }
    }
}
