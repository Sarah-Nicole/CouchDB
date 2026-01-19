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
        List<CoffeeConsumption> coffees = [];
        coffees.Add(new CoffeeConsumption
        {
            EmployeeName = "Maya",
            CupsPerDay = 5,
            IsStillAlive = true,
            LastCupTime = DateTime.Today.AddDays(-2)
        });

        coffees.Add(new CoffeeConsumption
        {
            EmployeeName = "Sarah",
            CupsPerDay = 3,
            IsStillAlive = true,
            LastCupTime = DateTime.Today.AddDays(-2)
        });

        coffees.Add(new CoffeeConsumption
        {
            EmployeeName = "Vesel",
            CupsPerDay = 15,
            IsStillAlive = false,
            LastCupTime = DateTime.Today.AddDays(-2)
        });

        // AddOrUpdateRangeAsync() könnte für eine Liste auch verwendet werden.
        foreach (var coffee in coffees) {
            await context.CoffeeConsumption.AddAsync(coffee);
            Console.WriteLine("Neuer Datensatz angelegt.");
        }

        Console.WriteLine("Alle Datensätze angelegt.");

        // -------------------- Alle Dokumente --------------------
        var all = await context.CoffeeConsumption.ToListAsync();

        Console.WriteLine("Alle Dokumente:");
        all.ForEach(x =>
        {
            string aliveStatus = x.IsStillAlive ? "Still drinking☕😎" : "💀 R.I.P.";
            Console.WriteLine($"{x.EmployeeName} - {x.CupsPerDay} Tassen - {aliveStatus}");
        });

        Console.WriteLine("Alle Datensätze geladen.");

        // -------------------- Abfrage nach Name --------------------
        var sarahs = await context.CoffeeConsumption
            .Where(x => x.EmployeeName != null && x.EmployeeName == "Sarah")
            .ToListAsync();

        Console.WriteLine("Mitarbeiter mit 'Sarah':");
        sarahs.ForEach(x => Console.WriteLine($"{x.EmployeeName} - {x.CupsPerDay} Tassen"));

        Console.WriteLine("Datensätze gesucht.");

        // -------------------- Bestehenden Datensatz ändern --------------------
        var myCoffee = coffees[0];
        myCoffee.CupsPerDay = 7;
        myCoffee.LastCupTime = DateTime.Today;
        await context.CoffeeConsumption.AddOrUpdateAsync(myCoffee);
        Console.WriteLine("Datensatz aktualisiert.");

        // -------------------- Datensatz löschen --------------------
        await context.CoffeeConsumption.RemoveAsync(myCoffee);
        Console.WriteLine("Datensatz gelöscht.");
    }
}
