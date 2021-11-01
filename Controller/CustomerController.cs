using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
namespace NinjaSystem
{
    public class CustomerController
    {
        public ResultRequest method_Register(CustomerTrialModel cus)
        {
            ResultRequest result = new ResultRequest();

            try
            {
                var client = new RestClient("http://ninjateam.vn/api/facebook");
                var request = new RestRequest(Method.POST);
                RequestParams re = new RequestParams();
                string data = FunctionHelper.mahoa(JsonConvert.SerializeObject(cus), "越南أنا أحب");

                request.AddHeader("Accept", "application/x-www-form-urlencoded");
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddParameter("type", "1");
                request.AddParameter("appid", "22");
                request.AddParameter("data", data);

                IRestResponse response = client.Execute(request);
                string html = response.Content;
                data = FunctionHelper.giaima(html, "越南أنا أحب");
                result = JsonConvert.DeserializeObject<ResultRequest>(data);
            }
            catch
            {
                result = new ResultRequest();
                result.status = false;
                result.mess = "Máy bạn không thể đăng ký dùng thử.Vui lòng liên hệ Ninja Team để đăng ký bản quyền";
            }
            return result;
        }
        public ResultRequest method_Update(int appid)
        {
            ResultRequest result = new ResultRequest();
            
            try
            {
                var client = new RestClient("http://ninjateam.vn/api/updatetool");
                var request = new RestRequest(Method.POST);

                RequestParams re = new RequestParams();
                AppModel app = new AppModel();

                app.ID = appid;
                app.Version = SettingTool.version;
                string data = FunctionHelper.mahoa(JsonConvert.SerializeObject(app), SettingTool.privatekey);

                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("type", "1");
                request.AddParameter("appid", "6");
                request.AddParameter("data", data);
                IRestResponse response = client.Execute(request);
                string html = response.Content.Replace("\"","");
                data = FunctionHelper.giaima(html, SettingTool.privatekey);
                result = JsonConvert.DeserializeObject<ResultRequest>(data);


            }
            catch
            {
                result = new ResultRequest();
                result.status = false;
                result.mess = "Bạn đang sử dụng phiên bản mới nhất của phần mềm";
            }
            return result;
        }
        public ResultRequest method_UpdateAll()
        {
            ResultRequest result = new ResultRequest();

            try
            {
                var client = new RestClient("http://ninjateam.vn/api/updatetool");
                var request = new RestRequest(Method.POST);
                RequestParams re = new RequestParams();
                AppModel app = new AppModel();
                app.ID = 6;
                app.Version = SettingTool.version;
                string data = FunctionHelper.mahoa(JsonConvert.SerializeObject(app), SettingTool.privatekey);

                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("type", "2");
                request.AddHeader("appid", "6");
                request.AddHeader("data", data);
                IRestResponse response = client.Execute(request);
                string html = response.Content;
                result = JsonConvert.DeserializeObject<ResultRequest>(data);
            }
            catch
            {
                result = new ResultRequest();
                result.status = false;
                result.mess = "Bạn đang sử dụng phiên bản mới nhất của phần mềm";
            }
            return result;
        }
        public TwoFaModel getCodeTwofa(string uid, string privatekey)
        {

            TwoFaModel result = new TwoFaModel();

            try
            {
                var client = new RestClient("http://unlock.ninjateam.vn/api/NinjaUnlock/SetupTwoFa");

                var request = new RestRequest(Method.POST);
                TwoFaModel model = new TwoFaModel();
                model.uid = uid;
                model.privatekey = privatekey;

                string data = FunctionHelper.mahoa(JsonConvert.SerializeObject(model), "097909098970979090897");

                request.AddHeader("Accept", "application/x-www-form-urlencoded");
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddParameter("type", "2");
                request.AddParameter("data", data);

                IRestResponse response = client.Execute(request);
                string html = response.Content.Replace("\"", "");
                data = FunctionHelper.giaima(html, "097909098970979090897");
                result = JsonConvert.DeserializeObject<TwoFaModel>(data);

            }
            catch
            {
                result = new TwoFaModel();
                result.status = false;

            }
            return result;
        }
        public ResultRequest method_Login(CustomerTrialModel cus)
        {
            ResultRequest result = new ResultRequest();

            try
            {
                var client = new RestClient("http://api.ninjateam.vn/api/login/NinjaSystem");

                var request = new RestRequest(Method.POST);

                string data = FunctionHelper.mahoa(JsonConvert.SerializeObject(cus), "876d906817656883f2220b0b77e9b6b1");

                request.AddHeader("Accept", "application/x-www-form-urlencoded");
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddParameter("type", "2");
                request.AddParameter("appid", "23");
                request.AddParameter("data", data);

                IRestResponse response = client.Execute(request);
                string html = response.Content.Replace("\"", "");
                data = FunctionHelper.giaima(html, "876d906817656883f2220b0b77e9b6b1");
                result = JsonConvert.DeserializeObject<ResultRequest>(data);
            }
            catch
            {
                result = new ResultRequest();
                result.status = false;
                result.mess = "Không thể đăng nhập tài khoản vui lòng kiểm tra kết nối mạng.";
            }
            return result;
        }
        public ResultRequest sendLogs(string function)
        {
            ResultRequest result = new ResultRequest();

            try
            {
                var client = new RestClient("http://api.ninjateam.vn/api/NinjaShareLive/Logsystem");

                var request = new RestRequest(Method.POST);
                SettingTool.client.function = function;
                string data = FunctionHelper.mahoa(JsonConvert.SerializeObject(SettingTool.client), "f42df5296dea6efd9454ca33bc12bcf0");

                request.AddHeader("Accept", "application/x-www-form-urlencoded");
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddParameter("type", "3");
                request.AddParameter("appid", "22");
                request.AddParameter("data", data);

                IRestResponse response = client.Execute(request);
                string html = response.Content.Replace("\"", "");
                data = FunctionHelper.giaima(html, "f42df5296dea6efd9454ca33bc12bcf0");
                result = JsonConvert.DeserializeObject<ResultRequest>(data);
            }
            catch
            {
                result = new ResultRequest();
                result.status = false;
                result.mess = "Không thể đăng nhập tài khoản vui lòng kiểm tra kết nối mạng.";
            }
            return result;
        }

        
    }
}
