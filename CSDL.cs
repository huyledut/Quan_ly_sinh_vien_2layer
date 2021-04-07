using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT2_ThayPhuong
{
    class CSDL
    {
        public DataTable DTSV { get; set; }
        public DataTable DTLSH { get; set; }
        private static CSDL _Instance;
        public static CSDL Instance
        {
            get
            {
                if(_Instance ==null)
                {
                    _Instance = new CSDL();
                }
                return _Instance;
            }
            private set
            {

            }
        }
        
        private CSDL()
        {
            DTSV = new DataTable();
            DTSV.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("MSSV",typeof(string)),
                new DataColumn("NameSV",typeof(string)),
                new DataColumn("Gender",typeof(bool)),
                new DataColumn("NS",typeof(DateTime)),
                new DataColumn("ID_Lop",typeof(int)),
            });
            // tạo sinh viên 1;
            DataRow dr = DTSV.NewRow();
            dr["MSSV"] = "101"; 
            dr["NameSV"] = "NVA";
            dr["NS"] = DateTime.Now;
            dr["Gender"] = true; 
            dr["ID_Lop"] = 1;
            DTSV.Rows.Add(dr);

            // Sv2
            // tạo sinh viên 2;
            DataRow dr1 = DTSV.NewRow();
            dr1["MSSV"] = "102";
            dr1["NameSV"] = "NVB";
            dr1["NS"] = DateTime.Now;
            dr1["Gender"] = true;
            dr1["ID_Lop"] = 1;
            DTSV.Rows.Add(dr1);

            // Sv3
            // tạo sinh viên 3;
            DataRow dr2 = DTSV.NewRow();
            dr2["MSSV"] = "103";
            dr2["NameSV"] = "NVC";
            dr2["NS"] = DateTime.Now;
            dr2["Gender"] = false;
            dr2["ID_Lop"] = 2;
            DTSV.Rows.Add(dr2);

            //Sv4
            // tạo sinh viên 4;
            DataRow dr3 = DTSV.NewRow();
            dr3["MSSV"] = "104";
            dr3["NameSV"] = "NVD";
            dr3["NS"] = DateTime.Now;
            dr3["Gender"] = true;
            dr3["ID_Lop"] =2;
            DTSV.Rows.Add(dr3);

            //tao data lop sinh hoat

            DTLSH = new DataTable();
            DTLSH.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ID_Lop",typeof(int)),
                new DataColumn("NameLop",typeof(string))
            });
            DataRow l1 = DTLSH.NewRow();
            l1["ID_Lop"] = 1;
            l1["NameLop"] ="19DT1";
            DTLSH.Rows.Add(l1);

            DataRow l2 = DTLSH.NewRow();
            l2["ID_Lop"] = 2;
            l2["NameLop"] = "19DT2";
            DTLSH.Rows.Add(l2);

            DataRow l3 = DTLSH.NewRow();
            l3["ID_Lop"] = 3;
            l3["NameLop"] = "19DT3";
            DTLSH.Rows.Add(l3);
        }
        public DataTable createDatatable(string value)
        {
            DataTable d = new DataTable();
            d.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("MSSV",typeof(string)),
                new DataColumn("NameSV",typeof(string)),
                new DataColumn("Gender",typeof(bool)),
                new DataColumn("NS",typeof(DateTime)),
                new DataColumn("ID_Lop",typeof(int)),
            });
            foreach(DataRow dtr in DTSV.Rows)
            {
                if (dtr["ID_Lop"].ToString() == value || dtr["NameSV"].ToString().Contains(value)) 
                    d.Rows.Add(dtr.ItemArray);
            }
            return d;   
        }
         public void setDataTable(object[] o)
         {
            DataRow d = DTSV.NewRow();
            d.ItemArray = o;
            DTSV.Rows.Add(d);
         }
         
        public int getIndex(string value)
        {
            int index = 0;
            foreach(DataRow d in DTSV.Rows)
            {
                if (d["MSSV"].ToString() == value) return index;
                index++;
            }
            return index;
        }

        public object[] getDataTable(object[] s, int index)
        {
            s = DTSV.Rows[index].ItemArray;
            return s;
        }
        public void changeDataTable(object[] s, int index)
        {
            DTSV.Rows[index].ItemArray = s;
        }

        public void deleteDataTable(int  index)
        {
            DTSV.Rows.Remove(DTSV.Rows[index]);
        }   
        public DataTable tmpDataTable(DataTable tmp)
        {
            tmp.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("MSSV",typeof(string)),
                new DataColumn("NameSV",typeof(string)),
                new DataColumn("Gender",typeof(bool)),
                new DataColumn("NS",typeof(DateTime)),
                new DataColumn("ID_Lop",typeof(int)),
            });
            foreach(DataRow d in DTSV.Rows)
            {
                tmp.Rows.Add(d.ItemArray);
            }
            return tmp;
        }
        public DataTable sortDataTable(DataTable tmp, string value)
        {
            tmp.DefaultView.Sort = value + " ASC";
            tmp = tmp.DefaultView.ToTable();
            return tmp;
        }

        public bool Check(string m)
        {
            bool check = true;
            foreach(DataRow i in DTSV.Rows)
            {
                if(m==i["MSSV"].ToString())
                {
                    check = false;
                    break;
                }
            }
            return check;
        }
    }
}

