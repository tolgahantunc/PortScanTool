using NetTools;
using PortScanTool.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PortScanTool
{
    public partial class Form1 : Form
    {
        private IPAddress m_StartIP;
        private IPAddress m_EndIp;

        private ObservableCollection<Data> m_IPsAndPorts;

        private Thread m_Thread;
        private bool m_IsAborted;
        private Int16 m_TaskNumber;

        struct PortRange { public IPAddress IP; public int StartPort; public int EndPort; }

        public Form1()
        {
            InitializeComponent();

            lvResult.View = View.Details;

            m_IPsAndPorts = new ObservableCollection<Data>();
            m_IPsAndPorts.CollectionChanged += this.OnCollectionChanged;
        }

        /// <summary>
        /// Updates Result list on the UI.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    try
                    {
                        Data d = (Data)item;
                        ListViewItem lvi = new ListViewItem(d.IP.ToString());
                        lvi.SubItems.Add(d.Port.ToString());
                        lvi.SubItems.Add(d.IsOpen.ToString());
                        lvResult.Invoke((MethodInvoker)delegate
                        {
                            lvResult.Items.Add(lvi);
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unexpected exception! For more details please check Windows Event Logs.");
                        Program.Log.WriteEntry($"Exception occured. Message: {ex.Message}");
                    }
                }

            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var item in e.NewItems)
                {
                    try
                    {
                        Data d = (Data)item;
                        IEnumerable<ListViewItem> listViewItems = lvResult.Items.Cast<ListViewItem>();
                        ListViewItem lvi = listViewItems.Where(x => x.Text == d.IP.ToString() &&
                                                  x.SubItems[0].Text == d.Port.ToString() &&
                                                  x.SubItems[1].Text == d.IsOpen.ToString()).FirstOrDefault();


                        lvResult.Invoke((MethodInvoker)delegate
                        {
                            lvResult.Items.Remove(lvi);
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unexpected exception! For more details please check Windows Event Logs.");
                        Program.Log.WriteEntry($"Exception occured. Message: {ex.Message}");
                    }
                }
            }
        }

        private void trckBarParallelTaskNumber_Scroll(object sender, EventArgs e)
        {
            var tb = sender as TrackBar;
            lblParallelTaskNumberValue.Text = tb.Value.ToString();
            if (!Int16.TryParse(tb.Value.ToString(), out m_TaskNumber))
            {
                MessageBox.Show("Task number cannot be parseble. For more details please check Windows Event Logs.");
                Program.Log.WriteEntry($"Exception occured. Task number cannot be parseble: {tb.Value.ToString()}");
                return;
            }
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            SetPropertiesOfUIControls(true);

            m_Thread = new Thread(() => StartScan());
            m_Thread.IsBackground = true;
            m_Thread.Start();
        }

        /// <summary>
        /// Get ready to scan ports.
        /// </summary>
        private void StartScan()
        {
            lvResult.Invoke((MethodInvoker)delegate
            {
                lvResult.Items.Clear();
            });

            bool isValid = ValidateIPs();
            if (!isValid)
            {
                MessageBox.Show("Invalid IP(s). Check IP addresses entered.");
                return;
            }

            List<IPAddress> lsIPs = GetAllIPAddresses();
            if (lsIPs == null || lsIPs.Count == 0)
            {
                MessageBox.Show("No IP found in the range between IPs you entered.");
                return;
            }

            StartScan(lsIPs);

            SetPropertiesOfUIControls(false);
        }

        /// <summary>
        /// Validate entered IP addresses.
        /// </summary>
        /// <returns>True if both of them are valid, False if at least one of them is not valid.</returns>
        private bool ValidateIPs()
        {
            if (!IPAddress.TryParse(tbIPStart.Text, out m_StartIP) || !IPAddress.TryParse(tbIPEnd.Text, out m_EndIp))
            {
                MessageBox.Show("IP addresses are not valid. For more details please check Windows Event Logs.");
                Program.Log.WriteEntry($"Exception occured. IP addresses are not valid: {tbIPStart.Text} and {tbIPEnd.Text}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get all IP addresses exist in the range defined by the user.
        /// </summary>
        /// <returns></returns>
        private List<IPAddress> GetAllIPAddresses()
        {
            try
            {
                string ipRange = $"{m_StartIP.ToString()} - {m_EndIp.ToString()}";
                IPAddressRange iar = IPAddressRange.Parse(ipRange);
                List<IPAddress> result = iar.AsEnumerable().Select(x => x).ToList();
                return result;
            }
            catch (ArgumentNullException ane)
            {
                MessageBox.Show("ArgumentNullException! For more details please check Windows Event Logs.");
                Program.Log.WriteEntry($"Exception occured. Message: {ane.Message}");
                return null;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Begin must be smaller than the"))
                    MessageBox.Show("First IP must be smaller than the second IP.");
                else
                    MessageBox.Show("Unexpected exception! For more details please check Windows Event Logs.");

                Program.Log.WriteEntry($"Exception occured. Message: {ex.Message}");

                return null;
            }
        }

        /// <summary>
        /// Create one task per IP address.
        /// </summary>
        /// <param name="IPAddresses"></param>
        /// <returns></returns>
        public void StartScan(List<IPAddress> IPAddresses)
        {
            Action[] actions = new Action[m_TaskNumber];

            foreach (IPAddress ip in IPAddresses)
            {
                int startPort, endPort, nextStartPort, nextEndPort, maxPort, initialEndPort;
                maxPort = 65535;

                //Calculate total parallel actions need to be run
                if (m_TaskNumber > 0)
                {
                    if (maxPort % m_TaskNumber == 0)
                    {
                        initialEndPort = maxPort / m_TaskNumber;
                        startPort = 1;
                        endPort = maxPort / m_TaskNumber;
                        for (int i = 0; i < m_TaskNumber; i++)
                        {
                            PortRange pr = new PortRange { IP = ip, StartPort = startPort, EndPort = endPort };

                            Action a = new Action(() => PerformScan(pr));
                            actions[i] = a;

                            nextStartPort = endPort + 1;
                            nextEndPort = endPort + initialEndPort;
                            startPort = nextStartPort;
                            endPort = nextEndPort;
                        }
                    }
                    else
                    {
                        int zp = m_TaskNumber - (maxPort % m_TaskNumber);
                        initialEndPort = maxPort / m_TaskNumber;

                        startPort = 1;
                        endPort = endPort = initialEndPort;
                        for (int i = 0; i < m_TaskNumber; i++)
                        {

                            if (i >= zp)
                            {
                                nextStartPort = endPort + 1;
                                nextEndPort = endPort + initialEndPort + 1;
                                startPort = nextStartPort;
                                endPort = nextEndPort;
                            }
                            else
                            {
                                nextStartPort = endPort + 1;
                                if (i == zp)
                                    nextEndPort = endPort + initialEndPort + 1;
                                else
                                    nextEndPort = endPort + initialEndPort;
                                startPort = nextStartPort;
                                endPort = nextEndPort;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Task number is invalid.");
                    return;
                }
            }

            Parallel.Invoke(actions);
        }

        /// <summary>
        /// Performs port scanning.
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        private void PerformScan(PortRange pr)
        {
            for (int port = pr.StartPort; port < pr.EndPort; port++)
            {
                using (TcpClient tcp = new TcpClient())
                {
                    try
                    {
                        //if thread has aborted, do not continue
                        if (m_IsAborted && (m_Thread.ThreadState == ThreadState.Aborted || m_Thread.ThreadState == ThreadState.AbortRequested))
                            return;

                        tcp.ConnectAsync(pr.IP, port).Wait();
                        Data d = new Data(pr.IP, port, true);
                        // do not add the same item again to the list!
                        if (!m_IPsAndPorts.Contains(d))
                            m_IPsAndPorts.Add(d);
                    }
                    catch (Exception ex)
                    {
                        Program.Log.WriteEntry($"Port cannot be reached: {pr.IP.ToString()}:{port}, Exception: {ex.Message}");

                        //Uncomment below code if you want to see closed ports too!
                        ////add try-catch block here and cover Data object creation.
                        //Data d = new Data(ip, port, false);
                        //// do not add the same item again to the list!
                        //if (m_IPsAndPorts.Contains(d))
                        //    m_IPsAndPorts.Remove(d);
                    }
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            SetPropertiesOfUIControls(false);
        }

        /// <summary>
        /// Set properties of of UI controls based on scan perform.
        /// </summary>
        /// <param name="isPerforming"></param>
        private void SetPropertiesOfUIControls(bool isPerforming)
        {
            if (isPerforming)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    m_IsAborted = false;
                    this.btnStop.Enabled = true;
                    this.btnScan.Enabled = false;
                    trckBarParallelTaskNumber.Enabled = false;
                    tbIPStart.Enabled = false;
                    tbIPEnd.Enabled = false;
                    this.m_IPsAndPorts.Clear();
                });
            }
            else
            {
                this.Invoke((MethodInvoker)delegate
                {
                    try
                    {
                        m_Thread.Abort();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unexpected exception! For more details please check Windows Event Logs.");
                        Program.Log.WriteEntry($"Exception occured. Message: {ex.Message}");
                    }
                    m_IsAborted = true;
                    this.btnStop.Enabled = false;
                    this.btnScan.Enabled = true;
                    trckBarParallelTaskNumber.Enabled = true;
                    tbIPStart.Enabled = true;
                    tbIPEnd.Enabled = true;
                    this.m_IPsAndPorts.Clear();
                });
            }
        }
    }
}
