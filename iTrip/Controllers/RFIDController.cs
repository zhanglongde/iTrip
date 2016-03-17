using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using iTrip.Codes.BLL;

namespace iTrip.Controllers
{
    public class RFIDController : Controller
    {
        //
        // GET: /RFID/
        public static IntPtr _handle = IntPtr.Zero;
        TrafficStatisticsBll tsb = new TrafficStatisticsBll();
        public ActionResult Index()
        {
            return View();
        }
        public string StartRfid()
        {
            string result = "";
            int portNo = Convert.ToInt32(Request.Params["portNo"]);
            if (_handle == IntPtr.Zero || (int)_handle == -1)
            {
                _handle = EPCSDKHelper.OpenComm(portNo);
                if (_handle != IntPtr.Zero && (int)_handle != -1)
                    result = "成功开启";
                else
                    result = "开启失败";
            }
            else
                result = "设备已开启";
            return result;
        }

        public void CloesRFID()
        {
            EPCSDKHelper.CloseComm(_handle);
            _handle = IntPtr.Zero;
        }

        public string StopReader()
        {
            if (EPCSDKHelper.StopReading(_handle, 0))
                return "读头成功";
            else
                return "读头失败";
        }

        public void MultiReadMod()
        {
            byte[] param_s = new byte[1] { 1 };
            EPCSDKHelper.ResumeReading(_handle, 0);
            EPCSDKHelper.SetReaderParameters(_handle, 0x87, 1, param_s, 0);
        }

        public string SingleReadRFID()
        {
            string result = "";
            byte[] data = new byte[12];
            if (EPCSDKHelper.ReadTag(_handle, 0x01, 0x02, 6, data, 0))
            {
                byte[] bTmp = new byte[12];
                Buffer.BlockCopy(data, 0, bTmp, 0, 12);
                result = TextEncoder.ByteArrayToHexString(bTmp);
                return result;
            }
            else
            {
                result = "读取失败";
                return result;
            }
        }

        public string MultiReadRFID()
        {
            string[] result = new string[10] { "", "", "", "", "", "", "", "", "", "" };
            byte idNum = 0;	// Max tag num is 200
            byte[] ids = new byte[12 * 10];
            byte[] devNos = new byte[10];
            byte[] antennaNos = new byte[10];
            byte[] param_s = new byte[1] { 1 };
            for (int i = 0; i < 10; i++)
                result[i] = "";
            if (EPCSDKHelper.SetReaderParameters(_handle, 0x87, 1, param_s, 0))
            {
                if (EPCSDKHelper.IdentifyUploadedMultiTags(_handle, out idNum, ids, devNos, antennaNos))
                {
                    for (int j = 0; j < idNum; j++)
                    {
                        byte[] bTmp = new byte[12];
                        Buffer.BlockCopy(ids, 12 * j, bTmp, 0, 12);
                        result[j] = TextEncoder.ByteArrayToHexString(bTmp);
                    }
                }
                else
                {
                    result[0] = "识别标签失败!";
                }
            }
            else
            {
                result[0] = "设置多卡模式失败";
            }
            if (result[0] != "识别标签失败!" && result[0] != "设置多卡模式失败")
            {
                int i = 1;
                while (i <= 10)
                {
                    if (result[i - 1] != "")
                    {
                        if (!tsb.CheckHaveReadCardInTimeBll(result[i - 1]))
                        {
                            tsb.AddUserNumBll(result[i - 1]);
                        }
                    }
                    i++;
                }
            }
            return result[0];
        }

        public void stopBeep()//读卡时不响beep声
        {
            EPCSDKHelper.BeepCtrl(_handle, 0, 0);
        }
    }
}
