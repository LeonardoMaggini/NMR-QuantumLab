using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using NMR_QuantumLab;


namespace Controller
{
    public class FormMainController
    {
        string  COMPort;
        

        private readonly FormMain formMain;
        public FormMainController(FormMain main)
        {
            this.formMain = main;
            this.formMain.buttonSend.Click += buttonSend_Click;
            this.formMain.ComboBox_COMPort.Click += ComboBox_click;
            this.formMain.ComboBox_COMPort.SelectedIndexChanged += comboBox_COM_List_SelectedIndexChanged;
        }



        private void buttonSend_Click(object sender, EventArgs e)
        {
            Model.Command Command = new Command();
            try
            {
                Command.SetCOMPort(COMPort);
                ///enciendo bobina de polarizacion 
                this.formMain.RichTextBox_log.AppendText(Command.Proccess(Command.Type.FW_VERSION));
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private void comboBox_COM_List_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
              COMPort = ((System.Windows.Forms.ComboBox)sender).Text;
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Lista los puertos COM que tienen algun dispositivo conectado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_click(object sender, EventArgs e)
        {
            try
            {
                this.formMain.ComboBox_COMPort.Items.Clear();

                string[] ports = System.IO.Ports.SerialPort.GetPortNames();

                for (int i = 0; i < ports.Length; i++)
                {
                    this.formMain.ComboBox_COMPort.Items.Add(ports[i]);
                }
            }
            catch (Exception)
            {

                ;
            }
        }

    }
}
