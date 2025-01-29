using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PushToTalkApps
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialize each PTTBox with proper values
            pttBox1.Title = " Radio Head 1";
        //    pttBox1.User = "1001 Tom";
            pttBox1.BorderColor = Color.Green;
       

            pttBox2.Title = "Radio Head 2";
         //   pttBox2.User = "Available";
            pttBox2.BorderColor = Color.Orange;
          

            pttBox3.Title = "Public 1 Digital";
         //   pttBox3.User = "Offline";
            pttBox3.BorderColor = Color.Orange;
    

            pttBox4.Title = "Public 2 Digital";
          //  pttBox4.User = "Busy";
            pttBox4.BorderColor = Color.Orange;
         

            // Ensure they are added to the form
            this.Controls.Add(pttBox1);
            this.Controls.Add(pttBox2);
            this.Controls.Add(pttBox3);
            this.Controls.Add(pttBox4);
        }
    }
}
