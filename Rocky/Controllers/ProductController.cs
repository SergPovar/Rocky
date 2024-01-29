using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rocky_DataAccess;
using Rocky_DataAccess.Repository.IRepository;
using Rocky_Models;
using Rocky_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Rocky.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class ProductController :  Controller
    {
        private readonly IProductRepository _prodRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IProductRepository prodRepo, IWebHostEnvironment webHostEnvironment)
        {
            _prodRepo = prodRepo;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> objlist = _prodRepo.GetAll(includeProperties: "Category,ApplicationType");
           // foreach (var item in objlist)
            //{
              //  item.Category = _db.Category.FirstOrDefault(u=>item.CategoryId==u.Id);
                //item.ApplicationType = _db.ApplicationType.FirstOrDefault(u=>item.ApplicationTypeId==u.Id);
            //}

            return View(objlist);
        }
        //Get-Upsert
        public IActionResult Upsert(int? id)
        {
            //  IEnumerable<SelectListItem> CategoryDropDown = _db.Category.Select(i => new SelectListItem
            //{
            //  Text = i.Name,
            //Value = i.Id.ToString()
            //}); 

            //ViewBag.CategoryDropDown = CategoryDropDown;


            ProductVM productVM = new ProductVM
            {
                Product = new Product(),
                CategorySelectList = _prodRepo.GetAllDropdownList(WC.CategoryName),
                ApplicationTypeSelectList = _prodRepo.GetAllDropdownList(WC.ApplicationTypeName)
            };
            if (id == null)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = _prodRepo.Find(id.GetValueOrDefault());
            
                if (productVM.Product == null)
                {
                    return NotFound();
                }
                return View(productVM);
            }
          
        }
        //POST - Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;
                if (obj.Product.Id == 0)
                {
                    //create
                    string upload = webRootPath + WC.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extention = Path.GetExtension(files[0].FileName);

                    using (var filesrteam = new FileStream(Path.Combine(upload, fileName + extention), FileMode.Create))
                    {
                        files[0].CopyTo(filesrteam);
                    }
                    obj.Product.Image = fileName + extention;
                    _prodRepo.Add(obj.Product);
                   
                }
                else
                {
                    //update
                    var objFromDB = _prodRepo.FirstOrDefault(x=>x.Id == obj.Product.Id, isTracking:false);
                    if (files.Count > 0)
                    {
                        string upload = webRootPath + WC.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extention = Path.GetExtension(files[0].FileName);

                        var oldFile = Path.Combine(upload,objFromDB.Image);
                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }
                        using (var filesrteam = new FileStream(Path.Combine(upload, fileName + extention), FileMode.Create))
                        {
                            files[0].CopyTo(filesrteam);
                        }
                        obj.Product.Image = fileName + extention;
                    }
                    else {
                        obj.Product.Image = objFromDB.Image;
                    }
                    _prodRepo.Update(obj.Product);
                }
                _prodRepo.Save();
                return RedirectToAction("Index");
            }
            obj.CategorySelectList = _prodRepo.GetAllDropdownList(WC.CategoryName);
            obj.ApplicationTypeSelectList = _prodRepo.GetAllDropdownList(WC.ApplicationTypeName);
           
            return View(obj);
        }
     
        //Get - Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _prodRepo.FirstOrDefault(u=>u.Id==id,includeProperties: "Category,ApplicationType");
            //obj.Category = _db.Category.Find(obj.CategoryId);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }
        //POST - Delete
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _prodRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            string upload = _webHostEnvironment.WebRootPath + WC.ImagePath;
            var oldFile = Path.Combine(upload, obj.Image);
            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }
            _prodRepo.Remove(obj);
            _prodRepo.Save();
            return View(obj);

        }
    }
}
