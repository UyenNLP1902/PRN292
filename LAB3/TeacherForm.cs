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
    public partial class TeacherForm : Form
    {
        public TeacherForm()
        {
            InitializeComponent();
            LoadCombobox();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string field = cbField.SelectedValue.ToString();
            bool gender = rbFemale.Checked;

            if (name.Trim().Length == 0
                && field.Trim().Length == 0)
            {
                MessageBox.Show("Please fill in all boxes!");
            }
            else
            {
                Teacher teacher = new Teacher(name, gender, field);
                bool check = Teacher.CreateTeacher(teacher);
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadCombobox()
        {
            DataTable table = DBHelper.GetData("Field");
            table.DefaultView.Sort = "Name";
            table = table.DefaultView.ToTable();

            cbField.DataSource = table;
            cbField.DisplayMember = "Name";
            cbField.ValueMember = "UUID";
        }
    }
}
