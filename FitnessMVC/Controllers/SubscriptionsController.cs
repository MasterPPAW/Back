using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NivelAccesDate.Accessors;
using LibrarieModele.DTOs;
using Microsoft.EntityFrameworkCore;
using LibrarieModele;

namespace FitnessMVC.Controllers
{
    public class SubscriptionsController : Controller
    {
        private readonly SubscriptionsAccessor _subscriptionsAccessor;
        private readonly UsersAccessor _usersAccessor;

        public SubscriptionsController(SubscriptionsAccessor accessor, UsersAccessor usersAccessor)
        {
            _subscriptionsAccessor = accessor;
            _usersAccessor = usersAccessor;
        }

        // GET: Subscriptions
        public async Task<IActionResult> Index()
        {
            var users = await _usersAccessor.GetUsers();
            ViewBag.Users = users.ToDictionary(u => u.UserId, u => u.Name);

            return View(await _subscriptionsAccessor.GetSubscriptions());
        }

        // GET: Subscriptions/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var sub = await _subscriptionsAccessor.GetSubscription(id);

            if (sub == null)
            {
                return NotFound();
            }

            var user = await _usersAccessor.GetUser(sub.UserId);
            ViewBag.User = user.Name;

            return View(sub);
        }

        // GET: Subscriptions/Create
        public async Task<IActionResult> Create()
        {
            var users = await _usersAccessor.GetUsers();
            ViewData["UserId"] = new SelectList(users, "UserId", "Name");
            return View();
        }

        // POST: Subscriptions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubscriptionDTO subscriptionDTO)
        {
            if (ModelState.IsValid)
            {
                await _subscriptionsAccessor.CreateSubscription(subscriptionDTO);
                return RedirectToAction(nameof(Index));
            }

            var users = await _usersAccessor.GetUsers();
            ViewData["UserId"] = new SelectList(users, "UserId", "Name", subscriptionDTO.UserId);
            return View(subscriptionDTO);
        }

        // GET: Subscriptions/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var subscription = await _subscriptionsAccessor.GetSubscription(id);
            if (subscription == null)
            {
                return NotFound();
            }

            var users = await _usersAccessor.GetUsers();
            ViewData["UserId"] = new SelectList(users, "UserId", "Name");

            return View(subscription);
        }

        // POST: Subscriptions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SubscriptionDTO subscriptionDTO)
        {
            if (id != subscriptionDTO.SubscriptionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _subscriptionsAccessor.UpdateSubscription(subscriptionDTO);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _subscriptionsAccessor.SubscriptionExistsAsync(subscriptionDTO.SubscriptionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }
            var users = await _usersAccessor.GetUsers();
            ViewData["UserId"] = new SelectList(users, "UserId", "Name", subscriptionDTO.UserId);
            return View(subscriptionDTO);
        }

        // GET: Subscriptions/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var sub = await _subscriptionsAccessor.GetSubscription(id);

            if (sub == null)
            {
                return NotFound();
            }

            var user = await _usersAccessor.GetUser(sub.UserId);
            ViewBag.User = user.Name;

            return View(sub);
        }

        // POST: Subscriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _subscriptionsAccessor.DeleteSubscription(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
