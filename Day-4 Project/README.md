# Fleet Manager

Console app that models a vehicle fleet with a small, testable domain model.

## Design Decisions
- Domain lives in `FleetManager.Core` to keep business logic separate from the console UI.
- `Vehicle` is abstract with encapsulated validation for `FuelLevel` (never below 0).
- `Truck` overrides `Start()` for polymorphism.
- `IMaintainable` and `ITrackable` show interface usage.
- `IStartable` and `IRemoteStartable` both define `Start()` and are implemented explicitly to avoid conflicts.
- A static helper field generates IDs for vehicles.

## How To Run
```bash
dotnet run --project FleetManager.App
```

## How To Test
```bash
dotnet test
```

## Known Limitations
- In-memory data only (no persistence).
- Simple location string instead of GPS coordinates.
- No real scheduling engine for maintenance.
