using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Graphic_Interface
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();

            
            form2.OpenFile();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var lv = new OrderCakes.LicenceValidator();
            if (!lv.HasLicense)
            {
                MessageBox.Show("Лицензия не найдена", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            if (!lv.IsValid)
            {
                MessageBox.Show("Срок действия лицензии истёк", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
    }
}
