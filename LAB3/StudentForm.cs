using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LAB3
{
    public partial class StudentForm : Form
    {
        public StudentForm()
        {
            InitializeComponent();
            LoadCombobox();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string name = textBoxName.Text;
            DateTime birthday = dateTimePickerBirthday.Value;
            string classUUID = comboBoxClass.SelectedValue.ToString();
            bool gender = radioButton1.Checked;

            if (name.Trim().Length == 0
                && classUUID.Trim().Length == 0)
            {
                MessageBox.Show("Please fill in all boxes!");
            }
            else
            {
                Student student = new Student(name, birthday, gender, classUUID);
                bool check = Student.CreateStudent(student);
                if (check)
                {
                    MessageBox.Show("Add successfully!", "Message");
                }
                else
                {
                    MessageBox.Show("Add failed!", "Message");
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadCombobox()
        {
            DataTable table = DBHelper.GetData("Class");
            table.DefaultView.Sort = "Name";
            table = table.DefaultView.ToTable();

            comboBoxClass.DataSource = table;
            comboBoxClass.DisplayMember = "Name";
            comboBoxClass.ValueMember = "UUID";
        }
    }
}
