﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OA_Server_Consol
{
    enum FHDATA { FH0000918 = 0, FH0000917,FH0000914, FH0000913, FH0000912 , FH0000911 , FH0000910 , FH0000909, FH0000908, FH0000907, FH0000906, FH0000905, FH0000904, FH0000903,
        FH0000902, FH0000901, FH0000900, FH0000183, FH0000180, FH0000179, FH0000178, FH0000177, FH0000175, FH0000174, FH0000129, FH0000096, FH0000095, FH0000094, FH0000093,
        FH0000092, FH0000091, FH0000090, FH0000089, FH0000088, FH0000087, FH0000086, FH0000084, FH0000083  }
    class ResturantSearcher
    {
        #region singleton pattern
        //프로퍼티 만들고
        public static ResturantSearcher Instance { get; private set; }
        //개체생성하고
        static ResturantSearcher()
        {
            Instance = new ResturantSearcher();
        }
        //default 생성자는 은닉
        private ResturantSearcher()
        {

        }
        #endregion
        public List<Resturant> reslist = new List<Resturant>();
        public List<Resturant> reslist3 = new List<Resturant>();
        public List<Etc> reslist2 = new List<Etc>();
        public List<Etc> reslist4 = new List<Etc>();

        public string XmlString { get; private set; }
        public string XmlString2 { get; private set; }
        XmlDocument doc;
        #region 시작할때 데이터 받아옴
        public void SearchResturant2()
        {
            for (FHDATA seq = FHDATA.FH0000918; seq <= FHDATA.FH0000083; seq++)
            {
                reslist4.Clear();
                XmlString2 = Data(seq.ToString());
                doc = new XmlDocument();
                doc.LoadXml(XmlString2);
                doc.Save("Search.xml");

                //==============================================

                XmlNode node = doc.SelectSingleNode("ServiceResult");
                XmlNode n = node.SelectSingleNode("msgBody");
                XmlNode d = n.SelectSingleNode("foodMenuEtcListAll");
                Resturant resturant = null;
                Etc etc = null;
                
                foreach (XmlNode el in node.SelectNodes("msgBody"))
                {
                    foreach (XmlNode el2 in d.SelectNodes("FoodMenuEtcList"))
                    {
                        etc = Resturant.MakeResturant_etc(el2);
                        reslist4.Add(etc);
                    }
                    resturant = Resturant.MakeResturant(el);
                    reslist3.Add(resturant); 
                }  
            }
        }
        public string Data(string seq)
        {
            string results = string.Empty;

            string url = "http://apis.data.go.kr/6300000/tourFoodDataService/tourFoodDataItem"; // URL
            url += "?ServiceKey=" + "jywHyRc4HyGPMS4YSqc%2BxCR6Md%2BdWGAL%2Fc3LbkX5%2FqyYLzacjZGzxccCY0YPJE%2Br5Imx%2FT9tKDgFDtur0k%2Bntw%3D%3D"; // Service Key
            url += "&foodSeq=" + seq;

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            HttpWebResponse response;
            using (response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                results = reader.ReadToEnd();
            }
            return results;
        }
        #endregion

        #region 데이터를 받아옴
        public void SearchResturant(string name)
        {
            //string name = Packet.Instance.name;
            reslist.Clear();
            XmlString = Find(name);
            doc = new XmlDocument();
            doc.LoadXml(XmlString);
            doc.Save("Search.xml");

            //==============================================

            XmlNode node = doc.SelectSingleNode("ServiceResult");
            XmlNode n = node.SelectSingleNode("msgBody");
            Resturant resturant = null;
            Etc etc = null;
            foreach (XmlNode el in node.SelectNodes("msgBody"))
            {
                resturant = Resturant.MakeResturant(el);
                reslist.Add(resturant);

                foreach (XmlNode el2 in n.SelectNodes("FoodMenuEtcList"))
                {
                    etc = Resturant.MakeResturant_etc(el2);
                    reslist2.Add(etc);
                }
            }
        }
        #endregion
        public string Find(string name)
        {
            string seq = GetSeq_Name(name);
            string url = "http://apis.data.go.kr/6300000/tourFoodDataService/tourFoodDataItem"; // URL
            url += "?ServiceKey=" + "jywHyRc4HyGPMS4YSqc%2BxCR6Md%2BdWGAL%2Fc3LbkX5%2FqyYLzacjZGzxccCY0YPJE%2Br5Imx%2FT9tKDgFDtur0k%2Bntw%3D%3D"; // Service Key
            url += "&foodSeq=" + seq;

            var request = (HttpWebRequest)WebRequest.Create(url);
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

        #region 데이터 찾는 함수

        public string GetSeq_Name(string name)
        {
            foreach (Resturant res in reslist3)
            {
                if (res.Name == name)
                {
                    return res.Seq;
                }
            }
            return null;
        }
        public string[] GetTitle_Name(string name)
        {
            foreach (Resturant res in reslist3)
            {
                if (res.Name == name)
                {
                    return res.Title;
                }
            }
            return null;
        }
        public string[] GetPrice_Name(string name)
        {
            foreach (Resturant res in reslist3)
            {
                if (res.Name == name)
                {
                    return res.Price;
                }
            }
            return null;
        }
        public string GetName_Menu(string title)
        {
            foreach (Resturant res in reslist3)
            {
                if (res.Title.ToString() == title)
                {
                    return res.Name;
                }
            }
            return null;
        }
        public string GetAddr_Seq(string seq)
        {
            foreach (Resturant res in reslist3)
            {
                if (res.Seq == seq)
                {
                    return res.Addr;
                }
            }
            return null;
        }

        #endregion
    }
}
