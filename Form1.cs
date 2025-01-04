using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
             string text = textBox1.Text;
             string word = textBox2.Text;
             int count = CountOccurrences(text, word);
             textBox3.Text = String.Format("{0} times", count);

        }
        private int CountOccurrences(string text, string word)
        {
            int count = 0;
            int index = text.IndexOf(word, StringComparison.OrdinalIgnoreCase);
            while (index != -1)
            {
            count++;
            index = text.IndexOf(word, index + 1, StringComparison.OrdinalIgnoreCase);
            }

            return count;


        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            string text = textBox1.Text;
            int topN = (int)numericUpDown1.Value;
            string[] words = text.Split(new char[] { ' ', ',', '.', ':', ';', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            List<string[]> wordCounts = new List<string[]>();
            foreach (string word in words)
            {
                bool found = false;
                foreach (string[] pair in wordCounts)
                {
                    if (pair[0].Equals(word, StringComparison.OrdinalIgnoreCase))
                    {
                        pair[1] = (int.Parse(pair[1]) + 1).ToString();
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    wordCounts.Add(new string[] { word, "1" });
                }
            }

            for (int i = 0; i < wordCounts.Count - 1; i++)
            {
                for (int j = 0; j < wordCounts.Count - i - 1; j++)
                {
                    if (int.Parse(wordCounts[j][1]) < int.Parse(wordCounts[j + 1][1]))
                    {
                        string[] temp = wordCounts[j];
                        wordCounts[j] = wordCounts[j + 1];
                        wordCounts[j + 1] = temp;
                    }
                }
            }

            listBox1.Items.Clear();

            for (int i = 0; i < topN && i < wordCounts.Count; i++)
            {
                listBox1.Items.Add(string.Format("{0}: {1}", wordCounts[i][0], wordCounts[i][1]));
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        
        }
    }
}
