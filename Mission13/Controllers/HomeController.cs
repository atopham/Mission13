using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mission13.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Controllers
{
    public class HomeController : Controller
    {
        //private BowlersDbContext _context { get; set; }

        private IBowlerRepository _context { get; set; }

        //public HomeController(BowlersDbContext temp)
        //{
        //    _context = temp;
        //}
        public HomeController(IBowlerRepository temp)
        {
            _context = temp;
        }

        //var someDate = _context.Teams.Include(x => "Teams").ToList();


        public IActionResult Index(int teamid, string teamname)
        {
            //var bowlersforpage = new BowlersViewModel
            //{
            //    Bowlers = _context.Bowlers.Where(b => b.TeamID == teamid).OrderBy(b => b.BowlerLastName)
            //};
            if (teamid == 0)
            {
                ViewBag.SelectedTeam = "";
            }
            else
            {
                //ViewBag.SelectedTeam = _context.Bowlers.Single(x => x.TeamID == teamid).Team;
                ViewBag.SelectedTeam = _context.Teams.Single(x => x.TeamID == teamid).TeamName;
            }
            
            var ListofBowlers = _context.Bowlers.Include("Team").Where(x => x.TeamID == teamid || teamid == 0).ToList();

            return View(ListofBowlers);
        }

        [HttpGet]
        public IActionResult BowlerChange()
        {
            ViewBag.Teams = _context.Teams.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult BowlerChange(Bowler b)
        {
            if (ModelState.IsValid)
            {

                var bowlers = _context.Bowlers.ToList();
                var max = 0;
                foreach (Bowler bowler in bowlers)
                {
                    if (max < bowler.BowlerID)
                    {
                        max = bowler.BowlerID;
                    }
                }
                b.BowlerID = max + 1;
                _context.Add(b);
                return View("Confirmation", b);
            }
            else
            {
                ViewBag.Teams = _context.Teams.ToList();
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int bowlerid)
        {
            ViewBag.Teams = _context.Teams.ToList();
            var bowler = _context.Bowlers.Single(x => x.BowlerID == bowlerid);
            return View("BowlerChange", bowler);
        }

        [HttpPost]
        public IActionResult Edit(Bowler b)
        {
            _context.Update(b);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int bowlerid)
        {
            var bowler = _context.Bowlers.Single(x => x.BowlerID == bowlerid);
            return View(bowler);
        }

        [HttpPost]
        public IActionResult Delete(Bowler bowler)
        {
            var b = _context.Bowlers.Single(x => x.BowlerID == bowler.BowlerID);
            _context.Delete(b);
            return RedirectToAction("Index");
        }


    }
}
