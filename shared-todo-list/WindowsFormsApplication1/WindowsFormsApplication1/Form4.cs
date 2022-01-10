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

    class MyForm4 : Form
    {
        // class variables
        public static List<Todo> todoList;
        private static TableLayoutPanel table;
        private static TableLayoutPanel todoListTable;
        private static IEnumerable<Todo> sortList;
        private static string sortChoice { get; set; }
        private static Label todoCounter { get; set; }
        private static string userfile;


        private static string fileName = ConfigurationManager.AppSettings["filepath"];

        public MyForm4(string userFile)
        {


            userfile = userFile;
            string json2 = File.ReadAllText(fileName + userfile + ".json");
            todoList = (new JavaScriptSerializer()).Deserialize<List<Todo>>(json2);
            sortList = todoList;


            table = new TableLayoutPanel
            {
                BackColor = Color.Lavender,
                RowCount = 2,
                Dock = DockStyle.Fill,
            };
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            table.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            this.MinimumSize = new Size(500, 500);
            Controls.Add(table);

            table.Controls.Add(new Label
            {
                Text = "todos",
                TextAlign = ContentAlignment.TopCenter,
                Font = new Font("consolas", 50),
                ForeColor = Color.CornflowerBlue,
                Dock = DockStyle.Fill
            });

            Panel itemCounterPanel = new Panel
            {
                BackColor = Color.Lavender,
                Width = 350,
                Height = 20,
                Anchor = AnchorStyles.Top
            };
            table.Controls.Add(itemCounterPanel);

            todoCounter = new Label
            {
                Text = "0",
                Location = new Point(145, 0),
                Visible = false
            };
            table.Controls.Add(itemCounterPanel);
            itemCounterPanel.Controls.Add(todoCounter);

            


            

            
            table.AutoScroll = true;

            todoListTable = new TableLayoutPanel
            {
                RowCount = 5,
                Dock = DockStyle.Fill,
                BackColor = Color.Lavender,
                Width = 350,
                Anchor = AnchorStyles.Top,
                AutoSize = true
            };
            table.Focus();
            table.Controls.Add(todoListTable);
            todoListTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            CreateTodoListDisplay();

        }

        private static void CreateTodoListDisplay()
        {
            todoListTable.Controls.Clear();
            foreach (Todo todo in sortList)
            {
                Panel todoPanel = new Panel
                {
                    BackColor = Color.Lavender,
                    Width = 350,
                    Height = 30,
                    Tag = todoList.IndexOf(todo)
                };
                CheckBox checkBox = new CheckBox
                {
                    BackColor = Color.Lavender,
                    Width = 20,
                    Height = 30,
                    Location = new Point(0, 0),
                    Anchor = AnchorStyles.Top,
                    Tag = todoList.IndexOf(todo)
                };
                Label todoText = new Label
                {
                    Text = todo.name,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Location = new Point(20, 0),
                    BackColor = Color.Lavender,
                    ForeColor = Color.CornflowerBlue,
                    Font = new Font("consolas", 20),
                    Width = 300,
                    Height = 30,
                    Anchor = AnchorStyles.Left,

                    Tag = todoList.IndexOf(todo)
                };
                Button removeButton = new Button
                {
                    Text = "X",
                    Name = "RemoveButton " + todoList.IndexOf(todo),
                    TextAlign = ContentAlignment.TopCenter,
                    Font = new Font("consolas", 14),
                    FlatStyle = FlatStyle.Flat,
                    Width = 20,
                    Height = 30,
                    Location = new Point(330, 0),
                    Tag = todoList.IndexOf(todo)
                };

                TodoIsDone(todo, checkBox);
                TodoStrikeOut(todo, todoText);

                todoListTable.Controls.Add(todoPanel);
                todoPanel.Controls.Add(checkBox);
                todoPanel.Controls.Add(todoText);
                todoPanel.Controls.Add(removeButton);
                removeButton.FlatAppearance.BorderSize = 0;
                todoListTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            }
        }

        private static bool TodoIsDone(Todo todo, CheckBox checkBox)
        {
            if (todo.completed == true)
            {
                return checkBox.Checked = true;
            }
            else
            {
                return checkBox.Checked = false;
            }
        }
        private static void TodoStrikeOut(Todo todo, Label label)
        {
            if (todo.completed == true)
            {
                label.Font = new Font("consolas", 20, FontStyle.Strikeout);
                label.ForeColor = Color.Gray;
            }
        }
    }
}