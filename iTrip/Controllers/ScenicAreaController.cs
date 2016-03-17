using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTrip.Codes.BLL;
using iTrip.Models;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Data;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
namespace iTrip.Controllers
{
    public class ScenicAreaController : Controller
    {
        //
        // GET: /ScenicArea/
        PictureModel pi = new PictureModel();
        ScenicSpotModel um = new ScenicSpotModel();
        ScenicSpotBll rb = new ScenicSpotBll();
        ScenicAreaBll sab = new ScenicAreaBll();
        public ActionResult ScenicArea()
        {
            return View();
        }
        [HttpPost]
        public object getScenicZoneInfo()
        {
            string userName = Request.Form["userName"].ToString();
            List<string> szi = new List<string>();
            szi.Add(sab.GetScenicZoneInfoBll(userName)[0]);
            szi.Add(sab.GetScenicZoneInfoBll(userName)[1]);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(szi);
        }
        public string Upload(FormContext from)
        {
            var file = Request.Files["Filedata"];
            string uploadPath = Server.MapPath("~/UploadFile/");
            //HttpContext.Current.Server.MapPath(@context.Request["folder"]) + "\\";
            string fileName = file.FileName;
            string type = fileName.Substring(fileName.Length - 3);
            string name = "";
            if (type == "mp3")
            {
                name = string.Format("{0}.mp3", DateTime.Now.Ticks.ToString());
            }
            else
            {
                name = string.Format("{0}.jpg", DateTime.Now.Ticks.ToString());
            }
            string a = "../../UploadFile/" + name;
            if (file != null)
            {
                file.SaveAs(uploadPath + name);
                //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失\
                if (type != "mp3")
                {
                    //SaveCompress("../../UploadFileTemp/" + name, "../../UploadFile/" + name, 100, 100);
                    //GetPicThumbnail("../../UploadFileTemp/" + name, "../../UploadFile/" + name,100,100,90);
                }
                return a;
            }
            else
            {
                return "shibai";
            }

            //
        }

        public string ScenicIntroduction()
        {
            string scenicSpotName = Request.Form["spotName"];
            string scenicSpotIntroduce = Request.Form["introduce"];
            string picturePath = Request.Form["picpath"];
            int scenicZoneID = Convert.ToInt32(Request.Form["scenciZoneId"].ToString());
            string storePath = Request.Form["soundpath"];
            return rb.AddScenicSpotBLL(scenicSpotName, scenicSpotIntroduce, scenicZoneID, picturePath, storePath);
        }
        public string modifyScenic()
        {
            //ScenicSpotModel um = new ScenicSpotModel();
            ScenicSpotBll rb = new ScenicSpotBll();
            string scenicspotid = Request.Form["scenicspotid"];
            DataTable dt = rb.ModifyScenic(scenicspotid);
            List<Spotvoicepic> lua = new List<Spotvoicepic>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lua.Add(new Spotvoicepic
                {
                    //UserName = dt.Rows[i]["username"].ToString(),
                    ScenicSpotName = dt.Rows[i]["scenicSpotName"].ToString(),
                    ScenicSpotIntroduce = dt.Rows[i]["scenicSpotIntroduce"].ToString(),
                    PicturePath = dt.Rows[i]["picturePath"].ToString(),
                    StorePath = dt.Rows[i]["storePath"].ToString()
                    //Picture = dt.Rows[i]["picture"].ToString(),


                });
            }

            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(lua);


        }
        public string modifyinformation()
        {
            int scenicSpotID = Convert.ToInt16(Request.Form["scenicspotid"]);
            string scenicSpotName = Request.Form["spotName"];
            string scenicSpotIntroduce = Request.Form["introduce"];
            string picture = Request.Form["picpath"];
            string sound = Request.Form["sound"];
            if (rb.ScenicModifyinfo(scenicSpotID, scenicSpotName, scenicSpotIntroduce, picture, sound))
            {
                return "true";

            }
            else
            {
                return "false";
            }

        }
        public string Scenicshow()
        {
            string scenicareaid = Request.Form["scenicareaid"].ToString();
            DataTable dt = rb.SpotshowBLL(scenicareaid);
            List<Spotvoicepic> lua = new List<Spotvoicepic>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lua.Add(new Spotvoicepic
                {
                    //UserName = dt.Rows[i]["username"].ToString(),
                    ScenicSpotID = Convert.ToInt16(dt.Rows[i]["spotid"]),
                    ScenicSpotName = dt.Rows[i]["scenicSpotName"].ToString(),
                    ScenicSpotIntroduce = dt.Rows[i]["scenicSpotIntroduce"].ToString(),
                    PicturePath = dt.Rows[i]["picturePath"].ToString(),
                    StorePath = dt.Rows[i]["storePath"].ToString()
                });
            }

            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(lua);
        }
        public string Scenicdelete()
        {
            int scenicspotid = Convert.ToInt16(Request.Form["scenicspotid"]);
            if (rb.ScenicDeleteBLL(scenicspotid))
            {
                return "true";

            }
            else
            {
                return "false";
            }

        }
        /// <summary>
        /// 把指定的图片按指定的大小保存到指定的文件中
        /// </summary>
        /// <param name="imagepath">源图地址</param>
        /// <param name="compresspath">压缩图保存地址</param>
        /// <param name="width">压缩后图片的宽度</param>
        /// <param name="height">压缩后图片的高度</param>
        /// <returns>保存是否成功</returns>
        /// 一般错误：到保存图片时出现GDI错误——一般为保存图片的地址不正确
        protected static bool SaveCompress(string imagepath, string compresspath, int width, int height)
        {

            bool issuccess = false;
            //如果文件已存在则删除该文件
            if (System.IO.File.Exists(compresspath))
            {
                //FileOperating.fileDelete(compresspath, false);
                System.IO.File.Delete(compresspath);
            }
            //判断文件是否存在
            System.Drawing.Image image = System.Drawing.Image.FromFile(imagepath);//获取源图
            //判断保存图片的文件夹是否存在，若不存在则创建
            string filepath = compresspath.Substring(0, compresspath.LastIndexOf("\\"));
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            System.Drawing.Image simage = new System.Drawing.Bitmap(width, height);//新建一张图片
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(simage);//新建一张画布
            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(System.Drawing.Color.Transparent);//清空画布以透明填充
            g.DrawImage(image, new Rectangle(0, 0, width, height), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

            try
            {
                simage.Save(compresspath, image.RawFormat);//保存压缩图
                issuccess = true;
                //log.Warn("保存简图成功！");
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                //释放资源
                image.Dispose();
                simage.Dispose();
                g.Dispose();
                //log.Warn("保存简图：源图地址：" + imagepath + "简图地址：" + compresspath);
            }
            return issuccess;
        }
        /// 无损压缩图片
        /// </summary>
        /// <param name="sFile">原图片</param>
        /// <param name="dFile">压缩后保存位置</param>
        /// <param name="dHeight">高度</param>
        /// <param name="dWidth"></param>
        /// <param name="flag">压缩质量 1-100</param>
        /// <returns></returns>
        //public bool GetPicThumbnail(string sFile, string dFile, int dHeight, int dWidth, int flag)
        //{
        //    System.Drawing.Image iSource = System.Drawing.Image.FromFile(sFile);
        //    ImageFormat tFormat = iSource.RawFormat;
        //    int sW = 0, sH = 0;
        //    //按比例缩放
        //    Size tem_size = new Size(iSource.Width, iSource.Height);

        //    if (tem_size.Width > dHeight ||tem_size.Width > dWidth) //将**改成c#中的或者操作符号
        //    {
        //        if ((tem_size.Width * dHeight) > (tem_size.Height * dWidth))
        //        {
        //            sW = dWidth;
        //            sH = (dWidth * tem_size.Height) / tem_size.Width;
        //        }
        //        else
        //        {
        //            sH = dHeight;
        //            sW = (tem_size.Width * dHeight) / tem_size.Height;
        //        }
        //    }
        //    else
        //    {
        //        sW = tem_size.Width;
        //        sH = tem_size.Height;
        //    }
        //    Bitmap ob = new Bitmap(dWidth, dHeight);
        //    Graphics g = Graphics.FromImage(ob);
        //    g.Clear(Color.WhiteSmoke);
        //    g.CompositingQuality = CompositingQuality.HighQuality;
        //    g.SmoothingMode = SmoothingMode.HighQuality;
        //    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //    g.DrawImage(iSource, new Rectangle((dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH), 0, 0, iSource.Width, iSource.Height, GraphicsUnit.Pixel);
        //    g.Dispose();
        //    //以下代码为保存图片时，设置压缩质量
        //    EncoderParameters ep = new EncoderParameters();
        //    long[] qy = new long[1];
        //    qy[0] = flag;//设置压缩的比例1-100
        //    EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
        //    ep.Param[0] = eParam;
        //    try
        //    {
        //        ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
        //        ImageCodecInfo jpegICIinfo = null;
        //        for (int x = 0; x < arrayICI.Length; x++)
        //        {
        //            if (arrayICI[x].FormatDescription.Equals("JPEG"))
        //            {
        //                jpegICIinfo = arrayICI[x];
        //                break;
        //            }
        //        }
        //        if (jpegICIinfo != null)
        //        {
        //            ob.Save(dFile, jpegICIinfo, ep);//dFile是压缩后的新路径
        //        }
        //        else
        //        {
        //            ob.Save(dFile, tFormat);
        //        }
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        iSource.Dispose();
        //        ob.Dispose();
        //    }

        //}

    }
}
