using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BT2_ThayPhuong
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = CSDL.Instance.DTSV;
            showLOP();
            showCCBsort();

        }

        public int Value { get; private set; }

        public void showLOP()
        {
            DataTable dt = CSDL.Instance.DTLSH;
            foreach(DataRow r in dt.Rows)
            {
                comboBox1.Items.Add(r["NameLop"]);
            }
            comboBox1.Items.Add("All");
            comboBox1.SelectedIndex = 0 ;
        }
        public void ShowGrid()
        {
            if (comboBox1.SelectedIndex == comboBox1.Items.Count - 1)
            {
                dataGridView1.DataSource = CSDL.Instance.DTSV;
                return;
            }

            CBBox c = new CBBox()
            {
                text = comboBox1.Items[comboBox1.SelectedIndex].ToString(),
                Value = Convert.ToInt32(CSDL.Instance.DTLSH.Rows[comboBox1.SelectedIndex]["ID_Lop"])
            };

            dataGridView1.DataSource = CSDL.Instance.createDatatable(c.Value.ToString());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowGrid();   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            int index = dataGridView1.CurrentRow.Index;
            f2.Sender(index, "add");
            f2.ShowDialog();
            f2.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            string SVID = dataGridView1.CurrentRow.Cells["MSSV"].Value.ToString();
            int index = CSDL.Instance.getIndex(SVID);
            f2.Sender(index, "edit");
            f2.ShowDialog();
            f2.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection r = dataGridView1.SelectedRows;
            if (r.Count == 0) MessageBox.Show("Khong co thang mo de xoa het");
            else
            {
                string s = dataGridView1.CurrentRow.Cells["MSSV"].Value.ToString();
                int index = CSDL.Instance.getIndex(s);
                CSDL.Instance.deleteDataTable(index);
                ShowGrid();
            }
        }

        public void showCCBsort()
        {
            DataTable d = CSDL.Instance.DTSV;
            foreach(DataColumn i in d.Columns)
            {
                comboBox2.Items.Add(i.ColumnName);                
            }
            comboBox2.SelectedItem = "MSSV";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "All";
            DataTable tmp = new DataTable();
            CSDL.Instance.tmpDataTable(tmp); //copy datatable ra tmp;
            CSDL.Instance.sortDataTable(tmp, comboBox2.SelectedItem.ToString());
            dataGridView1.DataSource = tmp;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string Name = textBox1.Text;
            dataGridView1.DataSource = CSDL.Instance.createDatatable(Name);
            dataGridView1.Refresh();
        }
    }   
}
    