using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wappa.Application;
using Wappa.DataAccess.Contracts;
using Wappa.Models;

namespace Wappa.ChallengeDev.Controllers
{
    public class TaxistaController : Controller
    {
        private ITaxistaFacade taxistaFacade;
        public TaxistaController(ITaxistaFacade taxistaFacade)
        {
            this.taxistaFacade = taxistaFacade;
        }
        public int PageSize = 5;


        public ViewResult Index(int page = 1, int order = 0, int orderby = 0)
        {
            var lista = taxistaFacade.PagedList(PageSize, page, order, orderby);
            var total = taxistaFacade.GetTotal();
            return View(new TaxistaListViewModel
            {
                Taxistas = lista,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = total,
                    Order = order,
                    OrderBy = orderby
                },
            });
        }

        public ViewResult Edit(int? id)
        {
            var taxista = id.HasValue ? taxistaFacade.Find(id.Value) : new Taxista();
            return View(taxista);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Taxista taxista)
        {
            if (ModelState.IsValid)
            {
                await taxistaFacade.Save(taxista);
                TempData["mensagemSucesso"] = $"{taxista.PrimeiroNome} foi salvo com sucesso";
                return RedirectToAction("Index");
            }
            else
                return View(taxista);
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            var deletedTaxista = taxistaFacade.Delete(id);
            if (id > 0)
                TempData["mensagemSucesso"] = "Taxista removido com sucesso";

            return RedirectToAction("Index");
        }

        public ViewResult Error()
        {
            return View();
        }

        public ViewResult IndexAngular() => View();
        public ViewResult EditAngular() => View();
    }
}