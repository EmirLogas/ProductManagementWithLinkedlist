using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace baglilistelerprojesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public class dugum
        {
            public long urunKodu;
            public string urunAdi;
            public double urunFiyati;
            public dugum sonraki;
            public dugum onceki;
        }
        dugum ilk = null;
        dugum son = null;
        int urunSayisi = 0;
        private void bttn_Ekle_Click(object sender, EventArgs e)
        {
            if (txtbox_ekleurunadi.Text == "" || txtbox_ekleurunfiyati.Text == "" || txtbox_ekleurunkodu.Text == "")
            {
                MessageBox.Show("Gerekli bilgiler doldurulmalı.");
            }
            else
            {
                dugum yeni = new dugum();
                dugum gecici = new dugum();
                yeni.urunKodu = Convert.ToInt64(txtbox_ekleurunkodu.Text);
                yeni.urunAdi = txtbox_ekleurunadi.Text;
                yeni.urunFiyati = Convert.ToDouble(txtbox_ekleurunfiyati.Text);
                if (ilk == null)
                {
                    urunSayisi++;
                    yeni.onceki = null;
                    yeni.sonraki = null;
                    ilk = yeni;
                    son = yeni;
                }
                else
                {
                    gecici = ilk;

                    bool uruneklendi = false;
                    for (int i = 0; i < urunSayisi; i++)
                    {
                        if (yeni.urunKodu != gecici.urunKodu)
                        {
                            uruneklendi = true;
                            gecici = gecici.sonraki;
                        }
                        else
                        {
                            uruneklendi = false;
                            break;
                        }
                    }
                    if (uruneklendi == true)
                    {
                        urunSayisi++;
                        ilk.onceki = yeni;
                        yeni.sonraki = ilk;
                        ilk = yeni;
                        ilk.onceki = null;
                    }
                    else
                    {
                        MessageBox.Show("Ürün kodu başka bir ürün ile çakışıyor.");
                    }
                }
            }
        }
        private void bttn_urunlistele_Click(object sender, EventArgs e)
        {
            listele(ilk);
        }
        public void listele(dugum ilk)
        {
            dataGridView1.Rows.Clear();
            while (ilk != null)
            {
                dataGridView1.Rows.Add(ilk.urunKodu, ilk.urunAdi, ilk.urunFiyati);
                ilk = ilk.sonraki;
            }
        }
        long silarananurunkodu;
        bool silurunbulundu;
        private void bttn_silbul_Click(object sender, EventArgs e)
        {
            silurunbulundu = false;
            dugum geciciilk;
            geciciilk = ilk;
            for (int i = 0; i < urunSayisi; i++)
            {
                if (geciciilk.urunKodu == Convert.ToInt64(txtbox_silurunkodu.Text))
                {
                    silurunbulundu = true;
                    silarananurunkodu = geciciilk.urunKodu;
                    txtbox_silurunadi.Text = geciciilk.urunAdi;
                    txtbox_silurunfiyati.Text = geciciilk.urunFiyati.ToString();
                }
                else
                {
                    geciciilk = geciciilk.sonraki;
                }
            }
            if (silurunbulundu == false)
            {
                MessageBox.Show("Ürün Bulunamadı");
                txtbox_silurunadi.Text = null;
                txtbox_silurunfiyati.Text = null;
            }
        }
        private void bttn_sil_Click(object sender, EventArgs e)
        {
            dugum gecici = new dugum();
            dugum silinecek = new dugum();
            gecici = ilk;
            if (silurunbulundu == true)
            {
                if (silarananurunkodu == ilk.urunKodu)
                {
                    urunSayisi--;
                    ilk = ilk.sonraki;
                }
                else if (silarananurunkodu == son.urunKodu)
                {
                    urunSayisi--;
                    son = son.onceki;
                    ilk.onceki = null;
                    son.sonraki = null;
                }
                else
                {
                    for (int i = 0; i < urunSayisi; i++)
                    {
                        if (silarananurunkodu == gecici.urunKodu)
                        {
                            urunSayisi--;
                            silinecek = gecici;
                            silinecek.onceki.sonraki = silinecek.sonraki;
                            silinecek.sonraki.onceki = silinecek.onceki;
                            break;
                        }
                        else
                        {
                            gecici = gecici.sonraki;
                        }
                    }
                }
            }
        }
        bool guncelleurunbulundu;
        long guncellearananurunkodu;
        private void bttn_guncellebul_Click(object sender, EventArgs e)
        {
            guncelleurunbulundu = false;
            dugum geciciilk;
            geciciilk = ilk;
            for (int i = 0; i < urunSayisi; i++)
            {
                if (geciciilk.urunKodu == Convert.ToInt64(txtbox_guncelleurunkodu.Text))
                {
                    guncelleurunbulundu = true;
                    guncellearananurunkodu = geciciilk.urunKodu;
                    txtbox_guncelleurunadi.Text = geciciilk.urunAdi;
                    txtbox_guncelleurunfiyati.Text = geciciilk.urunFiyati.ToString();
                }
                else
                {
                    geciciilk = geciciilk.sonraki;
                }
            }
            if (guncelleurunbulundu == false)
            {
                MessageBox.Show("Ürün Bulunamadı");
                txtbox_guncelleurunadi.Text = null;
                txtbox_guncelleurunfiyati.Text = null;
            }
        }
        private void bttn_guncelle_Click(object sender, EventArgs e)
        {
            dugum gecici = new dugum();
            dugum silinecek = new dugum();
            gecici = ilk;
            if (guncelleurunbulundu == true)
            {
                for (int i = 0; i < urunSayisi; i++)
                {
                    if (guncellearananurunkodu == gecici.urunKodu)
                    {
                        gecici.urunAdi = txtbox_guncelleurunadi.Text;
                        gecici.urunFiyati = Convert.ToDouble(txtbox_guncelleurunfiyati.Text);
                        break;
                    }
                    else
                    {
                        gecici = gecici.sonraki;
                    }
                }
            }
        }
    }
}