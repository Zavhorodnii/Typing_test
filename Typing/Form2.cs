using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Typing
{
    public partial class Form2 : Form
    {
        
        public Form2()
        {
            InitializeComponent();
        }

        public Form2(List<Data_table> data_table)
        {
            InitializeComponent();
            Show_table(data_table);
        }


        public void Show_table(List<Data_table> data_table)
        {
            //dataGridView1.ColumnCount = 5;
            foreach (Data_table d in data_table)
            {
                dataGridView1.Rows.Add(
                d.count_word,
                d.right_answer,
                (d.count_word - d.right_answer),
                d.time,
                d.speed);
            }
        }
    }
}
