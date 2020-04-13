using GameStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace GameStore.Pages.Helpers
{
    /// <summary>
    /// Перечисление которое содержит значения для типов данных, сохраняемых в сеансе.
    /// </summary>
    public enum SessionKey
    {
        CART,
        RETURN_URL
    }

    /// <summary>
    /// Клас содержащий статические методы для работы с данными сеанса.
    /// </summary>
    public static class SessionHelper
    {
        /// <summary>
        /// Метод для помещения нового объекта данных в состояние сеанса с использованием значения SessionKey.
        /// </summary>
        /// <param name="session">Сессия</param>
        /// <param name="key">Ключь</param>
        /// <param name="value">Значение</param>
        public static void Set(HttpSessionState session, SessionKey key, object value)
        {
            session[Enum.GetName(typeof(SessionKey), key)] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(HttpSessionState session, SessionKey key)
        {
            object dataValue = session[Enum.GetName(typeof(SessionKey), key)];
            if (dataValue != null && dataValue is T)
            {
                return (T)dataValue;
            }
            else
            {
                return default(T);
            }
        }

        public static Cart GetCart(HttpSessionState session)
        {
            Cart myCart = Get<Cart>(session, SessionKey.CART);
            if (myCart == null)
            {
                myCart = new Cart();
                Set(session, SessionKey.CART, myCart);
            }
            return myCart;
        }
    }
}