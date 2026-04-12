using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlTower
{
    /// <summary>
    /// Represents a control tower that manages airplane flights, including takeoff, landing, and status updates.
    /// </summary>
    public class ControlTower
    {
        private readonly List<Airplane> flights = new List<Airplane>();

        public event EventHandler<AirplaneEventArgs> TakeOff;
        public event EventHandler<AirplaneEventArgs> Landed;
        public event EventHandler<AirplaneEventArgs> StatusUpdated;
        /// <summary>
        /// Gets a read-only list of airplanes currently managed by the control tower, allowing external access to flight information.
        /// </summary>
        public IReadOnlyList<Airplane> Flights => flights.AsReadOnly();
        /// <summary>
        /// Adds a new airplane to the control tower's list of flights and subscribes to its events,
        /// while also notifying listeners of the new registration and destination.
        /// </summary>
        /// <param name="plane">The airplane to add to the control tower.</param>
        /// <exception cref="ArgumentNullException">Thrown when the plane is null.</exception>
        public void AddFlight(Airplane plane)
        {
            if (plane == null) throw new ArgumentNullException(nameof(plane));
            flights.Add(plane);
            SubscribeToPlaneEvents(plane);
            StatusUpdated?.Invoke(this, new AirplaneEventArgs(plane.FlightNumber, $"registered, destination {plane.Destination}"));
        }
        /// <summary>
        /// Removes an airplane from the control tower's list of flights based on the specified index, 
        /// ensuring that the flight is not currently in the air before allowing removal,
        /// and notifies listeners of the removal.
        /// </summary>
        /// <param name="index">The index of the airplane to remove.</param>
        /// <param name="message">A message indicating the result of the removal operation.</param>
        /// <returns>True if the airplane was successfully removed; otherwise, false.</returns>
        public bool RemoveFlight(int index, out string message)
        {
            message = string.Empty;
            if (index < 0 || index >= flights.Count)
            {
                message = "Invalid index.";
                return false;
            }

            
        }
    }
}
