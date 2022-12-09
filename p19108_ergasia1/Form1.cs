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
    public partial class Form1 : Form
    {
        //μεταβλητή για το topscore από όλους τους χρήστες
        int topscore = 0;
        //μεταβλητή για τον top player από όλους τους χρήστες
        String topplayer;
        int interval;
        String difficulty;
        public Form1()
        {
            InitializeComponent();
        }

        public Form1(String username)   // constructor για να περαστεί το username του χρήστη στο Form1
        {
            //παίρνουμε από την login φόρμα το username του χρήστη
            InitializeComponent();
            label2.Text = username;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //αναγκάζουμε τον χρήστη να επιλέξει δυσκολία για να παίξει
            if (radioButton1.Checked || radioButton2.Checked || radioButton3.Checked)
            {
                //κλείνουμε την φόρμα αυτή και ανοίγουμε την καινούρια για να παίξει το παιχνίδι
                this.Hide();
                Form2 form2 = new Form2(label2.Text, topscore.ToString(), topplayer, interval, difficulty);
                form2.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select difficulty!");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //με το άνοιγμα της φόρμας διαβάζουμε από τα αρχεία αν υπάρχουν στοιχεία του χρήστη και του απεικονίζουμε τα καλύτερα σκορ του και σε ποια πίστα τα έκανε
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
                if (s != null && s2 != null && s3 != null && s4 != null)
                {
                    StringBuilder sb = new StringBuilder();
                    while (s != null)
                    {
                        if (int.Parse(s2) > topscore)
                        {
                            topscore = int.Parse(s2);
                            topplayer = s;
                        }
                        if (s == label2.Text)
                        {
                            if (int.Parse(s2) >= int.Parse(label9.Text))
                            {
                                label9.Text = s2;
                                label10.Text = s3;
                            }
                            label12.Text = s4;  //βρίσκει την τελευταία ημερομηνία που ήταν μέσα ο χρήστης
                        }
                        s = players.ReadLine();
                        s2 = scores.ReadLine();
                        s3 = difficulty.ReadLine();
                        s4 = date.ReadLine();
                    }
                }
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

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Κλείνουμε τελείως την εφαρμογή σε περιπτωση που δεν εχει κλείσει
            Application.Exit();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            //για να κάνει logout κλέινουμε αυτή τη φόρμα και τον παραπέμπουμε στην αρχική
            this.Hide();
            Form3 form3 = new Form3();
            form3.MaximizeBox = false;
            form3.ShowDialog();
            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            //εμφανίζουμε στον χρήστη πληροφορίες για προηγούμενες φορές που έπαιξε
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
                    //προσέχουμε να πάρουμε μόνο στοιχεία που αντιστοιχούν στο όνομα του χρήστη
                    if (s == label2.Text)
                    {
                        sb.Append(s + " " + " " + s3 + " " + s2 + " " + s4);
                        sb.Append(Environment.NewLine);
                    }
                    s = players.ReadLine();
                    s2 = scores.ReadLine();
                    s3 = difficulty.ReadLine();
                    s4 = date.ReadLine();
                }
                if(sb.ToString() != "")
                {
                    MessageBox.Show(sb.ToString());
                }
                else
                {
                    MessageBox.Show("There is no info regarding this player.");
                }
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

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Ανάλογα την επιλογή δυσκολίας αλλάζουμε την συχνοτητα με την οποία θα εμφανίζονται τα ζάρια μαζί και με το μέγεθος
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            interval = 1000;
            difficulty = "Easy";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            interval = 750;
            difficulty = "Normal";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            interval = 700;
            difficulty = "Hard";
        }

        private void label13_Click(object sender, EventArgs e)
        {
            //ανοίγεται η φόρμα Leaderboards για να μας δείξει τις επιδόσεις όλων των χρηστών
            Form4 form4 = new Form4();
            form4.MaximizeBox = false;
            form4.ShowDialog();
        }
    }
}
