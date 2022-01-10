using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

using System.Configuration;
using System.IO;
using System.Web.Script.Serialization;

namespace WindowsFormsApplication1
{

    class MyForm2 : Form
    {
        // class variables
        private static TableLayoutPanel table;
        private static string fileName = ConfigurationManager.AppSettings["filepath"];
        private static string accFile = ConfigurationManager.AppSettings["accounts"];
        private string users_list;

        public MyForm2(string user)
        {



            string json = File.ReadAllText(accFile);
            List<Accounts> acc = (new JavaScriptSerializer()).Deserialize<List<Accounts>>(json);

            





            table = new TableLayoutPanel
            {
                BackColor = Color.Lavender,
                RowCount = 10,
                Dock = DockStyle.Fill,
            };
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 200));
            table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.MinimumSize = new Size(500, 500);
            Controls.Add(table);

            Panel userList = new Panel
            {
                BackColor = Color.Lavender,
                Width = 200,
                Height = 350,
                Anchor = AnchorStyles.Top
            };

            table.Controls.Add(userList);

            List<string> names = new List<string>(new string[] { "hussain", "haider", "mustaali" });


            int count = 1;
            foreach (Accounts val in acc)
            {
                if (String.Compare(val.username, user) == 0)
                {
                    
                    foreach ( Shared_Users elem in val.shared_user){
                        Label userName = new Label
                        {
                            Text = elem.shared_user,
                            Location = new Point(40, 40*count),
                        };
                        userName.Click += userNameEventHandler;
                        userList.Controls.Add(userName);
                        count += 1;
                    }
                    break;
                }
            }



        }

        private static void userNameEventHandler(object sender, EventArgs e)
        {
            Label labelData  = (Label)sender;
            string labeltext = labelData.Text;
            string[] authorsList = labeltext.Split('|');

            if (String.Compare(authorsList[1], " can edit") == 0)
            {
                MyForm frm = new MyForm(authorsList[0]);
                frm.Show();
            }
            else if (String.Compare(authorsList[1], " can view") == 0)
            {
                MyForm4 frm = new MyForm4(authorsList[0]);
                frm.Show();
            }

            
            
        }

        
    }
}