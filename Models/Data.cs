using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PortScanTool.Models
{
    public class Data
    {
        public IPAddress IP { get; set; }
        public int Port { get; set; }
        
        /// <summary>
        /// Status of port. True: open, False: closed
        /// </summary>
        public bool IsOpen { get; set; }

        public Data(IPAddress ip, int port, bool isOpen)
        {
            if(ip == null)
                throw new ArgumentNullException("Argument cannot be null.", "ip");

            if (port < 0 || port > 65535)
                throw new ArgumentException("Port number cannot be smaller than 0 or higher than 65535.", "port");

            this.IP = ip;
            this.Port = port;
            this.IsOpen = isOpen;
        }
    }
}
