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
	public partial class Home : Form
	{
		public Home()
		{
			InitializeComponent();
			

		}
		SqlConnection baglanti;
		SqlCommand komut;
		//SqlDataAdapter da;
		//baglanti bgl = new baglanti();


		private void barkodsuz_kaydet_Click(object sender, EventArgs e)
		{
			listBox2.Items.Clear();
			listBox3.Items.Clear();
			baglanti = new SqlConnection("Data Source=DESKTOP-OIN789I\\SQL;Initial Catalog=deneme;Integrated Security=True");
			try
			{
				if (baglanti.State == ConnectionState.Closed)
				{
					baglanti.Open();
					SqlCommand komut = new SqlCommand("insert into barkodsuz_urun_ekleme(id_ust_urun_grubu,id_alt_urun_adi,fiyat,fotograf) values(@p1,@p2,@p3,@p4)", baglanti);

					komut.Parameters.AddWithValue("@p1", textBox3.Text);
					komut.Parameters.AddWithValue("@p2", textBox4.Text);
					komut.Parameters.AddWithValue("@p3", float.Parse(textBox1.Text));
					komut.Parameters.AddWithValue("@p4", textBox2.Text);
					komut.ExecuteNonQuery();
					baglanti.Close();
					MessageBox.Show("Basarılı");
				}
			}
			catch (Exception hata)
			{

				MessageBox.Show("hata oluştu", hata.Message);
			}
			barkodsuz_urun_grup();
			barkodsuz_urun_adi();

			// textbox temizleme (Listbox2)
			textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear();

		}

		private void button7_Click(object sender, EventArgs e)
		{
			openFileDialog1.ShowDialog();
			pictureBox1.ImageLocation = openFileDialog1.FileName;
			textBox2.Text = openFileDialog1.FileName;
		}

		private void button8_Click(object sender, EventArgs e)
		{
			openFileDialog2.ShowDialog();
			pictureBox2.ImageLocation = openFileDialog2.FileName;
			textBox7.Text = openFileDialog2.FileName;
		}

		private void barkodlu_kaydet_Click(object sender, EventArgs e)
		{
			listBox4.Items.Clear();
			listBox5.Items.Clear();
			baglanti = new SqlConnection("Data Source=DESKTOP-OIN789I\\SQL;Initial Catalog=deneme;Integrated Security=True");
			try
			{
				if (baglanti.State == ConnectionState.Closed)
				{
					baglanti.Open();
					SqlCommand komut = new SqlCommand("insert into barkodlu_urun_ekleme(id_ust_urun_grubu,id_alt_urun_adi,fiyat,barkod,fotograf) values(@p1,@p2,@p3,@p4,@p5)", baglanti);

					komut.Parameters.AddWithValue("@p1", textBox8.Text);
					komut.Parameters.AddWithValue("@p2", textBox9.Text);
					komut.Parameters.AddWithValue("@p3", float.Parse(textBox5.Text));
					komut.Parameters.AddWithValue("@p4", textBox6.Text);
					komut.Parameters.AddWithValue("@p5", textBox7.Text);
					komut.ExecuteNonQuery();
					baglanti.Close();
					MessageBox.Show("Basarılı");
				}
			}
			catch (Exception hata)
			{

				MessageBox.Show("hata oluştu", hata.Message);
			}

			barkodlu_urun_grubu();
			barkodlu_urun_adi();

			textBox5.Clear(); textBox6.Clear(); textBox7.Clear(); textBox8.Clear(); textBox9.Clear();
		}
		// -------------------(VERİ GETİRME LİSTBOX *URUN EKLE* SEKMESİ)-----------------------
		void barkodsuz_urun_grup() {

			baglanti = new SqlConnection("Data Source=DESKTOP-OIN789I\\SQL;Initial Catalog=deneme;Integrated Security=True");
			baglanti.Open();
			komut = new SqlCommand("select id_ust_urun_grubu from barkodsuz_urun_ekleme", baglanti);
			SqlDataReader dr = komut.ExecuteReader();

			while(dr.Read())
			{
				listBox2.Items.Add(dr["id_ust_urun_grubu"].ToString());
			}
			baglanti.Close();
			/*
			DataTable tablo = new DataTable();
			da.Fill(tablo);
			listBox2.DataSource = tablo;
			baglanti.Close();
			*/


		}
		void barkodsuz_urun_adi() {

			baglanti = new SqlConnection("Data Source=DESKTOP-OIN789I\\SQL;Initial Catalog=deneme;Integrated Security=True");
			baglanti.Open();
			komut = new SqlCommand("select id_alt_urun_adi,fotograf from barkodsuz_urun_ekleme ", baglanti);
			SqlDataReader dr = komut.ExecuteReader();

			while (dr.Read())
			{
				listBox3.Items.Add(dr["id_alt_urun_adi"].ToString());
				//listBox3.Items.Add(dr["fotograf"].ToString());// hatalı çalışıyor
			}
			baglanti.Close();
		}
		public void barkodlu_urun_grubu() {

			baglanti = new SqlConnection("Data Source=DESKTOP-OIN789I\\SQL;Initial Catalog=deneme;Integrated Security=True");
			baglanti.Open();
			komut = new SqlCommand("select id_ust_urun_grubu from barkodlu_urun_ekleme ", baglanti);
			SqlDataReader dr = komut.ExecuteReader();

			while (dr.Read())
			{
				listBox4.Items.Add(dr["id_ust_urun_grubu"].ToString());
			}
			baglanti.Close();
		}
		public void barkodlu_urun_adi() {

			baglanti = new SqlConnection("Data Source=DESKTOP-OIN789I\\SQL;Initial Catalog=deneme;Integrated Security=True");
			baglanti.Open();
			komut = new SqlCommand("select id_alt_urun_adi from barkodlu_urun_ekleme ", baglanti);
			SqlDataReader dr = komut.ExecuteReader();

			while (dr.Read())
			{
				listBox5.Items.Add(dr["id_alt_urun_adi"].ToString());
			}
			baglanti.Close();
		}
		// combobox veri getirme --------------(ANALİZ)----------------------
		public void kasiyer_sais_raporları() {
			baglanti = new SqlConnection("Data Source=DESKTOP-OIN789I\\SQL;Initial Catalog=deneme;Integrated Security=True");
			baglanti.Open();
			komut = new SqlCommand("select username from login", baglanti);
			SqlDataReader read = komut.ExecuteReader();
			while (read.Read())
			{
				comboBox2.Items.Add(read["username"]);
			}
			baglanti.Close();
		}
		public void nakit_analizi()
		{
			baglanti = new SqlConnection("Data Source=DESKTOP-OIN789I\\SQL;Initial Catalog=deneme;Integrated Security=True");
			baglanti.Open();
			komut = new SqlCommand("select username from login", baglanti);
			SqlDataReader read = komut.ExecuteReader();
			while (read.Read())
			{
				comboBox3.Items.Add(read["username"]);
			}
			baglanti.Close();
		}
		public void urun_satis_analizi_urun_grup()
		{
			baglanti = new SqlConnection("Data Source=DESKTOP-OIN789I\\SQL;Initial Catalog=deneme;Integrated Security=True");
			baglanti.Open();
			komut = new SqlCommand("select id_ust_urun_grubu from barkodsuz_urun_ekleme", baglanti);
			SqlDataReader read = komut.ExecuteReader();
			while (read.Read())
			{
				comboBox6.Items.Add(read["id_ust_urun_grubu"]);
			}
			baglanti.Close();
		}
		public void urun_satis_analizi_urun_adi()
		{
			baglanti = new SqlConnection("Data Source=DESKTOP-OIN789I\\SQL;Initial Catalog=deneme;Integrated Security=True");
			baglanti.Open();
			komut = new SqlCommand("select id_alt_urun_adi from barkodsuz_urun_ekleme", baglanti);
			SqlDataReader read = komut.ExecuteReader();
			while (read.Read())
			{
				comboBox7.Items.Add(read["id_alt_urun_adi"]);
			}
			baglanti.Close();
		}

		private void Home_Load(object sender, EventArgs e)
		{
			//
			this.chart1.Series["yemek"].Points.AddY(20);
			this.chart1.Series["kahvaltilik"].Points.AddY(5);
			this.chart1.Series["icecek"].Points.AddY(10);
			this.chart1.Series["kahve"].Points.AddY(13);
			//
			barkodsuz_urun_grup();
			barkodsuz_urun_adi();
			barkodlu_urun_grubu();
			barkodlu_urun_adi();
			//------- Home - Analiz (Combobox)
			kasiyer_sais_raporları();
			nakit_analizi();
			urun_satis_analizi_urun_grup();
			urun_satis_analizi_urun_adi();
			/*
			String date = DateTime.UtcNow.ToString("MM-dd-yyyy");
			Console.WriteLine("tarih {0}", date);
			*/

			bugunun_tarihi();
		}
		public void bugunun_tarihi() {
			DateTime tarih = new DateTime();
			tarih = dateTimePicker1.Value;
			label20.Text = tarih.ToLongDateString();
		}

		private void barkodsuz_urun_grubu_guncelle_Click(object sender, EventArgs e)
		{
			baglanti = new SqlConnection("Data Source=DESKTOP-OIN789I\\SQL;Initial Catalog=deneme;Integrated Security=True");
			try
			{
				string sorgu = "UPDATE barkodlu_urun_ekleme SET id_ust_urun_grubu=@id_ust_urun_grubu WHERE id=@id";
				komut = new SqlCommand(sorgu, baglanti);
				komut.Parameters.AddWithValue("@id",textBox14.Text);
				komut.Parameters.AddWithValue("@id_ust_urun_grubu", textBox3.Text);
				baglanti.Open();
				komut.ExecuteNonQuery();
				baglanti.Close();
				barkodsuz_urun_grup();
			}
			catch (Exception hata)
			{

				MessageBox.Show("güncellenemedi",hata.Message);
			}
		}


		// textboxlara listbox dan tıklanan verileri çekme fonksiyonları (urun ekleme)
		private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBox3.Text= listBox2.SelectedItem.ToString();
		}

		private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBox4.Text = listBox3.SelectedItem.ToString();
			textBox2.Text = listBox3.SelectedItem.ToString();

			pictureBox1.ImageLocation = listBox3.SelectedItem.ToString();
		}

		private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBox8.Text = listBox4.SelectedItem.ToString();
		}

		private void listBox5_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBox9.Text = listBox5.SelectedItem.ToString();
		}

		private void button6_Click(object sender, EventArgs e)
		{
			baglanti = new SqlConnection("Data Source=DESKTOP-OIN789I\\SQL;Initial Catalog=deneme;Integrated Security=True");
			try
			{
				if (baglanti.State == ConnectionState.Closed) {
					baglanti.Open();
					SqlCommand komut = new SqlCommand("insert into saat(hafta_ici_sabah_basla,hafta_ici_sabah_bitis,hafta_ici_aksam_basla,hafta_ici_aksam_bitis,hafta_sonu_sabah_basla,hafta_sonu_sabah_bitis,hafta_sonu_aksam_basla,hafta_sonu_sabah_bitis,kahvalti_fiyat,aksam_fiyat) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,)", baglanti);

					komut.Parameters.AddWithValue("@p1", dateTimePicker6.Value.ToShortTimeString());
					komut.Parameters.AddWithValue("@p2", dateTimePicker9.Value.ToShortTimeString());
					komut.Parameters.AddWithValue("@p3", dateTimePicker8.Value.ToShortTimeString());
					komut.Parameters.AddWithValue("@p4", dateTimePicker10.Value.ToShortTimeString());
					komut.Parameters.AddWithValue("@p5", dateTimePicker5.Value.ToShortTimeString());
					komut.Parameters.AddWithValue("@p6", dateTimePicker11.Value.ToShortTimeString());
					komut.Parameters.AddWithValue("@p7", dateTimePicker7.Value.ToShortTimeString());
					komut.Parameters.AddWithValue("@p8", dateTimePicker12.Value.ToShortTimeString());
					komut.Parameters.AddWithValue("@p9", float.Parse(textBox12.Text));
					komut.Parameters.AddWithValue("@p10", float.Parse(textBox13.Text));
					komut.ExecuteNonQuery();
					baglanti.Close();
					MessageBox.Show("basarılı");
				}

			}
			catch (Exception hata)
			{
				MessageBox.Show("hata oluştu", hata.Message);
			}
			
			
		}

		private void button1_Click(object sender, EventArgs e)
		{
			grafik_analiz grafik_sayf = new grafik_analiz(); grafik_sayf.Show();
		}

		
	}
}
