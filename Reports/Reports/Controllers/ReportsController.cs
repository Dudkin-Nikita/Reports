using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Reports.Domain;
using Reports.Domain.Entities;

namespace Reports.Controllers
{
    public class ReportsController : Controller
    {
        private readonly DataManager dataManager;

        public ReportsController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        public IActionResult Create()
        {
            Report entity = new() { };
            return View(entity);
        }

        [HttpPost]
        public IActionResult Create(Report entity)
        {
            if (ModelState.IsValid)
            {
                if (entity.StartDate < entity.EndDate)
                {
                    List<Report> reports = dataManager.Reports.GetEntities().ToList();
                    List<Report> report = reports.Where(x => x.Address == entity.Address).ToList();
                    if (report.Count != 0)
                    {
                        foreach (Report rep in report)
                        if (rep.StartDate == entity.StartDate && rep.EndDate == entity.EndDate)
                        {
                            ModelState.AddModelError("", "Отчет с таким адресом и таким периодом уже существует");
                        }
                    }
                    else
                    {
                        dataManager.Reports.AddEntity(entity);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильная дата");
                }
            }
            return View(entity);
        }

        public IActionResult Edit(int id)
        {
            Report? entity = dataManager.Reports.GetEntityById(id);
            return View(entity);
        }

        [HttpPost]
        public IActionResult Edit(Report entity)
        {
            if (ModelState.IsValid)
            {
                if (entity.StartDate < entity.EndDate)
                { 
                    dataManager.Reports.UpdateEntity(entity);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Неправильная дата");
                }
            }
            return View(entity);
        }
    }
}