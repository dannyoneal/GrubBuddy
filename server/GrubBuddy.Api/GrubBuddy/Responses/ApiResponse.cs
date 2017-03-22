using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Text;
using System.Threading.Tasks;

namespace GrubBuddy.Responses
{
    public class ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public object Result { get; set; }
    }
}