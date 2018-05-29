using System;
using System.Windows.Forms;
using OrderCakes;
using System.Collections.Generic;


namespace Graphic_Interface
{
    public partial class Form2 : Form
    {
        List<List<Decoration>> DecorationsList = new List<List<Decoration>>();
        public Form2()
        {
            InitializeComponent();
            comboBox1.DataSource = Enum.GetValues(typeof(Shapes));
            comboBox2.DataSource = Enum.GetValues(typeof(Shells));
            comboBox3.DataSource = Enum.GetValues(typeof(Filling));
            comboBox4.DataSource = new[] { 1, 2, 3, 4 };
            comboBox5.DataSource = Enum.GetValues(typeof(Decoration));
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CheckOrder();
        }

        void CheckOrder()
        {
            if (textBox1.Text != "")
                if (dateTimePicker2.Value.Date > dateTimePicker1.Value.Date)
                    if (dataGridView1.Rows.Count != 0)
                        SaveOrder();
                    else MessageBox.Show("Вы не добавили торт в заказ!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else MessageBox.Show("Дата выполнения заказа не может быть меньше или равна дате заказа!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Введите ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void SaveOrder()
        {
            var saveOrder = new SaveFileDialog() { Filter = "Файлы заказов|*.cake" };
            var result = saveOrder.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                var order = GetModelFromUI();
                FileHelper.WriteToFile(saveOrder.FileName, order);

                MessageBox.Show("Заказ сохранён", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult resultMessage = MessageBox.Show("Хотите закрыть окно заказа?", "Закрытие", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (resultMessage == DialogResult.Yes)
                    Close();
            }

        }

        OrderCakes.CakeRequest GetModelFromUI()
        {
            return new OrderCakes.CakeRequest()
            {
                FullName = textBox1.Text,
                OrderDate = dateTimePicker1.Value.Date,
                Deadline = dateTimePicker2.Value.Date,
                TypeCakes = CakeRequirements(),
                Price = label10.Text
            };
        }

        List<Types> CakeRequirements()
        {
            List<Types> list = new List<Types>();


            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                list.Add(new Types
                {
                    CakeShape = (Shapes)dataGridView1.Rows[i].Cells[0].Value,
                    ShellType = (Shells)dataGridView1.Rows[i].Cells[1].Value,
                    NumberTiers = (int)dataGridView1.Rows[i].Cells[2].Value,
                    FillingType = (Filling)dataGridView1.Rows[i].Cells[3].Value,
                    DecorationType = DecorationsList[i],

                });

            return list;
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
            dateTimePicker1.Value = order.OrderDate;
            dateTimePicker2.Value = order.Deadline;
            label10.Text = order.Price;

            int countCakes=0;

            foreach (var Cake in order.TypeCakes)
                countCakes++;

            for (int i = 0; i < countCakes; i++)
            {
                DecorationsList.Add(order.TypeCakes[i].DecorationType);
                dataGridView1.Rows.Add(order.TypeCakes[i].CakeShape, order.TypeCakes[i].ShellType, order.TypeCakes[i].NumberTiers, order.TypeCakes[i].FillingType, GetItemsFromListFile(i));
            }

            this.ShowDialog();
        }
        string GetItemsFromListFile(int j)
        {
            string str = "";
            int count = DecorationsList[j].Count;
            for (int i = 0; i <count-1 ; i++)
                str = str + DecorationsList[j][i] + "\n";
            str = str + DecorationsList[j][count-1];
            return str;
        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e) //добавить
        {
            if (listBox1.Items.Count != 0)
            {
                dataGridView1.Rows.Add(comboBox1.SelectedValue, comboBox2.SelectedValue, comboBox4.SelectedValue, comboBox3.SelectedValue, GetItems());
                label10.Text = (dataGridView1.Rows.Count * 800).ToString();

                List<Decoration> listic = new List<Decoration>();
                foreach (Decoration el in listBox1.Items)
                    listic.Add(el);

                DecorationsList.Add(listic);
            }
            else MessageBox.Show("Выберите декорации", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        string GetItems()
        {
            string str = "";
            for (int i = 0; i < listBox1.Items.Count - 1; i++)
                str = str + listBox1.Items[i] + "\n";
            str = str + listBox1.Items[listBox1.Items.Count - 1];
            return str;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!listBox1.Items.Contains(comboBox5.SelectedItem))
                listBox1.Items.Add(comboBox5.SelectedValue);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.Rows.RemoveAt(index);
            label10.Text = (dataGridView1.Rows.Count * 800).ToString();

            DecorationsList.RemoveAt(index);

        }
    }
}
