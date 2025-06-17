using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.IO;

namespace Model
{
    public class Command

    {
        //constants
        public enum Type 
        {
         POLARIZE_COIL = 0xC1,
         SHORT_POLARIZATION_COIL = 0xC2,
         FW_VERSION = 0xCF,
         SET_LARMOR_FRECUENCY = 0x1F,
         CMD_ALARM = 0xCA

        }

        IConnect Connect = new COM();

        public void SetCOMPort(string Port)
        {
            Connect.SetCOMPort(115200,8,1,0,Port);
        }

        public string Proccess(Type CMD)
        {
            ///data...
            byte[] data = new byte[1];
            data[0] = 0x01;
            byte[] response = new byte[256];
            try
            {
                Connect.ExecuteCommand(data, (byte)CMD, ref response);

                return ToString(response);
            }
            catch (Exception)
            {

                throw;
            }
                    
        }

        private  String ToString(byte[] value)
        {
            System.Text.StringBuilder result = new StringBuilder();
            try
            {
                StringBuilder Result = new StringBuilder();
                string HexAlphabet = "0123456789ABCDEF";

                foreach (byte B in value)
                {
                    Result.Append(HexAlphabet[(int)(B >> 4)]);
                    Result.Append(HexAlphabet[(int)(B & 0xF)]);
                }

                return Result.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
