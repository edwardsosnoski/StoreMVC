using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using StoreMVC.Models;

namespace StoreMVC.DAL
{
    public interface ICartStore
    {
        IEnumerable<CartDALModel> SelectAllInCart();
    }

    public class CartStore : ICartStore
    {
        private readonly Database _config;

        public CartStore(StoreMVCConfiguration config)
        {
            _config = config.Database;
        }

        public IEnumerable<CartDALModel> SelectAllInCart()
        {
            var sql = @"SELECT * FROM cart";
            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                var result = connection.Query<CartDALModel>(sql) ?? new List<CartDALModel>();
                return result;
            }
        }
    }
}