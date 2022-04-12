using CarCaseTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCaseTest.Infrastructure.Persistence
{
    public class AdvertContext : DbContext
    {
        public AdvertContext(DbContextOptions options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Advert> Advert { get; set; }
        public DbSet<AdvertVisitHistory> AdvertVisitHistory { get; set; }
    }
}
