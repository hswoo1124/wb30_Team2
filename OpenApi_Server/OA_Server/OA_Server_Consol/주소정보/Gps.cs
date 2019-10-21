using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OA_Server_Consol
{
    class Gps
    {
        public string Position_X { get; private set; }
        public string Position_Y { get; private set; }
        public string Juso { get; private set; }

        public Gps(string x, string y, string juso)
        {
            Position_X = x;
            Position_Y = y;
            Juso = juso;
        }
        static public Gps MakeGps(XmlNode xn)
        {
            //초기화 (값을 잘 넣기위함)
            //초기화를 안해주면 안에 가비지값이 있기때문에 값을 넣는 과정이 힘듬
            string position_x = string.Empty;
            string position_y = string.Empty;
            string juso = string.Empty;
            

            XmlNode position_x_node = xn.SelectSingleNode("EPSG_4326_X");
            position_x = ConvertString(position_x_node.InnerText);

            XmlNode position_y_node = xn.SelectSingleNode("EPSG_4326_Y");
            position_y = ConvertString(position_y_node.InnerText);

            XmlNode juso_node = xn.SelectSingleNode("JUSO");
            juso = ConvertString(juso_node.InnerText);

            return new Gps(position_x, position_y, juso);
        }

        static private string ConvertString(string str)
        {
            int sindex = 0;
            int eindex = 0;
            while (true)
            {
                sindex = str.IndexOf("<![CDATA[");
                if (sindex == -1)
                {
                    break;
                }
                eindex = str.IndexOf("]]");
                str = str.Remove(sindex, eindex - sindex + 1);
            }
            return str;
        }
    }
}
