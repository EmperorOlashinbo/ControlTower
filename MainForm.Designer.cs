namespace ControlTower
{
    /// <summary>
    /// Partial class for the main form of the Airport Simulator application, responsible for defining the user interface components and layout,
    /// as well as handling user interactions and events.
    /// </summary>
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.Panel leftPanel;
        private System.Windows.Forms.GroupBox grpFlights;
        private System.Windows.Forms.GroupBox grpControls;
        private System.Windows.Forms.GroupBox grpLog;
        private System.Windows.Forms.ListBox lstFlights;

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblFlightId;
        private System.Windows.Forms.Label lblDestination;
        private System.Windows.Forms.Label lblFlightTime;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtFlightId;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.NumericUpDown numFlightTime;
        private System.Windows.Forms.NumericUpDown numAltitude;

        private System.Windows.Forms.Button btnAddPlane;
        private System.Windows.Forms.Button btnTakeOff;
        private System.Windows.Forms.Button btnRemoveFlight;
        private System.Windows.Forms.Button btnChangeAltitude;

        private System.Windows.Forms.TextBox txtLog;

        /// <summary>
        /// Cleans up any resources being used. This method is called when the form is being disposed, 
        /// allowing for proper cleanup of resources such as components and event handlers to prevent memory leaks and ensure efficient resource management.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated
        /// <summary>
        /// Required method for Designer support do not modify the contents of this method with the code editor.
        /// This method is responsible for initializing and configuring all the user interface components on the form,
        /// including setting their properties, arranging them in the layout, and attaching event handlers for user interactions. 
        /// Modifying this method manually can lead to issues with the designer and may cause unexpected behavior in the application, 
        /// so it is recommended to use the designer interface for making changes to the UI components.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // Main layout
            mainLayout = new System.Windows.Forms.TableLayoutPanel();
            mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            mainLayout.ColumnCount = 2;
            mainLayout.RowCount = 2;
            mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 320F)); // left panel fixed width
            mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));

            // Left panel (inputs & actions)
            leftPanel = new System.Windows.Forms.Panel();
            leftPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            leftPanel.Padding = new System.Windows.Forms.Padding(12);

            // Labels and inputs
            lblName = new System.Windows.Forms.Label { Text = "Name:", AutoSize = true, Left = 8, Top = 8 };
            txtName = new System.Windows.Forms.TextBox { Left = 100, Top = 4, Width = 200, Text = "Boeing 747 XL" };

            lblFlightId = new System.Windows.Forms.Label { Text = "Flight ID:", AutoSize = true, Left = 8, Top = 40 };
            txtFlightId = new System.Windows.Forms.TextBox { Left = 100, Top = 36, Width = 200, Text = "LFT 123" };

            lblDestination = new System.Windows.Forms.Label { Text = "Destination:", AutoSize = true, Left = 8, Top = 72 };
            txtDestination = new System.Windows.Forms.TextBox { Left = 100, Top = 68, Width = 200, Text = "New York" };

            lblFlightTime = new System.Windows.Forms.Label { Text = "Flight time (h):", AutoSize = true, Left = 8, Top = 104 };
            numFlightTime = new System.Windows.Forms.NumericUpDown
            {
                Left = 120,
                Top = 100,
                Width = 80,
                Minimum = 1,
                Maximum = 24,
                Value = 6
            };

            // Controls group (buttons)
            grpControls = new System.Windows.Forms.GroupBox();
            grpControls.Text = "Actions";
            grpControls.Left = 8;
            grpControls.Top = 140;
            grpControls.Width = 300;
            grpControls.Height = 110;
            grpControls.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            btnAddPlane = new System.Windows.Forms.Button { Text = "Add Plane", Width = 88, Height = 30, Left = 12, Top = 24 };
            btnAddPlane.Click += BtnAddPlane_Click;

            btnTakeOff = new System.Windows.Forms.Button { Text = "Take Off", Width = 88, Height = 30, Left = 110, Top = 24 };
            btnTakeOff.Click += BtnTakeOff_Click;

            btnRemoveFlight = new System.Windows.Forms.Button { Text = "Remove Flight", Width = 88, Height = 30, Left = 208, Top = 24 };
            btnRemoveFlight.Click += BtnRemoveFlight_Click;

            // Altitude controls placed within group for clarity
            numAltitude = new System.Windows.Forms.NumericUpDown { Left = 12, Top = 64, Width = 100, Minimum = 0, Maximum = 45000, Value = 10000 };
            btnChangeAltitude = new System.Windows.Forms.Button { Text = "Change Altitude", Width = 166, Height = 26, Left = 122, Top = 62 };
            btnChangeAltitude.Click += BtnChangeAltitude_Click;

            grpControls.Controls.Add(btnAddPlane);
            grpControls.Controls.Add(btnTakeOff);
            grpControls.Controls.Add(btnRemoveFlight);
            grpControls.Controls.Add(numAltitude);
            grpControls.Controls.Add(btnChangeAltitude);

            // Flights group on left - shows small preview (not the main list)
            grpFlights = new System.Windows.Forms.GroupBox();
            grpFlights.Text = "Selected flight preview";
            grpFlights.Left = 8;
            grpFlights.Top = 260;
            grpFlights.Width = 300;
            grpFlights.Height = 120;
            grpFlights.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            // Log group will be in bottom-left when window narrows
            grpLog = new System.Windows.Forms.GroupBox();
            grpLog.Text = "Status log (timestamps)";
            grpLog.Left = 8;
            grpLog.Top = 388;
            grpLog.Width = 300;
            grpLog.Height = 100;
            grpLog.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            // Right side: main flights list
            lstFlights = new System.Windows.Forms.ListBox();
            lstFlights.Dock = System.Windows.Forms.DockStyle.Fill;
            lstFlights.SelectedIndexChanged += LstFlights_SelectedIndexChanged;

            var flightsPanel = new System.Windows.Forms.Panel();
            flightsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            flightsPanel.Padding = new System.Windows.Forms.Padding(8);

            var flightsBox = new System.Windows.Forms.GroupBox { Text = "Registered flights", Dock = System.Windows.Forms.DockStyle.Fill };
            flightsBox.Controls.Add(lstFlights);
            flightsPanel.Controls.Add(flightsBox);

            // Log textbox (spans bottom row)
            txtLog = new System.Windows.Forms.TextBox
            {
                Multiline = true,
                ScrollBars = System.Windows.Forms.ScrollBars.Vertical,
                ReadOnly = true,
                Dock = System.Windows.Forms.DockStyle.Fill
            };

            var logBox = new System.Windows.Forms.GroupBox { Text = "Event Log", Dock = System.Windows.Forms.DockStyle.Fill };
            logBox.Controls.Add(txtLog);

            // Add controls to left panel
            leftPanel.Controls.Add(lblName);
            leftPanel.Controls.Add(txtName);
            leftPanel.Controls.Add(lblFlightId);
            leftPanel.Controls.Add(txtFlightId);
            leftPanel.Controls.Add(lblDestination);
            leftPanel.Controls.Add(txtDestination);
            leftPanel.Controls.Add(lblFlightTime);
            leftPanel.Controls.Add(numFlightTime);
            leftPanel.Controls.Add(grpControls);
            leftPanel.Controls.Add(grpFlights);
            leftPanel.Controls.Add(grpLog);

            // Place mainLayout children
            mainLayout.Controls.Add(leftPanel, 0, 0);
            mainLayout.SetRowSpan(leftPanel, 2); 

            mainLayout.Controls.Add(flightsPanel, 1, 0);
            mainLayout.Controls.Add(logBox, 1, 1);

            // Form settings
            this.Text = "Airport Simulator - Control Tower By Ibrahim";
            this.ClientSize = new System.Drawing.Size(980, 620);
            this.Controls.Add(mainLayout);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }

        #endregion
    }
}

