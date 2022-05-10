using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeiXingQi
{
    internal class Program
    {
        public static int[] Maps = new int[100];
        //プレイヤーA,B
        public static int[] playerPos = new int[2];

        public static string[] playerName = new string[2];

        public static bool[] Flags = new bool[2];
        static void Main(string[] args)
        {
            Gameshow();
            #region ニックネームを入力
            Console.WriteLine("プレイヤー  A  のニックネームを入力してください");
            playerName[0] = Console.ReadLine();
            while (playerName[0] == "")
            {
                Console.WriteLine("プレイヤー  A  のニックネームを入力してください");
                playerName[0] = Console.ReadLine();
            }
            Console.WriteLine("プレイヤー  B  のニックネームを入力してください");
            playerName[1] = Console.ReadLine();
            while (playerName[1] == "" || playerName[1] == playerName[0])
            {
                if (playerName[1] == "")
                {
                    Console.WriteLine("プレイヤー B のニックネームを入力してください");
                    playerName[1] = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("プレイヤー A と異なるニックネームを入力してください");
                    playerName[1] = Console.ReadLine();
                }
            }
            #endregion
            Console.Clear();
            Gameshow();
            Console.WriteLine(" {0} の駒は A ", playerName[0]);
            Console.WriteLine(" {0} の駒は B ", playerName[1]);
            InitialMap();
            DrawMap();
            //プレイヤーAとBは終点に到着する前にゲームを続く
            while (playerPos[0] < 99 && playerPos[1] < 99)
            {
                if (Flags[0] == false)
                {
                    PlayGame(0);
                }
                else
                {
                    Flags[0] = false;
                }
                if (playerPos[0]>=99)
                {
                    Console.WriteLine("プレイヤー{0}勝ちました！！！",playerName[0]);
                    break;
                }
                if (Flags[1] == false)
                {
                    PlayGame(1);
                }
                else
                {
                    Flags[1] = false;
                }
                if (playerPos[1] >= 99)
                {
                    Console.WriteLine("プレイヤー{0}勝ちました！！！", playerName[1]);
                    break;
                }
            }
            Console.ReadKey();  
        }
        

        #region タイトル
        public static void Gameshow()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("**************************************");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("**************************************");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("**********                  **********");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("**********   飛行棋ゲーム   **********");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("**********                  **********");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("**************************************");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("**************************************");
        }
        #endregion 

        #region 特殊マップ
        public static void InitialMap()
        {
            int[] luckyturn = { 6, 23, 40, 55, 69, 83 };//ラッキー●
            for (int i = 0; i < luckyturn.Length; i++)
            {
                Maps[luckyturn[i]] = 1;
            }

            int[] landMine = { 5, 13, 17, 33, 38, 50, 64, 80, 94 };//罠★
            for (int i = 0; i < landMine.Length; i++)
            {
                Maps[landMine[i]] = 2;
            }
            int[] pause= { 9, 27, 60, 93 };//ストップ▲
            for (int i = 0; i < pause.Length; i++)
            {
                Maps[pause[i]] = 3;
            }
            int[] timeTunnel = { 20, 25, 45, 63, 72, 88, 90 };//転送◆
            for (int i = 0; i < timeTunnel.Length; i++)
            {
                Maps[timeTunnel[i]] = 4;
            }
        }
        #endregion 

        #region マップを作成
        public static string DrawStringMap(int i)
        {
            string str = "";
            if (playerPos[0] == playerPos[1] && playerPos[1] == i)
            {
                //プレイヤーの位置が同じ
                Console.ForegroundColor = ConsoleColor.White;
                str = "AB";
            }
            else if (playerPos[0] == i)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                str = "A";
            }
            else if (playerPos[1] == i)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                str = "B";
            }
            else
            {
                switch (Maps[i])
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.White;
                        str = "□";
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Red;
                        str = "●";
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        str = "★";
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        str = "▲";
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        str = "◆";
                        break;

                }
            }
            return str;
        }//マップを作成
        #endregion

        public static void DrawMap()
        {
            Console.WriteLine("図例：ラッキー●　罠★　ストップ▲　転送◆");
            #region 横一行目
            for (int i = 0; i < 30; i++)//横一行目
            {
                Console.Write(DrawStringMap(i));
            }
            Console.WriteLine();//縦マップ作成の為改行
            #endregion 

            #region 縦一行目
            for (int i = 30; i < 35; i++)
            {
                for (int j = 0; j <=28; j++)
                {
                    Console.Write("  ");
                }
                Console.Write(DrawStringMap(i));
                Console.WriteLine();//コマを合わせる為改行
            }
            #endregion 

            #region 横二行目
            for (int i = 64; i >=35; i--)
            {
                Console.Write(DrawStringMap(i));
            }
            Console.WriteLine();//縦マップ作成の為改行
            #endregion 

            #region 縦二行目
            for (int i = 65; i <=69; i++)
            {
                Console.WriteLine(DrawStringMap(i));
            }
            #endregion

            #region 横三行目
            for (int i = 70; i <=99; i++)
            {
                Console.Write(DrawStringMap(i));
            }
            Console.WriteLine();
            #endregion 
        }

        public static void PlayGame(int playNumber)
        {
            Random r = new Random();
            int rNumber = r.Next(1,7);
            Console.WriteLine("{0} --任意のキーを押してサイコロを振ってください", playerName[playNumber]);
            Console.ReadKey(true);
            Console.WriteLine("{0} は{1}点を振り出しました", playerName[playNumber], rNumber);
            playerPos[playNumber] += rNumber;
            Console.ReadKey(true);

            if (playerPos[playNumber] == playerPos[1- playNumber])
            {
                Console.WriteLine("{0} は{1}を殴って、{2}は5コマ前に戻りました", playerName[playNumber], playerName[1- playNumber], playerName[1- playNumber]);
                playerPos[1- playNumber] -= 5;
                Console.ReadKey(true);
            }
            else
            {
                switch (Maps[playerPos[playNumber]])
                {
                    case 0:
                        Console.WriteLine("{0} は[普通]コマに到着しました！　何も起こらなかった", playerName[playNumber]);
                        Console.ReadKey(true);
                        break;
                    case 1:
                        Console.WriteLine("{0} は[ラッキー]コマに到着しました！　1:位置を交換　2:相手を攻撃", playerName[playNumber]);
                        string input = Console.ReadLine();
                        while (true)
                        {
                            if (input == "1")
                            {
                                Console.WriteLine("{0} は{1} との位置交換を選択しました", playerName[playNumber], playerName[1- playNumber]);
                                Console.ReadKey(true);
                                int temp = playerPos[playNumber];
                                playerPos[playNumber] = playerPos[1- playNumber];
                                playerPos[1- playNumber] = temp;
                                Console.WriteLine("位置を交換しました!!!  任意のキーを押してください");
                                break;
                            }
                            else if (input == "2")
                            {
                                Console.WriteLine("{0} は{1} を攻撃しました、{2} は6コマ後退します", playerName[playNumber], playerName[1- playNumber], playerName[1- playNumber]);
                                Console.ReadKey(true);
                                playerPos[1- playNumber] -= 6;
                                Console.WriteLine("{0} は6コマ後退しました!!!  任意のキーを押してください", playerName[1- playNumber]);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("1もしくは2を入力してください　1:位置を交換　2:相手を攻撃");
                                input = Console.ReadLine();
                                Console.ReadKey(true);
                            }
                        }
                        break;
                    case 2:
                        Console.WriteLine("{0} は[罠]コマに到着しました！　3コマ後退します", playerName[playNumber]);
                        Console.ReadKey(true);
                        playerPos[playNumber] -= 3;
                        break;
                    case 3:
                        Console.WriteLine("{0} は[ストップ]コマに到着しました！　1回ストップします", playerName[playNumber]);
                        Flags[playNumber] = true;
                        Console.ReadKey(true);
                        break;
                    case 4:
                        Console.WriteLine("{0} は[転送]コマに到着しました！　8コマ前進します", playerName[playNumber]);
                        Console.ReadKey(true);
                        playerPos[playNumber] += 8;
                        break;
                }
            }
            ChangPos();
            Console.Clear();
            DrawMap();
        }

        public static void ChangPos()
        {
            if (playerPos[0]<0)
            {
                playerPos[0] = 0;
            }
            if (playerPos[0] >= 99)
            {
                playerPos[0] = 99;
            }
            if (playerPos[1] < 0)
            {
                playerPos[1] = 0;
            }
            if (playerPos[1] >= 99)
            {
                playerPos[1] = 99;
            }
        }


    }
}
