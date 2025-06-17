using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.IO
{
    interface IConnect
    {
       bool ExecuteCommand(byte[] command, byte CMD, ref byte[] response);

        void SetCOMPort(int baudrate, int databits, int stopbits, int parity, string PortName);

        
    }
}
