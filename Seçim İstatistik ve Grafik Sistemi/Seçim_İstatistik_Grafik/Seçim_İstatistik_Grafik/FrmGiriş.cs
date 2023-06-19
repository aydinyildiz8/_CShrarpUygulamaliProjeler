using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Seçim_İstatistik_Grafik
{
    public partial class FormVtgs : Form
    {
        public FormVtgs()
        {
            InitializeComponent();
        }

        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-99S34I5;Initial Catalog=DbSecimProje;Integrated Security=True;TrustServerCertificate=true;");
        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {

            connection.Open();
            SqlCommand komut = new SqlCommand("insert into TBLSECIM (ILCEAD,APARTI,BPARTI,CPARTI,DPARTI,EPARTI) values (@P1,@P2,@P3,@P4,@P5,@P6)", connection);

            komut.Parameters.AddWithValue("@P1", txtIlce.Text);
            komut.Parameters.AddWithValue("@P2", txtA.Text);
            komut.Parameters.AddWithValue("@P3", txtB.Text);
            komut.Parameters.AddWithValue("@P4", txtC.Text);
            komut.Parameters.AddWithValue("@P5", txtD.Text);
            komut.Parameters.AddWithValue("@P6", txtE.Text);

            komut.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Oy Girişi Gerçekleşti.");
           
        }

        private void btnGrafikler_Click(object sender, EventArgs e)
        {
            FrmGrafikler fr = new FrmGrafikler();
            fr.Show();
        }
    }
}
