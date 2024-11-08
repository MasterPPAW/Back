using LibrarieModele.DTOs;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using NivelAccesDate.Accessors;

namespace FitnessMVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly UsersAccessor _accessor;

        public UsersController(UsersAccessor accessor)
        {
            _accessor = accessor;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _accessor.GetUsers());
        }

        // GET: UsersController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View(await _accessor.GetUser(id));
        }

        // GET: UsersController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                await _accessor.CreateUser(userDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(userDTO);
        }

        // GET: UsersController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _accessor.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserDTO userDTO)
        {
            if (id != userDTO.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _accessor.UpdateUser(userDTO);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _accessor.UserExistsAsync(userDTO.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(userDTO);
        }

        // GET: UsersController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _accessor.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: UsersController/Delete/5
        // POST: Subscriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _accessor.DeleteUser(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
