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
    public partial class Form3 : Form
    {
        public Form3()
        {
            this.MaximizeBox = false;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //αμα δεν έχει συμπληρώσει κάποιο από τα πεδία κάνουμε return ώστε να μην προχωρήσει η συνάρτηση
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Please complete all the fields.");
                return;
            }
            //διαβάζουμε τα αρχεία με χρήστες και κωδικούς να δούμε αν αντιστοιχούν και εμφανίζουμε κατάλληλο μήνυμα στον χρήστη
            StreamReader users = new StreamReader("users.txt");
            StreamReader passwords = new StreamReader("passwords.txt");
            try
            {
                String s = users.ReadLine();
                String s2 = passwords.ReadLine();
                StringBuilder sb = new StringBuilder();
                while (s != null)
                {
                    if (textBox1.Text == s && textBox2.Text == s2)
                    {
                        users.Close();
                        passwords.Close();
                        this.Hide();
                        Form1 form1 = new Form1(textBox1.Text);
                        form1.MaximizeBox = false;
                        form1.ShowDialog();
                        this.Close();
                        return;
                    }
                    s = users.ReadLine();
                    s2 = passwords.ReadLine();
                }
                if (textBox1.Text != s && textBox2.Text != s2)
                {
                    MessageBox.Show("Could not find user.");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                users.Close();
                passwords.Close();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //αμα δεν έχει συμπληρώσει κάποιο από τα πεδία κάνουμε return ώστε να μην προχωρήσει η συνάρτηση
            if (textBox4.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Please complete all the fields.");
                return;
            }
            //βλέπουμε άμα το username που έχει δώσει ο χρήστης είναι πιασμένο
            StreamReader user = new StreamReader("users.txt");
            try
            {
                String s = user.ReadLine();
                StringBuilder sb = new StringBuilder();
                while (s != null)
                {
                    if (textBox4.Text == s)
                    {
                        MessageBox.Show("User already registered!");
                        return;
                    }
                    s = user.ReadLine();
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                user.Close();
            }
            //άμα δεν είναι τότε καταγράφουμε τα στοιχεία του νέου χρήστη στα αρχεία
            StreamWriter users = new StreamWriter("users.txt", true);
            users.WriteLine(textBox4.Text);
            users.Close();
            StreamWriter passwords = new StreamWriter("passwords.txt", true);
            passwords.WriteLine(textBox3.Text);
            passwords.Close();
            MessageBox.Show("Successful signup!");
            /*try
            {
                

            }
            catch (Exception ex)
            {
                //by anyway you need to handle this error with below code
                if (ex.Message.StartsWith("The process cannot access the file"))
                {
                    //Wait for 5 seconds to free that file and then start execution again
                    MessageBox.Show("kek signup!");
                }
            }*/
        }
    }
}
