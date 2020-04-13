using System.Collections.Generic;
using System.Linq;

namespace GameStore.Models
{
    /// <summary>
    /// Клас представленеи корзины для покупок и ее содержимого.
    /// </summary>
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        /// <summary>
        /// Метод добавления товара
        /// </summary>
        /// <param name="game">Игры</param>
        /// <param name="quantity">Количество</param>
        public void AddItem(Game game, int quantity)
        {
            CartLine line = lineCollection
                .Where(p => p.Game.GameId == game.GameId)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Game = game,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        public void RemoveLine(Game game)
        {
            lineCollection.RemoveAll(l => l.Game.GameId == game.GameId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Game.Price * e.Quantity);

        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            lineCollection.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }

    /// <summary>
    /// Клас для представления выбранного пользователем товара и приобретаемого количества единиц этого товара.
    /// </summary>
    public class CartLine
    {
        public Game Game { get; set; }
        public int Quantity { get; set; }
    }
}