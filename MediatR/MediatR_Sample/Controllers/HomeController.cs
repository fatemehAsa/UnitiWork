﻿using MediatR_Sample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Models;
using Services.Categories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR_Sample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryService _categoryService;

        public HomeController(ILogger<HomeController> logger, ICategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var list = await _categoryService.GetAllCategories(cancellationToken);
            return View(list);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellation)
        {
            var cat = await _categoryService.FindCategoryById(id, cancellation);
            return View(cat);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Category category, CancellationToken cancellationToken)
        {
            await _categoryService.UpdateCategory(category, cancellationToken);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            bool isExistCatInPost = await _categoryService.IsExistCategoryInPost(id, cancellationToken);
            ViewData["text"] = false;
            if (isExistCatInPost)
            {
                ViewData["text"] = true;
            }
            var cat = await _categoryService.FindCategoryById(id, cancellationToken);
            return View(cat);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Category category, CancellationToken cancellationToken)
        {
            int catId = category.GenreId;
            var result = await _categoryService.RemoveCategory(catId, cancellationToken);
            return RedirectToAction("Index");
        }
    }
}
