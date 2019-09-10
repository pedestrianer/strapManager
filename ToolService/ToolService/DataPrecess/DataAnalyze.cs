using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MySql;
using System.Data;

namespace DBoperation
{
    class DataAnalyze
    {
        DBManager db;
        public static bool tempStrapResponse;
        public static bool fireStrapResponse;
        public static bool setConcentratorResponse;
        public static bool setTempStrapAddressResponse;
        public static bool setFireStrapAddressResponse;
        public DataAnalyze()
        {
            db = new DBManager();
            tempStrapResponse = false;
            fireStrapResponse = false;
            setConcentratorResponse = false;
            setTempStrapAddressResponse = false;
            setFireStrapAddressResponse = false;
        }

        public void DataAnalysis(byte[] revData)
        {

            switch (revData[3])
            {
                //下发测温表带回复
                case 0x00:
                    setTempStrapAddressResponse = true;
                    break;
                //下发火灾表带回复
                case 0x01:
                    setFireStrapAddressResponse = true;
                    break;
                //集中器发送的异常温度
                case 0x03:
                    if (revData[1] == 0x03 && revData[5] == 0x0C)
                    {
                        string sqlStr = "Insert into concentratortempdata (concentratoraddress,registeraddreess,strapaddress,straptype,temperture,voltage,upnumber) values ";
                        string temperture = "";
                        string voltage = "" + revData[15] + "." + revData[16];

                        if (revData[12] == 1)
                            temperture += "-";
                        temperture = temperture + revData[13] + "." + revData[14];
                        sqlStr = sqlStr + "('" + ToHex(revData.Skip(0).Take(1).ToArray()) + "','" + ToHex(revData.Skip(2).Take(2).ToArray()) + "','" + ToHex(revData.Skip(6).Take(5).ToArray()) + "','" + revData[11] + "','" + temperture + "','" + voltage + "','" + revData[17] + "')";
                        DataStore(sqlStr);
                    }
                    break;

                //集中器发送的火灾报警
                case 0x04:
                    if(revData[1] == 0x03 && revData[5] == 0x0A)
                    {
                        string sqlStr = "Insert into concentratorfiredata (concentratoraddress,registeraddreess,strapaddress,straptype,isfire,voltage,upnumber) values ";
                        string voltage = "" + revData[13] + "." + revData[14];
                        sqlStr = sqlStr + "('" + ToHex(revData.Skip(0).Take(1).ToArray()) + "','" + ToHex(revData.Skip(2).Take(2).ToArray()) + "','" + ToHex(revData.Skip(6).Take(5).ToArray()) + "','" + revData[11] + "','" + revData[12] + "','" + voltage + "','" + revData[15] + "')";
                        DataStore(sqlStr);
                    }
                    break;
                //服务器请求温度数据
                case 0x05:
                    int readByteNumber1 = revData[4] * 256 + revData[5];
                    if(readByteNumber1 % 12 == 0)
                    {
                        string sqlStr = "Insert into concentratortempdata (concentratoraddress,registeraddreess,strapaddress,straptype,temperture,voltage,upnumber) values ";
                        for(int i = 0; i<readByteNumber1 / 12; i++)
                        {
                            string temperture = "";
                            string voltage = "" + revData[i * 12 + 15] + "." + revData[i * 12 + 16];

                            if (revData[i * 12 + 12] == 1)
                                temperture += "-";
                            temperture = temperture + revData[i * 12 + 13] + "." + revData[i * 12 + 14];
                            sqlStr = sqlStr + "('" + ToHex(revData.Skip(0).Take(1).ToArray()) + "','" + ToHex(revData.Skip(2).Take(2).ToArray()) + "','" + ToHex(revData.Skip(i * 12 + 6).Take(5).ToArray()) + "','" + revData[i * 12 + 11] + "','" + temperture + "','" + voltage + "','" + revData[i * 12 + 17] + "'),";  
                        }
                        sqlStr = sqlStr.Substring(0, sqlStr.Length - 1);
                        if(DataStore(sqlStr))
                            tempStrapResponse = true;
                    }
                    
                    break;
                //服务器请求火灾数据
                case 0x06:
                    int readByteNumber2 = revData[4] * 256 + revData[5];
                    if(readByteNumber2 % 10 == 0)
                    {
                        string sqlStr = "Insert into concentratorfiredata (concentratoraddress,registeraddreess,strapaddress,straptype,isfire,voltage,upnumber) values ";
                        for(int i = 0; i<readByteNumber2 / 10; i++)
                        {
                            string voltage = "" + revData[i * 10 + 13] + "." + revData[i * 10 + 14];
                            sqlStr = sqlStr + "('" + ToHex(revData.Skip(0).Take(1).ToArray()) + "','" + ToHex(revData.Skip(2).Take(2).ToArray()) + "','" + ToHex(revData.Skip(i * 10 + 6).Take(5).ToArray()) + "','" + revData[i * 10 + 11] + "','" + revData[i * 10 + 12] + "','" + voltage + "','" + revData[i * 10 + 15] + "'),";
                        }
                        sqlStr = sqlStr.Substring(0, sqlStr.Length - 1);
                        if(DataStore(sqlStr))
                            fireStrapResponse = true;
                    }
                    break;
                case 0x08:
                    bool successFlag = true;
                    byte[] compare = new byte[] { 0x10, 0x00, 0x08, 0x00, 0x14, 0x00, 0x00 };
                     for(int i = 0;i < 7;i++)
                        {
                         if(compare[i] != revData[i+1])
                         {
                          successFlag = false;
                          break;
                         }
                      }
                    setConcentratorResponse = successFlag;
                    break;
                default:
                    break;

            }
        }

        private bool DataStore(string strStore)
        {

            if (db.Insert(strStore))
                return true;
            else
                return false;
                
        }
        
        private string ToHex(byte[] source)
        {
            String destination = "";
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] < 16) destination += "0";
                destination += String.Format("{0:X}", source[i]);
            }

            return destination;
        }
    }

    
}
