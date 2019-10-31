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
        bool AddToCart(StoreDALModel dalModel);
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

        public bool AddToCart(StoreDALModel dalModel)
        {
            var sql = $@"INSERT INTO cart (ProductID, ProductName, Quantity, Price)
                        VALUES (
                                @{nameof(dalModel.ProductID)},
                                @{nameof(dalModel.ProductName)},
                                @{nameof(dalModel.Quantity)},
                                @{nameof(dalModel.Price)}";

            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                int result = 0;

                if(dalModel.Quantity > 0)
                {
                    result = connection.Execute(sql, dalModel);
                }

                if (result == 1)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
