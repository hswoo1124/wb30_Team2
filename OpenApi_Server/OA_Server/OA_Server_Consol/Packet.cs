using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OA_Server_Consol
{
    class Packet
    {
        #region singleton pattern
        //프로퍼티 만들고
        public static Packet Instance { get; private set; }
        //개체생성하고
        static Packet()
        {
            Instance = new Packet();
        }
        //default 생성자는 은닉
        private Packet()
        {

        }
        #endregion

        wbServer server = new wbServer();
        public string name;

        #region 수신패킷 분석 및 처리
        public void PaserByteData(Socket sock, byte[] data)
        {
            string msg = Encoding.Default.GetString(data);
            string[] token = msg.Split('@');
            name = token[1];
            switch (token[0].Trim())
            {
                case "NEEDNAME": NeedName(sock, name); break;
                case "NEEDMENU": NeedMenu(sock, token[1]); break;
            }
        }

        void NeedName(Socket sock, string msg)
        {
            Console.WriteLine("[수신]" + msg);

            string ackmessage = NeedNameAck(msg, ResturantSearcher.Instance.GetTitle_Name(msg), ResturantSearcher.Instance.GetPrice_Name(msg)); 

            Console.WriteLine("[송신]" + ackmessage);

            server.Send(sock, ackmessage);
        }
        string NeedNameAck(string name, string[] title, string[] price)
        {
            string msg = null;

            msg += "NEEDNAMEACK@";
            msg += name + "#";
            for (int i = 0; i < title.Length; i++)
            {
                msg += title[i] + "#";
                msg += price[i] + "#";
            }
            msg = msg.Substring(0, msg.Length - 1);
            return msg;
        }


        void NeedMenu(Socket sock, string msg)
        {
            Console.WriteLine("[수신]" + msg);

            string ackmessage = NeedMenuAck(ResturantSearcher.Instance.GetName_Menu(msg));

            Console.WriteLine("[송신]" + ackmessage);

            server.Send(sock, ackmessage);
        }

        string NeedMenuAck(string name)
        {
            string msg = null;

            msg += "NEEDMENUACK@";
            msg += name + "#";

            return msg;
        }
        #endregion
    }
}

