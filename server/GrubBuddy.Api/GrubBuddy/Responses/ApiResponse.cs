using System.Collections.Generic;

namespace GrubBuddy.Responses
{
    public class ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public object Result { get; set; }
    }
}