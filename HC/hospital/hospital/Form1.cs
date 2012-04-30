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
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			tabPage1.Text = "Главная";
			tabPage2.Text = "Прием";
			label5.Text = "Новости нашей психушки:\nВсе вроде хорошо...";
			label3.Text = "Контакты: 8 800 2000 600 (бесплатно)";
			label4.Text = "Адрес: г. Воронеж Университетская площадь д. 1";
			dataGridView1.RowCount = 7;
			dataGridView1[0, 0].Value = "Понедельник";
			dataGridView1[0, 1].Value = "Вторник";
			dataGridView1[0, 2].Value = "Среда";
			dataGridView1[0, 3].Value = "Четверг";
			dataGridView1[0, 4].Value = "Пятница";
			dataGridView1[0, 5].Value = "Суббота";
			dataGridView1[0, 6].Value = "Воскресенье";
			List list = new List();
			list.list[1].values[3] = false;
			SetDL(list);
		}

		public void SetDL(List Array)
		{ 
			for(int i = 0; i < 7; i++)
				for (int j = 0; j < 17; j++)
				{
					if (Array.list[i].values[j])
					{
						dataGridView1[j + 1, i].Style.BackColor = System.Drawing.Color.LightGreen;
					}
					else
						dataGridView1[j + 1, i].Style.BackColor = System.Drawing.Color.LightPink;
				}
		}
	}
}
