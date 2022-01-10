using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Configuration;
using System.IO;
using System.Web.Script.Serialization;

namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {

        private static string accFile = ConfigurationManager.AppSettings["accounts"];
        private static string user;

        public Form3(string user_file)
        {

            user = user_file;

            InitializeComponent();
            label1.Text = "username";
            button1.Text = "edit acces";
            button2.Text = "view acces";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {


            string json = File.ReadAllText(accFile);
            List<Accounts> acc = (new JavaScriptSerializer()).Deserialize<List<Accounts>>(json);

            foreach (Accounts val in acc)
            {
                if (String.Compare(val.username, textBox1.Text ) == 0)
                {
                    val.shared_user.Add(new Shared_Users
                    {
                        shared_user = user + "|" + " can edit",
                    });
                }
            }
            json = new JavaScriptSerializer().Serialize(acc);
            File.WriteAllText(accFile, json);
            MessageBox.Show("Shared!");
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string json = File.ReadAllText(accFile);
            List<Accounts> acc = (new JavaScriptSerializer()).Deserialize<List<Accounts>>(json);

            foreach (Accounts val in acc)
            {
                if (String.Compare(val.username, textBox1.Text) == 0)
                {
                    val.shared_user.Add(new Shared_Users
                    {
                        shared_user = user + "|" + " can view",
                    });
                }
            }
            json = new JavaScriptSerializer().Serialize(acc);
            File.WriteAllText(accFile, json);
            MessageBox.Show("Shared!");
        }
    }
}
