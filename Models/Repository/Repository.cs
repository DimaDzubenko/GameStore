using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

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