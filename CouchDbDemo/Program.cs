using CouchDB.Driver.Extensions;
using CouchDbDemo;

internal class Program
{
    private static async Task Main(string[] args)
    {
        //Nur für die Darstellung der Emojis.
        Console.OutputEncoding = System.Text.Encoding.UTF8; 

        await using var context = new CoffeeContext();

        if (context.CoffeeConsumption == null)
        {
            Console.WriteLine("Datenbank nicht vorhanden!");
            return;
        }

        // -------------------- Neuen Datensatz anlegen --------------------
        var newCoffee = new CoffeeConsumption
        {
            EmployeeName = "Maya",
            CupsPerDay = 5,
            IsStillAlive = true,
            LastCupTime = DateTime.Today
        };
        await context.CoffeeConsumption.AddAsync(newCoffee);
        Console.WriteLine("Neuer Datensatz angelegt.");


        // -------------------- Alle Dokumente --------------------
        var all = await context.CoffeeConsumption.ToListAsync();

        Console.WriteLine("Alle Dokumente:");
        all.ForEach(x =>
        {
            string aliveStatus = x.IsStillAlive ? "Still drinking☕😎" : "💀 R.I.P.";
            Console.WriteLine($"{x.EmployeeName} - {x.CupsPerDay} Tassen - {aliveStatus}");
        });

        // -------------------- Abfrage nach Name --------------------
        var sarahs = await context.CoffeeConsumption
            .Where(x => x.EmployeeName != null && x.EmployeeName == "Sarah")
            .ToListAsync();

        Console.WriteLine("Mitarbeiter mit 'Sarah':");
        sarahs.ForEach(x => Console.WriteLine($"{x.EmployeeName} - {x.CupsPerDay} Tassen"));

        // -------------------- Bestehenden Datensatz ändern --------------------
        newCoffee.CupsPerDay = 7;
        newCoffee.LastCupTime = DateTime.Today;
        await context.CoffeeConsumption.AddOrUpdateAsync(newCoffee);
        Console.WriteLine("Datensatz aktualisiert.");

        // -------------------- Datensatz löschen --------------------
        await context.CoffeeConsumption.RemoveAsync(newCoffee);
        Console.WriteLine("Datensatz gelöscht.");
    }
}
