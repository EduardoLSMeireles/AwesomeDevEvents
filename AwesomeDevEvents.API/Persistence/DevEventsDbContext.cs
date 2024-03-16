using System;
using AwesomeDevEvents.API.Entities;
using AwesomeDevEvents.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AwesomeDevEvents.API.Persistence
{
	public class DevEventsDbContext : DbContext
	{
		public DevEventsDbContext(DbContextOptions<DevEventsDbContext> context) : base(context)
		{

		}

		public DbSet<DevEvent> DevEvents { get; set; }
	}
}
