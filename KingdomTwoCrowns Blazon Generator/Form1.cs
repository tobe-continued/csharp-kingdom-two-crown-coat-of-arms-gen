using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

namespace KingdomTwoCrowns_Blazon_Generator
{
    public partial class Form1 : Form
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public Form1()
        {
            XmlConfigurator.Configure();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string[] files = Directory.GetFiles(Application.StartupPath + "\\XML" + "\\lang\\");
                DataTable dataTable = new DataTable("Langage");
                dataTable.Columns.Add("Id");
                dataTable.Columns.Add("Name");
                int num = 0;
                List<Color> colors = new List<Color>();
                XmlDocument doc = new XmlDocument();
                foreach (string file in files)
                {
                    doc.Load(Application.StartupPath + "\\XML\\lang\\" + Path.GetFileName(file));
                    XmlNodeList nodes = doc.DocumentElement.SelectNodes("/catalog/translation");

                    foreach (XmlNode node in nodes)
                    {
                        if (int.Parse(node.Attributes["id"].Value) == 999)
                        {
                            dataTable.Rows.Add(int.Parse(node.SelectSingleNode("ID").InnerText), node.SelectSingleNode("txt").InnerText);
                            num++;
                        }
                    }
                }
                comboBoxLang.DataSource = dataTable;
                comboBoxLang.DisplayMember = "Name";
                comboBoxLang.ValueMember = "Id";

                comboBoxLang.SelectedIndex = int.Parse(Properties.Settings.Default.codelang);



                pictureBox1.BackgroundImage = Image.FromFile(Application.StartupPath + "\\lang\\" + Properties.Settings.Default.lang + ".png");
                
                Translator(Properties.Settings.Default.lang);
                //Create your private font collection object.
                PrivateFontCollection pfc = new PrivateFontCollection();
                //Select your font from the resources.
                //My font here is "Digireu.ttf"
                int fontLength = Properties.Resources.double_pixel.Length;
                // create a buffer to read in to
                byte[] fontdata = Properties.Resources.double_pixel;
                // create an unsafe memory block for the font data
                System.IntPtr data = Marshal.AllocCoTaskMem(fontLength);
                // copy the bytes to the unsafe memory block
                Marshal.Copy(fontdata, 0, data, fontLength);
                // pass the font to the font collection
                pfc.AddMemoryFont(data, fontLength);
                int COULEUR = int.Parse(Properties.Settings.Default.couleur);
                int CHARGE = int.Parse(Properties.Settings.Default.charge);
                int CHARGECLRS = int.Parse(Properties.Settings.Default.chargeclrs);
                int ORDINARIES = int.Parse(Properties.Settings.Default.ordinarie);
                int ORDINARIESCLRS = int.Parse(Properties.Settings.Default.ordinarieclrs);
                if (Properties.Settings.Default.langcode == "0")
                {
                    LetsRandom();
                    Properties.Settings.Default.langcode = "1";
                }
                else
                {
                    try
                    {
                        int color = int.Parse(Properties.Settings.Default.couleur);
                        int ordinarie = int.Parse(Properties.Settings.Default.ordinarie);
                        int ordinarieclrs = int.Parse(Properties.Settings.Default.ordinarieclrs);
                        int charge = int.Parse(Properties.Settings.Default.charge);
                        int chargeclrs = int.Parse(Properties.Settings.Default.chargeclrs);
                        if (Properties.Settings.Default.jeu == "Kingdom2")
                        {
                            panel3.BackgroundImage = Properties.Resources.Logo1;
                            label3.Visible = false;
                            label5.Visible = false;
                            Left_Charg_btn.Visible = false;
                            Right_Charg_btn.Visible = false;
                            Left_Charg_color_btn.Visible = false;
                            Right_Charg_color_btn.Visible = false;
                            panel5.BackgroundImage = ChangeColor(GetCharge(charge), GetChargeColor(chargeclrs));
                            panel7.BackgroundImage = Image.FromFile(GetColor(color));
                            panel8.BackgroundImage = ChangeColor(GetOrdinarie(ordinarie), GetOrdinarieColor(ordinarieclrs));
                            textBox1.Text = (GetColorText(color) + " " + GetOrdinarieText(ordinarie) + " " + GetOrdinarieColorText(ordinarieclrs) + " " + "A SIPS" + " " + "BISQUE");
                        }
                        else
                        {
                            panel3.BackgroundImage = Properties.Resources.Logo2;
                            label3.Visible = true;
                            label5.Visible = true;
                            Left_Charg_btn.Visible = true;
                            Right_Charg_btn.Visible = true;
                            Left_Charg_color_btn.Visible = true;
                            Right_Charg_color_btn.Visible = true;
                            panel7.BackgroundImage = Image.FromFile(GetColor(color));
                            panel8.BackgroundImage = ChangeColor(GetOrdinarie(ordinarie), GetOrdinarieColor(ordinarieclrs));
                            panel5.BackgroundImage = ChangeColor(GetCharge(charge), GetChargeColor(chargeclrs));
                            textBox1.Text = (GetColorText(color) + " " + GetOrdinarieText(ordinarie) + " " + GetOrdinarieColorText(ordinarieclrs) + " " + GetChargeText(charge) + " " + GetChargeColorText(chargeclrs));
                        }
                    }
                    catch (Exception ex)
                    {
                        _log.Fatal("ERREUR : " + ex);
                    }
                }

                Exit_btn.Font = new Font(pfc.Families[0], Exit_btn.Font.Size);
                label1.Font = new Font(pfc.Families[0], label1.Font.Size);
                label2.Font = new Font(pfc.Families[0], label2.Font.Size);
                label3.Font = new Font(pfc.Families[0], label3.Font.Size);
                label4.Font = new Font(pfc.Families[0], label4.Font.Size);
                label5.Font = new Font(pfc.Families[0], label5.Font.Size);
                button3.Font = new Font(pfc.Families[0], button3.Font.Size);
                textBox1.Font = new Font(pfc.Families[0], textBox1.Font.Size);
            }
            catch (Exception ex)
            {
                _log.Fatal("ERREUR : " + ex);
            }
        }

        private void Exit_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.MessageLoop)
                {
                    // WinForms app
                    Properties.Settings.Default.FormSize = this.Size;
                    Properties.Settings.Default.Location = this.Location;
                    Properties.Settings.Default.Save();
                    System.Windows.Forms.Application.Exit();
                }
                else
                {
                    // Console app
                    Properties.Settings.Default.FormSize = this.Size;
                    Properties.Settings.Default.Location = this.Location;
                    Properties.Settings.Default.Save();
                    System.Environment.Exit(1);
                }
            }
            catch (Exception ex)
            {
                _log.Fatal("ERREUR : " + ex);
            }
        }

        private void Left_Tinkt_btn_Click(object sender, EventArgs e)
        {
            try
            {
                int color = int.Parse(Properties.Settings.Default.couleur);
                int charge = int.Parse(Properties.Settings.Default.charge);
                int chargeclrs = int.Parse(Properties.Settings.Default.chargeclrs);
                int ordinarie = int.Parse(Properties.Settings.Default.ordinarie);
                int ordinarieclrs = int.Parse(Properties.Settings.Default.ordinarieclrs);
                color--;
                Properties.Settings.Default.couleur = color.ToString();
                panel7.BackgroundImage = Image.FromFile(GetColor(color));
                textBox1.Text = (GetColorText(color) + " " + GetOrdinarieText(ordinarie) + " " + GetOrdinarieColorText(ordinarieclrs) + " " + GetChargeText(charge) + " " + GetChargeColorText(chargeclrs));
            }
            catch (Exception ex)
            {
                _log.Fatal("ERREUR : " + ex);
            }
        }

        private void Right_Tinkt_btn_Click(object sender, EventArgs e)
        {
            try
            {
                int color = int.Parse(Properties.Settings.Default.couleur);
                int charge = int.Parse(Properties.Settings.Default.charge);
                int chargeclrs = int.Parse(Properties.Settings.Default.chargeclrs);
                int ordinarie = int.Parse(Properties.Settings.Default.ordinarie);
                int ordinarieclrs = int.Parse(Properties.Settings.Default.ordinarieclrs);
                color++;
                Properties.Settings.Default.couleur = color.ToString();
                panel7.BackgroundImage = Image.FromFile(GetColor(color));
                textBox1.Text = (GetColorText(color) + " " + GetOrdinarieText(ordinarie) + " " + GetOrdinarieColorText(ordinarieclrs) + " " + GetChargeText(charge) + " " + GetChargeColorText(chargeclrs));
            }
            catch (Exception ex)
            {
                _log.Fatal("ERREUR : " + ex);
            }
        }

        private void Left_Ordin_btn_Click(object sender, EventArgs e)
        {
            try
            {
                int charge = int.Parse(Properties.Settings.Default.charge);
                int chargeclrs = int.Parse(Properties.Settings.Default.chargeclrs);
                int ordinarie = int.Parse(Properties.Settings.Default.ordinarie);
                int ordinarieclrs = int.Parse(Properties.Settings.Default.ordinarieclrs);
                int color = int.Parse(Properties.Settings.Default.couleur);
                ordinarie--;
                Properties.Settings.Default.ordinarie = ordinarie.ToString();
                panel8.BackgroundImage = new Bitmap(ChangeColor(GetOrdinarie(ordinarie), GetOrdinarieColor(ordinarieclrs)));
                textBox1.Text = (GetColorText(color) + " " + GetOrdinarieText(ordinarie) + " " + GetOrdinarieColorText(ordinarieclrs) + " " + GetChargeText(charge) + " " + GetChargeColorText(chargeclrs));
            }
            catch (Exception ex)
            {
                _log.Fatal("ERREUR : " + ex);
            }
        }

        private void Right_Ordin_btn_Click(object sender, EventArgs e)
        {
            try
            {
                int charge = int.Parse(Properties.Settings.Default.charge);
                int chargeclrs = int.Parse(Properties.Settings.Default.chargeclrs);
                int ordinarie = int.Parse(Properties.Settings.Default.ordinarie);
                int ordinarieclrs = int.Parse(Properties.Settings.Default.ordinarieclrs);
                int color = int.Parse(Properties.Settings.Default.couleur);
                ordinarie++;
                Properties.Settings.Default.ordinarie = ordinarie.ToString();
                panel8.BackgroundImage = new Bitmap(ChangeColor(GetOrdinarie(ordinarie), GetOrdinarieColor(ordinarieclrs)));
                textBox1.Text = (GetColorText(color) + " " + GetOrdinarieText(ordinarie) + " " + GetOrdinarieColorText(ordinarieclrs) + " " + GetChargeText(charge) + " " + GetChargeColorText(chargeclrs));
            }
            catch (Exception ex)
            {
                _log.Fatal("ERREUR : " + ex);
            }
        }

        private void Left_Ordin_color_btn_Click(object sender, EventArgs e)
        {
            try
            {
                int charge = int.Parse(Properties.Settings.Default.charge);
                int chargeclrs = int.Parse(Properties.Settings.Default.chargeclrs);
                int ordinarie = int.Parse(Properties.Settings.Default.ordinarie);
                int ordinarieclrs = int.Parse(Properties.Settings.Default.ordinarieclrs);
                int color = int.Parse(Properties.Settings.Default.couleur);
                ordinarieclrs--;
                Properties.Settings.Default.ordinarieclrs = ordinarieclrs.ToString();
                panel8.BackgroundImage = new Bitmap(ChangeColor(GetOrdinarie(ordinarie), GetOrdinarieColor(ordinarieclrs)));
                textBox1.Text = (GetColorText(color) + " " + GetOrdinarieText(ordinarie) + " " + GetOrdinarieColorText(ordinarieclrs) + " " + GetChargeText(charge) + " " + GetChargeColorText(chargeclrs));
            }
            catch (Exception ex)
            {
                _log.Fatal("ERREUR : " + ex);
            }
        }

        private void Right_Ordin_color_btn_Click(object sender, EventArgs e)
        {
            try
            {
                int charge = int.Parse(Properties.Settings.Default.charge);
                int chargeclrs = int.Parse(Properties.Settings.Default.chargeclrs);
                int ordinarie = int.Parse(Properties.Settings.Default.ordinarie);
                int ordinarieclrs = int.Parse(Properties.Settings.Default.ordinarieclrs);
                int color = int.Parse(Properties.Settings.Default.couleur);
                ordinarieclrs++;
                Properties.Settings.Default.ordinarieclrs = ordinarieclrs.ToString();
                panel8.BackgroundImage = new Bitmap(ChangeColor(GetOrdinarie(ordinarie), GetOrdinarieColor(ordinarieclrs)));
                textBox1.Text = (GetColorText(color) + " " + GetOrdinarieText(ordinarie) + " " + GetOrdinarieColorText(ordinarieclrs) + " " + GetChargeText(charge) + " " + GetChargeColorText(chargeclrs));
            }
            catch (Exception ex)
            {
                _log.Fatal("ERREUR : " + ex);
            }
        }

        private void Left_Charg_btn_Click(object sender, EventArgs e)
        {
            try
            {
                int charge = int.Parse(Properties.Settings.Default.charge);
                int chargeclrs = int.Parse(Properties.Settings.Default.chargeclrs);
                int ordinarie = int.Parse(Properties.Settings.Default.ordinarie);
                int ordinarieclrs = int.Parse(Properties.Settings.Default.ordinarieclrs);
                int color = int.Parse(Properties.Settings.Default.couleur);
                charge--;
                Properties.Settings.Default.charge = charge.ToString();
                panel5.BackgroundImage = new Bitmap(ChangeColor(GetCharge(charge), GetChargeColor(chargeclrs)));
                textBox1.Text = (GetColorText(color) + " " + GetOrdinarieText(ordinarie) + " " + GetOrdinarieColorText(ordinarieclrs) + " " + GetChargeText(charge) + " " + GetChargeColorText(chargeclrs));
            }
            catch (Exception ex)
            {
                _log.Fatal("ERREUR : " + ex);
            }
        }

        private void Right_Charg_btn_Click(object sender, EventArgs e)
        {
            try
            {
                int charge = int.Parse(Properties.Settings.Default.charge);
                int chargeclrs = int.Parse(Properties.Settings.Default.chargeclrs);
                int ordinarie = int.Parse(Properties.Settings.Default.ordinarie);
                int ordinarieclrs = int.Parse(Properties.Settings.Default.ordinarieclrs);
                int color = int.Parse(Properties.Settings.Default.couleur);
                charge++;
                Properties.Settings.Default.charge = charge.ToString();
                panel5.BackgroundImage = new Bitmap(ChangeColor(GetCharge(charge), GetChargeColor(chargeclrs)));
                textBox1.Text = (GetColorText(color) + " " + GetOrdinarieText(ordinarie) + " " + GetOrdinarieColorText(ordinarieclrs) + " " + GetChargeText(charge) + " " + GetChargeColorText(chargeclrs));
            }
            catch (Exception ex)
            {
                _log.Fatal("ERREUR : " + ex);
            }
        }

        private void Left_Charg_color_btn_Click(object sender, EventArgs e)
        {
            try
            {
                int charge = int.Parse(Properties.Settings.Default.charge);
                int chargeclrs = int.Parse(Properties.Settings.Default.chargeclrs);
                int ordinarie = int.Parse(Properties.Settings.Default.ordinarie);
                int ordinarieclrs = int.Parse(Properties.Settings.Default.ordinarieclrs);
                int color = int.Parse(Properties.Settings.Default.couleur);
                chargeclrs--;
                Properties.Settings.Default.chargeclrs = chargeclrs.ToString();
                panel5.BackgroundImage = new Bitmap(ChangeColor(GetCharge(charge), GetChargeColor(chargeclrs)));
                textBox1.Text = (GetColorText(color) + " " + GetOrdinarieText(ordinarie) + " " + GetOrdinarieColorText(ordinarieclrs) + " " + GetChargeText(charge) + " " + GetChargeColorText(chargeclrs));
            }
            catch (Exception ex)
            {
                _log.Fatal("ERREUR : " + ex);
            }
        }

        private void Right_Charg_color_btn_Click(object sender, EventArgs e)
        {
            try
            {
                int charge = int.Parse(Properties.Settings.Default.charge);
                int chargeclrs = int.Parse(Properties.Settings.Default.chargeclrs);
                int ordinarie = int.Parse(Properties.Settings.Default.ordinarie);
                int ordinarieclrs = int.Parse(Properties.Settings.Default.ordinarieclrs);
                int color = int.Parse(Properties.Settings.Default.couleur);
                chargeclrs++;
                Properties.Settings.Default.chargeclrs = chargeclrs.ToString();
                panel5.BackgroundImage = new Bitmap(ChangeColor(GetCharge(charge), GetChargeColor(chargeclrs)));
                textBox1.Text = (GetColorText(color) + " " + GetOrdinarieText(ordinarie) + " " + GetOrdinarieColorText(ordinarieclrs) + " " + GetChargeText(charge) + " " + GetChargeColorText(chargeclrs));
            }
            catch (Exception ex)
            {
                _log.Fatal("ERREUR : " + ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string game = Properties.Settings.Default.jeu;
                if (game == "Kingdom1")
                {
                    Properties.Settings.Default.jeu = "Kingdom2";
                    panel3.BackgroundImage = Properties.Resources.Logo1;
                }
                else if (game == "Kingdom2")
                {
                    Properties.Settings.Default.jeu = "Kingdom1";
                    panel3.BackgroundImage = Properties.Resources.Logo2;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                _log.Fatal("ERREUR : " + ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string game = Properties.Settings.Default.jeu;
                if (game == "Kingdom1")
                {
                    Properties.Settings.Default.jeu = "Kingdom2";
                    panel3.BackgroundImage = Properties.Resources.Logo1;
                }
                else if (game == "Kingdom2")
                {
                    Properties.Settings.Default.jeu = "Kingdom1";
                    panel3.BackgroundImage = Properties.Resources.Logo2;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                _log.Fatal("ERREUR : " + ex);
            }
        }

        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        private static readonly Random getrandom = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            lock (getrandom) // synchronize
            {
                return getrandom.Next(min, max);
            }
        }

        private void LetsRandom()
        {
            try
            {
                Properties.Settings.Default.couleur = GetRandomNumber(0, 48).ToString();
                Properties.Settings.Default.ordinarie = GetRandomNumber(0, 13).ToString();
                Properties.Settings.Default.ordinarieclrs = GetRandomNumber(0, 48).ToString();
                int color = int.Parse(Properties.Settings.Default.couleur);
                int ordinarie = int.Parse(Properties.Settings.Default.ordinarie);
                int ordinarieclrs = int.Parse(Properties.Settings.Default.ordinarieclrs);
                int charge = int.Parse(Properties.Settings.Default.charge);
                int chargeclrs = int.Parse(Properties.Settings.Default.chargeclrs);
                if (Properties.Settings.Default.jeu == "Kingdom2")
                {
                    panel3.BackgroundImage = Properties.Resources.Logo1;
                    label3.Visible = false;
                    label5.Visible = false;
                    Left_Charg_btn.Visible = false;
                    Right_Charg_btn.Visible = false;
                    Left_Charg_color_btn.Visible = false;
                    Right_Charg_color_btn.Visible = false;
                    panel5.BackgroundImage = ChangeColor(GetCharge(charge), GetChargeColor(chargeclrs));
                    panel7.BackgroundImage = Image.FromFile(GetColor(color));
                    panel8.BackgroundImage = ChangeColor(GetOrdinarie(ordinarie), GetOrdinarieColor(ordinarieclrs));
                    textBox1.Text = (GetColorText(color) + " " + GetOrdinarieText(ordinarie) + " " + GetOrdinarieColorText(ordinarieclrs) + " " + "A SIPS" + " " + "BISQUE");
                }
                else
                {
                    panel3.BackgroundImage = Properties.Resources.Logo2;
                    label3.Visible = true;
                    label5.Visible = true;
                    Left_Charg_btn.Visible = true;
                    Right_Charg_btn.Visible = true;
                    Left_Charg_color_btn.Visible = true;
                    Right_Charg_color_btn.Visible = true;
                    Properties.Settings.Default.charge = GetRandomNumber(0, 17).ToString();
                    Properties.Settings.Default.chargeclrs = GetRandomNumber(0, 48).ToString();
                    panel7.BackgroundImage = Image.FromFile(GetColor(color));
                    panel8.BackgroundImage = ChangeColor(GetOrdinarie(ordinarie), GetOrdinarieColor(ordinarieclrs));
                    panel5.BackgroundImage = ChangeColor(GetCharge(charge), GetChargeColor(chargeclrs));
                    textBox1.Text = (GetColorText(color) + " " + GetOrdinarieText(ordinarie) + " " + GetOrdinarieColorText(ordinarieclrs) + " " + GetChargeText(charge) + " " + GetChargeColorText(chargeclrs));
                }
            }
            catch (Exception ex)
            {
                _log.Fatal("ERREUR : " + ex);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LetsRandom();
        }

        public static string GetColor(int couleur)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Application.StartupPath + @"\XML\colors.xml");
                XmlNodeList nodes = doc.DocumentElement.SelectNodes("/catalog/color");

                List<Color> colors = new List<Color>();

                if (couleur == -1)
                {
                    foreach (XmlNode node in nodes)
                    {
                        int retour = 48;
                        string nom = node.SelectSingleNode("nom").InnerText;
                        string images = Application.StartupPath + node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == retour)
                        {
                            Properties.Settings.Default.couleur = retour.ToString();
                            return images;
                        }
                    }

                }
                else if (couleur == 49)
                {
                    foreach (XmlNode node in nodes)
                    {
                        int retour = 0;
                        string nom = node.SelectSingleNode("nom").InnerText;
                        string images = Application.StartupPath + node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == retour)
                        {
                            Properties.Settings.Default.couleur = retour.ToString();
                            return images;
                        }
                    }

                }
                else
                {
                    foreach (XmlNode node in nodes)
                    {
                        string nom = node.SelectSingleNode("nom").InnerText;
                        string images = Application.StartupPath + node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == couleur)
                        {
                            return images;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _log.Fatal("ERREUR : " + ex);
                return null;
            }
        }

        public static string GetColorText(int couleur)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(Application.StartupPath + @"\XML\colors.xml");
                XmlNodeList nodes = doc.DocumentElement.SelectNodes("/catalog/color");

                List<Color> colors = new List<Color>();

                if (couleur == -1)
                {
                    foreach (XmlNode node in nodes)
                    {
                        int retour = 48;
                        string noms = node.SelectSingleNode("nom").InnerText;
                        string images = node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == retour)
                        {
                            Properties.Settings.Default.couleur = retour.ToString();
                            return noms;
                        }
                    }

                }
                else if (couleur == 49)
                {
                    foreach (XmlNode node in nodes)
                    {
                        int retour = 0;
                        string noms = node.SelectSingleNode("nom").InnerText;
                        string images = node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == retour)
                        {
                            Properties.Settings.Default.couleur = retour.ToString();
                            return noms;
                        }
                    }

                }
                else
                {
                    foreach (XmlNode node in nodes)
                    {
                        string noms = node.SelectSingleNode("nom").InnerText;
                        string images = node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == couleur)
                        {
                            return noms;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _log.Fatal("ERREUR : " + ex);
                return null;
            }
        }

        public static string GetOrdinarie(int ordinarie)
        {
            try
            {
                string game = Properties.Settings.Default.jeu;
                XmlDocument doc = new XmlDocument();
                doc.Load(Application.StartupPath + @"\XML\ordinaries.xml");
                XmlNodeList nodes = doc.DocumentElement.SelectNodes("/catalog/ordinaries");

                List<Color> colors = new List<Color>();

                if (ordinarie == -1)
                {
                    foreach (XmlNode node in nodes)
                    {
                        int retour = 13;
                        string nom = node.SelectSingleNode("nom").InnerText;
                        string images = node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == retour)
                        {
                            Properties.Settings.Default.ordinarie = retour.ToString();
                            return images;
                        }
                    }

                }
                else if (ordinarie == 14)
                {
                    foreach (XmlNode node in nodes)
                    {
                        int retour = 0;
                        string nom = node.SelectSingleNode("nom").InnerText;
                        string images = node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == retour)
                        {
                            Properties.Settings.Default.ordinarie = retour.ToString();
                            return images;
                        }
                    }

                }
                else
                {
                    foreach (XmlNode node in nodes)
                    {
                        string nom = node.SelectSingleNode("nom").InnerText;
                        string images = node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == ordinarie)
                        {
                            return images;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _log.Fatal("ERREUR : " + ex);
                return null;
            }
        }

        public static string GetOrdinarieText(int couleur)
        {
            try
            {
                string game = Properties.Settings.Default.jeu;
                XmlDocument doc = new XmlDocument();
                doc.Load(Application.StartupPath + @"\XML\ordinaries.xml");
                XmlNodeList nodes = doc.DocumentElement.SelectNodes("/catalog/ordinaries");

                List<Color> colors = new List<Color>();
                if (couleur == -1)
                {
                    foreach (XmlNode node in nodes)
                    {
                        int retour = 13;
                        string noms = node.SelectSingleNode("nom").InnerText;
                        string images = node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == retour)
                        {
                            Properties.Settings.Default.ordinarie = retour.ToString();
                            return noms;
                        }
                    }

                }
                else if (couleur == 14)
                {
                    foreach (XmlNode node in nodes)
                    {
                        int retour = 0;
                        string noms = node.SelectSingleNode("nom").InnerText;
                        string images = node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == retour)
                        {
                            Properties.Settings.Default.ordinarie = retour.ToString();
                            return noms;
                        }
                    }

                }
                else
                {
                    foreach (XmlNode node in nodes)
                    {
                        string noms = node.SelectSingleNode("nom").InnerText;
                        string images = node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == couleur)
                        {
                            return noms;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _log.Fatal("ERREUR : " + ex);
                return null;
            }
        }

        public static string GetOrdinarieColor(int couleur)
        {
            try
            {
                string game = Properties.Settings.Default.jeu;
                XmlDocument doc = new XmlDocument();
                doc.Load(Application.StartupPath + @"\XML\ordinaries.xml");
                XmlNodeList nodes = doc.DocumentElement.SelectNodes("/catalog/color");

                List<Color> colors = new List<Color>();

                if (couleur == -1)
                {
                    foreach (XmlNode node in nodes)
                    {
                        int retour = 48;
                        string noms = node.SelectSingleNode("nom").InnerText;
                        string image = node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == retour)
                        {
                            Properties.Settings.Default.ordinarieclrs = retour.ToString();
                            return image;
                        }
                    }

                }
                else if (couleur == 49)
                {
                    foreach (XmlNode node in nodes)
                    {
                        int retour = 0;
                        string noms = node.SelectSingleNode("nom").InnerText;
                        string image = node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == retour)
                        {
                            Properties.Settings.Default.ordinarieclrs = retour.ToString();
                            return image;
                        }
                    }

                }
                else
                {
                    foreach (XmlNode node in nodes)
                    {
                        string noms = node.SelectSingleNode("nom").InnerText;
                        string image = node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == couleur)
                        {
                            return image;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _log.Fatal("ERREUR : " + ex);
                return null;
            }
        }

        public static string GetOrdinarieColorText(int couleur)
        {
            try
            {
                string game = Properties.Settings.Default.jeu;
                XmlDocument doc = new XmlDocument();
                doc.Load(Application.StartupPath + @"\XML\ordinaries.xml");
                XmlNodeList nodes = doc.DocumentElement.SelectNodes("/catalog/color");

                List<Color> color = new List<Color>();

                if (couleur == -1)
                {
                    foreach (XmlNode node in nodes)
                    {
                        int retour = 48;
                        string noms = node.SelectSingleNode("nom").InnerText;
                        string argb = node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == retour)
                        {
                            Properties.Settings.Default.ordinarieclrs = retour.ToString();
                            return noms;
                        }
                    }

                }
                else if (couleur == 49)
                {
                    foreach (XmlNode node in nodes)
                    {
                        int retour = 0;
                        string noms = node.SelectSingleNode("nom").InnerText;
                        string argb = node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == retour)
                        {
                            Properties.Settings.Default.ordinarieclrs = retour.ToString();
                            return noms;
                        }
                    }

                }
                else
                {
                    foreach (XmlNode node in nodes)
                    {
                        string noms = node.SelectSingleNode("nom").InnerText;
                        string argb = node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == couleur)
                        {
                            return noms;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _log.Fatal("ERREUR : " + ex);
                return null;
            }
        }

        public static string GetCharge(int charge)
        {
            try
            {
                string game = Properties.Settings.Default.jeu;
                XmlDocument doc = new XmlDocument();
                doc.Load(Application.StartupPath + @"\XML\charges.xml");
                XmlNodeList nodes = doc.DocumentElement.SelectNodes("/catalog/charge");

                List<Color> colors = new List<Color>();

                if (charge == -1)
                {
                    foreach (XmlNode node in nodes)
                    {
                        int retour = 17;
                        string nom = node.SelectSingleNode("nom").InnerText;
                        string images = node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == retour)
                        {
                            Properties.Settings.Default.charge = retour.ToString();
                            return images;
                        }
                    }

                }
                else if (charge == 18)
                {
                    foreach (XmlNode node in nodes)
                    {
                        int retour = 0;
                        string nom = node.SelectSingleNode("nom").InnerText;
                        string images = node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == retour)
                        {
                            Properties.Settings.Default.charge = retour.ToString();
                            return images;
                        }
                    }

                }
                else
                {
                    foreach (XmlNode node in nodes)
                    {
                        string nom = node.SelectSingleNode("nom").InnerText;
                        string images = node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == charge)
                        {
                            return images;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _log.Fatal("ERREUR : " + ex);
                return null;
            }
        }

        public static string GetChargeText(int couleur)
        {
            try
            {
                string game = Properties.Settings.Default.jeu;
                XmlDocument doc = new XmlDocument();
                doc.Load(Application.StartupPath + @"\XML\charges.xml");
                XmlNodeList nodes = doc.DocumentElement.SelectNodes("/catalog/charge");

                List<Color> colors = new List<Color>();
                if (couleur == -1)
                {
                    foreach (XmlNode node in nodes)
                    {
                        int retour = 18;
                        string noms = node.SelectSingleNode("nom").InnerText;
                        string images = node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == retour)
                        {
                            Properties.Settings.Default.charge = retour.ToString();
                            return noms;
                        }
                    }

                }
                else if (couleur == 19)
                {
                    foreach (XmlNode node in nodes)
                    {
                        int retour = 0;
                        string noms = node.SelectSingleNode("nom").InnerText;
                        string images = node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == retour)
                        {
                            Properties.Settings.Default.charge = retour.ToString();
                            return noms;
                        }
                    }

                }
                else
                {
                    foreach (XmlNode node in nodes)
                    {
                        string noms = node.SelectSingleNode("nom").InnerText;
                        string images = node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == couleur)
                        {
                            return noms;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _log.Fatal("ERREUR : " + ex);
                return null;
            }
        }

        public static string GetChargeColor(int couleur)
        {
            try
            {
                string game = Properties.Settings.Default.jeu;
                XmlDocument doc = new XmlDocument();
                doc.Load(Application.StartupPath + @"\XML\charges.xml");
                XmlNodeList nodes = doc.DocumentElement.SelectNodes("/catalog/color");

                List<Color> colors = new List<Color>();

                if (couleur == -1)
                {
                    foreach (XmlNode node in nodes)
                    {
                        int retour = 48;
                        string noms = node.SelectSingleNode("nom").InnerText;
                        string image = node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == retour)
                        {
                            Properties.Settings.Default.chargeclrs = retour.ToString();
                            return image;
                        }
                    }

                }
                else if (couleur == 49)
                {
                    foreach (XmlNode node in nodes)
                    {
                        int retour = 0;
                        string noms = node.SelectSingleNode("nom").InnerText;
                        string image = node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == retour)
                        {
                            Properties.Settings.Default.chargeclrs = retour.ToString();
                            return image;
                        }
                    }

                }
                else
                {
                    foreach (XmlNode node in nodes)
                    {
                        string noms = node.SelectSingleNode("nom").InnerText;
                        string image = node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == couleur)
                        {
                            return image;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _log.Fatal("ERREUR : " + ex);
                return null;
            }
        }

        public static string GetChargeColorText(int couleur)
        {
            try
            {
                string game = Properties.Settings.Default.jeu;
                XmlDocument doc = new XmlDocument();
                doc.Load(Application.StartupPath + @"\XML\charges.xml");
                XmlNodeList nodes = doc.DocumentElement.SelectNodes("/catalog/color");

                List<Color> color = new List<Color>();

                if (couleur == -1)
                {
                    foreach (XmlNode node in nodes)
                    {
                        int retour = 48;
                        string noms = node.SelectSingleNode("nom").InnerText;
                        string argb = node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == retour)
                        {
                            Properties.Settings.Default.chargeclrs = retour.ToString();
                            return noms;
                        }
                    }

                }
                else if (couleur == 49)
                {
                    foreach (XmlNode node in nodes)
                    {
                        int retour = 0;
                        string noms = node.SelectSingleNode("nom").InnerText;
                        string argb = node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == retour)
                        {
                            Properties.Settings.Default.chargeclrs = retour.ToString();
                            return noms;
                        }
                    }

                }
                else
                {
                    foreach (XmlNode node in nodes)
                    {
                        string noms = node.SelectSingleNode("nom").InnerText;
                        string argb = node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == couleur)
                        {
                            return noms;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _log.Fatal("ERREUR : " + ex);
                return null;
            }
        }

        public static Bitmap ChangeColor(string file, string colorcode)
        {
            try
            {
                string game = Properties.Settings.Default.jeu;
                // Load the images.
                Bitmap mask = (Bitmap)Image.FromFile(Application.StartupPath + "\\" + game + file);
                Bitmap input = (Bitmap)Image.FromFile(Application.StartupPath + "\\" + colorcode);


                Bitmap output = new Bitmap(input.Width, input.Height, PixelFormat.Format32bppArgb);
                output.MakeTransparent();
                var rect = new Rectangle(0, 0, input.Width, input.Height);

                BitmapData bitsMask = mask.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                BitmapData bitsInput = input.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                BitmapData bitsOutput = output.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                unsafe
                {
                    for (int y = 0; y < input.Height; y++)
                    {
                        byte* ptrMask = (byte*)bitsMask.Scan0 + y * bitsMask.Stride;
                        byte* ptrInput = (byte*)bitsInput.Scan0 + y * bitsInput.Stride;
                        byte* ptrOutput = (byte*)bitsOutput.Scan0 + y * bitsOutput.Stride;
                        for (int x = 0; x < input.Width; x++)
                        {
                            //I think this is right - if the blue channel is 0 than all of them are (monochrome mask) which makes the mask black
                            if (ptrMask[4 * x] == 0)
                            {
                                ptrOutput[4 * x] = ptrInput[4 * x]; // blue
                                ptrOutput[4 * x + 1] = ptrInput[4 * x + 1]; // green
                                ptrOutput[4 * x + 2] = ptrInput[4 * x + 2]; // red

                                //Ensure opaque
                                ptrOutput[4 * x + 3] = 255;
                            }
                            else
                            {
                                ptrOutput[4 * x] = 0; // blue
                                ptrOutput[4 * x + 1] = 0; // green
                                ptrOutput[4 * x + 2] = 0; // red

                                //Ensure Transparent
                                ptrOutput[4 * x + 3] = 0; // alpha
                            }
                        }
                    }

                }
                mask.UnlockBits(bitsMask);
                input.UnlockBits(bitsInput);
                output.UnlockBits(bitsOutput);

                return output;

            }
            catch (Exception ex)
            {
                _log.Fatal("ERREUR : " + ex);
                return null;
            }
        }

        public static Bitmap ChangeOpacity(Image img)
        {
            try
            {
                Bitmap bmp = new Bitmap(img.Width, img.Height); // Determining Width and Height of Source Image
                Graphics graphics = Graphics.FromImage(bmp);
                ColorMatrix colormatrix = new ColorMatrix();
                colormatrix.Matrix33 = 100 / 100;
                ImageAttributes imgAttribute = new ImageAttributes();
                imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                graphics.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttribute);
                graphics.Dispose();   // Releasing all resource used by graphics 
                return bmp;
            }
            catch (Exception ex)
            {
                _log.Fatal("ERREUR : " + ex);
                return null;
            }
        }

        public string GetResourceTextFile(string filename)
        {
            string result = string.Empty;
            using (Stream stream = this.GetType().Assembly.GetManifestResourceStream("assembly.folder." + filename))
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    result = sr.ReadToEnd();
                }
            }
            return result;
        }

        private void Translator(string lang)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(Application.StartupPath + "\\XML\\lang\\" + lang + ".xml");
            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/catalog/translation");

            List<Color> colors = new List<Color>();


            foreach (XmlNode node in nodes)
            {
                if (int.Parse(node.Attributes["id"].Value) == 0)
                {
                    label1.Text = node.SelectSingleNode("txt").InnerText;
                }
                else if (int.Parse(node.Attributes["id"].Value) == 1)
                {
                    label2.Text = node.SelectSingleNode("txt").InnerText;
                }
                else if (int.Parse(node.Attributes["id"].Value) == 2)
                {
                    label4.Text = node.SelectSingleNode("txt").InnerText;
                }
                else if (int.Parse(node.Attributes["id"].Value) == 3)
                {
                    label3.Text = node.SelectSingleNode("txt").InnerText;
                }
                else if (int.Parse(node.Attributes["id"].Value) == 4)
                {
                    label5.Text = node.SelectSingleNode("txt").InnerText;
                }
                else if (int.Parse(node.Attributes["id"].Value) == 5)
                {
                    button3.Text = node.SelectSingleNode("txt").InnerText;
                }
                else if (int.Parse(node.Attributes["id"].Value) == 6)
                {
                    Exit_btn.Text = node.SelectSingleNode("txt").InnerText;
                }
                else if (int.Parse(node.Attributes["id"].Value) == 7)
                {
                    tabPage1.Text = node.SelectSingleNode("txt").InnerText;
                }
                else if (int.Parse(node.Attributes["id"].Value) == 8)
                {
                    tabPage2.Text = node.SelectSingleNode("txt").InnerText;
                }
                else if (int.Parse(node.Attributes["id"].Value) == 9)
                {
                    label6.Text = node.SelectSingleNode("txt").InnerText;
                }
                else if (int.Parse(node.Attributes["id"].Value) == 1000)
                {
                    this.Text = node.SelectSingleNode("txt").InnerText;
                }
                else
                {

                }
            }
        }

        private void comboBoxLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Application.StartupPath + "\\XML\\lang\\" + Properties.Settings.Default.lang + ".xml");
            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/catalog/translation");

            List<Color> colors = new List<Color>();

            foreach (XmlNode node in nodes)
            {
                if (int.Parse(node.Attributes["id"].Value) == 999)
                {
                    DataRow selectedDataRow = ((DataRowView)comboBoxLang.SelectedItem).Row;
                    Properties.Settings.Default.lang = selectedDataRow["Name"].ToString();
                    pictureBox1.BackgroundImage = Image.FromFile(Application.StartupPath + "\\lang\\" + selectedDataRow["Name"].ToString() + ".png");
                    Translator(Properties.Settings.Default.lang);
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.FormSize = this.Size;
            Properties.Settings.Default.Location = this.Location;
            Properties.Settings.Default.Save();
        }
    }

}
