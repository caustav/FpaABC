using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common
{
    public class Error
    {
        private int code;
        private string message;

        public Error(int code, string message)
        {
            this.code = code;
            this.message = message;
        }

        public new string ToString()
        {
            return $"code:{code}, message:{message}";
        }
    }
}
