using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace hospital
{
    public partial class FormEdit : Form
    {
        int patid;

        public FormEdit(int id)
        {
            InitializeComponent();
            patid = id;

            patient p;
            using (hc_dbEntities1 h = new hc_dbEntities1())
            {
                p = h.patient.Where(pat => pat.id_patient == patid).First();
            }

            textBox1.Text = p.FIO;
            textBox2.Text = p.Adress;
            textBox3.Text = p.tel;
            textBox4.Text = p.Safety_card.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (hc_dbEntities1 h = new hc_dbEntities1())
            {
                patient p = h.patient.Where(pat => pat.id_patient == patid).First();
                p.FIO = textBox1.Text;
                p.Adress = textBox2.Text;
                p.tel = textBox3.Text;
                p.Safety_card = Convert.ToInt32(textBox4.Text);
                h.SaveChanges();
            }
            this.Close();
        }
    }
}
