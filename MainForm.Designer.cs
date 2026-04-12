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

