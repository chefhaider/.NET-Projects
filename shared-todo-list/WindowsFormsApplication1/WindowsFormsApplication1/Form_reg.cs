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
    public partial class Form_reg : Form
    {

        private static string accFile = ConfigurationManager.AppSettings["accounts"];
        private static string fileName = ConfigurationManager.AppSettings["filepath"];

        public Form_reg()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
            label1.Text = "Username";
            label2.Text = "Password";
            button1.Text = "Login";
            button2.Text = "Register";

        }

        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            bool auth = false;

            if (File.Exists(accFile))
            {
                string json_acc = File.ReadAllText(accFile);
                List<Accounts> acc = (new JavaScriptSerializer()).Deserialize<List<Accounts>>(json_acc);

                foreach (Accounts val in acc)
                {
                    if (String.Compare(val.username, textBox1.Text) == 0 & String.Compare(val.password, textBox2.Text) == 0)
                    {
                        auth = true;
                    }
                }
            }
            
            
            

            if (auth)
            {
                MyForm frm = new MyForm(textBox1.Text);
                frm.Show();
            }
            else
            {
                MessageBox.Show("unssucessful login attempt!");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {



            bool exists = false;
            List<Accounts> acc;

            if (File.Exists(accFile)) {

                var json = File.ReadAllText(accFile);
                acc = (new JavaScriptSerializer()).Deserialize<List<Accounts>>(json);

                foreach (Accounts val in acc)
                {
                    if (String.Compare(val.username, textBox1.Text) == 0)
                    {
                        exists = true;
                    }
                }
            }
            else
            {
                acc = new List<Accounts>();
                exists = false;
            }

            if (!exists)
            {

                List<Shared_Users> temp_shuser = new List<Shared_Users>();
                temp_shuser.Add(new Shared_Users
                {
                    shared_user = "Click on below name to open Todo",
                });

                acc.Add(new Accounts
                {
                    username = textBox1.Text,
                    password = textBox2.Text,
                    shared_user = temp_shuser 

                });

                var json = new JavaScriptSerializer().Serialize(acc);
                File.WriteAllText(accFile, json);

                List<Todo> newtodo = new List<Todo>();
                newtodo.Add(new Todo
                {
                    id = "todo-0",
                    name = "start adding tasks by typing",
                    completed = false
                    
                });

                

                
                json = new JavaScriptSerializer().Serialize(newtodo);
                File.WriteAllText(fileName + textBox1.Text +".json", json);

                MessageBox.Show("user succesfully registered");

            }
            else
            {
                MessageBox.Show("user already exists");
            }

            
        }
    }
}
