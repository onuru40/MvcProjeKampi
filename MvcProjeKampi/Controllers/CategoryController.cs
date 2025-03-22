using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        CategoryManager cm = new CategoryManager(new EfCategoryDal());

        [Authorize] // Giriş yapınca görünsün
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetCategoryList()
        {
            var categoryValues = cm.GetList();
            return View(categoryValues);
        }
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category p)
        {
            //cm.CategoryAddBL(p);
            CategoryValidator categoryValidator = new CategoryValidator();
            ValidationResult results = categoryValidator.Validate(p); // CategoryValidator sınıfında olan değerlere göre doğruluk kontrolü yap. Geçerliliği kontrol et.
            if(results.IsValid) // Sonuç geçerli ise. Doğrulanmış ise. Case lere uygunsa.
            {
                cm.CategoryAddBL(p);
                return RedirectToAction("GetCategoryList");
            }
            else
            {
                foreach (var item in results.Errors) //Validation result tan gelen errorlar
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage); // Hataları ekle. Property ismini (Örnk. Category name) ekle. Ve hatanın kendisini ekle.
                }
            }
            return View();
        }
    }
}