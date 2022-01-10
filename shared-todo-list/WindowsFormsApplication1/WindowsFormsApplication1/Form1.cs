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

    class MyForm : Form
    {
        // class variables
        public static List<Todo> todoList;
        private static TableLayoutPanel table;
        private static TableLayoutPanel todoListTable;
        private static TextBox todoInput;
        private static IEnumerable<Todo> sortList;
        private static string sortChoice { get; set; }
        private static Label todoCounter { get; set; }
        private static string user;

        private static string fileName = ConfigurationManager.AppSettings["filepath"];

        public MyForm(string usern)
        {


            user = usern;
            string json2 = File.ReadAllText(fileName + user + ".json");
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

            Panel sortButtonsPanel = new Panel
            {
                BackColor = Color.Lavender,
                Width = 350,
                Height = 20,
                Anchor = AnchorStyles.Top
            };
            table.Controls.Add(sortButtonsPanel);
            Panel sortButtons1Panel = new Panel
            {
                BackColor = Color.Lavender,
                Width = 350,
                Height = 20,
                Anchor = AnchorStyles.Top
            };
            table.Controls.Add(sortButtons1Panel);
            Button allButton = new Button
            {
                Text = "All",
                Location = new Point(20, 0),
                Width = 40,
                Height = 20
            };
            Button shareButton = new Button
            {
                Text = "Share",
                Location = new Point(20, 0),
                Width = 50,
                Height = 20
            };
            shareButton.Click += SortButtonClickEventHandler;
            sortButtons1Panel.Controls.Add(shareButton);

            Button viewSharedButton = new Button
            {
                Text = "View Shared",
                Location = new Point(75, 0),
                Width = 110,
                Height = 20
            };
            viewSharedButton.Click += ViewSharedEventHandler;
            sortButtons1Panel.Controls.Add(viewSharedButton);

            Button activeButton = new Button
            {
                Text = "Active",
                Location = new Point(65, 0),
                Width = 50,
                Height = 20
            };
            activeButton.Click += SortButtonClickEventHandler;
            sortButtonsPanel.Controls.Add(allButton);
            sortButtonsPanel.Controls.Add(activeButton);

            Button completedButton = new Button
            {
                Text = "Completed",
                Location = new Point(120, 0),
                Width = 60,
                Height = 20
            };
            sortButtonsPanel.Controls.Add(completedButton);
            completedButton.Click += SortButtonClickEventHandler;

            Button clearCompletedButton = new Button
            {
                Text = "Clear Completed",
                Location = new Point(230, 0),
                Width = 100,
                Height = 20
            };
            sortButtonsPanel.Controls.Add(clearCompletedButton);
            clearCompletedButton.Click += ClearCompletedEventHandler;
            allButton.Click += SortButtonClickEventHandler;

            todoInput = new TextBox
            {
                Font = new Font("consolas", 20),
                ForeColor = Color.CornflowerBlue,
                BackColor = Color.Lavender,
                BorderStyle = BorderStyle.None,
                Width = 350,
                Height = 50,
                Dock = DockStyle.Fill,
                Anchor = AnchorStyles.Top
            };
            table.Controls.Add(todoInput);
            table.AutoScroll = true;
            todoInput.KeyDown += TodoInputEventHandler;

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
            allButton.Select();
            todoInput.Select();
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
                UpdateTodoCounter();

                todoListTable.Controls.Add(todoPanel);
                todoPanel.Controls.Add(checkBox);
                todoPanel.Controls.Add(todoText);
                todoPanel.Controls.Add(removeButton);
                checkBox.Click += ClickedCheckBoxEventHandler;
                removeButton.FlatAppearance.BorderSize = 0;
                removeButton.Click += RemoveButtonEventHandler;
                todoListTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            }
        }

        private static void TodoInputEventHandler(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                TextBox textBox = (TextBox)sender;
                if (textBox.Text != "")
                {
                    todoListTable.Controls.Clear();
                    todoList.Add(new Todo
                    {
                        id = "todo-" + todoList.Count().ToString(),
                        name = textBox.Text
                    });
                }
                textBox.Clear();
                CreateTodoListDisplay();

                var json = new JavaScriptSerializer().Serialize(todoList);
                File.WriteAllText(fileName + user + ".json", json);

            }
            else { }
        }
        private static void RemoveButtonEventHandler(object sender, EventArgs e)
        {
            Button info = (Button)sender;
            int todoIndex = (int)info.Tag;
            todoList.RemoveAt(todoIndex);
            CreateTodoListDisplay();

            var json = new JavaScriptSerializer().Serialize(todoList);
            File.WriteAllText(fileName + user + ".json", json);

        }
        private static void ClickedCheckBoxEventHandler(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            int index = (int)checkBox.Tag;
            if (todoList[index].completed == true)
            {
                todoList[index].completed = false;
            }
            else
            {
                todoList[index].completed = true;
            }
            CreateTodoListDisplay();

            var json = new JavaScriptSerializer().Serialize(todoList);
            File.WriteAllText(fileName + user + ".json", json);

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
        private static void UpdateTodoCounter()
        {
            todoCounter.Visible = true;
            string singularOrPlural = "";
            int count = todoList.Count(t => t.completed == false);
            if (count == 1)
            {
                singularOrPlural = " item left";
            }
            else if (count > 1)
            {
                singularOrPlural = " items left";
            }
            else
            {
                todoCounter.Visible = false;
            }
            todoCounter.Text = count + singularOrPlural;
        }
        private static void SortButtonClickEventHandler(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            sortChoice = button.Text;
            if (sortChoice == "All")
            {
                sortList = todoList;
            }
            else if (sortChoice == "Completed")
            {
                sortList = todoList.Where(t => t.completed == true);
            }
            else if (sortChoice == "Active")
            {
                sortList = todoList.Where(t => t.completed == false);
            }
            else if (sortChoice == "Share")
            {
                Form3 shareList = new Form3(user);
                shareList.Show();
            }
            CreateTodoListDisplay();

            var json = new JavaScriptSerializer().Serialize(todoList);
            File.WriteAllText(fileName + user + ".json", json);

        }
        private static void ClearCompletedEventHandler(object sender, EventArgs e)
        {
            todoList.RemoveAll(t => t.completed == true);
            CreateTodoListDisplay();

            var json = new JavaScriptSerializer().Serialize(todoList);
            File.WriteAllText(fileName + user + ".json", json);

        }
        private static void ViewSharedEventHandler(object sender, EventArgs e)
        {
            MyForm2 viewShared = new MyForm2(user);
            viewShared.Show();
        }
    }
}