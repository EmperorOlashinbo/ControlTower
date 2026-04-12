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

        
    }
}
