using Microsoft.EntityFrameworkCore;

var _context = new CrmContext();

_context.Database.Migrate();
SeedData(_context);

var customers = _context.Customers.ToList();

Console.WriteLine("ALL CUSTOMERS");
foreach (var customer in customers)
{
    Console.WriteLine($"Customer ID: {customer.CustomerId}, Name: {customer.Name}, Email: {customer.Email}");
}

Console.WriteLine();
Console.WriteLine("FILTER -> SORT -> SELECT");
var resultsA = _context.Customers
    .Where(c => c.Email.Contains("@example.com"))
    .OrderBy(c => c.Name)
    .Select(c => new { c.CustomerId, c.Name, c.Email })
    .ToList();

foreach (var item in resultsA)
{
    Console.WriteLine($"Id: {item.CustomerId}, Name: {item.Name}, Email: {item.Email}");
}

Console.WriteLine();
Console.WriteLine("FILTER -> SELECT -> SORT");
var resultsB = _context.Customers
    .Where(c => c.Name.Length >= 4)
    .Select(c => new { c.Name, c.Email })
    .OrderBy(c => c.Name)
    .ToList();

foreach (var item in resultsB)
{
    Console.WriteLine($"Name: {item.Name}, Email: {item.Email}");
}

Console.WriteLine();
Console.WriteLine("PROJECTION + ANONYMOUS TYPE");
var projectionResults = _context.Customers
    .Select(c => new
    {
        c.Name,
        c.Email,
        NameLength = c.Name.Length,
        IsExampleEmail = c.Email.EndsWith("@example.com")
    })
    .ToList();

foreach (var item in projectionResults)
{
    Console.WriteLine(
        $"Name: {item.Name}, Email: {item.Email}, Length: {item.NameLength}, ExampleEmail: {item.IsExampleEmail}");
}

Console.WriteLine();
Console.WriteLine("JOIN -> CUSTOMERS WITH ORDERS");
var joinResults = _context.Customers
    .Join(
        _context.Orders,
        c => c.CustomerId,
        o => o.CustomerId,
        (c, o) => new
        {
            c.Name,
            c.Email,
            o.OrderId,
            o.OrderDate,
            o.TotalAmount
        })
    .OrderBy(x => x.Name)
    .ThenBy(x => x.OrderDate)
    .ToList();

foreach (var item in joinResults)
{
    Console.WriteLine(
        $"Customer: {item.Name}, OrderId: {item.OrderId}, Date: {item.OrderDate:yyyy-MM-dd}, Total: {item.TotalAmount}");
}

Console.WriteLine();
Console.WriteLine("EAGER LOADING (Include)");
var eagerCustomers = _context.Customers
    .Include(c => c.Orders)
    .OrderBy(c => c.Name)
    .ToList();

foreach (var customer in eagerCustomers)
{
    Console.WriteLine($"Customer: {customer.Name}, OrdersLoaded: {customer.Orders.Count}");
}

Console.WriteLine();
Console.WriteLine("EXPLICIT LOADING (Entry().Collection().Load())");
using (var explicitContext = new CrmContext())
{
    var explicitCustomer = explicitContext.Customers.First(c => c.Name == "Alice");
    explicitContext.Entry(explicitCustomer).Collection(c => c.Orders).Load();
    Console.WriteLine($"Customer: {explicitCustomer.Name}, OrdersLoaded: {explicitCustomer.Orders.Count}");
}

Console.WriteLine();
Console.WriteLine("LAZY LOADING (virtual navigation)");
using (var lazyContext = new CrmContext())
{
    var lazyCustomer = lazyContext.Customers.First(c => c.Name == "Bob");
    Console.WriteLine($"Customer: {lazyCustomer.Name}, OrdersLoadedOnAccess: {lazyCustomer.Orders.Count}");
}

static void SeedData(CrmContext context)
{
    if (!context.Customers.Any())
    {
        context.Customers.AddRange(
            new Customer { Name = "Alice", Email = "alice@example.com" },
            new Customer { Name = "Bob", Email = "bob@example.com" },
            new Customer { Name = "Sara", Email = "sara@example.com" }
        );
        context.SaveChanges();
    }

    if (!context.Orders.Any())
    {
        var aliceId = context.Customers.Where(c => c.Name == "Alice").Select(c => c.CustomerId).First();
        var bobId = context.Customers.Where(c => c.Name == "Bob").Select(c => c.CustomerId).First();

        context.Orders.AddRange(
            new Order { OrderDate = DateTime.UtcNow.Date.AddDays(-2), TotalAmount = 120.50m, CustomerId = aliceId },
            new Order { OrderDate = DateTime.UtcNow.Date.AddDays(-1), TotalAmount = 75.00m, CustomerId = bobId },
            new Order { OrderDate = DateTime.UtcNow.Date, TotalAmount = 199.99m, CustomerId = aliceId }
        );
        context.SaveChanges();
    }
}

public class CrmContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<CustomerType> CustomerTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        optionsBuilder.UseSqlServer(
            "Server=localhost,1433;Database=CrmDb;User Id=sa;Password=Revature@12345;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerType>()
            .HasData(
                new CustomerType { Id = 1, TypeName = "Regular" },
                new CustomerType { Id = 2, TypeName = "Premium" }
            );

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId);

        modelBuilder.Entity<Order>()
            .Property(o => o.TotalAmount)
            .HasPrecision(18, 2);
    }
}

public class Customer
{
    public int CustomerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

public class CustomerType
{
    public int Id { get; set; }
    public string TypeName { get; set; } = string.Empty;
}

public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }

    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; } = null!;
}
