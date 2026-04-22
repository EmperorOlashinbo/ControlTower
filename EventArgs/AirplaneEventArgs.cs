using System;

namespace ControlTower
{
    /// <summary>
    /// Provides event data for airplane related events, including flight number, message, and timestamp.
    /// </summary>
    public class AirplaneEventArgs : System.EventArgs
    {
        public string FlightNumber { get; }
        public string Message { get; }
        public DateTime Timestamp { get; }
        /// <summary>
        /// Initializes a new instance of the AirplaneEventArgs class with the specified flight number and message,
        /// setting the timestamp to the current date and time.
        /// </summary>
        /// <param name="flightNumber">The flight number associated with the event.</param>
        /// <param name="message">The message describing the event.</param>
        public AirplaneEventArgs(string flightNumber, string message)
        {
            FlightNumber = flightNumber;
            Message = message;
            Timestamp = DateTime.Now;
        }
        /// <summary>
        /// Returns a string representation of the object containing the timestamp, flight number, and message.
        /// </summary>
        /// <returns>A formatted string with the time, flight number, and message.</returns>
        public override string ToString()
        {
            return $"{Timestamp:HH:mm:ss} - {FlightNumber}: {Message}";
        }
    }
}