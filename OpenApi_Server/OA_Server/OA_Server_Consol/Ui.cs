using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Server_Consol
{
    class Ui
    {
        #region singleton pattern
        //프로퍼티 만들고
        public static Ui Instance { get; private set; }
        //개체생성하고
        static Ui()
        {
            Instance = new Ui();
        }
        //default 생성자는 은닉
        private Ui()
        {

        }
        #endregion


        #region 외부 호출 가능 함수
        public void Ui_Start()
        {
            Console.WriteLine("우송 비트 30기 2조");
            Console.WriteLine("OpenApi 공모전");
            Console.WriteLine("조장 : 홍성우");
            Console.WriteLine("조원 : 강명수");
            Console.WriteLine("조원 : 강희원");
            Console.WriteLine("조원 : 김대정");
            Console.WriteLine("조원 : 조진수");
        }

        public void Ui_Running()
        {
            Console.WriteLine("프로그램 구동중...");
        }

        public void Ui_Ending()
        {
            Console.WriteLine("프로그램을 종료합니다");
        }
        #endregion
    }
}
