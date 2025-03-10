﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventify_Tutorial_Series.Domain.Common;
using Eventify_Tutorial_Series.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Eventify_Tutorial_Series.Persistence.DbContexts
{
	public class EventifyDbContext : DbContext
	{
		public DbSet<Event> Events { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Event>().OwnsOne(x=>x.Location);//bir event için location eklenıyor location un id si yok çünkü
			base.OnModelCreating(modelBuilder);
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseInMemoryDatabase("EventifyDb");
		}
		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)//değiliklik yapılan entitileri veritabanına kaydetmek için kullanılır
		{//asıl metot çalışmadan önce araya girmek
			var data = ChangeTracker.Entries<EntityBase>();
			foreach (var entry in data)
			{
				if(entry.State==EntityState.Added)
				   entry.Entity.CreateDate = DateTime.UtcNow;
				else if(entry.State==EntityState.Modified)
					entry.Entity.UpdateDate = DateTime.UtcNow;
			}
			return base.SaveChangesAsync(cancellationToken);
		}
	}
}
