using Microsoft.Data.SqlClient;
using System.Data;

namespace Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=empdemo;Integrated Security=True");
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "Select Department";
            LoadData();
            FindCount();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            int age = int.Parse(textBox2.Text);

            string gender = "";
            if (radioButton1.Checked) gender = "Male";
            else if (radioButton2.Checked) gender = "Female";

            string department = comboBox1.Text;
            double salary = double.Parse(textBox3.Text);

            con.Open();
            SqlCommand cmd = new SqlCommand("insert into emp(name,age,gender,department,salary) values(@n,@a,@g,@d,@s)", con);
      
            cmd.Parameters.AddWithValue("@n", name);
            cmd.Parameters.AddWithValue("@a", age);
            cmd.Parameters.AddWithValue("@g", gender);
            cmd.Parameters.AddWithValue("@d", department);
            cmd.Parameters.AddWithValue("@s", salary);

            int r = cmd.ExecuteNonQuery();

            MessageBox.Show(r != 0 ? "saved" : "failed");
            con.Close();

            LoadData();
            FindCount();
        }

        private void FindCount()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select count(*) from emp", con);
            label7.Text = "No of Employee : " + cmd.ExecuteScalar();
            con.Close();


        }

        private void LoadData()
        {
            SqlDataAdapter sda = new SqlDataAdapter("select *from emp", con);

            DataTable dt = new DataTable();

            sda.Fill(dt);

            dataGridView1.DataSource = dt;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

            comboBox1.Text = "select department";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox4.Text);
            con.Open();
            SqlCommand cmd = new SqlCommand("select *from emp where id=@i", con);
            cmd.Parameters.AddWithValue("@i", id);
            SqlDataReader sdr = cmd.ExecuteReader();
            int k = 0;
            while (sdr.Read())
            {
                textBox1.Text = sdr["name"].ToString();
                textBox2.Text = sdr["age"].ToString();
                string g = sdr["gender"].ToString();

                if (g == "Male") radioButton1.Checked = true;
                else if (g == "Female") radioButton2.Checked = true;


                comboBox1.Text = sdr["department"].ToString();
                textBox3.Text = sdr["salary"].ToString();
                k++;
            }
            textBox1.ReadOnly = true;
            button1.Enabled = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            if (k == 0) MessageBox.Show("ID Not Found");
            con.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select count(*) from emp", con);
            label7.Text = "No of Employee : " + cmd.ExecuteScalar();
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox4.Text);
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from emp where id=@i", con);
            cmd.Parameters.AddWithValue("@i", id);

            int r = cmd.ExecuteNonQuery();

            MessageBox.Show(r != 0 ? "deleted" : "failed");

            con.Close();

            LoadData();
            FindCount();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = int.Parse(textBox4.Text);
            int age = int.Parse(textBox2.Text);
            string department = comboBox1.Text;
            double salary = double.Parse(textBox3.Text);

            con.Open();
            SqlCommand cmd = new SqlCommand("update emp set age=@a,department=@d,salary=@s where id=@i", con);
            cmd.Parameters.AddWithValue("@i", id);
            cmd.Parameters.AddWithValue("@a", age);
            cmd.Parameters.AddWithValue("@d", department);
            cmd.Parameters.AddWithValue("@s", salary);

            int r = cmd.ExecuteNonQuery();

            MessageBox.Show(r != 0 ? "modified" : "failed");
            LoadData();
            con.Close();
        }
    }
}

