using _11_12_WCFChatClient.ServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;

namespace _11_12_WCFChatClient
{
    public partial class Form1 : Form, IChatCallback
    {
        private ChatClient _proxy = null;
        public Form1()
        {
            InitializeComponent();
            textBox3.Text = "윤준서";
            _proxy = new ChatClient(new InstanceContext(this));
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (_proxy.Join(textBox3.Text, DateTime.Now) == true) 
                textBox1.AppendText("연결 성공\r\n");
            
            else 
                textBox1.AppendText("연결 실패\r\n");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                _proxy.Leave(textBox3.Text, DateTime.Now);
            }
            catch (Exception ex)
            {
                textBox1.AppendText(ex.Message + "\r\n");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string msg = textBox2.Text;
            try
            {
                _proxy.Say(textBox3.Text, msg, DateTime.Now);
                textBox2.Clear();
            }
            catch (Exception ex)
            {
                textBox1.AppendText(ex.Message + "\r\n");
            }

        }
        public void Join_Ack(string nickname, DateTime time)
        {
            textBox1.AppendText(nickname + "님이 " + time + "에 입장하였습니다.\r\n");
        }

        public void Leave_Ack(string nickname, DateTime time)
        {
            textBox1.AppendText(nickname + "님이 " + time + "에 퇴장하였습니다.\r\n");
        }

        public void Say_Ack(string nickname, string msg, DateTime time)
        {
            textBox1.AppendText(nickname + " : " + msg + "\r\n");
        }
    }
}
