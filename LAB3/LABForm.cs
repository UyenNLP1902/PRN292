using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Schema;

namespace LAB3
{
    public partial class LABForm : Form
    {
        public LABForm()
        {
            InitializeComponent();
            SetEnableButton(false);
        }

        private void SetEnableButton(bool status)
        {
            buttonAdd.Enabled = status;
            buttonDelete.Enabled = status;
            buttonUpdate.Enabled = status;
        }

        private void LoadDatabase()
        {
            if (Util.Config != null)
            {
                treeView.Nodes.Clear();
                TreeNode root = new TreeNode();
                root.Text = Util.Config.Database;
                treeView.Nodes.Add(root);
                string[] names = DBHelper.GetTableName();

                foreach (string name in names)
                {
                    TreeNode node = new TreeNode();
                    node.Text = name;
                    node.Tag = DBHelper.GetData(name);
                    root.Nodes.Add(node);
                }

                dataGridView.Refresh();
            }
        }

        public TreeNode GetSelectedNode()
        {
            return treeView.SelectedNode;
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = treeView.SelectedNode;
            if (node.Tag != null)
            {
                node.Tag = DBHelper.GetData(node.Text);
                dataGridView.DataSource = node.Tag;
                dataGridView.Columns[0].Visible = false;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON Database File (JSON)|*.JSON";
            openFileDialog.ShowDialog();
            Util.Path = openFileDialog.FileName;
            if (Util.Path != null)
            {
                if (Util.CheckJsonFile() && DBHelper.IsDatabaseExist(Util.Config.Database))
                {
                    LoadDatabase();
                    SetEnableButton(true);
                }
                else
                {
                    MessageBox.Show("Database does not exist");
                    SetEnableButton(false);
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView.SelectedNode;

            if (node != null
                && Type.GetType("LAB3." + node.Text + "Form") != null)
            {
                var form = Activator.CreateInstance(Type.GetType("LAB3." + node.Text + "Form")) as Form;

                form.ShowDialog();

                node.Tag = DBHelper.GetData(node.Text);
                dataGridView.DataSource = null;
                dataGridView.DataSource = node.Tag;
                dataGridView.Columns[0].Visible = false;
            }
            else
            {
                MessageBox.Show("Please choode a node");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView.SelectedNode;

            if (node != null)
            {
                DataRow row = ((DataRowView)dataGridView.CurrentRow.DataBoundItem).Row;

                string table = node.Text;
                string key = (string)row["UUID"];

                bool deleted = DBHelper.DeleteData(table, key);

                if (deleted)
                {
                    node.Tag = DBHelper.GetData(node.Text);
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = node.Tag;
                    dataGridView.Columns[0].Visible = false;
                    MessageBox.Show("Delete successfully!");
                } 
                else
                {
                    MessageBox.Show("Please delete other constrain first.");
                }
            }
            else
            {
                MessageBox.Show("Please choode a row");
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView.SelectedNode;

            if (node != null && node.Tag != null
                && (dataGridView.SelectedRows.Count > 0 || dataGridView.CurrentCell != null))
            {
                DataTable dt = (DataTable)node.Tag;
                DataColumnCollection columns = dt.Columns;

                DataRow row = ((DataRowView)dataGridView.CurrentRow.DataBoundItem).Row;
                string table = node.Text;
                string key = (string)row["UUID"];

                bool check = DBHelper.UpdateData(table, key, columns, row);
                if (check)
                {
                    MessageBox.Show("Update successfully!");
                } 
                else
                {
                    MessageBox.Show("Update failed!");
                }
            }
            else
            {
                MessageBox.Show("Please select a row");
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
