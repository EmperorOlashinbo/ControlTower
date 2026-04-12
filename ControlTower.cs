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
    }
}
