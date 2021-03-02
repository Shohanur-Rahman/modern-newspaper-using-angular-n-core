using System;
using System.Collections.Generic;
using System.Text;

namespace App.Common.Response
{
    public class ResponseMessage
    {
        public object data { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public bool isSuccess { get; set; }

    }
}
