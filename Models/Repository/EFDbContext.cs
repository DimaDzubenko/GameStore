using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GameStore.Models.Repository
{
    /// <summary>
    /// Клас асоциирующий модель с БД.
    /// </summary>
    public class EFDbContext : DbContext
    {
        /// <summary>
        /// Свойство по работе с таблицей Games.
        /// </summary>
        public DbSet<Game> Games { get; set; }

        /// <summary>
        /// Свойство по работе с таблицей Orders.
        /// </summary>
        public DbSet<Order> Orders { get; set; }
    }
}