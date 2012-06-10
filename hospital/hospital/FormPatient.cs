using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.Objects;
using System.Text;
using System.Windows.Forms;

namespace hospital
{
	public partial class FormPatient : Form
	{
        int patid;
        List<doctor> doctorlist;
        List<int> specidlist = new List<int>();
        int depid;
        int specid;
        int docid;

		public FormPatient(int id)
		{
            InitializeComponent();
            patid = id;

            FillProfile();

            List<department> departmentlist = new List<department>();

            using (hc_dbEntities1 h = new hc_dbEntities1())
            {
                departmentlist = h.department.ToList();
            }

            for (int i = 0; i < departmentlist.Count; i++)
            {
                comboBox1.Items.Add(departmentlist[i].name);
            }

            label5.Text = "Новости нашей психушки:\nВсе вроде хорошо...";

			dataGridView1.RowCount = 7;
			dataGridView1[0, 0].Value = "Понедельник";
			dataGridView1[0, 1].Value = "Вторник";
			dataGridView1[0, 2].Value = "Среда";
			dataGridView1[0, 3].Value = "Четверг";
			dataGridView1[0, 4].Value = "Пятница";
			dataGridView1[0, 5].Value = "Суббота";
			dataGridView1[0, 6].Value = "Воскресенье";
            FillHistory();
		}

        private void FillProfile()
        {
            patient p;
            using (hc_dbEntities1 h = new hc_dbEntities1())
            {
                p = h.patient.Where(pat => pat.id_patient == patid).First();
            }

            label1.Text = p.FIO;
            label8.Text = p.Adress;
            label9.Text = p.tel;
            label11.Text = p.Safety_card.ToString();
        }

        private void FillHistory()
        {
            dataGridView2.Rows.Clear();
            List<healing> heal;
            using (hc_dbEntities1 h = new hc_dbEntities1())
            {
                heal = h.healing.Where(hea => hea.id_patient == patid).ToList();
            }
            for (int i = 0; i < heal.Count; i++)
            {
                dataGridView2.Rows.Add();
                dataGridView2[0, i].Value = heal[i].healing_time.ToString();
                dataGridView2[0, i].ToolTipText = heal[i].healing_time.ToString();
                dataGridView2[1, i].Value = GetDocByID(heal[i].id_doctor);
                dataGridView2[1, i].ToolTipText = GetDocByID(heal[i].id_doctor);
                dataGridView2[2, i].Value = heal[i].diagnosis;
                dataGridView2[2, i].ToolTipText = heal[i].diagnosis;
                dataGridView2[3, i].Value = heal[i].drugs;
                dataGridView2[3, i].ToolTipText = heal[i].drugs;
                dataGridView2[4, i].Value = heal[i].comments;
                dataGridView2[4, i].ToolTipText = heal[i].comments;
            }
            
        }

        private string GetDocByID(int id)
        {
            string name;
            using (hc_dbEntities1 h = new hc_dbEntities1())
            {
                name = h.doctor.Where(doc => doc.id_doctor == id).First().FIO;
            }
            return name;
        }

		public void SetDL()
		{ 
			for(int i = 0; i < 7; i++)
				for (int j = 1; j < 18; j++)
				{
                    if (DateTime.Compare(DateTime.Now, GetGridDate(i, j)) > 0)
						dataGridView1[j, i].Style.BackColor = System.Drawing.Color.LightPink;
                    else
                        dataGridView1[j, i].Style.BackColor = System.Drawing.Color.LightGreen;
				}
		}

		private void button1_Click(object sender, EventArgs e)
		{
            if (dataGridView1.SelectedCells[0].Style.BackColor != System.Drawing.Color.LightPink && docid != 0 && dataGridView1.SelectedCells[0].ColumnIndex != 0)
                using (hc_dbEntities1 h = new hc_dbEntities1())
                {
                    healing heal = new healing();
                    heal.id_doctor = docid;
                    heal.id_patient = patid;
                    heal.comments = richTextBox1.Text;
                    heal.healing_time = GetGridDate();
                    h.AddTohealing(heal);
                    h.SaveChanges();
                    dataGridView1.SelectedCells[0].Style.BackColor = System.Drawing.Color.LightPink;
                    FillHistory();
                    MessageBox.Show("Заявка подана");
                }
            else
                MessageBox.Show("Ошибка записи!");
		}

        private DateTime GetGridDate()
        {
            DateTime dt = new DateTime();
            dt = DateTime.Now.Date;
            dt = dt.AddDays(dataGridView1.SelectedCells[0].RowIndex + 1 - GetDayOfWeek(DateTime.Now.DayOfWeek));
            string str = dataGridView1.Columns[dataGridView1.SelectedCells[0].ColumnIndex].HeaderText;
            string m = str.Substring(str.IndexOf(":") + 1);
            string h = str.Substring(0, str.IndexOf(":"));
            dt = dt.AddHours(Convert.ToDouble(h));
            dt = dt.AddMinutes(Convert.ToDouble(m));
            return dt;
        }

        private DateTime GetGridDate(int row, int col)
        {
            DateTime dt = new DateTime();
            dt = DateTime.Now.Date;
            dt = dt.AddDays(row + 1 - GetDayOfWeek(DateTime.Now.DayOfWeek));
            string str = dataGridView1.Columns[col].HeaderText;
            string m = str.Substring(str.IndexOf(":") + 1);
            string h = str.Substring(0, str.IndexOf(":"));
            dt = dt.AddHours(Convert.ToDouble(h));
            dt = dt.AddMinutes(Convert.ToDouble(m));
            return dt;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<spec> speclist = new List<spec>();
            depid = comboBox1.SelectedIndex;

            using (hc_dbEntities1 h = new hc_dbEntities1())
            {
                speclist = h.spec.Where(spe=>spe.id_department == depid).ToList();
            }

            comboBox2.Items.Clear();
            specid = 0;
            comboBox3.Items.Clear();
            docid = 0;

            for (int i = 0; i < speclist.Count; i++)
            {
                comboBox2.Items.Add(speclist[i].name);
                specidlist.Add(speclist[i].id_spec);
            }

            comboBox2.Enabled = true;
        }
        
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            doctorlist = new List<doctor>();
            specid = specidlist[comboBox2.SelectedIndex];

            using (hc_dbEntities1 h = new hc_dbEntities1())
            {
                doctorlist = h.doctor.Where(doc=>doc.id_spec == specid).ToList();
            }

            comboBox3.Items.Clear();

            for (int i = 0; i < doctorlist.Count; i++)
            {
                comboBox3.Items.Add(doctorlist[i].FIO);
            }

            comboBox3.Enabled = true;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<healing> healinglist = new List<healing>();
            docid = doctorlist[comboBox3.SelectedIndex].id_doctor;

            using (hc_dbEntities1 h = new hc_dbEntities1())
            {
                healinglist = h.healing.Where(hea => hea.id_doctor == docid).ToList();
            }

            SetGrid(healinglist);

            dataGridView1.Enabled = true;
        }

        private void SetGrid(List<healing> list)
        {
            SetDL();
            for (int i = 0; i < list.Count; i++)
            {
                DateTime date = list[i].healing_time;
                
                if (DateTime.Compare(GetMinWeekDate(), DateTime.Now) <= 0 && DateTime.Compare(GetMaxWeekDate(), DateTime.Now) >= 0)
                    for (int j = 1; j < 18; j++)
                    {
                        if (dataGridView1.Columns[j].HeaderText == (date.Hour + ":" + date.Minute/10 + "0"))
                            dataGridView1[j, GetDayOfWeek(date.DayOfWeek) - 1].Style.BackColor = System.Drawing.Color.LightPink;
                    }
            }
        }

        private int GetDayOfWeek(DayOfWeek day)
        {
            if (Convert.ToInt16(day) > 0)
                return Convert.ToInt16(day);
            return 7;
        }

        private DateTime GetMinWeekDate()
        {
            DateTime min;
            min = DateTime.Now.Date;
            min = min.AddDays(1 - GetDayOfWeek(DateTime.Now.DayOfWeek));
            return min;
        }

        private DateTime GetMaxWeekDate()
        {
            DateTime max;
            max = DateTime.Now.Date;
            max = max.AddDays(7 - GetDayOfWeek(DateTime.Now.DayOfWeek));
            return max;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormEdit form = new FormEdit(patid);
            form.ShowDialog();
            FillProfile();
        }
	}
}
