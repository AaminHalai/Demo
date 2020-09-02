using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoAPI.ViewModels
{
    public class APIResponse<T>
    {
        public bool status { get; set; }
        public string message { get; set; }
        public string code { get; set; }
        public Dictionary<string,DataModel> data { get; set; }

        public APIResponse(bool apiStatus, string apiMessage, string technicalStatusCode, Dictionary<string, DataModel> responsedata)
        {
            status = apiStatus;
            message = apiMessage;
            code = technicalStatusCode;

            if (responsedata != null)
                data = responsedata;
        }

    }
}