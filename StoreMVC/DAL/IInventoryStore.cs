using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreMVC.DAL
{
    public interface IInventoryStore
    {

    }

    public class InventoryStore
    {
        private readonly object _config;

        public InventoryStore(StoreMVCConfiguration config)
        {
            _config = config.Database;
        }
    }
}
