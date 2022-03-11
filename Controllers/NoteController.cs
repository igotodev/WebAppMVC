using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppMVC.Data;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers;

public class NoteController : Controller
{
    private readonly ApplicationDbContext _db;

    public NoteController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        IEnumerable<NoteViewModel> list = await _db.NoteViewModels.ToListAsync();

        return View(list);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return await Task.Run(() => View());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(NoteViewModel model)
    {
        if (model.Name == model.Message)
        {
            ModelState.AddModelError("Name", "The Message cannot exactly match the Name.");
        }

        if (ModelState.IsValid)
        {
            await _db.NoteViewModels.AddAsync(model);
            await _db.SaveChangesAsync();
            TempData["success"] = "Note created successfully";

            return RedirectToAction("Index");
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var categoryFromDb = await _db.NoteViewModels.FindAsync(id);
        if (categoryFromDb == null)
        {
            return NotFound();
        }

        return View(categoryFromDb);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(NoteViewModel model)
    {
        if (model.Name == model.Message)
        {
            ModelState.AddModelError("Name", "The Message cannot exactly match the Name.");
        }

        if (ModelState.IsValid)
        {
            _db.NoteViewModels.Update(model);
            await _db.SaveChangesAsync();

            TempData["success"] = "Note updated successfully";
            return RedirectToAction("Index");
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var categoryFromDb = await _db.NoteViewModels.FindAsync(id);
        if (categoryFromDb == null)
        {
            return NotFound();
        }

        return View(categoryFromDb);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePost(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var categoryFromDb = await _db.NoteViewModels.FindAsync(id);
        if (categoryFromDb == null)
        {
            return NotFound();
        }

        _db.NoteViewModels.Remove(categoryFromDb);
        await _db.SaveChangesAsync();
        TempData["success"] = "Note deleted successfully";

        return RedirectToAction("Index");
    }
}