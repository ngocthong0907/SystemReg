using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Threading;
namespace NinjaSystem
{
    public partial class frm_RegisterHana : Form
    {
        public frm_RegisterHana(string i_token)
        {
            InitializeComponent();
            token = i_token;
        }


        Account acc;
        string token;
        private void frmAddUser_Load(object sender, EventArgs e)
        {

        }


        private void btnTao_Click(object sender, EventArgs e)
        {
            try
            {
                var client = new RestClient("http://duy-tool.amaiteam.com/farmer/api/v1/register");
                var request = new RestRequest(Method.POST);

                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("authorization", "bearer " + token);
                request.AddHeader("accept", "application/json");
                request.AddHeader("content-type", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW");
                request.AddParameter("ref", "AM224536");
                request.AddParameter("amai_key", "qB8VtVreIaxzNqmEeeqqquB");
                request.AddParameter("password", txtPass.Text);
                request.AddParameter("password_confirmation", txtPass.Text);
                request.AddParameter("username", txtUID.Text);
                IRestResponse response = client.Execute(request);

                var data = response.Content;
                JObject obj = JObject.Parse(data);
                string result = obj["success"].ToString();
                if (result == "False")
                    MessageBox.Show(obj["message"].ToString());
                else
                {
                    Thread.Sleep(3000);
                    client = new RestClient("http://duy-tool.amaiteam.com/farmer/api/v1/login");
                    request = new RestRequest(Method.POST);
                    request.AddParameter("username", txtUID.Text);
                    request.AddParameter("password", txtPass.Text);
                    response = client.Execute(request);
                    data = response.Content;
                    obj = JObject.Parse(data);
                    string tokenmoney = obj["data"]["token"].ToString();

                    if (!string.IsNullOrEmpty(tokenmoney))
                    {
                        client = new RestClient("http://duy-tool.amaiteam.com/farmer/api/v1/facebook-account/ruPhonefarm");
                        request = new RestRequest(Method.POST);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("authorization", "bearer " + tokenmoney);
                        request.AddHeader("accept", "application/json");
                        request.AddHeader("content-type", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW");
                        request.AddParameter("user_id", obj["data"]["user"]["id"].ToString());
                        request.AddParameter("amai_key", "qB8VtVreIaxzNqmEeeqqquB");
                        response = client.Execute(request);

                        data = response.Content;
                        obj = JObject.Parse(data);

                        MessageBox.Show(obj["message"].ToString());
                    }
                   
                }
            }
            catch
            {
                MessageBox.Show("Có lỗi xảy ra vui lòng thử lại");
            }
        }


        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
