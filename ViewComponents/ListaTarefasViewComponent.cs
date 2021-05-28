using Agenda.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.ViewComponents
{
    public class ListaTarefasViewComponent : ViewComponent
    {
        private readonly AgendaContext Context;

        public ListaTarefasViewComponent(AgendaContext context)
        {
            Context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string data)
        {
            return View(await Context.Tarefas.OrderBy(t => t.Horario).Where(t => t.Data == data).ToListAsync());
        }
    }
}
