using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GameStore.Models.Repository
{
    /// <summary>
    /// Клас хранилище используется в качестве шлюза между бизнес-логикой приложения и базой данных.
    /// </summary>
    public class Repository
    {
        private EFDbContext context = new EFDbContext();

        /// <summary>
        /// Свойство возвращающее результаты чтения свойства с таким же именем в классе EFDbContext
        /// </summary>
        public IEnumerable<Game> Games
        {
            get { return context.Games; }
        }


        /// <summary>
        /// Чтение данных из таблицы Orders
        /// </summary>      
        public IEnumerable<Order> Orders
        {
            get
            {
                return context.Orders
                    .Include(o => o.OrderLines.Select(ol => ol.Game));
            }
        }


        public void SaveGame(Game game)
        {
            if (game.GameId == 0)
            {
                game = context.Games.Add(game);
            }
            else
            {
                Game dbGame = context.Games.Find(game.GameId);
                if (dbGame != null)
                {
                    dbGame.Name = game.Name;
                    dbGame.Description = game.Description;
                    dbGame.Price = game.Price;
                    dbGame.Category = game.Category;
                }
            }
            context.SaveChanges();
        }

        public void DeleteGame(Game game)
        {
            IEnumerable<Order> orders = context.Orders
                .Include(o => o.OrderLines.Select(ol => ol.Game))
                .Where(o => o.OrderLines
                    .Count(ol => ol.Game.GameId == game.GameId) > 0)
                .ToArray();

            foreach (Order order in orders)
            {
                context.Orders.Remove(order);
            }
            context.Games.Remove(game);
            context.SaveChanges();
        }

        /// <summary>
        /// Сохранить данные заказа в базе данных
        /// </summary>
        /// <param name="order"></param>        
        public void SaveOrder(Order order)
        {
            if (order.OrderId == 0)
            {
                order = context.Orders.Add(order);

                foreach (OrderLine line in order.OrderLines)
                {
                    context.Entry(line.Game).State
                        = EntityState.Modified;
                }

            }
            else
            {
                Order dbOrder = context.Orders.Find(order.OrderId);
                if (dbOrder != null)
                {
                    dbOrder.Name = order.Name;
                    dbOrder.Line1 = order.Line1;
                    dbOrder.Line2 = order.Line2;
                    dbOrder.Line3 = order.Line3;
                    dbOrder.City = order.City;
                    dbOrder.GiftWrap = order.GiftWrap;
                    dbOrder.Dispatched = order.Dispatched;
                }
            }
            context.SaveChanges();
        }
    }
}