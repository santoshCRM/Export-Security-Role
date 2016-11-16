using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;

namespace MsCrmTools.Soaplogger
{
    public partial class SoapLogger :  PluginControlBase
    {
        public SoapLogger()
        {
            InitializeComponent();
        }

        private void Execute_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_request.Text != null && txt_logicalname.Text != null && txt_recordid.Text != null)
                {
                     SoapLoggerOrganizationService slos = new SoapLoggerOrganizationService(Service);
                    OrganizationRequest req = new OrganizationRequest(txt_request.Text);
                    req.Parameters["Target"] = new EntityReference(txt_logicalname.Text, new Guid(txt_recordid.Text));
                    foreach (DataGridViewRow row in grd_parameter.Rows)
                    {
                        if (row.Cells[0].Value != null)
                        {
                            req.Parameters[(string)row.Cells[0].Value] = (string)row.Cells[1].Value;
                        }

                    }
                    slos.Execute(req);

                    txt_output.Text = slos.SoapResult;
                   
                }
                else
                    MessageBox.Show("All fields are required");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btn_convert_Click(object sender, EventArgs e)
        {
         txt_stringoutput.Text= SttingConverter.ConvertString(txt_output.Text);
        }
        private void lbl_copyoutput_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txt_output.Text);
            MessageBox.Show("Output copied");
        }
        private void lbl_copystroutput_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txt_stringoutput.Text);
            MessageBox.Show("Text copied");
        }
    }
}
