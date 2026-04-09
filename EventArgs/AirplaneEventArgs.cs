using System;

namespace ControlTower
{
    public class AirplaneEventArgs
    {
        public string FlightNumber { get; }
        public string Message { get; }
        public DateTime Timestamp { get; }

        public AirplaneEventArgs(string flightNumber, string message)
        {
            FlightNumber = flightNumber;
            Message = message;
            Timestamp = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Timestamp:HH:mm:ss} - {FlightNumber}: {Message}";
        }
    }
}