namespace ControlTower
{
    /// <summary>
    /// Partial class for the main form of the Airport Simulator application, responsible for defining the user interface components and layout,
    /// as well as handling user interactions and events.
    /// </summary>
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblFlightId;
        private System.Windows.Forms.Label lblDestination;
        private System.Windows.Forms.Label lblFlightTime;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtFlightId;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.NumericUpDown numFlightTime;
        private System.Windows.Forms.Button btnAddPlane;
        private System.Windows.Forms.Button btnTakeOff;
        private System.Windows.Forms.Button btnChangeAltitude;
        private System.Windows.Forms.Button btnRemoveFlight;
        private System.Windows.Forms.ListBox lstFlights;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.NumericUpDown numAltitude;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Text = "Airport Simulator - Control Tower";
            this.ClientSize = new System.Drawing.Size(900, 520);

            lblName = new System.Windows.Forms.Label { Left = 12, Top = 12, Width = 80, Text = "Name:" };
            txtName = new System.Windows.Forms.TextBox { Left = 100, Top = 10, Width = 200, Text = "Boeing 747 XL" };

            lblFlightId = new System.Windows.Forms.Label { Left = 12, Top = 42, Width = 80, Text = "Flight ID:" };
            txtFlightId = new System.Windows.Forms.TextBox { Left = 100, Top = 40, Width = 200, Text = "LFT 123" };

            lblDestination = new System.Windows.Forms.Label { Left = 12, Top = 72, Width = 80, Text = "Destination:" };
            txtDestination = new System.Windows.Forms.TextBox { Left = 100, Top = 70, Width = 200, Text = "New York" };

            lblFlightTime = new System.Windows.Forms.Label { Left = 12, Top = 102, Width = 80, Text = "Flight time (h):" };
            numFlightTime = new System.Windows.Forms.NumericUpDown { Left = 100, Top = 100, Width = 80, Minimum = 1, Maximum = 24, Value = 6 };

            btnAddPlane = new System.Windows.Forms.Button { Left = 12, Top = 140, Width = 140, Text = "Add Plane" };
            btnAddPlane.Click += BtnAddPlane_Click;

            btnTakeOff = new System.Windows.Forms.Button { Left = 160, Top = 140, Width = 140, Text = "Take Off" };
            btnTakeOff.Click += BtnTakeOff_Click;

            btnRemoveFlight = new System.Windows.Forms.Button { Left = 308, Top = 140, Width = 140, Text = "Remove Flight" };
            btnRemoveFlight.Click += BtnRemoveFlight_Click;

            lstFlights = new System.Windows.Forms.ListBox { Left = 320, Top = 10, Width = 560, Height = 280 };
            lstFlights.SelectedIndexChanged += LstFlights_SelectedIndexChanged;

            

        }

        #endregion

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
    }
}

