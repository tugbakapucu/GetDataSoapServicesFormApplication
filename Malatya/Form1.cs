using Malatya.AsisService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Malatya
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BasicHttpBinding httpBinding = new BasicHttpBinding();
            httpBinding.MaxReceivedMessageSize = int.MaxValue;
            httpBinding.MaxBufferSize = int.MaxValue;
            httpBinding.ReceiveTimeout = new TimeSpan(0, 3, 0);
            httpBinding.SendTimeout = new TimeSpan(0, 3, 0);

            WSRequestHeader hd = new WSRequestHeader();
            hd.Username = "teknarteknoloji";
            hd.Password = "6rQf8k65";

            //Soap  servis get values
            using (var s = new DenemeServiceSoapClient(httpBinding, new EndpointAddress("ServisUrl")))
            {
                try
                {
                    List<string> deger = new List<string>();
                    var ret = s.RealTimeData(hd, textBox1.Text, "");
                    foreach (var dt in ret.data.Tables)
                    {
                        foreach (DataRow dr in ((DataTable)dt).Rows)
                        {
                            var kod = dr["HatKodu"].ToString();
                            var lat = dr["enlem"].ToString();
                           
                            if (deger.Contains(kod)) continue;
                            else  deger.Add(kod);
                       }
                        
                    }
                    string y = "";
                    listView1.View = View.Details;
                    listView1.Columns.Add("Ad", 100);
                    foreach (string pl   in deger)
                    {
                        if (pl == "") continue;
                        y += pl + "\r" + "\n";
                        
                        listView1.Items.Add(pl);
                    }
                    Console.WriteLine(y);
                }
                catch (Exception ec)
                {
                    Console.WriteLine(ec.Message);
                }
            }
        }
    }
}
