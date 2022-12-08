using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;

namespace project
{
	public partial class Login : Form
	{
		public Login()
		{
			InitializeComponent();
		}
		//baglanti bgl = new baglanti();
		SqlConnection baglanti;
		SqlCommand komut;
		//SqlDataAdapter da;


		private void button11_Click(object sender, EventArgs e)
		{
			baglanti = new SqlConnection("Data Source=DESKTOP-OIN789I\\SQL;Initial Catalog=deneme;Integrated Security=True");
			baglanti.Open();
			komut = new SqlCommand("select password from login where username=@username", baglanti);
			komut.Parameters.AddWithValue("@username", comboBox1.Text);

			int gelensifre = (int) komut.ExecuteScalar();
			baglanti.Close();
			if (Convert.ToInt32(textBox1.Text) != gelensifre)
			{
				MessageBox.Show("Şifre yNLIŞ");
			}
			else
			{

				Anasayfa frm1 = new Anasayfa();
				frm1.kullanici = comboBox1.Text;
				
				frm1.ShowDialog();
			}
		}

		private void secilibutonlar(object sender, EventArgs e)
		{
			if (textBox1.Text == "0") {
				textBox1.Text = "";
			}
			textBox1.Text = textBox1.Text + ((Button)sender).Text;
		}

		private void button12_Click(object sender, EventArgs e)
		{
			try
			{
				textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
			}
			catch
			{
				MessageBox.Show("Hatali Giriş");
			}
		}

		private void Login_Load(object sender, EventArgs e)
		{
			baglanti = new SqlConnection("Data Source=DESKTOP-OIN789I\\SQL;Initial Catalog=deneme;Integrated Security=True");
			baglanti.Open();
			komut = new SqlCommand("select * from login",baglanti);
			SqlDataReader read = komut.ExecuteReader();
			while (read.Read()){
				comboBox1.Items.Add(read["username"]);
			}
			baglanti.Close();
			/*	
			SqlCommand sorgu = new SqlCommand("select deneme from login where username",baglanti);
			

			foreach (DataRow dr in dt.Rows) {
				comboBox1.Items.Add(dr["login"].ToString());
			}
			baglanti.Close();
*/
		}

		
	}
}
