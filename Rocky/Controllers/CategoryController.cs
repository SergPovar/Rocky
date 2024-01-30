﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rocky_DataAccess;
using Rocky_DataAccess.Repository.IRepository;
using Rocky_Models;
using System.Collections.Generic;

namespace Rocky.Controllers
{
    //[Authorize(Roles = WC.AdminRole)]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _catRepo;
        public CategoryController(ICategoryRepository catRepo)
        {
            _catRepo = catRepo;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objlist = _catRepo.GetAll();
            return View(objlist);
        }
        //Get-Create
        public IActionResult Create()
        {
            return View();
        }
        //POST -Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _catRepo.Add(obj);
                _catRepo.Save();
                TempData[WC.Success] = "Category created successfuly";
                return RedirectToAction("Index");
            }
            TempData[WC.Error] = "Error while creating category";

            return View(obj);
        }
        //Get - Edit 
        public IActionResult Edit(int? id)
        {
            if(id==null|| id == 0)
            {
               
                return NotFound();
            }
            var obj = _catRepo.Find(id.GetValueOrDefault());
            if (obj==null) {
              
                return NotFound();
            }
            TempData[WC.Success] = "Category edited successfuly";
            return View(obj);
        }
        //POST -Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _catRepo.Update(obj);
                _catRepo.Save();
                TempData[WC.Success] = "Category edited successfuly";
                return RedirectToAction("Index");
            }
            TempData[WC.Error] = "Error while edit category";
            return View(obj);
        }
        //Get - Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
               
                return NotFound();
            }
            var obj = _catRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                
                return NotFound();
            }
            
            return View(obj);
        }
        //POST - Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _catRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
               
                return NotFound();
            }
             _catRepo.Remove(obj);
             _catRepo.Save();
            TempData[WC.Success] = "Category deleted successfuly";
            return RedirectToAction("Index");
         
        }
    }
}
