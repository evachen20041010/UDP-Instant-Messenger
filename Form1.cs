using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace _20230424
{
    public partial class Form1 : Form
    {
        UdpClient U;    //UDP通訊物件
        Thread Th;  //監聽用執行緒
        public Form1()
        {
            InitializeComponent();
        }

        private void Listen()
        {
            int Port = int.Parse(textBox1.Text);    //設定監聽用的通訊埠
            U = new UdpClient(Port);    //監聽UDP監聽器實體

            //建立本機端點資訊
            IPEndPoint EP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), Port);
            
            //持續監聽的無限迴圈：有訊息(True)就處理，無訊息就等待
            while (true)
            {
                byte[] B = U.Receive(ref EP);   //訊息到達時讀取資訊到B[]
                textBox2.Text = Encoding.Default.GetString(B);  //翻譯B[]為字串
            }
        }

        //啟動監聽按鈕程序
        private void button1_Click(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;    //忽略跨執行緒錯誤
            Th = new Thread(Listen);    // 建立監聽執行緒，目標副執行緒(Listen)
            Th.Start(); //啟動監聽執行緒
            button1.Enabled = false;    //使按鍵失效，不能(也不需要)重複開啟監聽
        }

        //關閉監聽執行緒
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Th.Abort(); //關閉監聽執行緒
                U.Close();  //關閉監聽器
            }
            catch
            {
                //忽略錯誤繼續執行
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string IP = textBox3.Text;   //發送目標IP
            int Port = int.Parse(textBox4.Text);    //發送目標Port
            byte[]B = Encoding.Default.GetBytes(textBox5.Text); //字串翻譯成位元組陣列
            UdpClient S = new UdpClient();  //UDP通訊器
            S.Send(B, B.Length, IP, Port);  //發送資料到指定位置
            S.Close();  //關閉通訊器
        }
    }
}