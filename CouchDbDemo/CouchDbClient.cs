using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CouchDB.Driver;
using CouchDB.Driver.Extensions;
using CouchDB.Driver.Query;

namespace CouchDbDemo
{

    public class CouchDbClient
    {
        private readonly CouchClient _client;
        private readonly ICouchDatabase<CoffeeConsumption> _dbCoffeeConsumption;

        public CouchDbClient()
        {
            _client = new CouchClient("http://localhost:5984", builder =>
            {
                builder.UseBasicAuthentication("admin", "admin");
            });

            _dbCoffeeConsumption = _client.GetDatabase<CoffeeConsumption>("coffeeconsumption");
        }

        // -------------------- Alle Dokumente --------------------
        public async Task<List<CoffeeConsumption>> GetAllAsync()
        {
            return await _dbCoffeeConsumption.ToListAsync();
        }

        // -------------------- Suche nach Name --------------------
        public async Task<List<CoffeeConsumption>> GetByNameAsync(string name)
        {
            // 
            return await _dbCoffeeConsumption.Where(x => x.EmployeeName == name).ToListAsync();
        }

        // -------------------- Neuen Datensatz anlegen --------------------
        public async Task AddAsync(CoffeeConsumption coffee)
        {
            // Id generieren, wenn nicht gesetzt
            if (string.IsNullOrEmpty(coffee.Id))
            {
                coffee.Id = Guid.NewGuid().ToString();
            }

            await _dbCoffeeConsumption.AddAsync(coffee);
        }

        // -------------------- Bestehenden Datensatz ändern --------------------
        public async Task UpdateAsync(CoffeeConsumption coffee)
        {
            await _dbCoffeeConsumption.AddOrUpdateAsync(coffee);
        }

        // -------------------- Datensatz löschen --------------------
        public async Task DeleteAsync(CoffeeConsumption coffee)
        {
            await _dbCoffeeConsumption.RemoveAsync(coffee);
        }
    }
}
