using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DVB
{
    public partial class DoTyper : Form
    {
        int speedtotype;
        int whattotypeloc;
        string whattotype;
        public bool whattotypecompleted;
        public DoTyper(string pWhatToType, int pSpeedToType)
        {
            InitializeComponent();
            if (pSpeedToType<1)
            {
                pWhatToType = pWhatToType.Replace("{BACKTAB}", "+{TAB}");
                pWhatToType = pWhatToType.Replace("{SELECTALL}", "^a");
                pWhatToType = pWhatToType.Replace("{COPY}", "+^c");
                SendKeys.Send(pWhatToType);
                tmrSendKeys2.Enabled = false;
                this.Dispose();
                return;
            }
            speedtotype = pSpeedToType;
            whattotype = pWhatToType;
            tmrSendKeys2.Interval = speedtotype;
            tmrSendKeys2.Start();
        }
        private void tmrSendKeys2_Tick(object sender, EventArgs e)
        {
            //tmrSendKeys2.Stop();
            Console.WriteLine("The Elapsed event was raised at {0}", "NOW");
            string test1 = "";
            string test2 = "";
            string whattotypeall = "";
            try
            {
                test1 = whattotype.Substring(whattotypeloc, 1);
            }
            catch (Exception ex)
            {
                whattotypecompleted = true;
                tmrSendKeys2.Enabled = false;
                this.Dispose();
                return;
            }
            if (test1 == "")
            {
                whattotypecompleted = true;
                tmrSendKeys2.Enabled = false;
                this.Dispose();
                return;
            }
            if (test1 == "{")
            {
                do
                {
                    whattotypeloc++;
                    test2 = whattotype.Substring(whattotypeloc, 1);
                    if (test2 == "}")
                    {
                        whattotypeall = "{" + whattotypeall + "}";
                        break;
                    }
                    else
                    {
                        whattotypeall = whattotypeall + test2;
                    }
                } while (true);
            }
            else
            {
                whattotypeall = test1;
            }

            //101-132 are uppercase
            string testchar = whattotypeall.Substring(0, 1);
            byte[] asciiBytes = Encoding.ASCII.GetBytes(testchar);
            int testcharval = asciiBytes[0];
            if ((testcharval > 64) && (testcharval < 91))
            {
                try
                {
                    SendKeys.Send("+" + whattotypeall);
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                try
                {
                    if (whattotypeall == "{BACKTAB}")
                    {
                        //!= ALT ^= CTRL += SHIFT #=WIN
                        SendKeys.Send("+{TAB}");
                        tmrSendKeys2.Interval = 100;
                    }
                    else if (whattotypeall == "{SELECTALL}")
                    {
                        SendKeys.Send("^a");
                    }
                    else if (whattotypeall == "{COPY}")
                    {
                        SendKeys.Send("^c");
                    }
                    else
                    {
                        SendKeys.Send(whattotypeall);
                        if (whattotypeall == "{TAB}")
                        {
                            tmrSendKeys2.Interval = 100;
                        }
                        else
                        {
                            tmrSendKeys2.Interval = speedtotype;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            whattotypeall = "";
            whattotypeloc++;
            Random rnd = new Random();
            tmrSendKeys2.Interval = rnd.Next(1, 500);
            tmrSendKeys2.Interval = speedtotype;

        }
    }
}
