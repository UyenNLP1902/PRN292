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
    public partial class GradeForm : Form
    {
        public GradeForm()
        {
            InitializeComponent();
            LoadCombobox();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string subject = cbSubject.SelectedValue.ToString();
            string student = cbStudent.SelectedValue.ToString();
            string point = txtPoint.Text;

            if (subject.Trim().Length == 0
                && student.Trim().Length == 0
                && point.Trim().Length == 0)
            {
                MessageBox.Show("Please fill in all boxes!");
            }
            else if (Int32.TryParse(point, out int j) == false)
            {
                MessageBox.Show("Number should be integer");
            }
            else
            {
                Grade grade = new Grade(subject, student, j);
                bool check = Grade.CreateGrade(grade);
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
            DataTable subject = DBHelper.GetData("Subject");
            subject.DefaultView.Sort = "UUID";
            subject = subject.DefaultView.ToTable();

            cbSubject.DataSource = subject;
            cbSubject.DisplayMember = "UUID";
            cbSubject.ValueMember = "UUID";

            //-----------------------

            DataTable student = DBHelper.GetData("Student");
            student.DefaultView.Sort = "Name";
            student = student.DefaultView.ToTable();

            cbStudent.DataSource = student;
            cbStudent.DisplayMember = "Name";
            cbStudent.ValueMember = "UUID";
        }
    }
}