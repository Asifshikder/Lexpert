﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lexpert.Helpers
{
    public interface IEmailSender
    {
        void Post(string subject, string body, string recipients, string sender);
        void PostWithFile(string subject, string body, string recipients, string sender,string filename);
     
    }
}
