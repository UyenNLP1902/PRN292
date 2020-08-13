using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace LAB3
{
    public partial class RoomForm : Form
    {
        public RoomForm()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string @class = txtClass.Text;
            string no = txtNo.Text;

            if (@class.Trim().Length == 0
                && no.Trim().Length == 0)
            {
                MessageBox.Show("Please fill in all boxes!");
            }
            else if (Int32.TryParse(no, out int j) == false)
            {
                MessageBox.Show("Number should be integer");
            }
            else
            {
                Class _class = new Class("", @class);
                Int32.TryParse(no, out j);
                Room room = new Room(_class.UUID, j);
                _class.Room = room.UUID;

                bool check = Room.CreateRoom(room);
                if (check)
                {
                    Class.CreateClass(_class);
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
    }
}
