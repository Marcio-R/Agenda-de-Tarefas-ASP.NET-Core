using Agenda.Data;
using Agenda.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.Controllers
{
    public class TarefasController : Controller
    {

        private readonly AgendaContext Context;

        public TarefasController(AgendaContext context)
        {
            Context = context;
        }

        public IActionResult Index()
        {
            return View(PegarDatas());
        }
        private List<DatasViewModel> PegarDatas()
        {
            DateTime dataAtual = DateTime.Now;
            DateTime dataLimite = DateTime.Now.AddDays(10);
            int qtdDias = 0;
            DatasViewModel data;
            List<DatasViewModel> listaDatas = new List<DatasViewModel>();
            while (dataAtual < dataLimite)
            {
                data = new DatasViewModel();
                data.Datas = dataAtual.ToShortDateString();
                data.Identificadores = "collapse" + dataAtual.ToShortDateString().Replace("/", "");
                listaDatas.Add(data);
                qtdDias = qtdDias + 1;
                dataAtual = DateTime.Now.AddDays(qtdDias);
            }
            return listaDatas;
        }
        [HttpGet]
        public IActionResult CriarTarefa(string dataTarefa)
        {
            Tarefas tarefas = new Tarefas
            {
                Data = dataTarefa
            };
            return View(tarefas);
        }
        [HttpPost]
        public async Task<IActionResult> CriarTarefa(Tarefas tarefas)
        {
            if (ModelState.IsValid)
            {
                Context.Tarefas.Add(tarefas);
                await Context.SaveChangesAsync();
                return RedirectPermanent(nameof(Index));
            }
            else
            {
                return View(tarefas);
            }
        }
        [HttpGet]
        public async Task<IActionResult> AtualizarTarefa(int id)
        {
            Tarefas tarefas = await Context.Tarefas.FindAsync(id);
            if (tarefas == null)
            {
                return NotFound();
            }
            else
            {
                return View(tarefas);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AtualizarTarefa(Tarefas tarefas)
        {
            if (ModelState.IsValid)
            {
                Context.Update(tarefas);
                await Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tarefas);
        }
        [HttpPost]
        public async Task<JsonResult> ExcluirTarefa(int id)
        {
            Tarefas tarefas = await Context.Tarefas.FindAsync(id);
            Context.Remove(tarefas);
            await Context.SaveChangesAsync();
            return Json(true);
        }
    }
}
