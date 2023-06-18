using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Not_Kayıt_Sistemi
{
    public partial class frmOgretmenDetay : Form
    {
        public frmOgretmenDetay()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-99S34I5;Initial Catalog=DbNotKayıt;Integrated Security=True;TrustServerCertificate=true;");

        private void frmOgretmenDetay_Load(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand komut = new SqlCommand("Select * From TBLDers", connection);
            SqlDataReader dr = komut.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Load(dr);
            dataGridView1.DataSource = dataTable;
            connection.Close();

            OgrenciDurumlariniGuncelle();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();
            SqlCommand komut = new SqlCommand("Insert into TBLDers (OGRNUMARA,OGRAD,OGRSOYAD,DURUM,ORTALAMA) VALUES (@P1, @P2, @P3, @P4, @P5)", connection);
            komut.Parameters.AddWithValue("@P1", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@P2", textBox1.Text);
            komut.Parameters.AddWithValue("@P3", textBox2.Text);
            komut.Parameters.AddWithValue("@P4", false);
            komut.Parameters.AddWithValue("@P5", 0);
            komut.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Öğrenci Sisteme Başarılı Bir Şekilde Eklendi.");

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM TBLDers", connection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;

            OgrenciDurumlariniGuncelle();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            maskedTextBox1.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            textBox1.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            textBox2.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtSinav1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtSinav2.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            TxtSinav3.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();

            label10.Text = dataGridView1.Rows[secilen].Cells[8].Value.ToString();
            lblOrtalama.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double ogrOrtalama, s1, s2, s3;
            string durum;

            s1 = Convert.ToDouble(TxtSinav1.Text);
            s2 = Convert.ToDouble(TxtSinav2.Text);
            s3 = Convert.ToDouble(TxtSinav3.Text);


            ogrOrtalama = (s1 + s2 + s3) / 3;
            lblOrtalama.Text = ogrOrtalama.ToString();

            if (ogrOrtalama >= 50)
            {
                durum = "True";
                label10.Text = durum;
                MessageBox.Show("GEÇTİ.");
            }
            else
            {
                durum = "False";
                label10.Text = durum;
                MessageBox.Show("KALDI.");
            }

            connection.Open();
            SqlCommand komut = new SqlCommand("Update TBLDers set OGRSINAV1=@P1,OGRSINAV2=@P2,OGRSINAV3=@P3,ORTALAMA=@P4,DURUM=@P5 where OGRNUMARA=@P6", connection);

            komut.Parameters.AddWithValue("@P1", TxtSinav1.Text);
            komut.Parameters.AddWithValue("@P2", TxtSinav2.Text);
            komut.Parameters.AddWithValue("@P3", TxtSinav3.Text);
            komut.Parameters.AddWithValue("@P4", decimal.Parse(lblOrtalama.Text));
            komut.Parameters.AddWithValue("@P5", durum);
            komut.Parameters.AddWithValue("@P6", maskedTextBox1.Text);


            komut.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Öğrenci Sisteme Başarılı Bir Şekilde Eklendi.");

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM TBLDers", connection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;

            OgrenciDurumlariniGuncelle();

        }

        private void OgrenciDurumlariniGuncelle()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM TBLDers", connection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            int gecenSayisi = dataTable.AsEnumerable().Count(row => row.Field<bool>("DURUM") == true);
            int kalanSayisi = dataTable.AsEnumerable().Count(row => row.Field<bool>("DURUM") == false);

            //int gecenSayisi = dataTable.AsEnumerable().Count(row => Convert.ToBoolean(row.Field<object>("DURUM") ?? false));
            //int kalanSayisi = dataTable.AsEnumerable().Count(row => !Convert.ToBoolean(row.Field<object>("DURUM") ?? false));

            label11.Text = gecenSayisi.ToString();
            label12.Text = kalanSayisi.ToString();

            decimal toplamOrtalama = dataTable.AsEnumerable().Average(row => row.Field<decimal>("ORTALAMA"));
            label15.Text = Convert.ToDouble(toplamOrtalama).ToString();
        }



    }
}
