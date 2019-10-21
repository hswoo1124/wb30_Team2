using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OA_Server_Consol
{ 
    class GpsSearcher
    {
        #region singleton pattern
        //프로퍼티 만들고
        public static GpsSearcher Instance { get; private set; }
        //개체생성하고
        static GpsSearcher()
        {
            Instance = new GpsSearcher();
        }
        //default 생성자는 은닉
        private GpsSearcher()
        {

        }
        #endregion
        public List<Gps> gpslist = new List<Gps>();

        public string XmlString { get; private set; }
        XmlDocument doc;

        public void SearchGps(string find)
        {
            gpslist.Clear();
            XmlString = Find(find);
            doc = new XmlDocument();
            doc.LoadXml(XmlString);
            doc.Save("Search.xml");

            //==============================================

            Gps gps = null;
            foreach (XmlNode el in doc.SelectSingleNode("result"))
            {
                gps = Gps.MakeGps(el);
                gpslist.Add(gps);
            }
        }
        public string Find(string juso)
        {
            string msg = "http://apis.vworld.kr/new2coord.do?q=" + juso + "&apiKey=C2972184-5B16-34AB-9718-AC02390CCD22&domain=http://www.naver.com";

            var request = (HttpWebRequest)WebRequest.Create(msg);
            request.Method = "GET";

            string results = string.Empty;
            HttpWebResponse response;
            using (response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                results = reader.ReadToEnd();
            }
            Console.WriteLine(results);
            return results;
        }
    }
}
