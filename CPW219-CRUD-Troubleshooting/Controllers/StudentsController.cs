using CPW219_CRUD_Troubleshooting.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CPW219_CRUD_Troubleshooting.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext context;

        public StudentsController(SchoolContext dbContext)
        {
            context = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            List<Student> students = await (from student in context.Students
                                            select student).ToListAsync();
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student p)
        {
            if (ModelState.IsValid)
            {
                context.Students.Add(p);
                await context.SaveChangesAsync();

                ViewData["Message"] = $"{p.Name} was added!";
                return View();
            }

            //Show web page with errors
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            //get the product by id
            Student? pToEdit = await context.Students.FindAsync(id);
            if (pToEdit == null)
            {
                return NotFound();
            }

            //show it on web page
            return View(pToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student pModel)
        {
            if (ModelState.IsValid)
            {
                context.Students.Update(pModel);
                await context.SaveChangesAsync();

                ViewData["Message"] = $"{pModel.Name} has been Updated!";
                return RedirectToAction("Index");
            }
            //return view with errors
            return View(pModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Student? pToDelete = await context.Students.FindAsync (id);
            if (pToDelete == null)
            {
                return NotFound();
            }

            return View(pToDelete);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            //Get Product from database
            Student? pToDelete = await context.Students.FindAsync(id);
            if (pToDelete != null)
            {
               context.Students.Remove(pToDelete);
                await context.SaveChangesAsync();
                TempData["Message"] = pToDelete.Name + "deleted successfully";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            Student? pDetails = await context.Students.FindAsync(id);
            
            if (Details == null) 
            { 
                return NotFound();
            }

            return View(pDetails);
        }
    }
}
