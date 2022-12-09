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
    public partial class Form2 : Form
    {
        Random random;
        String mode;
        //πίνακας που περιέχει τα "μονοπάτια" προς τις εικόνες των ζαριών αμα το mode είναι hard θα επιλέξει τις εικόνες με τα μικρότερα ζαρια για να είναι πιο δύσκολο
        String[] arr = { "Images/dice1.png", "Images/dice2.png", "Images/dice3.png", "Images/dice4.png", "Images/dice5.png", "Images/dice6.png" };
        String[] hardarr = { "Images/harddice1.png", "Images/harddice2.png", "Images/harddice3.png", "Images/harddice4.png", "Images/harddice5.png", "Images/harddice6.png" };
        int dicesum;    //score του χρήστη
        int randomimg;  //μας δείχνει ποια εικόνα θα εμφανιστεί
        int countDown = 20; //χρόνος που έχει ο χρήστης για να παίξει
        int highscore = 0;
        public Form2()
        {
            InitializeComponent();
        }

        public Form2(String username,String topscore, String topplayer, int interval,String difficulty)  // constructor για να περαστεί το username του χρήστη και το highscore στο Form2
        {
            InitializeComponent();
            label7.Text = username;
            label12.Text = topplayer;
            label13.Text = topscore;
            timer1.Interval = interval;
            mode = difficulty;
            label9.Text = difficulty + " Mode";
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //αμα πάει σε full screen ο χρήστης τότε πάει και το πάνελ που περιέχει το ζάρι
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            //διαβάζουμε τα αρχεία που περιέχουν τους παίχτες και τα σκορ για να τα συμπληρώσουμε στα labels
            StreamReader players = new StreamReader("players.txt");
            StreamReader scores = new StreamReader("scores.txt");
            try
            {
                String s = players.ReadLine();
                String s2 = scores.ReadLine();
                StringBuilder sb = new StringBuilder();
                while (s != null)
                {
                    //δείχνει το highscore του χρήστη με τον οποίο είμαστε logged in
                    if (s == label7.Text)
                    {
                        if(int.Parse(s2) > highscore)
                        {
                            highscore = int.Parse(s2);
                        }
                    }
                    s = players.ReadLine();
                    s2 = scores.ReadLine();
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
            }
            label8.Text = highscore.ToString();
            random = new Random();
            if (timer1.Enabled)
            {
                timer1.Enabled = false;
            }
            else
            {
                timer1.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            randomimg = random.Next(6);
            //άμα ο χρήστης επιλέξει το Hard difficulty τότε αλλάζουμε τις φωτογραφίες των ζαριων σε μικρότερες για να είναι πιο δύσκολο
            if (mode != "Hard")
            {
                pictureBox1.ImageLocation = arr[randomimg];
            }
            else
            {
                pictureBox1.ImageLocation = hardarr[randomimg];
            }
            int x1, y1;
            //κινούμε το ζάρι τυχαία μέσα στα πλαίσια του πάνελ
            x1 = random.Next(panel1.Width - pictureBox1.Width);
            y1 = random.Next(panel1.Height - pictureBox1.Height);
            pictureBox1.Location = new Point(x1, y1);
            //μειώνουμε τον χρόνο κάθε φορά που μετακινείται το ζάρι για να προσομοιώσουμε δευτερόλεπτα
            countDown--;
            label2.Text = countDown.ToString();
            //όταν τελειώσει ο χρόνος γράφουμε το νέο σκορ και τα άλλα στοιχεία του χρήστη μέσα σε αρχεία
            if (countDown == 0)
            {
                timer1.Enabled = false;
                pictureBox1.Enabled = false;
                pictureBox1.Visible = false;
                StreamWriter scores = new StreamWriter("scores.txt", true);
                scores.WriteLine(dicesum);
                scores.Close();
                StreamWriter players = new StreamWriter("players.txt", true);
                players.WriteLine(label7.Text);
                players.Close();
                StreamWriter difficulty = new StreamWriter("difficulty.txt", true);
                difficulty.WriteLine(mode);
                difficulty.Close();
                StreamWriter date = new StreamWriter("date.txt", true);
                date.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
                date.Close();
                MessageBox.Show("Game Over!");
                this.Hide();
                Form1 form1 = new Form1(label7.Text);
                form1.MaximizeBox = false;
                form1.ShowDialog();
                this.Close();
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //άμα ο χρήστης κλικάρει στο ζάρι ανεβαίνει το σκορ του αναλογα με την τιμή του ζαριού
            dicesum += randomimg + 1;
            label1.Text = dicesum.ToString();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Έξοδος από την εφαρμογή
            Application.Exit();
        }
    }
}
