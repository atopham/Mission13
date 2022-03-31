using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Models
{
    public interface IBowlerRepository
    {
        public IQueryable<Bowler> Bowlers { get; }
        public IQueryable<Team> Teams { get; }

        public void Add(Bowler b);
        public void Update(Bowler b);
        public void Delete(Bowler b);

    }
}
