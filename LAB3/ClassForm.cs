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
    public partial class ClassForm : Form
    {
        public ClassForm()
        {
            InitializeComponent();
            LoadCombobox();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string level = cbLevel.SelectedValue.ToString();
            string room = cbRoom.SelectedValue.ToString();
            string name = txtName.Text;
            if (level.Trim().Length == 0
                && room.Trim().Length == 0
                && name.Trim().Length == 0)
            {
                MessageBox.Show("Please fill in all boxes!");
            }
            else
            {
                Class @class = new Class(level, room, name);
                bool check = Class.CreateClass(@class);
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
            DataTable room = DBHelper.GetData("Room");
            room.DefaultView.Sort = "No";
            room = room.DefaultView.ToTable();

            cbRoom.DataSource = room;
            cbRoom.DisplayMember = "No";
            cbRoom.ValueMember = "UUID";

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
