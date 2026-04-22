using ControlTower.EventArgs;
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

            tower.FlightHeightChanged += Tower_FlightHeightChanged;

            UpdateListView();
        }

        /// <summary>
        /// Handles the FlightHeightChanged event of the control tower, appending the event information to the log and updating
        /// the list view to reflect any changes in flight altitude in the registry.
        /// </summary>
        private void Tower_FlightHeightChanged(object sender, FlightHeightEventArgs e)
        {
            // FlightHeightEventArgs overrides ToString; include it in log and refresh UI.
            AppendLog(e.ToString());
            UpdateListView();
        }

        /// <summary>
        /// Handles the Click event of the BtnAddPlane button, validating input and adding a new flight to the control tower registry, while also updating the log and list view accordingly.
        /// </summary>
        private void BtnAddPlane_Click(object sender, System.EventArgs e)
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

        /// <summary>
        /// Handles the Click event of the BtnTakeOff button, authorizing a selected flight for takeoff if it is not already in the air,
        /// and updating the log and list view based on the outcome of the operation.
        /// </summary>
        private void BtnTakeOff_Click(object sender, System.EventArgs e)
        {
            int idx = lstFlights.SelectedIndex;
            if (idx < 0)
            {
                MessageBox.Show("Select a flight to authorize takeoff.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (tower.OrderTakeOff(idx, out string message))
            {
                AppendLog(message);
                UpdateListView();
            }
            else
            {
                MessageBox.Show(message, "Takeoff", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Handles the Click event of the BtnRemoveFlight button, removing a selected flight if allowed.
        /// </summary>
        private void BtnRemoveFlight_Click(object sender, System.EventArgs e)
        {
            int idx = lstFlights.SelectedIndex;
            if (idx < 0)
            {
                MessageBox.Show("Select a flight to remove.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (tower.RemoveFlight(idx, out string message))
            {
                AppendLog(message);
                UpdateListView();
            }
            else
            {
                MessageBox.Show(message, "Remove Flight", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Handles the Click event of the BtnChangeAltitude button, allowing the user to change the altitude of a selected flight if it is currently in the air,
        /// and updating the log and list view based on the outcome of the operation.
        /// </summary>
        private void BtnChangeAltitude_Click(object sender, System.EventArgs e)
        {
            int idx = lstFlights.SelectedIndex;
            if (idx < 0)
            {
                MessageBox.Show("Select a flight to change altitude.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int newAlt = (int)numAltitude.Value;
            int result = tower.ChangeFlightHeight(idx, newAlt, out string message);
            AppendLog(message);

            if (result == -1)
            {
                MessageBox.Show(message, "Altitude", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            UpdateListView();
        }

        /// <summary>
        /// Handles the TakeOff event of the control tower, appending the event information to the log and updating the list view to reflect the current status of flights in the registry.
        /// </summary>
        private void Tower_TakeOff(object sender, AirplaneEventArgs e)
        {
            AppendLog(e.ToString());
            UpdateListView();
        }

        /// <summary>
        /// Handles the Landed event of the control tower, appending the event information to the log 
        /// and updating the list view to reflect the current status of flights in the registry.
        /// </summary>
        private void Tower_Landed(object sender, AirplaneEventArgs e)
        {
            AppendLog(e.ToString());
            UpdateListView();
        }

        /// <summary>
        /// Handles the StatusUpdated event of the control tower, appending the event information to the log and 
        /// updating the list view to reflect any changes in flight status or altitude in the registry.
        /// </summary>
        private void Tower_StatusUpdated(object sender, AirplaneEventArgs e)
        {
            AppendLog(e.ToString());
            UpdateListView();
        }

        /// <summary>
        /// Appends a message to the log text box, ensuring that the most recent messages
        /// are visible to the user by automatically scrolling to the end of the log.
        /// </summary>
        private void UpdateListView()
        {
            lstFlights.BeginUpdate();
            lstFlights.Items.Clear();
            foreach (var f in tower.Flights)
            {
                string status = f.InFlight ? $"Airborne, ALT {f.FlightHeight}" : "On ground";
                lstFlights.Items.Add($"{f.FlightNumber} -> {f.Destination} [{status}]");
            }
            lstFlights.EndUpdate();
        }

        /// <summary>
        /// Appends a message to the log text box with a timestamp, ensuring that the most recent 
        /// messages are visible to the user by automatically scrolling to the end of the log.
        /// </summary>
        private void AppendLog(string message)
        {
            string line = $"{DateTime.Now:HH:mm:ss} - {message}";
            txtLog.AppendText(line + Environment.NewLine);
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the lstFlights ListBox, 
        /// allowing the user to view details of the selected flight in the input fields for potential editing or review.
        /// </summary>
        private void LstFlights_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // Display selected flight details in the text boxes
            int idx = lstFlights.SelectedIndex;
            if (idx >= 0 && idx < tower.Flights.Count)
            {
                var f = tower.Flights[idx];
                txtFlightId.Text = f.FlightNumber;
                txtDestination.Text = f.Destination;
                numFlightTime.Value = (decimal)Math.Max(1, Math.Ceiling(f.FlightTime));
            }
        }
    }
}