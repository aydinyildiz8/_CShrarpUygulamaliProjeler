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
    public partial class FrmÖgrenciDetay : Form
    {
        public FrmÖgrenciDetay()
        {
            InitializeComponent();
        }

        public string numara;

        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-99S34I5;Initial Catalog=DbNotKayıt;Integrated Security=True;TrustServerCertificate=true;");

        private void FrmÖgrenciDetay_Load(object sender, EventArgs e)
        {
            lblNumara.Text = numara;

            connection.Open();
            SqlCommand komut = new SqlCommand("Select * From TBLDers where OGRNUMARA=@P1", connection);
            komut.Parameters.AddWithValue("@P1", numara);

            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[2].ToString() + " " + dr[3].ToString();
                lblSınav1.Text = dr[4].ToString();
                lblSınav2.Text = dr[5].ToString();
                lblSınav3.Text = dr[6].ToString();
                lblOrtalama.Text = dr[7].ToString();
                lblDurum.Text = dr[8].ToString();
            }
            connection.Close();
        }
    }
}
