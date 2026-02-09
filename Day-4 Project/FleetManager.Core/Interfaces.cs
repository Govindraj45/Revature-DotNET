namespace FleetManager.Core;

public interface IMaintainable
{
    void ScheduleMaintenance(DateTime when);
}

public interface ITrackable
{
    string GetLocation();
}

// These two interfaces intentionally conflict to demonstrate explicit implementation.
public interface IStartable
{
    void Start();
}

public interface IRemoteStartable
{
    void Start();
}
