using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace LOL_Ping_Checker
{
    public partial class Form1 : Form
    {
        Dictionary<string, string> Servers;
        String currentCheck = "";
        Form2 form2;

        public Form1()
        {
            InitializeComponent();
        }

        public static double PingTimeAverage(string host, int echoNum)
        {
            try
            {
                Ping pingSender = new Ping();
                PingReply reply = pingSender.Send(host, 120);
                if (reply.Status == IPStatus.Success)
                {
                    return reply.RoundtripTime;
                }
                return -1;
        
            }catch(Exception)
            {
                return 0;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Servers = new Dictionary<string, string>()
            {
                {"NA", "104.160.131.3"},
                {"EUW", "104.160.141.3"},
                {"EUNE", "104.160.142.3"},
                {"OCE", "104.160.156.1"},
                {"LAN", "104.160.136.3"},
                {"BR", "104.160.152.3"}
            };
            label1.Text = "";
            timer1.Interval = 800;
            form2 = null;
        }

        private void radio_CheckChanged(object sender, EventArgs e)
        {
            timer1.Stop();
            RadioButton btn = (RadioButton)sender;
            if (btn.Text.ToString() != "None")
            {
                currentCheck = btn.Text.ToString();
                if (!timer1.Enabled)
                    timer1.Start();
            }
            else
            {
                label1.Text = "";
            }
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            double temp = PingTimeAverage(Servers[currentCheck], 10);
            if (temp != 0)
            {
                if (temp != -1)
                    label1.Text = temp.ToString();
            }
            else
            {
                timer1.Stop();
                MessageBox.Show("Connection Error");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            form2 = new Form2();
            form2.Show();
        }

    }
}
