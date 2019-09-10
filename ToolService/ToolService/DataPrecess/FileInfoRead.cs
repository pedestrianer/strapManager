using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DBoperation
{
    sealed class FileInfoRead
    {
        #region 文件读取字段
        private string FilePath;
        public string concentratorIP;
        public string concentratorPORT;
        public string port;
        #endregion

        #region 构造方法
        public FileInfoRead()
        {
            FilePath = AppDomain.CurrentDomain.BaseDirectory + @"\Set.INI";
            concentratorPORT = ReadIniData("NetCfg", "CONCENTRATORPORT", "", FilePath);
            concentratorIP = ReadIniData("NetCfg", "CONCENTRATORIP", "", FilePath);
            port = ReadIniData("NetCfg", "Port", "", FilePath);
        }
        #endregion

        /// <summary>
        /// 读取指定文件路径下文件的指定项
        /// </summary>
        /// <param name="Section">INI文件中要读取的段落名</param>
        /// <param name="Key">键值</param>
        /// <param name="NoText">读取异常的情况下的缺省值</param>
        /// <param name="iniFilePath">文件路径</param>
        /// <returns></returns>
        private string ReadIniData(string Section, string Key, string NoText, string iniFilePath)
        {
            if (File.Exists(iniFilePath))
            {
                StringBuilder temp = new StringBuilder(1024);
                GetPrivateProfileString(Section, Key, NoText, temp, 1024, iniFilePath);
                return temp.ToString();
            }
            else
            {
                return String.Empty;
            }
        }

        //写配置文件集中器IP和PORT
        public bool WriteIniData(string concenIP, string concenPORT)
        {
           string FilePath = AppDomain.CurrentDomain.BaseDirectory + @"\Set.INI";
           if (File.Exists(FilePath))
           {

               WritePrivateProfileString("NetCfg", "CONCENTRATORIP", concenIP, FilePath);
               WritePrivateProfileString("NetCfg", "CONCENTRATORPORT", concenPORT, FilePath);
               return true;
           }
           else
           {
               return false;
           }
            
        }

        #region 引用WIN32 API
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key,
            string val, string filePath);
        [DllImport("kernel32")]
        private static extern long GetPrivateProfileString(string section, string key,
            string def, StringBuilder retVal, int size, string filePath);
        #endregion
    }
}
