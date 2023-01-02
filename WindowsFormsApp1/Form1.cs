using Keystroke.API;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static bool flag = true;

        globalKeyboardHook gkh = new globalKeyboardHook();

        private void Form1_Load(object sender, EventArgs e)
        {
            string resxFile = @".\Resources.resx";
            try
            {
                using (ResXResourceReader resxReader = new ResXResourceReader(resxFile))
                {
                    foreach (DictionaryEntry item in resxReader)
                    {
                        if (item.Key.ToString() == "LogLocation")
                        {
                            label3.Text = Path.Combine(item.Value.ToString(), "data.log");
                        }
                    }
                }
            }
            catch 
            {

                
            }
            gkh.HookedKeys.Add(Keys.F1);
            gkh.HookedKeys.Add(Keys.Down);
            gkh.KeyDown += new KeyEventHandler(gkh_KeyDown);
        }

        void gkh_KeyDown(object sender, KeyEventArgs e)
        {
            if (flag)
            {
                this.Hide();
                flag = false;
            }
            else
            {
                this.Show();
                flag = true;
            }

            e.Handled = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
                Environment.SpecialFolder root = folderBrowserDialog1.RootFolder;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (ResXResourceWriter resx = new ResXResourceWriter(@".\Resources.resx"))
            {
                resx.AddResource("LogLocation", textBox1.Text);
            }
            MessageBox.Show("Settings saved successfully.");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            gkh.unhook();
        }
    }
}
