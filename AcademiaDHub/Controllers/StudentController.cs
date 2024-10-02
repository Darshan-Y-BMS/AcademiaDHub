using AcademiaDHub.Models;
using AcademiaDHub.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AcademiaDHub.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentContext context;
		private readonly IWebHostEnvironment environment;

		public StudentController(StudentContext context, IWebHostEnvironment environment)
        {
            this.context = context;
			this.environment = environment;
		}
        public IActionResult Index()
        {
            var students=context.Students.OrderByDescending(p=>p.EnrollmentNo).ToList();
            return View(students);
        }
		public IActionResult Create()
		{
			var ClassBag = context.Classes
				.Select(c => new SelectListItem
				{
					Value = c.ClassId.ToString(),
					Text = c.ClassName
				}).ToList();

			ViewBag.Classes = ClassBag;

			var SectionBag = context.Sections
				.Select(c => new SelectListItem
				{
					Value = c.SectionId.ToString(),
					Text = c.SectionName
				}).ToList();

			ViewBag.Sections = SectionBag;


			var HostelBag = context.Hostels
	.Select(c => new SelectListItem
	{
		Value = c.HostelId.ToString(),
		Text = c.HostelName+"  "+c.HostelGender
	}).ToList();

			ViewBag.Hosteles = HostelBag;

			return View();
		}
		[HttpPost]
		public IActionResult Create(StudentDto studentDto)
		{
            if (studentDto.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "The image file is required");
            }

            if (!ModelState.IsValid) 
            { 
                return View(studentDto);            
            }
				string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
				newFileName += Path.GetExtension(studentDto.ImageFile!.FileName);
				string imageFullPath = environment.WebRootPath + "/students/" + newFileName;
				using (var strem = System.IO.File.Create(imageFullPath))
				{
					studentDto.ImageFile.CopyTo(strem);
				}
		
			Student student = new Student()
			{
				EnrollmentNo = studentDto.EnrollmentNo,
				ImageFileName = newFileName,
				FirstName = studentDto.FirstName,
				LastName = studentDto.LastName,
				MotherTongue = studentDto.MotherTongue,
				Religion = studentDto.Religion,
				Gender = studentDto.Gender,
				Village = studentDto.Village,
				Town = studentDto.Town,
				State = studentDto.State,
				Email = studentDto.Email,
				PlaceOfBirth = studentDto.PlaceOfBirth,
				Mobile = studentDto.Mobile,
				AadharNumber = studentDto.AadharNumber,
				ClassId = studentDto.ClassId,
				SectionId = studentDto.SectionId,
				ParentId = studentDto.ParentId,
				GuardianId = studentDto.GuardianId,
				HostelerId = studentDto.HostelerId,
				DateOfBirth = studentDto.DateOfBirth,
				AdmissionDate = studentDto.AdmissionDate,
				IsActive = studentDto.IsActive
			};
			context.Students.Add(student);
			context.SaveChanges();
			return RedirectToAction("Index", "Student");
		}

		public IActionResult Edit(int id)
		{
			var student = context.Students.Find(id);
			if (student == null)
			{
			 return	RedirectToAction("Index", "Student");
			}

			var studentDto = new StudentDto()
			{
				EnrollmentNo = student.EnrollmentNo,
				Gender = student.Gender,
				FirstName = student.FirstName,
				LastName = student.LastName,
				MotherTongue = student.MotherTongue,
				Religion = student.Religion,
				Village = student.Village,
				Town = student.Town,
				State = student.State,
				Email = student.Email,
				PlaceOfBirth = student.PlaceOfBirth,
				Mobile = student.Mobile,
				AadharNumber = student.AadharNumber,
				ClassId = student.ClassId,
				SectionId = student.SectionId,
				ParentId = student.ParentId,
				GuardianId = student.GuardianId,
				HostelerId = student.HostelerId,
				DateOfBirth = student.DateOfBirth,
				AdmissionDate = student.AdmissionDate,
				IsActive = student.IsActive
			};

			ViewData["EnrollmentNo"] = student.EnrollmentNo;
			ViewData["ImageFileName"] = student.ImageFileName;

			return View(studentDto);



		}

		[HttpPost]
		public IActionResult Edit(int id,StudentDto studentDto)
		{
			var student = context.Students.Find(id);
			if (student == null)
			{
				return RedirectToAction("Index", "Student");
			}

			if (!ModelState.IsValid)
			{
				ViewData["EnrollmentNo"] = student.EnrollmentNo;
				ViewData["ImageFileName"] = student.ImageFileName;
				return View(studentDto);
			}

			string newFileName = student.ImageFileName;
			if (studentDto.ImageFile != null)
			{
				newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
				newFileName += Path.GetExtension(studentDto.ImageFile.FileName);
				string imageFullPath = environment.WebRootPath + "/students/" + newFileName;
				using (var strem = System.IO.File.Create(imageFullPath))
				{
					studentDto.ImageFile.CopyTo(strem);
				}
				string oldImageFullPath = environment.WebRootPath + "/students/" + student.ImageFileName;
				System.IO.File.Delete(oldImageFullPath);
			}
			student.ImageFileName = newFileName; 
			student.FirstName = studentDto.FirstName;
			student.LastName = studentDto.LastName;
			student.MotherTongue = studentDto.MotherTongue;
			student.Religion = studentDto.Religion;
			student.Gender = studentDto.Gender;
			student.Village = studentDto.Village;
			student.Town = studentDto.Town;
			student.State = studentDto.State;
			student.Email = studentDto.Email;
			student.PlaceOfBirth = studentDto.PlaceOfBirth;
			student.Mobile = studentDto.Mobile;
			student.AadharNumber = studentDto.AadharNumber;
			student.ClassId = studentDto.ClassId;
			student.SectionId = studentDto.SectionId;
			student.ParentId = studentDto.ParentId;
			student.GuardianId = studentDto.GuardianId;
			student.HostelerId = studentDto.HostelerId;
			student.DateOfBirth = studentDto.DateOfBirth;
			student.AdmissionDate = studentDto.AdmissionDate;
			student.IsActive = studentDto.IsActive;
			
			
			context.SaveChanges();

			return RedirectToAction("Index", "Student");
		}


		public IActionResult Delete(int id)
		{
			var student = context.Students.Find(id);
			if (student == null)
			{
				return RedirectToAction("Index", "Student");
			}
			string imageFullPath = environment.WebRootPath + "/students/" + student.ImageFileName;
			System.IO.File.Delete(imageFullPath);
			context.Students.Remove(student);
			context.SaveChanges(true);
			return RedirectToAction("Index", "Student");
		}

		public IActionResult Show(int id)
		{
			var student = context.Students.Find(id);
			if (student == null)
			{
				return RedirectToAction("Index");
			}

			return View(student);
		}



	}
}
