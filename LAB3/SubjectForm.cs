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
    public partial class SubjectForm : Form
    {
        public SubjectForm()
        {
            InitializeComponent();
            LoadCombobox();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string level = cbLevel.SelectedValue.ToString();
            string field = cbField.SelectedValue.ToString();

            if (level.Trim().Length == 0
                && field.Trim().Length == 0)
            {
                MessageBox.Show("Please fill in all boxes!");
            }
            else
            {
                Subject subject = new Subject(level, field);
                bool check = Subject.CreateSubject(subject);
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
            DataTable field = DBHelper.GetData("Field");
            field.DefaultView.Sort = "Name";
            field = field.DefaultView.ToTable();

            cbField.DataSource = field;
            cbField.DisplayMember = "Name";
            cbField.ValueMember = "UUID";

            //-----------------------
            DataTable level = DBHelper.GetData("Level");
            level.DefaultView.Sort = "Name";
            level = level.DefaultView.ToTable();

            cbLevel.DataSource = level;
            cbLevel.DisplayMember = "Name";
            cbLevel.ValueMember = "UUID";
        }
    }
}
