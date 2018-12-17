using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

namespace KingdomTwoCrowns_Blazon_Generator
{
    public partial class Form1 : Form
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static string GetColor(int couleur)
        {
            try
            {
                string path = Directory.GetCurrentDirectory();
                XmlDocument doc = new XmlDocument();
                doc.Load(path + @"\Colors\colors.xml");
                XmlNodeList nodes = doc.DocumentElement.SelectNodes("/catalog/color");

                List<Color> colors = new List<Color>();

                if (couleur == -1)
                {
                    foreach (XmlNode node in nodes)
                    {
                        int retour = 48;
                        string nom = node.SelectSingleNode("nom").InnerText;
                        string images = node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == retour)
                        {
                            ConfigurationManager.AppSettings["couleur"] = retour.ToString();
                            return path + "\\" + images;
                        }
                    }

                }
                else if (couleur == 49)
                {
                    foreach (XmlNode node in nodes)
                    {
                        int retour = 0;
                        string nom = node.SelectSingleNode("nom").InnerText;
                        string images = node.SelectSingleNode("image").InnerText;
                        int id = int.Parse(node.Attributes["id"].Value);
                        if (id == retour)
                        {
                            ConfigurationManager.AppSettings["couleur"] = retour.ToString();
                            return path + "\\" + images;
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
                        if (id == couleur)
                        {
                            return path + "\\" + images;
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
                string path = Directory.GetCurrentDirectory();
                XmlDocument doc = new XmlDocument();
                doc.Load(path + @"\Colors\colors.xml");
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
                            ConfigurationManager.AppSettings["couleur"] = retour.ToString();
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
                            ConfigurationManager.AppSettings["couleur"] = retour.ToString();
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

        public Form1()
        {
            XmlConfigurator.Configure();
            InitializeComponent();
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                _log.Fatal("ERREUR : " + ex);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
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

                LetsRandom();

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

        public Image SetImageOpacity(Image image, float opacity)
        {
            try
            {
                //create a Bitmap the size of the image provided  
                Bitmap bmp = new Bitmap(image.Width, image.Height);
                //create a graphics object from the image  
                using (Graphics gfx = Graphics.FromImage(bmp))
                {
                    //create a color matrix object  
                    ColorMatrix matrix = new ColorMatrix();
                    //set the opacity  
                    matrix.Matrix33 = opacity;
                    //create image attributes  
                    ImageAttributes attributes = new ImageAttributes();
                    //set the color(opacity) of the image  
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                    //now draw the image  
                    gfx.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                }
                return bmp;
            }
            catch (Exception ex)
            {
                _log.Fatal(ex.Message);
                return null;
            }
        }

        private void Exit_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.MessageLoop)
                {
                    // WinForms app
                    System.Windows.Forms.Application.Exit();
                }
                else
                {
                    // Console app
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
                int color = int.Parse(ConfigurationManager.AppSettings["couleur"]);
                int charge = int.Parse(ConfigurationManager.AppSettings["charge"]);
                int chargeclrs = int.Parse(ConfigurationManager.AppSettings["chargeclrs"]);
                int ordinarie = int.Parse(ConfigurationManager.AppSettings["ordinarie"]);
                int ordinarieclrs = int.Parse(ConfigurationManager.AppSettings["ordinarieclrs"]);
                color--;
                ConfigurationManager.AppSettings["couleur"] = color.ToString();
                panel7.BackgroundImage = SetImageOpacity(Image.FromFile(GetColor(color)), 1);
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
                int color = int.Parse(ConfigurationManager.AppSettings["couleur"]);
                int charge = int.Parse(ConfigurationManager.AppSettings["charge"]);
                int chargeclrs = int.Parse(ConfigurationManager.AppSettings["chargeclrs"]);
                int ordinarie = int.Parse(ConfigurationManager.AppSettings["ordinarie"]);
                int ordinarieclrs = int.Parse(ConfigurationManager.AppSettings["ordinarieclrs"]);
                color++;
                ConfigurationManager.AppSettings["couleur"] = color.ToString();
                panel7.BackgroundImage = SetImageOpacity(Image.FromFile(GetColor(color)), 1);
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
                int charge = int.Parse(ConfigurationManager.AppSettings["charge"]);
                int chargeclrs = int.Parse(ConfigurationManager.AppSettings["chargeclrs"]);
                int ordinarie = int.Parse(ConfigurationManager.AppSettings["ordinarie"]);
                int ordinarieclrs = int.Parse(ConfigurationManager.AppSettings["ordinarieclrs"]);
                int color = int.Parse(ConfigurationManager.AppSettings["couleur"]);
                ordinarie--;
                ConfigurationManager.AppSettings["ordinarie"] = ordinarie.ToString();
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
                int charge = int.Parse(ConfigurationManager.AppSettings["charge"]);
                int chargeclrs = int.Parse(ConfigurationManager.AppSettings["chargeclrs"]);
                int ordinarie = int.Parse(ConfigurationManager.AppSettings["ordinarie"]);
                int ordinarieclrs = int.Parse(ConfigurationManager.AppSettings["ordinarieclrs"]);
                int color = int.Parse(ConfigurationManager.AppSettings["couleur"]);
                ordinarie++;
                ConfigurationManager.AppSettings["ordinarie"] = ordinarie.ToString();
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
                int charge = int.Parse(ConfigurationManager.AppSettings["charge"]);
                int chargeclrs = int.Parse(ConfigurationManager.AppSettings["chargeclrs"]);
                int ordinarie = int.Parse(ConfigurationManager.AppSettings["ordinarie"]);
                int ordinarieclrs = int.Parse(ConfigurationManager.AppSettings["ordinarieclrs"]);
                int color = int.Parse(ConfigurationManager.AppSettings["couleur"]);
                ordinarieclrs--;
                ConfigurationManager.AppSettings["ordinarieclrs"] = ordinarieclrs.ToString();
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
                int charge = int.Parse(ConfigurationManager.AppSettings["charge"]);
                int chargeclrs = int.Parse(ConfigurationManager.AppSettings["chargeclrs"]);
                int ordinarie = int.Parse(ConfigurationManager.AppSettings["ordinarie"]);
                int ordinarieclrs = int.Parse(ConfigurationManager.AppSettings["ordinarieclrs"]);
                int color = int.Parse(ConfigurationManager.AppSettings["couleur"]);
                ordinarieclrs++;
                ConfigurationManager.AppSettings["ordinarieclrs"] = ordinarieclrs.ToString();
                panel8.BackgroundImage = new Bitmap(ChangeColor(GetOrdinarie(ordinarie), GetOrdinarieColor(ordinarieclrs)));
                textBox1.Text = (GetColorText(color) + " " + GetOrdinarieText(ordinarie) + " " + GetOrdinarieColorText(ordinarieclrs) + " " + GetChargeText(charge) + " " + GetChargeColorText(chargeclrs));
            }
            catch (Exception ex)
            {
                _log.Fatal("ERREUR : " + ex);
            }
        }

        public static string GetCharge(int charge)
        {
            try
            {
                string game = ConfigurationManager.AppSettings["jeu"];
                string path = Directory.GetCurrentDirectory();
                XmlDocument doc = new XmlDocument();
                doc.Load(path + "\\" + game + @"\Charges\charges.xml");
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
                            ConfigurationManager.AppSettings["charge"] = retour.ToString();
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
                            ConfigurationManager.AppSettings["charge"] = retour.ToString();
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
                string game = ConfigurationManager.AppSettings["jeu"];
                string path = Directory.GetCurrentDirectory();
                XmlDocument doc = new XmlDocument();
                doc.Load(path + "\\" + game + @"\Charges\charges.xml");
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
                            ConfigurationManager.AppSettings["charge"] = retour.ToString();
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
                            ConfigurationManager.AppSettings["charge"] = retour.ToString();
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
                string game = ConfigurationManager.AppSettings["jeu"];
                string path = Directory.GetCurrentDirectory();
                XmlDocument doc = new XmlDocument();
                doc.Load(path + "\\" + game + @"\Charges\charges.xml");
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
                            ConfigurationManager.AppSettings["chargeclrs"] = retour.ToString();
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
                            ConfigurationManager.AppSettings["chargeclrs"] = retour.ToString();
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
                string game = ConfigurationManager.AppSettings["jeu"];
                string path = Directory.GetCurrentDirectory();
                XmlDocument doc = new XmlDocument();
                doc.Load(path + "\\" + game + @"\Charges\charges.xml");
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
                            ConfigurationManager.AppSettings["chargeclrs"] = retour.ToString();
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
                            ConfigurationManager.AppSettings["chargeclrs"] = retour.ToString();
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

        private void Left_Charg_btn_Click(object sender, EventArgs e)
        {
            try
            {
                int charge = int.Parse(ConfigurationManager.AppSettings["charge"]);
                int chargeclrs = int.Parse(ConfigurationManager.AppSettings["chargeclrs"]);
                int ordinarie = int.Parse(ConfigurationManager.AppSettings["ordinarie"]);
                int ordinarieclrs = int.Parse(ConfigurationManager.AppSettings["ordinarieclrs"]);
                int color = int.Parse(ConfigurationManager.AppSettings["couleur"]);
                charge--;
                ConfigurationManager.AppSettings["charge"] = charge.ToString();
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
                int charge = int.Parse(ConfigurationManager.AppSettings["charge"]);
                int chargeclrs = int.Parse(ConfigurationManager.AppSettings["chargeclrs"]);
                int ordinarie = int.Parse(ConfigurationManager.AppSettings["ordinarie"]);
                int ordinarieclrs = int.Parse(ConfigurationManager.AppSettings["ordinarieclrs"]);
                int color = int.Parse(ConfigurationManager.AppSettings["couleur"]);
                charge++;
                ConfigurationManager.AppSettings["charge"] = charge.ToString();
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
                int charge = int.Parse(ConfigurationManager.AppSettings["charge"]);
                int chargeclrs = int.Parse(ConfigurationManager.AppSettings["chargeclrs"]);
                int ordinarie = int.Parse(ConfigurationManager.AppSettings["ordinarie"]);
                int ordinarieclrs = int.Parse(ConfigurationManager.AppSettings["ordinarieclrs"]);
                int color = int.Parse(ConfigurationManager.AppSettings["couleur"]);
                chargeclrs--;
                ConfigurationManager.AppSettings["chargeclrs"] = chargeclrs.ToString();
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
                int charge = int.Parse(ConfigurationManager.AppSettings["charge"]);
                int chargeclrs = int.Parse(ConfigurationManager.AppSettings["chargeclrs"]);
                int ordinarie = int.Parse(ConfigurationManager.AppSettings["ordinarie"]);
                int ordinarieclrs = int.Parse(ConfigurationManager.AppSettings["ordinarieclrs"]);
                int color = int.Parse(ConfigurationManager.AppSettings["couleur"]);
                chargeclrs++;
                ConfigurationManager.AppSettings["chargeclrs"] = chargeclrs.ToString();
                panel5.BackgroundImage = new Bitmap(ChangeColor(GetCharge(charge), GetChargeColor(chargeclrs)));
                textBox1.Text = (GetColorText(color) + " " + GetOrdinarieText(ordinarie) + " " + GetOrdinarieColorText(ordinarieclrs) + " " + GetChargeText(charge) + " " + GetChargeColorText(chargeclrs));
            }
            catch (Exception ex)
            {
                _log.Fatal("ERREUR : " + ex);
            }
        }

        public static Bitmap ChangeColor(string file, string colorcode)
        {
            try
            {
                string game = ConfigurationManager.AppSettings["jeu"];
                string path = Directory.GetCurrentDirectory();

                // Load the images.
                Bitmap mask = (Bitmap)Image.FromFile(path + "\\" + game + file);
                Bitmap input = (Bitmap)Image.FromFile(path + colorcode);


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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string game = ConfigurationManager.AppSettings["jeu"];
                if (game == "Kingdom1")
                {
                    ConfigurationManager.AppSettings["jeu"] = "Kingdom2";
                    panel3.BackgroundImage = Properties.Resources.Logo1;
                    LetsRandom();
                }
                else if (game == "Kingdom2")
                {
                    ConfigurationManager.AppSettings["jeu"] = "Kingdom1";
                    panel3.BackgroundImage = Properties.Resources.Logo2;
                    LetsRandom();
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
                string game = ConfigurationManager.AppSettings["jeu"];
                if (game == "Kingdom1")
                {
                    ConfigurationManager.AppSettings["jeu"] = "Kingdom2";
                    panel3.BackgroundImage = Properties.Resources.Logo1;
                    LetsRandom();
                }
                else if (game == "Kingdom2")
                {
                    ConfigurationManager.AppSettings["jeu"] = "Kingdom1";
                    panel3.BackgroundImage = Properties.Resources.Logo2;
                    LetsRandom();
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
        
        public static string GetOrdinarie(int ordinarie)
        {
            try
            {
                string game = ConfigurationManager.AppSettings["jeu"];
                string path = Directory.GetCurrentDirectory();
                XmlDocument doc = new XmlDocument();
                doc.Load(path + "\\" + game + @"\Ordinaries\ordinaries.xml");
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
                            ConfigurationManager.AppSettings["ordinarie"] = retour.ToString();
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
                            ConfigurationManager.AppSettings["ordinarie"] = retour.ToString();
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
                string game = ConfigurationManager.AppSettings["jeu"];
                string path = Directory.GetCurrentDirectory();
                XmlDocument doc = new XmlDocument();
                doc.Load(path + "\\" + game + @"\Ordinaries\ordinaries.xml");
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
                            ConfigurationManager.AppSettings["ordinarie"] = retour.ToString();
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
                            ConfigurationManager.AppSettings["ordinarie"] = retour.ToString();
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
                string game = ConfigurationManager.AppSettings["jeu"];
                string path = Directory.GetCurrentDirectory();
                XmlDocument doc = new XmlDocument();
                doc.Load(path + "\\" + game + @"\Ordinaries\ordinaries.xml");
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
                            ConfigurationManager.AppSettings["cordinarieclrs"] = retour.ToString();
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
                            ConfigurationManager.AppSettings["ordinarieclrs"] = retour.ToString();
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
                string game = ConfigurationManager.AppSettings["jeu"];
                string path = Directory.GetCurrentDirectory();
                XmlDocument doc = new XmlDocument();
                doc.Load(path + "\\" + game + @"\Ordinaries\ordinaries.xml");
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
                            ConfigurationManager.AppSettings["ordinarieclrs"] = retour.ToString();
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
                            ConfigurationManager.AppSettings["ordinarieclrs"] = retour.ToString();
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

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
                ConfigurationManager.AppSettings["couleur"] = GetRandomNumber(0, 48).ToString();
                ConfigurationManager.AppSettings["charge"] = GetRandomNumber(0, 17).ToString();
                ConfigurationManager.AppSettings["chargeclrs"] = GetRandomNumber(0, 48).ToString();
                ConfigurationManager.AppSettings["ordinarie"] = GetRandomNumber(0, 13).ToString();
                ConfigurationManager.AppSettings["ordinarieclrs"] = GetRandomNumber(0, 48).ToString();
                int charge = int.Parse(ConfigurationManager.AppSettings["charge"]);
                int chargeclrs = int.Parse(ConfigurationManager.AppSettings["chargeclrs"]);
                int ordinarie = int.Parse(ConfigurationManager.AppSettings["ordinarie"]);
                int ordinarieclrs = int.Parse(ConfigurationManager.AppSettings["ordinarieclrs"]);
                int color = int.Parse(ConfigurationManager.AppSettings["couleur"]);
                panel5.BackgroundImage = ChangeColor(GetCharge(charge), GetChargeColor(chargeclrs));
                panel7.BackgroundImage = SetImageOpacity(Image.FromFile(GetColor(color)), 1);
                panel8.BackgroundImage = ChangeColor(GetOrdinarie(ordinarie), GetOrdinarieColor(ordinarieclrs));
                textBox1.Text = (GetColorText(color) + " " + GetOrdinarieText(ordinarie) + " " + GetOrdinarieColorText(ordinarieclrs) + " " + GetChargeText(charge) + " " + GetChargeColorText(chargeclrs));
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
    }

}
