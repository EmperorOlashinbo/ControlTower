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
        public event EventHandler<AirplaneEventArgs> Landed;

        /// <summary>
        /// Initializes a new instance of the Airplane class with the specified flight number, destination, and flight
        /// time.
        /// </summary>
        /// <param name="flightNumber">The flight number assigned to the airplane.</param>
        /// <param name="destination">The destination of the flight.</param>
        /// <param name="flightTime">The duration of the flight in hours.</param>
        /// <exception cref="ArgumentNullException">Thrown when flightNumber is null.</exception>
        public Airplane(string flightNumber, string destination, double flightTime)
        {
            FlightNumber = flightNumber ?? throw new ArgumentNullException(nameof(flightNumber));
            Destination = destination ?? string.Empty;
            FlightTime = Math.Max(0.0, flightTime);
            FlightHeight = 0;
            InFlight = false;
            dispatchTimer = new Timer { Interval = 1000 };
            dispatchTimer.Tick += DispatchTimer_Tick;
        }
        /// <summary>
        /// Starts the airplane's flight, initializing flight parameters and notifying listeners of takeoff.
        /// </summary>
        public void Start()
        {
            if (InFlight) return;
            InFlight = true;
            departureTime = DateTime.Now;
            elapsedSeconds = 0;
            // Notify that the airplane is preparing for takeoff
            OnTakeOff(new AirplaneEventArgs(FlightNumber, $"Preparing for takeoff, heading for {Destination}"));
            // Start the timer to simulate flight time
            dispatchTimer.Start();
            OnTakeOff(new AirplaneEventArgs(FlightNumber, $"Took off, heading for {Destination}"));
        }
        /// <summary>
        /// Handles the timer tick event to simulate flight time and triggers landing when the flight time is reached.
        /// </summary>
        /// <param name="newAltitude">The new altitude to set for the airplane.</param>
        /// <returns>The updated flight height.</returns>
        public int ChangeAltitude(int newAltitude)
        {
            if (!InFlight)
            {
                // Not airborne; ignore change
                return -1;
            }

            FlightHeight = newAltitude;
            return FlightHeight;
        }
    }
}
