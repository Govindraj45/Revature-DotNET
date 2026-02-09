namespace FleetManager.Core;

public abstract class Vehicle : IMaintainable, ITrackable, IStartable, IRemoteStartable
{
    private static int _nextId = 1;

    private int _fuelLevel;
    private string _location = "Yard";
    private DateTime _nextMaintenance;

    protected Vehicle(string make, string model, int fuelLevel)
    {
        Id = _nextId++;
        Make = make;
        Model = model;
        FuelLevel = fuelLevel;
    }

    public int Id { get; }
    public string Make { get; }
    public string Model { get; }

    public int FuelLevel
    {
        get => _fuelLevel;
        protected set => _fuelLevel = value < 0 ? 0 : value;
    }

    public bool IsLowFuel => FuelLevel <= 10;

    public virtual string Start() => $"{GetType().Name} {Id} started.";

    public string Stop() => $"{GetType().Name} {Id} stopped.";

    public void Refuel(int amount)
    {
        if (amount <= 0) return;
        FuelLevel += amount;
    }

    public void ConsumeFuel(int amount)
    {
        if (amount <= 0) return;
        FuelLevel -= amount;
    }

    public void SetLocation(string location)
    {
        if (!string.IsNullOrWhiteSpace(location))
        {
            _location = location;
        }
    }

    public void ScheduleMaintenance(DateTime when)
    {
        _nextMaintenance = when;
    }

    public string GetLocation() => _location;

    public DateTime NextMaintenance => _nextMaintenance;

    // Explicit interface implementations to resolve Start() conflicts.
    void IStartable.Start() => Start();
    void IRemoteStartable.Start() => Start();
}
