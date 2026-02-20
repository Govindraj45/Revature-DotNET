using System;

namespace DelegatesDemo
{
    // Publisher
    public class Button
    {
        public delegate void OnClickHandler();
        public event OnClickHandler OnClick;

        public void Click()
        {
            OnClick?.Invoke();
        }
    }

    public static class EventHandlerDemo
    {
        public static void Run()
        {
            Button button = new Button();

            // Subscribers
            button.OnClick += () => Console.WriteLine("Bell Rings!");
            button.OnClick += () => Console.WriteLine("Charge for Electricity!");
            button.OnClick += () => Console.WriteLine("Button clicked!");

            // Raise event
            button.Click();
        }
    }
}
