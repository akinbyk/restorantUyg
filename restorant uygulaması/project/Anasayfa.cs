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
	public partial class Anasayfa : Form
	{
		public string kullanici = string.Empty;
		SqlConnection baglanti;
		SqlCommand komut;

		SqlDataReader dr;

		public Anasayfa()
		{
			InitializeComponent();
		}
		//baglanti bgl = new baglanti();
		// Ayarlar Butonu
		private void button1_Click(object sender, EventArgs e)
		{
			Home frm2 = new Home(); frm2.Show();
		}


		void tab_grup_isimleri_ekleme()
		{
			baglanti = new SqlConnection("Data Source=DESKTOP-OIN789I\\SQL;Initial Catalog=deneme;Integrated Security=True");
			komut = new SqlCommand("select id_ust_urun_grubu from barkodsuz_urun_ekleme", baglanti);
			baglanti.Open();
			dr = komut.ExecuteReader();
			tabControl1.TabPages.Clear();
			while (dr.Read())
			{
				tabControl1.TabPages.Add(dr["id_ust_urun_grubu"].ToString());
			}
			baglanti.Close();
			dr.Close();
			//string first_page = tabControl1.TabPages[0].Text;
		}

		// -------------------------------ANASAYFA-LOAD-------------------------------
		private void Anasayfa_Load(object sender, EventArgs e)
		{
			//SqlConnection conn = new SqlConnection(bgl.Adres);

			tab_grup_isimleri_ekleme();
			label1.Text = kullanici;

			// Kasiyer Yetki Kontrolu
			baglanti = new SqlConnection("Data Source=DESKTOP-OIN789I\\SQL;Initial Catalog=deneme;Integrated Security=True");
			baglanti.Open();
			komut = new SqlCommand("select yetki from login where username=@username", baglanti);
			komut.Parameters.AddWithValue("@username", kullanici);
			int gelenyetki = (int)komut.ExecuteScalar();
			if (gelenyetki == 1) { button1.Visible = true; } else { button1.Visible = false; }
			baglanti.Close();


			tablarin_indexinden_buton_cekme();
			hesap();
			

		}

		public void hesap()
		{
			double toplam = 0;
			for (int i = 0; i < listBox2.Items.Count; i++)
			{
				toplam += Convert.ToDouble(listBox2.Items[i]);
			}
			label6.Text = ":"+toplam;
		}
		
		 
		
		// -----dinamik butonla listbox'a ekleme----
		private void Btn_Click(object sender, EventArgs e)
		{
			listBox1.Items.Add(((Button)sender).Tag  +
			"		" + ((Button)sender).Text + "TL");

			
			listBox2.Items.Add(((Button)sender).Text);
			hesap();
			

		}
		// Anasayfa Sil Butonu
		private void sil_Click(object sender, EventArgs e)
		{
			try
			{
				int a = listBox1.SelectedIndex;
				listBox1.Items.Remove(listBox1.SelectedItem);
				listBox2.Items.RemoveAt(a);
				hesap();
				
				MessageBox.Show("silindi");
			}
			catch
			{
				MessageBox.Show("Seçili Ürün Yok");
			}	
		}

		public void deneme() {
			
			if (listBox1.Items.Count == 0) {
				listBox2.Items.Clear();
				label6.Text = "0";
			}
			
		}
		// Hesabı kapat butonu(listbox silinir veriler db ye kaydolur).
		private void hesabi_kapat_Click(object sender, EventArgs e)
		{
			listBox1.Items.Clear();
			MessageBox.Show("hesabı kapat fonk");
			deneme();



		}
		// Bu fonksiyon tabpage indexinin text 'ini veritabanında sorgulayarak oluşturulan dinamik flowlayoutpanel e db den çekilen dinamik butonları ekler.
		public void tablarin_indexinden_buton_cekme() {
			if (tabControl1.TabPages.Count != 0)
			{
				FlowLayoutPanel pnl = new FlowLayoutPanel();
				pnl.Size = new Size(820, 480);
				pnl.AutoScroll = true;
				string selected_page = tabControl1.TabPages[tabControl1.SelectedIndex].Text;
				baglanti = new SqlConnection("Data Source=DESKTOP-OIN789I\\SQL;Initial Catalog=deneme;Integrated Security=True");
				komut = new SqlCommand("select id_alt_urun_adi,fiyat,fotograf from barkodsuz_urun_ekleme as bue where bue.id_ust_urun_grubu = '" + selected_page + "'", baglanti);
				baglanti.Open();
				dr = komut.ExecuteReader();
				int sayac = 0;
				while (dr.Read())
				{
					sayac++;
					Button btn = new Button();
					btn.Text = String.Concat(dr["fiyat"] + "");
					btn.Tag = String.Concat(dr["id_alt_urun_adi"] + "");
					string resim = String.Concat(dr["fotograf"]);
					btn.BackgroundImage = Image.FromFile(resim);
					btn.BackgroundImageLayout = ImageLayout.Stretch;
					btn.Name = sayac + "";
					btn.Size = new Size(90, 90);
					btn.Location = new Point(80, 80 * sayac);
					tabControl1.TabPages[tabControl1.SelectedIndex].Controls.Add(pnl);
					pnl.Controls.Add(btn);

					btn.Click += Btn_Click;

				}

				baglanti.Close();
				dr.Close();
			}
		}
		private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			tablarin_indexinden_buton_cekme();
		}
		// Parmak İzi Geçiş Butonu.
		private void button4_Click(object sender, EventArgs e)
		{
			if (button3.Visible == false)
			{
				button3.Visible = true;
			}
			else if(button3.Visible == true)
			{
				button3.Visible = false;
			}
		}
		// parmak izi saatlerinde çalışacak fonk.
		private void button3_Click(object sender, EventArgs e)
		{
			listBox1.Items.Clear();
			MessageBox.Show("işlem");
			deneme();
		}

		
	}

		

}

