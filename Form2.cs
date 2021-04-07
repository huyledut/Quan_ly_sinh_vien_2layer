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
    public partial class Form2 : Form
    {
        public delegate void getData(int value, string option);
        public getData Sender;
        static int index = 0;
        static string option = "";
        static private void getIndex(int value, string s)
        {
            index = value;
            option = s;
        }
        public Form2()
        {
            Sender = new getData(getIndex);
            InitializeComponent();
            radioButton1.Checked = true;
            showLOP();
        }
        private int getID()
        {
            int index = comboBox1.SelectedIndex;
            CBBox c = new CBBox()
            {
                text = comboBox1.Items[index].ToString(),
                Value = Convert.ToInt32(CSDL.Instance.DTLSH.Rows[index]["ID_Lop"])
            };
            return c.Value; 
        }
        private void button1_Click(object sender, EventArgs e)
        {
            switch (option)
            {
                case "add":
                    if (CSDL.Instance.Check(comboBox1.Text)) 
                        AddFunc();
                    else MessageBox.Show("MSSV da co trong ds!");
                    break;
                case "edit":
                    EditFunc();
                    break;
            }
            Form1 f1 = new Form1();
            f1.Show();
            this.Close();
            f1.Close();
        }
        public void showLOP()
        {
            DataTable dt = CSDL.Instance.DTLSH;
            foreach (DataRow r in dt.Rows)
            {
                comboBox1.Items.Add(r["NameLop"]);
            }
        }

        private void ShowEdit()
        {
            object[] SV = new object[CSDL.Instance.DTSV.Columns.Count];
            SV = CSDL.Instance.getDataTable(SV, index);
            textBox1.Text = SV[0].ToString();
            textBox2.Text = SV[1].ToString();
            dateTimePicker1.Value = Convert.ToDateTime(SV[3]);
            if (Convert.ToBoolean(SV[2])) radioButton1.Checked = true;
            else radioButton2.Checked = true;
            CBBox c = new CBBox()
            {
                text = comboBox1.Items[Convert.ToInt32(SV[4]) - 1].ToString()
            };
            comboBox1.SelectedItem = c.text;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (option == "edit") ShowEdit();
        }

        private void EditFunc()
        {
            object[] o = new object[CSDL.Instance.DTSV.Columns.Count];
            o[0] = textBox1.Text;
            o[1] = textBox2.Text;
            o[3] = dateTimePicker1.Value;
            if (radioButton1.Checked == true) o[2] = true;
            else o[2] = false;
            o[4] = getID();
            CSDL.Instance.changeDataTable(o, index);
        }

        private void AddFunc()
        {
            object[] o = new object[CSDL.Instance.DTSV.Columns.Count];
            o[0] = textBox1.Text;
            o[1] = textBox2.Text;
            o[3] = dateTimePicker1.Value;
            if (radioButton1.Checked == true) o[2] = true;
            else o[2] = false;
            o[4] = getID();
            CSDL.Instance.setDataTable(o);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Close();
        }
    }   
}
