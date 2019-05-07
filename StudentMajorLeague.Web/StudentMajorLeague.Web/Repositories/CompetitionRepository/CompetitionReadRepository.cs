﻿using StudentMajorLeague.Web.Infrastructure;
using StudentMajorLeague.Web.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace StudentMajorLeague.Web.Repositories.CompetitionRepository
{
    public class CompetitionReadRepository : ICompetitionReadRepository
    {
        private SMLDbContext dbContext;

        public CompetitionReadRepository(SMLDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<Competition> GetByIdAsync(int id)
        {
            var result = dbContext.Competitions.Where(m => m.Id == id).SingleOrDefaultAsync();

            return result;
        }

        public Task<List<Competition>> GetAllAsync()
        {
            var result = dbContext.Competitions.ToListAsync();

            return result;
        }
    }
}