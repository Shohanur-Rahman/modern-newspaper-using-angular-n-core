using App.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Common.Static
{
    public static class ResponseMapping
    {
        public static ResponseMessage GetResponseMessage(object any, int statusCode, string messge)
        {
            ResponseMessage responseMessage = new ResponseMessage();
            responseMessage.data = any;
            responseMessage.statusCode = statusCode;
            responseMessage.message = messge;
            responseMessage.isSuccess = (statusCode == 1)? true : false;
            return responseMessage;
        }
    }
}
