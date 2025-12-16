using CouchDB.Driver;
using CouchDB.Driver.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchDbDemo
{
    public class CoffeeContext : CouchContext
    {
        // Datenbank für CoffeeConsumption-Dokumente
        // Achtung DB-Name muss Mehrzahl sein ("coffeeconsumptions")
        public CouchDatabase<CoffeeConsumption>? CoffeeConsumption { get; set; }

        protected override void OnConfiguring(CouchOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                // CouchDB URL
                .UseEndpoint("http://localhost:5984/")
                // DB automatisch erstellen, falls nicht vorhanden
                .EnsureDatabaseExists()
                // Login
                .UseBasicAuthentication("admin", "admin");
        }
    }
}
