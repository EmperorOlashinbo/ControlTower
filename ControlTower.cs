using ControlTower.CommonFiles;
using ControlTower.EventArgs;
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
        /// Event raised when a flight height (altitude) change occurs.
        /// Subscribers receive a <see cref="FlightHeightEventArgs"/>.
        /// </summary>
        public event EventHandler<FlightHeightEventArgs> FlightHeightChanged;

        /// <summary>
        /// Creates a new ControlTower with a default ListManager backing store.
        /// </summary>
        public ControlTower()
        {
            flights = new ListManager<Airplane>();
            flights.ItemAdded += Flights_ItemAdded;
            flights.ItemRemoved += Flights_ItemRemoved;
        }

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
           // SubscribeToPlaneEvents(plane);
           // StatusUpdated?.Invoke(this, new AirplaneEventArgs(plane.FlightNumber, $"registered, destination {plane.Destination}"));
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

            var plane = flights[index];
            if (plane.InFlight)
            {
                message = "Cannot remove an airborne flight.";
                return false;
            }

            // Unsubscribe to avoid leftover handlers
            UnsubscribeFromPlaneEvents(plane);

            if (flights.RemoveAt(index))
            {
                message = $"Flight {plane.FlightNumber} removed.";
                StatusUpdated?.Invoke(this, new AirplaneEventArgs(plane.FlightNumber, "removed from registry"));
                return true;
            }

            message = "Remove failed.";
            return false;
        }
        /// <summary>
        /// Subscribes to the takeoff and landing events of the specified airplane,
        /// allowing the control tower to respond to changes in flight status.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool OrderTakeOff(int index, out string message)
        {
            message = string.Empty;
            if (index < 0 || index >= flights.Count)
            {
                message = "Invalid flight selection.";
                return false;
            }

            var plane = flights[index];

            if (plane.InFlight)
            {
                message = "Plane already airborne.";
                return false;
            }

            // Ensure we are subscribed to its events (resubscribe if previously unsubscribed)
            SubscribeToPlaneEvents(plane);

            plane.StartTakeOff();
            message = $"Take-off authorized for {plane.FlightNumber}.";
            return true;
        }
        /// <summary>
        /// Subscribes to the takeoff and landing events of the specified airplane, 
        /// allowing the control tower to respond to changes in flight status.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="newHeight"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public int ChangeFlightHeight(int index, int newHeight, out string message)
        {
            message = string.Empty;
            if (index < 0 || index >= flights.Count)
            {
                message = "Invalid flight selection.";
                return -1;
            }

            var plane = flights[index];

            // The regular delegate is used here
            FlightHeightHandler handler = plane.ChangeAltitude;
            int result = handler?.Invoke(newHeight) ?? -1;

            if (result == -1)
            {
                message = $"Flight {plane.FlightNumber} is not airborne. Altitude not changed.";
            }
            else
            {
                message = $"Flight {plane.FlightNumber} altitude changed to {result}.";
                StatusUpdated?.Invoke(this, new AirplaneEventArgs(plane.FlightNumber, $"altitude changed to {result}"));
                FlightHeightChanged?.Invoke(this, new FlightHeightEventArgs(plane.FlightNumber, result, message));
            }

            return result;
        }
        /// <summary>
        /// Handles the event when an airplane is removed from the backing store by unsubscribing from its events and notifying listeners of the removal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Flights_ItemAdded(object sender, ItemEventArgs<Airplane> e)
        {
            // When an airplane is added to the backing store, subscribe to its events and notify listeners.
            var plane = e.Item;
            SubscribeToPlaneEvents(plane);
            StatusUpdated?.Invoke(this, new AirplaneEventArgs(plane.FlightNumber, $"registered, destination {plane.Destination}"));
        }
        /// <summary>
        /// Subscribes to the takeoff and landing events of the specified airplane, 
        /// allowing the control tower to respond to changes in flight status.
        /// </summary>
        /// <param name="plane">The airplane whose events to subscribe to.</param>
        private void SubscribeToPlaneEvents(Airplane plane)
        {
            // Prevent multiple subscriptions by removing first (safe)
            plane.TakeOff -= HandlePlaneTakeOff;
            plane.Landed -= HandlePlaneLanded;

            plane.TakeOff += HandlePlaneTakeOff;
            plane.Landed += HandlePlaneLanded;
        }
        /// <summary>
        /// Unsubscribes from the takeoff and landing events of the specified airplane,
        /// preventing the control tower from responding to changes in flight status.
        /// </summary>
        /// <param name="plane">The airplane whose events to unsubscribe from.</param>
        private void UnsubscribeFromPlaneEvents(Airplane plane)
        {
            plane.TakeOff -= HandlePlaneTakeOff;
            plane.Landed -= HandlePlaneLanded;
        }
        /// <summary>
        /// Handles the takeoff event of an airplane by invoking the TakeOff event and updating the status with the flight number and message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandlePlaneTakeOff(object sender, AirplaneEventArgs e)
        {
            TakeOff?.Invoke(this, e);
            StatusUpdated?.Invoke(this, new AirplaneEventArgs(e.FlightNumber, e.Message));
        }
        /// <summary>
        /// Handles the landing event of an airplane by unsubscribing from its events,
        /// invoking the Landed event, and updating the status with the flight number and message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandlePlaneLanded(object sender, AirplaneEventArgs e)
        {
            // Unsubscribe from this plane's events
            if (sender is Airplane plane)
            {
                UnsubscribeFromPlaneEvents(plane);
            }

            Landed?.Invoke(this, e);
            StatusUpdated?.Invoke(this, new AirplaneEventArgs(e.FlightNumber, e.Message));
        }
    }
}
