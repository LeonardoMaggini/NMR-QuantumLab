using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace Model.IO
{
    public class COM:IConnect
    {
        SerialPort serialPort = new SerialPort();


        #region Properties

        public string PortName
        {
            get { return serialPort.PortName; }
            set { serialPort.PortName = value; }
        }

       
        public int BaudRate
        {
            get { return serialPort.BaudRate; }
            set { serialPort.BaudRate = value; }
        }


      

        public System.IO.Ports.Parity Parity
        {
            get { return serialPort.Parity; }
            set { serialPort.Parity = value; }
        }

      
        public int DataBits
        {
            get { return serialPort.DataBits; }
            set { serialPort.DataBits = value; }
        }

        
        public System .IO.Ports.StopBits StopBits
        {
            get { return serialPort.StopBits; }
            set { serialPort.StopBits = value; }
        }


        #endregion


        /// <summary>
        /// Ejecuta un comando
        /// </summary>
        /// <param name="data"></param>
        /// <param name="response"></param>
        /// <returns>True, si la comunicacion fue exitosa. False si no se obtuvo respuesta</returns>
        public bool ExecuteCommand(byte[] data, byte CMD, ref byte[] response)
        {
            data = PrepareProtocol(data, CMD);

            response = SendAndReceiveSERIAL(data);

            if (response == null)
                return false;

            return true;
        }

        /// <summary>
        /// Construye el mensaje a enviar al NMR
        /// </summary>
        /// <param name="Data"><param>
        /// <param name="CMD">El codigo de comando a ejecutar</param>
        /// <returns>Array de bytes con el mensaje a enviar al LG ACG</returns>
        private byte[] PrepareProtocol(byte[] Data, byte CMD)
        {
            try
            {
                byte[] protocol;
                int bookmark = 2;

                protocol = new byte[Data.Length + 3];
               
                protocol[0] = 0x02; // STX
                protocol[1] = CMD; // Reader
               
                for (int i = 0; i < Data.Length; i++)
                    protocol[i + bookmark] = Data[i];

                byte bcc = 0x00;

                //for (int i = 1; i < protocol.Length - 2; i++)
                //    bcc ^= protocol[i];

                //protocol[protocol.Length - 2] = bcc;    // XOR del paquete
                protocol[protocol.Length - 1] = 0x03;   // ETX

                return protocol;
            }
            catch (Exception ex)
            {
               
                return null;
            }
        }

        /// <summary>
        /// Envia y recibe un mensaje hacia y desde el LG ACG
        /// </summary>
        /// <param name="Data">el mensaje a enviar</param>
        /// <returns>Un array de bytes que represnta el mensaje de respuesta</returns>
        private byte[] SendAndReceiveSERIAL(byte[] Data)
        {
            try
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                }

                // Enviar Commando
                serialPort.DiscardInBuffer();
                serialPort.Write(Data, 0, Data.Length);

                // Recibir Data
                byte[] receiveBytes = new byte[256];
                int idx = 0;
                int LenData = 256;

               
                while (idx < LenData) 
                {
                    
                    receiveBytes[idx] = (byte)serialPort.ReadByte();                                  
                    if (idx == 2)
                    {
                        LenData = receiveBytes[idx] + 4;
                    }
                    idx++;
                                        
                }

                serialPort.DiscardInBuffer();

                Array.Resize(ref receiveBytes, idx);

                //if (!VerifyResponse(ref receiveBytes))
                //    throw new Exception("Error recibiendo respuesta!");


                return receiveBytes;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                }

            }
        }



        public void SetCOMPort(int baudrate, int databits, int stopbits, int parity, string PortName)
        {
            try
            {
                serialPort.BaudRate = baudrate;
                serialPort.DataBits = databits;
                serialPort.StopBits = (StopBits)stopbits;
                serialPort.Parity = (Parity)parity;
                serialPort.PortName = PortName;
            }
            catch (Exception)
            {

                throw;
            }
            

        }

    }


}
