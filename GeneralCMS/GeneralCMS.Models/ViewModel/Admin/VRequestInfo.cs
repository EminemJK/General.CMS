using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralCMS.Models.ViewModel.Admin
{
    public class VRequestInfo
    {

        public const int Success = 200;
        public const int Error = 1;
        public const int Default = 0;

        public int Code { get; set; }
        public string Msg { get; set; }
        public object Data { get; set; }

        public VRequestInfo() { }

        public VRequestInfo(int code, string msg)
        {
            this.Code = code;
            this.Msg = msg;
        }

        public VRequestInfo(int code, string msg,object data):this(code,msg)
        {
            this.Data = data; 
        }

        public static VRequestInfo SuccessResult(string msg = "")
        {
            return new VRequestInfo(Success, msg, null);
        }

        public static VRequestInfo SuccessResult(string msg, object data)
        {
            return new VRequestInfo(Success, msg, data);
        }

        public static VRequestInfo ErrorResult(string msg, object data)
        {
            return new VRequestInfo(Error, msg, data);
        }

        public static VRequestInfo ErrorResult(string msg)
        {
            return new VRequestInfo(Error, msg, "");
        }

        public static VRequestInfo DefaultResult(string msg, object data = null)
        {
            return new VRequestInfo(Default, msg, data);
        }


    }
}
