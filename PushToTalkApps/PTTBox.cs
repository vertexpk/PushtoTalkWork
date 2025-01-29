using System;
using System.Drawing;
using System.Windows.Forms;

namespace PushToTalkApps
{
    public class PTTBox : Panel
    {
        private Label titleLabel;
        private Label userLabel;
        private Button pttButton;
        private PictureBox transmitIcon;
        private PictureBox callTypeIcon;
        private bool isTransmitting = false;
        private bool isReceiving = false;
        private bool isGatewayConnected = true; // Default to connected

        public string Title
        {
            get { return titleLabel.Text; }
            set { titleLabel.Text = value; }
        }

        public bool IsGatewayConnected
        {
            get { return isGatewayConnected; }
            set
            {
                isGatewayConnected = value;
                UpdateStatus(); // Refresh UI status
            }
        }

        public Color BorderColor { get; set; } = Color.Blue;

        public event EventHandler PTTPressed;
        public event EventHandler PTTReleased;

        public PTTBox()
        {
            this.Size = new Size(180, 140);
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.FixedSingle;

            // Title Label
            titleLabel = new Label
            {
                Size = new Size(160, 20),
                Location = new Point(10, 5),
                Text = "Channel",
                Font = new Font("Arial", 9, FontStyle.Bold),
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // User Status Label
            userLabel = new Label
            {
                Size = new Size(160, 20),
                Location = new Point(10, 30),
                Text = "Available", // Default state
                Font = new Font("Arial", 9, FontStyle.Regular),
                ForeColor = Color.Green,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // PTT Button
            pttButton = new Button
            {
                Size = new Size(100, 30),
                Location = new Point(40, 70),
                Text = "PTT",
                BackColor = Color.LightGray,
                Font = new Font("Arial", 9, FontStyle.Bold)
            };
            pttButton.MouseDown += PTTButton_MouseDown;
            pttButton.MouseUp += PTTButton_MouseUp;

            // Transmit Icon (Hidden by Default)
            transmitIcon = new PictureBox
            {
                Size = new Size(20, 20),
                Location = new Point(150, 5),
                BackColor = Color.Transparent,
                Visible = false
            };
            transmitIcon.BackColor = Color.Red; // Placeholder for visualization

            // Call Type Icon (Private / Group) - Hidden initially
            callTypeIcon = new PictureBox
            {
                Size = new Size(20, 20),
                Location = new Point(10, 50),
                BackColor = Color.Transparent,
                Visible = false
            };
            callTypeIcon.BackColor = Color.Blue; // Placeholder for visualization

            // Add controls to the panel
            this.Controls.Add(titleLabel);
            this.Controls.Add(userLabel);
            this.Controls.Add(pttButton);
            this.Controls.Add(transmitIcon);
            this.Controls.Add(callTypeIcon);
        }

        // 🔴 PTT Pressed (MouseDown) - Transmitting
        private void PTTButton_MouseDown(object sender, MouseEventArgs e)
        {
            isTransmitting = true;
            isReceiving = false; // Cannot receive while transmitting
            UpdateStatus();

            PTTPressed?.Invoke(this, EventArgs.Empty);
        }

        // ⚪ PTT Released (MouseUp) - Return to Normal State
        private void PTTButton_MouseUp(object sender, MouseEventArgs e)
        {
            isTransmitting = false;
            UpdateStatus();

            PTTReleased?.Invoke(this, EventArgs.Empty);
        }

        // 📡 Receiving Call - Triggered externally
        public void StartReceiving()
        {
            isReceiving = true;
            isTransmitting = false; // Cannot transmit while receiving
            UpdateStatus();
        }

        // 📡 Stop Receiving Call
        public void StopReceiving()
        {
            isReceiving = false;
            UpdateStatus();
        }

        // ✅ Update the Status Label Based on Current State
        private void UpdateStatus()
        {
            if (isTransmitting)
            {
                userLabel.Text = "Transmitting";
                userLabel.ForeColor = Color.Red;
                pttButton.BackColor = Color.Red;
                transmitIcon.Visible = true;
                callTypeIcon.Visible = true;
            }
            else if (isReceiving)
            {
                userLabel.Text = "Receiving";
                userLabel.ForeColor = Color.Blue;
                pttButton.BackColor = Color.LightGray;
                transmitIcon.Visible = true;
                callTypeIcon.Visible = true;
            }
            else
            {
                transmitIcon.Visible = false;
                callTypeIcon.Visible = false;
                pttButton.BackColor = Color.LightGray;

                if (isGatewayConnected)
                {
                    userLabel.Text = "Available";
                    userLabel.ForeColor = Color.Green;
                }
                else
                {
                    userLabel.Text = "Unavailable";
                    userLabel.ForeColor = Color.Gray;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Pen pen = new Pen(BorderColor, 3))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
            }
        }
    }
}
