using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF1016
{
    class Client
    {

        private Form1 form;
        Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        
        public int Port { get; private set; }
        public string Ip { get; private set; }

        public void ParentInfo(Form1 f)
        {
            form = f;
        }

        public bool Connect(string ip, int port)
        {
            Ip = ip;
            Port = port;

            try
            {
                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(Ip), Port);
                client.Connect(ipep);

                Thread th = new Thread(new ParameterizedThreadStart(ComThread));
                th.Start(this);
                th.IsBackground = true;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                return false;
            }
        }

        public void DisConnect()
        {
            try
            {
                client.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SendMessage(string name, string msg)
        {
            byte[] bytes = (Encoding.Default.GetBytes("[" + name + "]" + ":" + msg));

            SendData(client, bytes);
        }

        public void ComThread(object Odata)
        {
            while(true)
            {
                byte[] data = new byte[1024];

                //수신
                ReceiveData(client, ref data);
                form.ShortMessage(Encoding.Default.GetString(data));
            }
        }

        //실제 네트워크를 통해 전송/수신되는함수
        private void SendData(Socket sock, byte[] data)
        {
            try
            {
                int total = 0;
                int size = data.Length;
                int left_data = size;
                int send_data = 0;

                // 전송할 데이터의 크기 전달
                byte[] data_size = new byte[4];
                data_size = BitConverter.GetBytes(size);
                send_data = sock.Send(data_size);

                // 실제 데이터 전송
                while (total < size)
                {
                    send_data = sock.Send(data, total, left_data, SocketFlags.None);
                    total += send_data;
                    left_data -= send_data;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ReceiveData(Socket sock, ref byte[] data)
        {
            try
            {
                int total = 0;
                int size = 0;
                int left_data = 0;
                int recv_data = 0;

                // 수신할 데이터 크기 알아내기 
                byte[] data_size = new byte[4];
                recv_data = sock.Receive(data_size, 0, 4, SocketFlags.None);
                size = BitConverter.ToInt32(data_size, 0);
                left_data = size;

                data = new byte[size];

                // 실제 데이터 수신
                while (total < size)
                {
                    recv_data = sock.Receive(data, total, left_data, 0);
                    if (recv_data == 0) break;
                    total += recv_data;
                    left_data -= recv_data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
