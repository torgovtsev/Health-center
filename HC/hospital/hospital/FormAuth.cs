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
    public partial class FormAuth : Form
    {
        bool reg = false;

        public FormAuth()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!reg)
            {
                this.Height = 496;
                button2.Text = "Закрыть";
            }
            else
            {
                this.Height = 280;
                button2.Text = "Регистрация";
            }
            reg = !reg;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (hc_dbEntities1 h = new hc_dbEntities1())
            {
                string log = textBox1.Text;
                string pas = textBox2.Text;
                int id;
                try
                {
                    id = h.user.Where(use => use.login == log && use.password == pas).First().id_user;
                }
                catch(InvalidOperationException ex)
                {
                    MessageBox.Show("Неверный логин или пароль");
                    return;
                }
                this.Visible = false;
                if (h.user.Where(use => use.id_user == id).First().id_user_type == 2)
                {
                    int patid = h.patient.Where(pat => pat.id_user == id).First().id_patient;
                    FormPatient form = new FormPatient(patid);
                    form.ShowDialog();
                }
                else
                {
                    int docid = h.doctor.Where(doc => doc.id_user == id).First().id_doctor;
                    FormDoctor form = new FormDoctor(docid);
                    form.ShowDialog();
                }
                Application.Exit();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                button1_Click(null, null);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == textBox5.Text)
                using (hc_dbEntities1 h = new hc_dbEntities1())
                {
                    try
                    {
                        if(h.user.Where(use => use.login == textBox4.Text).First().id_user > 0)
                            MessageBox.Show("Ошибка регистрации!\nПользователь с таким логином уже существует.");
                    }
                    catch(InvalidOperationException ex)
                    {
                        if (textBox4.Text != "" && textBox3.Text != "")
                        {
                            user u = new user();
                            u.id_user_type = 2;
                            u.login = textBox4.Text;
                            u.password = textBox3.Text;
                            h.user.AddObject(u);
                            h.SaveChanges();
                            int id = h.user.Where(use => use.login == textBox4.Text && use.password == textBox3.Text).First().id_user;
                            patient p = new patient();
                            p.id_user = id;
                            h.patient.AddObject(p);
                            h.SaveChanges();
                            MessageBox.Show("Регистрация завершена успешно!\nЗаполните информацию о себе во вкладке \"Профиль\".");
                        }
                        else
                            MessageBox.Show("Ошибка регистрации!\nЗаполните пустые поля.");
                    
                    }
                }
            else
                MessageBox.Show("Ошибка регистрации!\nПароли не совпадают.");
        }
    }
}
