using Microsoft.AspNetCore.Mvc;
using Reports.Domain;
using Reports.Domain.Entities;

namespace Reports.Controllers
{
    public class ItemsController : Controller
    {
        private readonly DataManager dataManager;

        public ItemsController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        public IActionResult Create(int id)
        {
            Report? report = dataManager.Reports.GetEntityById(id);
            if (report != null)
            {
                List<Item> items = new();
                if (report.ItemsCount != 0)
                { 
                    for (int i = 1; i <= report.ItemsCount - report.Items.Count; i++)
                    {
                        items.Add(new Item() { } );
                    }  
                }
                return View(items);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Create(List<Item> entities, int id)
        {
            if (ModelState.IsValid)
            {
                Report? report = dataManager.Reports.GetEntityById(id);
                if (report != null)
                {
                    foreach (Item entity in entities)
                    {
                        report.Items.Add(entity);  
                    }
                    dataManager.Reports.UpdateEntity(report);
                }
                return RedirectToAction("Index", "Home");
            }
            return View(entities);
        }

        public IActionResult Edit(int id)
        {
            Report? report = dataManager.Reports.GetEntityById(id);

            return View(report.Items);
        }

        [HttpPost]
        public IActionResult Edit(List<Item> entities, int id)
        {
            if (ModelState.IsValid)
            {
                Report? report = dataManager.Reports.GetEntityById(id);
                if (report != null)
                {
                    report.Items.Clear();
                    foreach (Item entity in entities)
                    {
                        report.Items.Add(entity);
                    }
                    dataManager.Reports.UpdateEntity(report);
                }
                return RedirectToAction("Index", "Home");
            }
            return View(entities);
        }
    }
}
