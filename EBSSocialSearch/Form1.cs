using BastamaTextSave;
using EbubekirBastamatxtokuma;
using OpenQA.Selenium.Chrome;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Windows.Forms;


namespace EBSSocialSearch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }
        Thread th;OpenFileDialog op; ChromeDriver drv;
        string[] urlall = { "https://www.facebook.com/login/identify/", "https://twitter.com/account/begin_password_reset", "https://www.instagram.com/" };
        private void button1_Click(object sender, EventArgs e)
        {
            th = new Thread(analizet);th.Start();
        }

        private void analizet()
        {
            drv = new ChromeDriver();
            for (int j = 0; j < listBox1.Items.Count; j++)
            {
                for (int i = 0; i < urlall.Length; i++)
                {
                    drv.Navigate().GoToUrl(urlall[i].ToString());
                    Thread.Sleep(5000);

                    if (urlall[i] == "https://www.facebook.com/login/identify/")  //EBS Coding...
                    {
                        drv.FindElementsByXPath("//input[@name='email']")[1].SendKeys(listBox1.Items[j].ToString());
                        drv.FindElementByXPath("//*[@name='did_submit']").Click();
                        Thread.Sleep(5000);
                        if (drv.PageSource.IndexOf("Şifreni yenilemek için kodu nasıl almak istiyorsun?") != -1 )
                        {
                            listBox2.Items.Add("Bu numara :" + listBox1.Items[j].ToString() + " Facebook'da Kayıtlı.");
                        }
                        else
                        {
                            listBox2.Items.Add("Bu numara :" + listBox1.Items[j].ToString() + " Facebook'da Kayıtlı değil.");
                        }
                    }
                    else if (urlall[i] == "https://twitter.com/account/begin_password_reset")
                    {
                        drv.FindElementByXPath("//input[@name='account_identifier']").SendKeys(listBox1.Items[j].ToString());
                        drv.FindElementByXPath("//*[@value='Arama']").Click();
                        Thread.Sleep(5000);
                        if (drv.PageSource.IndexOf("Bu bilgi ile hesabını bulamadık") == -1)
                        {
                            listBox2.Items.Add("Bu numara :" + listBox1.Items[j].ToString() + " twitter'da Kayıtlı.");
                        }
                        else
                        {
                            listBox2.Items.Add("Bu numara :" + listBox1.Items[j].ToString() + " twitter'da Kayıtlı değil.");
                        }

                    }
                    else if (urlall[i] == "https://www.instagram.com/")
                    {
                        drv.FindElementByXPath("//input[@name='username']").SendKeys(listBox1.Items[j].ToString());
                        drv.FindElementByXPath("//input[@aria-label='Şifre']").SendKeys("ha ha haa :)");
                        drv.FindElementByXPath("//*[@class='sqdOP  L3NKy   y3zKF     ']").Click();
                        Thread.Sleep(5000);
                        if (drv.PageSource.IndexOf("Girdiğin kullanıcı adı bir hesaba ait değil.") == -1)
                        {
                            listBox2.Items.Add("Bu numara :" + listBox1.Items[j].ToString() + " instagram'da Kayıtlı.");
                        }
                        else
                        {
                            listBox2.Items.Add("Bu numara :" + listBox1.Items[j].ToString() + " instagram'da Kayıtlı değil.");
                        }
                    }
                }
            }
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                th = new Thread(basla); th.Start();
            }
        }

        private void basla()
        {
            BekraTxtOkuma.Txtİmport(op.FileName,listBox1,false);  
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txt_save.txtlistsave(listBox2,"Bütün veriler Aktarıldı.",Application.StartupPath+@"/","socialhacker.txt");
        }
    }
}
