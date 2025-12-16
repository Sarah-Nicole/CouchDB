using CouchDbDemo;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var client = new CouchDbClient();

        // -------------------- Alle anzeigen --------------------
        var all = await client.GetAllAsync();
        Console.WriteLine("Alle Dokumente:");
        all.ForEach(x => Console.WriteLine($"{x.EmployeeName} - {x.CupsPerDay} Tassen"));

        // -------------------- Nach Name suchen --------------------
        var sarahs = await client.GetByNameAsync("Sarah");
        Console.WriteLine("Mitarbeiter mit 'Sarah':");
        sarahs.ForEach(x => Console.WriteLine($"{x.EmployeeName} - {x.CupsPerDay} Tassen"));

        // -------------------- Neuen Datensatz anlegen --------------------
        var newCoffee = new CoffeeConsumption
        {
            EmployeeName = "Maya",
            CupsPerDay = 5,
            IsStillAlive = true,
            LastCupTime = DateTime.UtcNow
        };

        await client.AddAsync(newCoffee);
        Console.WriteLine("Neuer Datensatz angelegt.");

        // -------------------- Datensatz ändern --------------------
        newCoffee.CupsPerDay = 7;
        newCoffee.LastCupTime = DateTime.UtcNow;
        await client.UpdateAsync(newCoffee);
        Console.WriteLine("Datensatz aktualisiert.");

        // -------------------- Datensatz löschen --------------------
        await client.DeleteAsync(newCoffee);
        Console.WriteLine("Datensatz gelöscht.");
    }
}
