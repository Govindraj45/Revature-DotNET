namespace FleetManager.Core;

public class Truck : Vehicle
{
    public Truck(string make, string model, int fuelLevel)
        : base(make, model, fuelLevel)
    {
    }

    public override string Start() => $"Truck {Id} Starting.";
}
