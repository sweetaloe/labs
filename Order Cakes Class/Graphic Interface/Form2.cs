using System;
using System.Windows.Forms;
using OrderCakes;


namespace Graphic_Interface
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            comboBox1.DataSource = Enum.GetValues(typeof(Shapes));
            comboBox2.DataSource = Enum.GetValues(typeof(Shells));
            comboBox3.DataSource = Enum.GetValues(typeof(Filling));
            comboBox4.DataSource = new[] { 1, 2, 3, 4 };
            comboBox5.DataSource = Enum.GetValues(typeof(Decoration));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var saveOrder = new SaveFileDialog() { Filter = "Файлы заказов|*.cake" };
            var result = saveOrder.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                var order = GetModelFromUI();
                FileHelper.WriteToFile(saveOrder.FileName, order);
            }

            MessageBox.Show("Заказ сохранён", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }

        OrderCakes.CakeRequest GetModelFromUI()
        {
            return new OrderCakes.CakeRequest()
            {
                FullName = textBox1.Text,
                OrderDate = dateTimePicker1.Value.Date,
                Deadline = dateTimePicker2.Value.Date,
                TypeCakes = CakeRequirements()
            };
        }

        Types CakeRequirements()
        {
            return new Types()
            {
                CakeShape = (Shapes)comboBox1.SelectedValue,
                ShellType = (Shells)comboBox2.SelectedValue,
                NumberTiers = (int)comboBox3.SelectedValue,
                FillingType = (Filling)comboBox4.SelectedValue,
                DecorationType = (Decoration)comboBox5.SelectedValue,

            };
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void OpenFile()
        {
            var openOrder = new OpenFileDialog() { Filter = "Файл заказа|*.cake" };
            var result = openOrder.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                var order = FileHelper.LoadFromFile(openOrder.FileName);
                SetModelToUI(order);
            }
        }

        private void SetModelToUI(CakeRequest order)
        {
            
            textBox1.Text = order.FullName;
            comboBox1.SelectedItem = order.TypeCakes.CakeShape;
            comboBox2.SelectedItem = order.TypeCakes.ShellType;
            comboBox3.SelectedItem = order.TypeCakes.FillingType;
            comboBox4.SelectedItem = order.TypeCakes.NumberTiers;
            comboBox4.SelectedItem = order.TypeCakes.DecorationType;
            dateTimePicker1.Value = order.OrderDate;
            dateTimePicker2.Value = order.Deadline;

            this.ShowDialog();
        }
    }
}
