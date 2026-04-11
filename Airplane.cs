using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlTower
{
    /// <summary>
    /// Represents a method that handles changes to flight height and returns an integer result.
    /// </summary>
    /// <param name="newHeight">The new flight height value.</param>
    /// <returns>The result of processing the new flight height.</returns>
    public delegate int FlightHeightHandler(int newHeight);
    /// <summary>
    /// Represents an airplane with flight details such as flight number, destination, flight time, height, and flight
    /// status.
    /// </summary>
    public class Airplane
    {
        private readonly Timer dispatchTimer;
        private int elapsedSeconds;
        private DateTime departureTime;

        public string FlightNumber { get; }
        public string Destination { get; set; }
        public double FlightTime { get; set; }
        public int FlightHeight { get; private set; }
        public bool InFlight { get; private set; }

        /// <summary>
        /// Occurs when an airplane takes off.
        /// </summary>
        public event EventHandler<AirplaneEventArgs> TakeOff;
        /// <summary>
        /// Occurs when an airplane is landing.
        /// </summary>
        public event EventHandler<AirplaneEventArgs> Landing;
    }
}
