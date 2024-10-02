using AcademiaDHub.Models;
using AcademiaDHub.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcademiaDHub.Controllers
{
	public class ClassController : Controller
	{
		private readonly StudentContext context;
		public ClassController(StudentContext context)
		{
			this.context = context;
		}

		public IActionResult Index()
		{
			var ClassList = context.Classes.ToList();
			return View(ClassList);
		}



        // Action to display the form for creating a new class
        public IActionResult Create()
        {
            return View();
        }

        // POST: Action to handle form submission for creating a new class
        [HttpPost]
        public IActionResult Create(Class newClass)
        {
            if (!ModelState.IsValid)
            {
                return View(newClass);
            }

            context.Classes.Add(newClass);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var classEntity = context.Classes.Find(id);
            if (classEntity == null)
            {
                return RedirectToAction("Index");
            }

            // Return the entity directly to the view
            return View(classEntity);
        }

        [HttpPost]
        public IActionResult Edit(int id, Class classData)
        {
            var classEntity = context.Classes.Find(id);
            if (classEntity == null)
            {
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                return View(classData);
            }

            // Update the existing entity
            classEntity.ClassName = classData.ClassName;

            // Save changes
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        // Action to delete a class
        public IActionResult Delete(int id)
        {
            var classEntity = context.Classes.Find(id);
            if (classEntity == null)
            {
                return RedirectToAction("Index");
            }

            context.Classes.Remove(classEntity);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

    }

}