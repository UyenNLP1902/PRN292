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
    public partial class AttendanceForm : Form
    {
        public AttendanceForm()
        {
            InitializeComponent();
            LoadCombobox();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string teacher = cbTeacher.SelectedValue.ToString();
            string @class = cbClass.SelectedValue.ToString();
            string subject = cbSubject.SelectedValue.ToString();

            if (teacher.Trim().Length == 0
                && @class.Trim().Length == 0
                && subject.Trim().Length == 0)
            {
                MessageBox.Show("Please fill in all boxes!");
            }
            else
            {
                Attendance attendance = new Attendance(teacher, @class, subject);
                bool check = Attendance.CreateAttendance(attendance);
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
            DataTable teacher = DBHelper.GetData("Teacher");
            teacher.DefaultView.Sort = "UUID";
            teacher = teacher.DefaultView.ToTable();

            cbTeacher.DataSource = teacher;
            cbTeacher.DisplayMember = "Name";
            cbTeacher.ValueMember = "UUID";

            //-----------------------

            DataTable @class = DBHelper.GetData("Class");
            @class.DefaultView.Sort = "UUID";
            @class = @class.DefaultView.ToTable();

            cbClass.DataSource = @class;
            cbClass.DisplayMember = "Name";
            cbClass.ValueMember = "UUID";

            //-----------------------

            DataTable subject = DBHelper.GetData("Subject");
            subject.DefaultView.Sort = "UUID";
            subject = subject.DefaultView.ToTable();

            cbSubject.DataSource = subject;
            cbSubject.DisplayMember = "Name";
            cbSubject.ValueMember = "UUID";
        }
    }
}
