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
    public partial class FormDoctor : Form
    {
        int docid;
        int healid;
        List<int> healidlist = new List<int>();

        public FormDoctor(int id)
        {
            InitializeComponent();
            docid = id;

            using (hc_dbEntities1 h = new hc_dbEntities1())
            {
                List<healing> heal = h.healing.Where(hea => hea.id_doctor == docid).ToList();
                for (int i = 0; i < heal.Count; i++)
                {
                    listView1.Items.Add("[" + heal[i].id_healing.ToString() + "] (" + heal[i].healing_time.ToString() + ") " + heal[i].patient.FIO);
                    healidlist.Add(heal[i].id_healing);
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count != 0)
            {
                healid = healidlist[listView1.SelectedIndices[0]];
                using (hc_dbEntities1 h = new hc_dbEntities1())
                {
                    textBox1.Text = h.healing.Where(hea => hea.id_healing == healid).First().diagnosis;
                    textBox2.Text = h.healing.Where(hea => hea.id_healing == healid).First().drugs;
                }
            }
            else
            {
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (hc_dbEntities1 h = new hc_dbEntities1())
            {
                healing heal = h.healing.Where(hea => hea.id_healing == healid).First();
                heal.diagnosis = textBox1.Text;
                heal.drugs = textBox2.Text;
                h.SaveChanges();
            }
        }
    }
}
