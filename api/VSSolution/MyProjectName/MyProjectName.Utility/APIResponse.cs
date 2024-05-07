using System;
using System.Collections.Generic;
using System.Text;

namespace MyProjectName.Utility
{
   public class APIResponse
    {
        public APIResponse()
        {

        }
        public APIResponse(ResponseCode code, string message, object data = null)
        {
            this.code = code;
            this.message = message;
            this.document = data;
        }
        public ResponseCode code { get; set; }
        public string message { get; set; }
        public object document { get; set; }
    }

    public enum ResponseCode
    {
        ERROR=0,
        SUCCESS=1
    }
}

