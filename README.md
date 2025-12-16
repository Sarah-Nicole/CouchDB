**CouchDB Demo**
Dieses Projekt zeigt, wie man CouchDB mit C# (CouchDB.Driver) verwendet, um Daten zu speichern, abzufragen und zu bearbeiten.

**Voraussetzungen**
- Docker installiert
- .NET 8 SDK oder höher

**Statup**
*Docker-Container starten.*
Im Projektordner <docker-compose up -d> ausführen.

CouchDB ist nun erreichbar unter: http://localhost:5984/_utils
Login: admin / admin
Datenbank coffeeconsumptions wird automatisch erstellt

*Programm starten* 
dotnet build
dotnet run

**CRUD-Funktionen - Beispiele**
Add: await context.CoffeeConsumption.AddAsync(coffee)
Update: await context.CoffeeConsumption.UpdateAsync(coffee)
Delete: await context.CoffeeConsumption.RemoveAsync(coffee)
Query alle: await context.CoffeeConsumption.ToListAsync()
Query nach Name: LINQ Where(x => x.EmployeeName == "Sarah")
