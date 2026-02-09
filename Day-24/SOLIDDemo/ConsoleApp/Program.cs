using System.Diagnostics.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create the DI container (service registry).
            var services = new ServiceCollection();

            // DIP: register abstractions (interfaces) to implementations.
            services.AddScoped<IMessageReader, TwitterMessageReader>();
            services.AddScoped<IMessageWriter, InstagramMessageWriter>();
            services.AddScoped<IMessageWriter, PdfMessageWriter>();
            services.AddScoped<IMyLogger, ConsoleLogger>();
            services.AddScoped<App>();

            // Build the provider (object factory).
            var serviceProvider = services.BuildServiceProvider();

            // Ask container for App; it injects dependencies.
            var app = serviceProvider.GetService<App>();

            if (app != null)
            {
                app.Run();
            }

            // Violation of DIP - new keyword in front of custom classes
            //MessageReader _reader = new MessageReader();
            //MessageWriter _writer = new MessageWriter();
            //App _app = new App(_reader, _writer);
            //_app.Run();

            // Console.WriteLine("Hello, World!");
        }
    }

    public class App
    {
        IMessageReader _messageReader;
        IMessageWriter _messageWriter;

        // DIP: App depends on interfaces, not concrete classes.
        public App(IMessageReader reader, IMessageWriter writer)
        {
            _messageReader = reader;
            _messageWriter = writer;
        }

        public void Run()
        {
            // Read then write using abstractions.
            _messageWriter.WriteMessage(_messageReader.ReadMessage());
        }
    }

    // Violation of Interface Segregation Principle
    //public interface IMessagesApp
    //{
    //    string ReadMessage();

    //    void WriteMessage(string message);
    //}
    public interface IMessageReader
    {
        string ReadMessage();

    }

    // SRP: This class only reads messages.
    public class MessageReader : IMessageReader
    {
        public string ReadMessage() => "Hello, World!";
    }

    // OCP/LSP: Can swap this reader without changing App.
    public class TwitterMessageReader : IMessageReader
    {
        // twitter integration
        public string ReadMessage() => "Hello, From Twitter!";
    }

    public interface IMessageWriter
    {
        void WriteMessage(string message);
    }

    // SRP: This class only writes to console.
    public class MessageWriter : IMessageWriter
    {
        public void WriteMessage(string message)
        {
            Console.WriteLine(message);
        }
    }


    public interface IMyLogger
    {
        void Log();
    }

    // SRP: Logger only logs.
    public class ConsoleLogger : IMyLogger
    {
        public void Log()
        {
            Console.WriteLine("Entering Console");
        }
    }

    public class InstagramMessageWriter : IMessageWriter
    {
        IMyLogger _logger;
        // DIP: depends on IMyLogger, not ConsoleLogger.
        public InstagramMessageWriter(IMyLogger logger)
        {
            _logger = logger;
        }
        public void WriteMessage(string message)
        {
            _logger.Log();
            Console.WriteLine($"{message} posted to instagram");
        }
    }

    // OCP: New writer added without changing App.
    public class PdfMessageWriter : IMessageWriter
    {
        public void WriteMessage(string message)
        {
            Console.WriteLine($"PDF {message}");
        }
    }

    //public class MessagesApp : IMessagesApp
    //{
    //    public string ReadMessage() => "Hello World";
    //    public void WriteMessage(string message) => Console.WriteLine(message
    //}
}
