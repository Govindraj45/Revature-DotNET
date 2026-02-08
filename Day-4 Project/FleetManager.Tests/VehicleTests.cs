using FleetManager.Core;
using Xunit;

namespace FleetManager.Tests;

public class VehicleTests
{
    [Fact]
    public void FuelCannotDropBelowZero()
    {
        var car = new Car("Honda", "Civic", 5);
        car.ConsumeFuel(10);
        Assert.Equal(0, car.FuelLevel);
    }

    [Fact]
    public void TruckOverridesStart()
    {
        var truck = new Truck("Volvo", "FH", 20);
        string message = truck.Start();
        Assert.Contains("rumbling", message);
    }

    [Fact]
    public void ScheduleMaintenanceStoresDate()
    {
        var car = new Car("Toyota", "Camry", 10);
        DateTime when = DateTime.Today.AddDays(7);
        car.ScheduleMaintenance(when);
        Assert.Equal(when, car.NextMaintenance);
    }

    [Fact]
    public void TrackableReturnsLocation()
    {
        var truck = new Truck("Ford", "F-150", 25);
        truck.SetLocation("Dock 3");
        ITrackable trackable = truck;
        Assert.Equal("Dock 3", trackable.GetLocation());
    }
}
