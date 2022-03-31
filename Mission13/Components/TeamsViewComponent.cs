using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Mission13.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Components
{
    public class TeamsViewComponent : ViewComponent
    {
        private IBowlerRepository _repo { get; set; }

        public TeamsViewComponent(IBowlerRepository temp)
        {
            _repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            
            ViewBag.SelectedTeam = RouteData?.Values["teamname"];

            var teams = _repo.Bowlers.Select(x => x.Team).Distinct().OrderBy(x => x);

            return View(teams);
        }

    }
}
