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
    public partial class FieldForm : Form
    {
        public FieldForm()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            if (name.Trim().Length == 0)
            {
                MessageBox.Show("Please fill in all boxes!");
            }
            else
            {
                Field field = new Field(name);
                bool check = Field.CreateField(field);
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
    }
}
