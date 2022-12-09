using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace p19108_ergasia1
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            //φτιάχνουμε δυο λίστες μία που θα κρατάει όλα τα στοιχεία και μία μόνο με τα σκορ
            List<String> leaderboard = new List<String>();
            List<int> scoreslist = new List<int>();
            //διαβάζουμε από τα αρχεία όλα τα στατιστικά των χρηστών και τα εμφανίζουμε
            StreamReader players = new StreamReader("players.txt");
            StreamReader scores = new StreamReader("scores.txt");
            StreamReader difficulty = new StreamReader("difficulty.txt");
            StreamReader date = new StreamReader("date.txt");
            try
            {
                String s = players.ReadLine();
                String s2 = scores.ReadLine();
                String s3 = difficulty.ReadLine();
                String s4 = date.ReadLine();
                StringBuilder sb = new StringBuilder();
                while (s != null)
                {
                    leaderboard.Add(s + " " + s3 + "  " + s2 + "  " + s4);
                    scoreslist.Add(int.Parse(s2));
                    s = players.ReadLine();
                    s2 = scores.ReadLine();
                    s3 = difficulty.ReadLine();
                    s4 = date.ReadLine();
                }
                //χρησιμοποιούμε τον bubble sort για να αλλάξουμε τα στοιχεία μέσα στο leaderboard με φθίνουσα σειρά
                for (int i = 0; i < scoreslist.Count - 1; i++)
                    for (int j = 0; j < scoreslist.Count - i - 1; j++)
                        if (scoreslist[j] < scoreslist[j + 1])
                        {
                            int temp = scoreslist[j];
                            scoreslist[j] = scoreslist[j + 1];
                            scoreslist[j + 1] = temp;
                            String temp2 = leaderboard[j];
                            leaderboard[j] = leaderboard[j + 1];
                            leaderboard[j + 1] = temp2;
                        }
                //βάζουμε τα στοιχεία της λίστας σε ένα stringbuilder και μετα τα εμφανίζουμε στον χρήστη
                foreach(String str in leaderboard)
                {
                    sb.Append(str);
                    sb.Append(Environment.NewLine);
                }
                label2.Text = sb.ToString();
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                players.Close();
                scores.Close();
                difficulty.Close();
                date.Close();
            }
        }
    }
}
