using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication1.letvCloud;

namespace WebApplication1.Controllers
{
    

    public class LetvController : Controller
    {
        //public static List<string> letvObj = null;
        public static Dictionary<string, string> letvObj = null;
        public String uploadIp = "";
        public ArrayList arr = null;
        // 视频初始化
       


        // GET: Letv
        public ActionResult Index()
        {


            return View();
        }
        [HttpPost]
        public string todu(string api,string video_name,string file_size,string index,string size,string video_id,string token,string vu,string type,string width,string height)
        {
            var letv = new LetvCloud();
            string api_name = api;
            string res = "";
            switch (api_name)
            {
                case "video.upload.init":
                    Console.WriteLine("{视频上传初始化}");
                    res = letv.videoUploadInit( video_name,  file_size,"1","");
                    break;
                case "video.upload.resume":
                    Console.WriteLine("{视频断点续传}");
                    res = letv.videoUploadResume(token);
                    break;
                case "video.list":
                    Console.WriteLine("{视频列表}");
                    res = letv.videoList( index,  "20","0");
                    break;
                case "video.del":
                    Console.WriteLine("{删除视频}");
                    res = letv.videoDel( video_id);
                    break;
                case "video.get":
                    Console.WriteLine("{获取视频信息}");
                    res = letv.videoGet( video_id);
                    break;
                case "play.interface":
                    Console.WriteLine("{获取视频播放接口}");
                    res = letv.videoGetPlayInterface(vu, width, height,"js");
                    break;
                default:
                    break;
            }
            //JArray rs = (JArray)JsonConvert.DeserializeObject(res);
            //JObject rs = (JObject)JsonConvert.DeserializeObject(res);

            return res;
        }

        //public string getVideoList(string _index,string _size)
        //{
        //    var videolist = new VideoList(_index,_size);
        //    //videolist.
        //    string result = videolist.getResult();
        //    return result;
        //}
         
        //public string videoGet(string _video_id)
        //{
        //    var videoget = new VideoGet(_video_id);
            
        //    string result = videoget.getResult();
        //    return result;
        //}
        //public string videoDel(string _video_id)
        //{
        //    var videodel = new VideoDel(_video_id);
        //    string result = videodel.getResult();
        //    return result;
        //}
        //public string videoUploadInit(string _video_name,string _file_size)
        //{
        //    var videouploadinit = new VideoUploadInit(_video_name,_file_size);
        //    ////videouploadinit.client_ip = "116.226.32.159";
        //    videouploadinit.client_ip = "";
        //    string result = videouploadinit.getResult();
        //    return result;
        //}
        //public string videoUploadResume(string _token)
        //{
        //    var videouploadresume = new VideoUploadResume(_token);
        //    string result = videouploadresume.getResult();
        //    return result;
        //}
        //public string videoGetPlayInterface(string _vu, string _type, string _width, string _height)
        //{
        //    var videogetplayinterface = new VideoGetPlayInterface();
        //    string result = videogetplayinterface.getResult( _vu,  _type,  _width,  _height);
        //    return result;
        //}
    }
}