using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Server_Consol
{
    class App
    {
        #region singleton pattern
        //프로퍼티 만들고
        public static App Instance { get; private set; }
        //개체생성하고
        static App()
        {
            Instance = new App();
        }
        //default 생성자는 은닉
        private App()
        {

        }
        #endregion

        wbServer server = new wbServer();

        public void AppControl()
        {
            ServerStart();
            ServerRun();
            while (true)
            {
                string name = wblib.inputstring("이름을 검색하세요 : ");
                ResturantSearcher.Instance.SearchResturant(name);
                switch (Console.ReadKey().Key)
                { 
                    case ConsoleKey.Escape: ServerEnd(); break;
                }
            }
        }

        #region 서버
        private void ServerStart()
        {//서버 시작할때
            Ui.Instance.Ui_Start();
        }

        private void ServerRun()
        {//서버 구동
            Ui.Instance.Ui_Running();
            server.ServerRun(12345);
            ResturantSearcher.Instance.SearchResturant2();
            
        }

        private void ServerEnd()
        {//서버 끝날때
            Ui.Instance.Ui_Ending();
            server.ServerStop();
        }
        #endregion
    }
}
