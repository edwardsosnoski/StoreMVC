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
        CartDALModel SelectFromCartById(int id);
        bool InsertIntoCart(CartDALModel dalModel);
        bool DeleteFromCart(CartDALModel dalModel);
        bool UpdateQuantityInCart(CartDALModel dalModel);
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

        public CartDALModel SelectFromCartById(int id)
        {
            var sql = @"SELECT * FROM cart
                        WHERE ProductID = @ProductID";

            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                var result = connection.QueryFirstOrDefault<CartDALModel>(sql, new { ProductID = id });

                return result;
            }
        }

        public bool InsertIntoCart(CartDALModel dalModel)
        {
            var sql = $@"INSERT INTO cart (ProductID, ProductName, Quantity, Price)
                        VALUES (
                                @{nameof(dalModel.ProductID)},
                                @{nameof(dalModel.ProductName)},
                                @{nameof(dalModel.Quantity)},
                                @{nameof(dalModel.Price)}
                        )";

            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                var result = connection.Execute(sql, dalModel);

                if (result == 1)
                {
                    return true;
                }

                return false;
            }
        }

        public bool DeleteFromCart(CartDALModel dalModel)
        {
            var sql = @"DELETE FROM cart
                        WHERE ProductID = @ProductID";

            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                var result = connection.Execute(sql, dalModel);
                if (result == 1)
                {
                    return true;
                }

                return false;
            }
        }

        public bool UpdateQuantityInCart(CartDALModel dalModel)
        {
            var sql = @"UPDATE cart
                        SET Quantity = @Quantity";

            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                var result = connection.Execute(sql, dalModel);

                if (result == 1)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
