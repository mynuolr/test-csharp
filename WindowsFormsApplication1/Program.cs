using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
namespace mynuolr
{
    class net_tools
    {
        /// <summary></summary>  
        /// 显示本机各网卡的详细信息  
        /// <summary></summary>  
        public static Dictionary<String, string[]> ShowNetworkInterfaceMessage()
        {
            Dictionary<String, string[]> data = new Dictionary<string, string[]>();
            NetworkInterface[] fNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();ue
            foreach (NetworkInterface adapter in fNetworkInterfaces)
            {
                #region " 网卡类型 "
                string fCardType = "未知网卡";
                string fRegistryKey = "SYSTEM\\CurrentControlSet\\Control\\Network\\{4D36E972-E325-11CE-BFC1-08002BE10318}\\" + adapter.Id + "\\Connection";
                RegistryKey rk = Registry.LocalMachine.OpenSubKey(fRegistryKey, false);
                #endregion
                #region 筛选物理网卡
                if (rk != null)
                {
                    // 区分 PnpInstanceID   
                    // 如果前面有 PCI 就是本机的真实网卡  
                    // MediaSubType 为 01 则是常见网卡，02为无线网卡。  
                    string fPnpInstanceID = rk.GetValue("PnpInstanceID", "").ToString();

                    int fMediaSubType = Convert.ToInt32(rk.GetValue("MediaSubType", 0));
                    if ((fPnpInstanceID.Length > 3) &&
                        (fPnpInstanceID.Substring(0, 3) == "PCI" || fPnpInstanceID.Substring(0, 3) == "USB"))
                    {
                        fCardType = "物理网卡";
                    }
                    else
                    {
                        continue;
                    }

                    //else if (fMediaSubType == 1) {
                    //    continue;

                    //}

                    //else if (fMediaSubType == 2) {
                    //    fCardType = "无线网卡";
                    //}
                }
                IPInterfaceProperties fIPInterfaceProperties = adapter.GetIPProperties();
                UnicastIPAddressInformationCollection UnicastIPAddressInformationCollection = fIPInterfaceProperties.UnicastAddresses;
                string ip = null;
                foreach (UnicastIPAddressInformation UnicastIPAddressInformation in UnicastIPAddressInformationCollection)
                {
                    if (UnicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
                        ip = UnicastIPAddressInformation.Address.ToString();

                }
                if (ip == null || ip == "127.0.0.1")
                {
                    continue;
                }
                #endregion

               
                #region 添加到字典
                string[] st = { ip, toos.InsertFormat(adapter.GetPhysicalAddress().ToString(), 2, "-") };
                data.Add(adapter.Name, st);
                #endregion
            }
            return data;


        }

    }
    class toos
    {
        /// <summary> 
        /// 每隔n个字符插入一个字符 
        /// </summary> 
        /// <param name="input">源字符串</param> 
        /// <param name="interval">间隔字符数</param>
        /// <param name="value">待插入值</param> 
        /// <returns>返回新生成字符串</returns> 
        //用例 public static void Main() { string stest= "EMKEMEMM3335368CKGE43MI3"; Console.WriteLine( InsertFormat(stest,4,"-")); // "EMKE-MEMM-3335-368C-KGE4-3MI3" Console.ReadKey(); } 
        public static string InsertFormat(string input, int interval, string value)
        {
            for (int i = interval; i < input.Length; i += interval + 1)
                input = input.Insert(i, value);
            return input;
        }
    }
}
