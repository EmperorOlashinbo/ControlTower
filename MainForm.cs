using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlTower
{
    /// <summary>
    /// Represents the main form of the Airport Simulator application, responsible for managing the user interface and interactions with the control tower,
    /// including adding flights, authorizing takeoffs, changing altitudes, and removing flights.
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly ControlTower tower = new ControlTower();
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            // Subscribe to tower events
            tower.TakeOff += Tower_TakeOff;
            tower.Landed += Tower_Landed;
            tower.StatusUpdated += Tower_StatusUpdated;

            UpdateListView();
        }
        /// <summary>
        /// Handles the Click event of the BtnAddPlane button, validating input and adding a new flight to the control tower registry, 
        /// while also updating the log and list view accordingly.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void BtnAddPlane_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string id = txtFlightId.Text.Trim();
            string destination = txtDestination.Text.Trim();
            double time = (double)numFlightTime.Value;

            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("Provide a flight ID.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var plane = new Airplane(id, destination, time);
            tower.AddFlight(plane);
            AppendLog($"Added flight {plane.FlightNumber} to registry, destination {plane.Destination}");
            UpdateListView();
        }

        
    }
}
