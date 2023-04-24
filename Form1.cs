using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace _20230424
{
    public partial class Form1 : Form
    {
        UdpClient U;    //UDP�q�T����
        Thread Th;  //��ť�ΰ����
        public Form1()
        {
            InitializeComponent();
        }

        private void Listen()
        {
            int Port = int.Parse(textBox1.Text);    //�]�w��ť�Ϊ��q�T��
            U = new UdpClient(Port);    //��ťUDP��ť������

            //�إߥ������I��T
            IPEndPoint EP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), Port);
            
            //�����ť���L���j��G���T��(True)�N�B�z�A�L�T���N����
            while (true)
            {
                byte[] B = U.Receive(ref EP);   //�T����F��Ū����T��B[]
                textBox2.Text = Encoding.Default.GetString(B);  //½ĶB[]���r��
            }
        }

        //�Ұʺ�ť���s�{��
        private void button1_Click(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;    //�������������~
            Th = new Thread(Listen);    // �إߺ�ť������A�ؼаư����(Listen)
            Th.Start(); //�Ұʺ�ť�����
            button1.Enabled = false;    //�ϫ��䥢�ġA����(�]���ݭn)���ƶ}�Һ�ť
        }

        //������ť�����
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Th.Abort(); //������ť�����
                U.Close();  //������ť��
            }
            catch
            {
                //�������~�~�����
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string IP = textBox3.Text;   //�o�e�ؼ�IP
            int Port = int.Parse(textBox4.Text);    //�o�e�ؼ�Port
            byte[]B = Encoding.Default.GetBytes(textBox5.Text); //�r��½Ķ���줸�հ}�C
            UdpClient S = new UdpClient();  //UDP�q�T��
            S.Send(B, B.Length, IP, Port);  //�o�e��ƨ���w��m
            S.Close();  //�����q�T��
        }
    }
}