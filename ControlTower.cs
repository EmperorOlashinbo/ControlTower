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
    /// Represents a control tower that manages airplane flights, including takeoff, landing, altitude changes and status updates.
    /// Now supports constructor injection of the backing IListManager{Airplane} to allow tests to supply mocks.
    /// </summary>
    public class ControlTower
    {
        private readonly IListManager<Airplane> flights;

        public event EventHandler<AirplaneEventArgs> TakeOff;
        public event EventHandler<AirplaneEventArgs> Landed;
        public event EventHandler<AirplaneEventArgs> StatusUpdated;

        /// <summary>
        /// Event raised when a flight height (altitude) change occurs.
        /// Subscribers receive a <see cref="FlightHeightEventArgs"/>.
        /// </summary>
        public event EventHandler<FlightHeightEventArgs> FlightHeightChanged;

        /// <summary>
        /// Default constructor — creates a ControlTower with the default ListManager backing store.
        /// Keeps existing behaviour while enabling DI-friendly constructor overload for tests.
        /// </summary>
        public ControlTower()
            : this(new ListManager<Airplane>())
        {
        }

        /// <summary>
        /// Primary constructor that accepts an IListManager{Airplane} instance.
        /// Use this overload to inject mocks or alternative collection implementations in tests.
        /// </summary>
        /// <param name="flights">The backing IListManager{Airplane} to use (required).</param>
        public ControlTower(IListManager<Airplane> flights)
        {
            if (flights == null) throw new ArgumentNullException(nameof(flights));
            this.flights = flights;

            // Wire collection events so the tower can react to add/remove operations.
            this.flights.ItemAdded += Flights_ItemAdded;
            this.flights.ItemRemoved += Flights_ItemRemoved;
        }

        /// <summary>
        /// Gets a read-only snapshot of current flights.
        /// </summary>
        public IReadOnlyList<Airplane> Flights => flights.ToList().AsReadOnly();

        /// <summary>
        /// Adds a new airplane to the control tower's list of flights.
        /// </summary>
        public void AddFlight(Airplane plane)
        {
            if (plane == null) throw new ArgumentNullException(nameof(plane));
            // Add to underlying store; ItemAdded handler will subscribe and publish registration status.
            flights.Add(plane);
        }

        /// <summary>
        /// Removes an airplane from the control tower's list of flights based on the specified index.
        /// Ensures the flight is not airborne and unsubscribes from its events before removal.
        /// </summary>
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
        /// Authorize takeoff for a flight at the selected index.
        /// Ensures subscription is active (resubscribe when necessary).
        /// </summary>
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
        /// Change flight height using the regular delegate approach and publish a FlightHeightChanged event.
        /// </summary>
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
                // Publish both a general status update and a typed height change event
                StatusUpdated?.Invoke(this, new AirplaneEventArgs(plane.FlightNumber, $"altitude changed to {result}"));
                FlightHeightChanged?.Invoke(this, new FlightHeightEventArgs(plane.FlightNumber, result, message));
            }

            return result;
        }

        private void Flights_ItemAdded(object sender, ItemEventArgs<Airplane> e)
        {
            // When an airplane is added to the backing store, subscribe to its events and notify listeners.
            var plane = e.Item;
            SubscribeToPlaneEvents(plane);
            StatusUpdated?.Invoke(this, new AirplaneEventArgs(plane.FlightNumber, $"registered, destination {plane.Destination}"));
        }

        private void Flights_ItemRemoved(object sender, ItemEventArgs<Airplane> e)
        {
            // When removed from backing store, ensure unsubscribed (defensive).
            var plane = e.Item;
            UnsubscribeFromPlaneEvents(plane);
        }

        private void SubscribeToPlaneEvents(Airplane plane)
        {
            // Prevent multiple subscriptions by removing first (safe)
            plane.TakeOff -= HandlePlaneTakeOff;
            plane.Landed -= HandlePlaneLanded;

            plane.TakeOff += HandlePlaneTakeOff;
            plane.Landed += HandlePlaneLanded;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="plane"></param>
        private void UnsubscribeFromPlaneEvents(Airplane plane)
        {
            plane.TakeOff -= HandlePlaneTakeOff;
            plane.Landed -= HandlePlaneLanded;
        }
        /// <summary>
        /// Handles the airplane takeoff event by invoking the TakeOff and StatusUpdated events.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The AirplaneEventArgs containing event data.</param>
        private void HandlePlaneTakeOff(object sender, AirplaneEventArgs e)
        {
            TakeOff?.Invoke(this, e);
            StatusUpdated?.Invoke(this, new AirplaneEventArgs(e.FlightNumber, e.Message));
        }
        /// <summary>
        /// Handles the event when a plane has landed by unsubscribing from its events and raising the Landed and
        /// StatusUpdated events.
        /// </summary>
        /// <param name="sender">The airplane that triggered the event.</param>
        /// <param name="e">Event data containing information about the landed airplane.</param>
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
