using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Models
{
    public class EFBowlerRepository : IBowlerRepository
    {
        private BowlersDbContext _context { get; set; }
        public EFBowlerRepository(BowlersDbContext temp)
        {
            _context = temp;
        }
        public IQueryable<Bowler> Bowlers => _context.Bowlers;

        public IQueryable<Team> Teams => _context.Teams;

        public void Add(Bowler b)
        {
            _context.Add(b);
            _context.SaveChanges();

        }

        public void Update(Bowler b)
        {
            _context.Bowlers.Update(b);
            _context.SaveChanges();
        }

        public void Delete(Bowler b)
        {
            _context.Bowlers.Remove(b);
            _context.SaveChanges();
        }

    }
}
