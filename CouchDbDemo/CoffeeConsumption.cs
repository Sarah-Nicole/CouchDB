using CouchDB.Driver.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CouchDbDemo
{
    // muss von CouchDocument erben (für _id und _rev)
    public class CoffeeConsumption : CouchDocument
    {
        [JsonProperty("employeeName")]
        public string? EmployeeName { get; set; }

        [JsonProperty("cupsPerDay")]
        public int CupsPerDay { get; set; }

        [JsonProperty("isStillAlive")]
        public bool IsStillAlive { get; set; }

        [JsonProperty("lastCupTime")]
        public DateTime LastCupTime { get; set; }
    }
}
