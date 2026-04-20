using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlTower.EventArgs
{
    /// <summary>
    /// Event data for flight height (altitude) related operations.
    /// </summary>
    public class FlightHeightEventArgs : System.EventArgs
    {
        /// <summary>
        /// Flight identifier (flight number).
        /// </summary>
        public string FlightNumber { get; }

        /// <summary>
        /// Requested or resulting altitude in feet/meters (as used by the application).
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Optional message providing additional context about the altitude change 
        /// (e.g., "Climbing to 30,000 feet", "Descending to 10,000 feet", "Altitude change denied due to traffic").
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Timestamp when the event args were created.
        /// </summary>
        public DateTime Timestamp { get; }
        /// <summary>
        /// Initializes a new instance of the FlightHeightEventArgs class with the specified flight number, height, and an optional message.
        /// </summary>
        /// <param name="flightNumber">The flight identifier (flight number).</param>
        /// <param name="height">The requested or resulting altitude in feet/meters.</param>
        /// <param name="message">An optional message providing additional context about the altitude change.</param>
        public FlightHeightEventArgs(string flightNumber, int height, string message = null)
        {
            FlightNumber = flightNumber ?? string.Empty;
            Height = height;
            Message = message ?? string.Empty;
            Timestamp = DateTime.Now;
        }
        /// <summary>
        /// Provides a string representation of the flight height event, 
        /// including the timestamp, flight number, and message or height information.
        /// </summary>
        /// <returns>A string representation of the flight height event.</returns>
        public override string ToString()
        {
            var msg = string.IsNullOrWhiteSpace(Message) ? $"altitude {Height}" : Message;
            return $"{Timestamp:HH:mm:ss} - {FlightNumber}: {msg}";
        }
    }
}
