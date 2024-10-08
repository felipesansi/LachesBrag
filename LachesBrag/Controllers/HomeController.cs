﻿using LachesBrag.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using LachesBrag.Repositories;
using LachesBrag.Repositories.Interfaces;
using LachesBrag.ViewModel;

namespace LachesBrag.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILanchesRepository _lanchesRepository;

        public HomeController(ILanchesRepository lanchesRepository)
        {
            _lanchesRepository = lanchesRepository;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                LanchesPreferidos = _lanchesRepository.LanchesPreferidos
            };
            return View(homeViewModel);
        }

    
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}