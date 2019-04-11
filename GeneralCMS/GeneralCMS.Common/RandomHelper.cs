using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace GeneralCMS.Common
{
    /// <summary>
    /// 随机数生成
    /// </summary>
    public class RandomHelper
    {
        private readonly static List<string> _code = new List<string>() {"0","1","2","3","4", "5", "6", "7", "8", "9",
            "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
            "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"};
        /// <summary>
        /// 加密随机数生成器 生成随机种子
        /// </summary>
        /// <returns></returns>
         
        public static string GetRandomCode(int length = 8)
        {
            int[] randMembers = new int[length];
            string[] validateNums = new string[length];
            string validateNumberStr = "";
            //生成起始序列值
            int seekSeek = unchecked((int)DateTime.Now.Ticks);
            Random seekRand = new Random(seekSeek);
            int beginSeek = (int)seekRand.Next(0, Int32.MaxValue - length * 10000);
            int[] seeks = new int[length];
            for (int i = 0; i < length; i++)
            {
                beginSeek += 10000;
                seeks[i] = beginSeek;
            }
            //生成随机数字
            for (int i = 0; i < length; i++)
            {
                Random rand = new Random(seeks[i]);
                int pownum = 1 * (int)Math.Pow(10, length);
                randMembers[i] = rand.Next(pownum, Int32.MaxValue);
            }
            //抽取随机数字
            for (int i = 0; i < length; i++)
            {
                string numStr = randMembers[i].ToString();
                int numLength = numStr.Length;
                Random rand = new Random();
                int numPosition = rand.Next(0, numLength - 1);
                string num = numStr.Substring(numPosition, 2);
                int code = Int32.Parse(num);
                if (code > _code.Count - 1)
                {
                    num = num.Substring(1, 1) + num.Substring(0, 1);
                    code = Int32.Parse(num);
                    if (code > _code.Count - 1)
                    {
                        numPosition = rand.Next(0, numLength - 1);
                        code = Int32.Parse(numStr.Substring(numPosition, 1));
                    }
                }
                validateNums[i] = _code[code];
            }
            //生成验证码
            for (int i = 0; i < length; i++)
            {
                validateNumberStr += validateNums[i].ToString();
            }
            return validateNumberStr;
        }
    }
}
