using FleetManager.Core;

Console.WriteLine("Fleet Manager Demo");

var fleet = new List<Vehicle>
{
    new Car("Toyota", "Corolla", 30),
    new Truck("Volvo", "FH", 50)
};

foreach (var vehicle in fleet)
{
    Console.WriteLine(vehicle.Start());
    vehicle.ConsumeFuel(12);
    Console.WriteLine($"Id={vehicle.Id} Make={vehicle.Make} Model={vehicle.Model} Fuel={vehicle.FuelLevel}");
}

IMaintainable maintainable = fleet[0];
maintainable.ScheduleMaintenance(DateTime.Today.AddDays(30));
Console.WriteLine($"Next maintenance: {fleet[0].NextMaintenance:yyyy-MM-dd}");

ITrackable trackable = fleet[1];
fleet[1].SetLocation("Depot A");
Console.WriteLine($"Truck location: {trackable.GetLocation()}");

IStartable startable = fleet[1];
IRemoteStartable remoteStartable = fleet[1];
startable.Start();
remoteStartable.Start();
