using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Typing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            progressBar1.Maximum = 1;
            progressBar2.Maximum = 1;
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
        }
        List<Data_table> data_table = new List<Data_table>();
        

        int row = 0;
        int time = 0;
        int right_answer = 0, wrong_answer = 0;
        int answers = 0;
        string result_time = "0";
        int speed = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = openFileDialog1.FileName;
            // читаем файл в строку
            string fileText = System.IO.File.ReadAllText(filename);
            string[] newText = fileText.Split(' ');

            //foreach (DataGridViewColumn column in dataGridView1.Columns)
            //   column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView1.Columns[0].Width = 578;

            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            for (int i = 0; i < newText.Length; i++)
            {
                dataGridView1.Rows.Add(newText[i]);
            }
            dataGridView1.Rows[row].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#9dc4a2");
            dataGridView1.Rows[dataGridView1.RowCount - 1].DefaultCellStyle.BackColor = Color.White;

            progressBarStep(newText.Length);
            textBox3.Enabled = true;

            dataGridView1.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
            if (numericUpDown1.Value != 0)
            {
                progressBar2.Maximum += (int)numericUpDown1.Value * 60;
                progressBar2.Step = 1;
                timer1.Enabled = true;

            }
            textBox3.Focus();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            time += 1;
            progressBar2.PerformStep();

            if (progressBar2.Value == progressBar2.Maximum)
            {
                Console.WriteLine("timer STOPE");
                textBox3.Enabled = false;
                for (; row < dataGridView1.RowCount - 1; row++)
                {
                    if (textBox3.Text == dataGridView1.Rows[row].Cells[0].Value.ToString())
                    {
                        right_answer += 1;
                        dataGridView1.Rows[row].DefaultCellStyle.ForeColor = Color.Black;
                        dataGridView1.Rows[row].DefaultCellStyle.BackColor = Color.White;
                    }
                    else
                    {
                        wrong_answer += 1;
                        dataGridView1.Rows[row].DefaultCellStyle.ForeColor = Color.Red;
                        dataGridView1.Rows[row].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#f5abab");
                    }
                    textBox3.Text = "";
                }

                dataGridView1.Rows[row].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#9dc4a2");




                timer1.Enabled = false;
            }
        }


        public void Result()
        {
            if (numericUpDown1.Value != 0)
            {
                int minute = 0, local = time;
                if (local > 59)
                {
                    for (; local > 59; local -= 60)
                    {
                        minute += 1;
                    }
                }
                result_time = minute + ":" + local;
                label7.Text = result_time;
                speed = (int)(((float)answers / time) * 60);
                label11.Text = speed.ToString();
            }
            else
            {
                label7.Text = "0";
                label11.Text = "0";
            }
            label8.Text = right_answer.ToString();
            label9.Text = wrong_answer.ToString();
            label10.Text = answers.ToString();
            Add_to_table_form2();
        }


        public void Clock(object obj)
        {
            Console.WriteLine(DateTime.Now);
            this.Invoke((MethodInvoker)delegate
            {
                progressBar2.PerformStep();
            });
        }

        private void progressBarStep(int maxvalue)
        {
            //progressBar1.Value = 0;
            progressBar1.Maximum += maxvalue;
            progressBar1.Minimum = 0;
            progressBar1.Step = 1;
        }



        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //Console.WriteLine(textBox3.Text + "==" + dataGridView1.Rows[row].Cells[0].Value.ToString() + " row=" + row);
            //Console.WriteLine(textBox3.Text.Length);


            try
            {
                if (textBox3.Text[textBox3.Text.Length - 1].Equals(' '))
                {
                    progressBar1.PerformStep();
                    answers += 1;
                    string text = textBox3.Text;
                    text = text.Substring(0, text.Length - 1);
                    if (text != dataGridView1.Rows[row].Cells[0].Value.ToString())
                    {
                        wrong_answer += 1;
                        dataGridView1.Rows[row].DefaultCellStyle.ForeColor = Color.Red;
                        //dataGridView1.Rows[row].DefaultCellStyle.BackColor = Color.White;
                        dataGridView1.Rows[row].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#f5abab");
                    }
                    else
                    {
                        right_answer += 1;
                        dataGridView1.Rows[row].DefaultCellStyle.BackColor = Color.White;
                    }
                    textBox3.Text = "";

                    //dataGridView1.Rows[row].DefaultCellStyle.BackColor = Color.White;
                    row++;
                    dataGridView1.Rows[row].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#9dc4a2");
                    dataGridView1.FirstDisplayedScrollingRowIndex = row;
                }
                Check();
            }
            catch (Exception ex) { }



            if (row == dataGridView1.RowCount - 1)
            {
                timer1.Enabled = false;
                Result();
                textBox3.Enabled = false;
                return;
            }


        }

        private void Check()
        {
            if (dataGridView1.Rows[row].Cells[0].Value != null)
            {
                if (Equal(textBox3.Text, dataGridView1.Rows[row].Cells[0].Value.ToString()))
                {
                    dataGridView1.Rows[row].DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    dataGridView1.Rows[row].DefaultCellStyle.ForeColor = Color.Red;
                }
            }
            else
            {
                progressBar1.PerformStep();
            }
        }

        private bool Equal(string check_str, string correct_str)
        {
            if (check_str == "")
                return false;
            if (check_str.Length > correct_str.Length)
                return false;
            for (int i = 0; i < check_str.Length; i++)
            {
                if (check_str[i] != correct_str[i])
                    return false;
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            timer1.Enabled = false;
            numericUpDown1.Value = 0;
            while (true)
            {
                Console.WriteLine("dataGridView1.Rows.Count = " + dataGridView1.Rows.Count);
                if (dataGridView1.Rows.Count == 1)
                    break;
                else
                    dataGridView1.Rows.RemoveAt(0);
            }

            textBox3.Text = "";
            progressBar1.Value = 0;
            progressBar2.Value = 0;
            row = 0;
            time = 0;
            right_answer = 0; wrong_answer = 0;
            answers = 0;
            speed = 0;
            result_time = "0";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            Result();
            textBox3.Text = "";
            textBox3.Enabled = false;
            //Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 newForm = new Form2(data_table);
            newForm.Show();
        }

        private void Add_to_table_form2()
        {
            Data_table data_table_ = new Data_table();
            data_table_.count_word = answers;
            data_table_.right_answer = right_answer;
            data_table_.time = result_time;
            data_table_.speed = speed;

            data_table.Add(data_table_);
        }



        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }
    }


    public class Data_table
    {
        public int count_word;
        public int right_answer;
        public string time;
        public int speed;
    }
}
