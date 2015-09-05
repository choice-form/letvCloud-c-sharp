using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Web;

namespace WebApplication1.letvCloud
{
    public class LetvCloud
    {
        public const string serverUrl = "http://api.letvcloud.com/open.php";
        public const string secret_key = "768cabd0d7806dcd4da13586029be607";
        public const string format = "json";
        public const string timestamp = "1369300735578";
        public const string user_unique = "fcba45089f";
        public const string ver = "2.0";

        public string videoList(string _index, string _size,string _status)
        {
            string api = "video.list";
            string status = _status;
            string index = _index;
            string size = _index;

            string signParams = "";
            signParams = "api" + api + "format" + format + "index" + index + "size" + size + "status" + status + "timestamp" + timestamp + "user_unique" + user_unique + "ver" + ver;
            string sign = getSign(signParams);
            string queryUrl = serverUrl + "?api=" + api + "&format=" + format + "&index=" + index + "&size=" + size + "&status=" + status + "&timestamp=" + timestamp + "&user_unique=" + user_unique + "&ver=" + ver + "&sign=" + sign;
            string res = getResponse(queryUrl);
            return res;
        }


        public string videoDel(string _video_id)
        {
            string api = "video.del";
            string video_id = _video_id;

            string signParams = "";
            signParams = "api" + api + "format" + format + "timestamp" + timestamp + "user_unique" + user_unique + "ver" + ver + "video_id" + video_id;
            string sign = getSign(signParams);
            string queryUrl = serverUrl + "?api=" + api + "&format=" + format + "&timestamp=" +timestamp + "&user_unique=" + user_unique + "&ver=" + ver + "&video_id=" + video_id + "&sign=" + sign;
            string res = getResponse(queryUrl);
            return res;
        }
        public string videoGet(string _video_id)
        {
            string api = "video.get";
            string video_id = _video_id;

            string signParams = "";
            signParams = "api" + api + "format" + format + "timestamp" + timestamp + "user_unique" + user_unique + "ver" + ver + "video_id" + video_id;
            string sign = getSign(signParams);
            string queryUrl = serverUrl + "?api=" + api + "&format=" + format + "&timestamp=" + timestamp + "&user_unique=" + user_unique + "&ver=" + ver + "&video_id=" + video_id + "&sign=" + sign;
            string res = getResponse(queryUrl);
            return res;
        }

        public string videoUploadInit(string _file_name,string _file_size,string _uploadtype,string _client_ip)
        {
            string api = "video.upload.init";
            string video_name = _file_name;
            string decodeUrlString = Uri.EscapeUriString(video_name);
            string file_size = _file_size;
            string uploadtype = _uploadtype;
            string client_ip = _client_ip;
            string signParams = "";
            signParams = "api" + api + "client_ip" + client_ip + "file_size" + file_size + "format" + format + "timestamp" + timestamp + "uploadtype" + uploadtype + "user_unique" + user_unique + "ver" + ver + "video_name" + video_name;
            string sign = getSign(signParams);
            string queryUrl = serverUrl + "?api=" + api + "&client_ip=" + client_ip + "&file_size=" + file_size + "&format=" + format + "&timestamp=" + timestamp + "&uploadtype=" + uploadtype + "&user_unique=" + user_unique + "&ver=" + ver + "&video_name=" + decodeUrlString + "&sign=" + sign;
            string res = getResponse(queryUrl);
            return res;
        }

        public string videoUploadResume(string _token)
        {
            string api = "video.upload.resume";
            string token = _token;
            string signParams = "";
            signParams = "api" + api + "format" + format + "timestamp" + timestamp + "token" + token + "user_unique" + user_unique + "ver" + ver;
            string sign = getSign(signParams);
            string queryUrl = serverUrl + "?api=" + api + "&format=" + format + "&timestamp=" + timestamp + "&token=" + token + "&user_unique=" + user_unique + "&ver=" + ver + "&sign=" + sign;
            string res = getResponse(queryUrl);
            return res;
        }

        public string videoGetPlayInterface(string _vu,string _width,string _height,string _type)
        {
            var playObj = new PlayObj();
            playObj.vu = _vu;
            playObj.uu = user_unique;

            if (Convert.ToInt32(_width) > 0)
            {
                playObj.width = _width;
            }
            else
            {
                playObj.width = "640";
            }
            if (Convert.ToInt32(_height) > 0)
            {
                playObj.height = _height;
            }
            else
            {
                playObj.height = "480";
            }
            playObj.height = _height;
            playObj.pu = "";
            playObj.auto_play = "0";

            string type = _type;
            string res = "";
            string str = "uu=" + playObj.uu + "&vu=" + playObj.vu + "&pu=" + playObj.pu + "&auto_play=" + playObj.auto_play + "&width=" + playObj.width + "&height=" + playObj.height;
            string jsString = JsonConvert.SerializeObject(playObj);

            if (type == "url")
            {

                res = "http://yuntv.letv.com/bcloud.html?" + str;
            }
            if (type == "js")
            {

                res = "<script type='text/javascript'>var letvcloud_player_conf = " + jsString + ";</script><script type='text/javascript' src='http://yuntv.letv.com/bcloud.js'></script>";
            }
            if (type == "flash")
            {
                res = "http://yuntv.letv.com/bcloud.swf?" + str;
            }
            if (type == "html")
            {
                res = "<embed src='http://yuntv.letv.com/bcloud.swf' allowFullScreen='true' quality='high' width='800' height='450' align='middle' allowScriptAccess='always' flashvars='" + str + "' type='application/x-shockwave-flash'></embed>";
            }
            if (type == "json")
            {
                res = jsString;
            }
            return res;
        }


        /// <summary>
        /// 返回sign字符串
        /// </summary>
        /// <returns></returns>
        protected string getSign(string signParams)
        {
            signParams += secret_key;
            Console.WriteLine("[sign的原字符串: ]" + signParams);
            string hash = ConvertMD5(signParams);
            return hash;
        }
        /// <summary>
        /// 像服务器发送数据并返回接收的数据
        /// </summary>
        /// <returns></returns>
        protected string getResponse(string queryUrl)
        {
            //string url = getQueryUrl();
            string url = queryUrl;
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/text"));

            //HTTP GET
            HttpResponseMessage response = client.GetAsync(url).Result;
            string res = response.Content.ReadAsStringAsync().Result;
            //string jsonArrayText1 = "[{'a':'a1','b':'b1'},{'a':'a2','b':'b2'}]";
            //string ja = (JArray)JsonConvert.DeserializeObject(res);
            return res;
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        protected String ConvertMD5(String s)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(s);
            bytes = md5.ComputeHash(bytes);
            md5.Clear();
            string ret = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                ret += Convert.ToString(bytes[i], 16).PadLeft(2, '0');
            }
            return ret.PadLeft(32, '0');
        }

    }

    public class PlayObj
    {
        public string uu { set; get; }
        public string vu { set; get; }
        public string pu { set; get; }
        public string auto_play { set; get; }
        public string width { set; get; }
        public string height { set; get; }
    }
}