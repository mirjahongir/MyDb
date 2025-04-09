
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Error
    {
        private Error(int code, string message)
        {
            Code = code;
            Message = message;
        }
        public static Error Create(int code, [Required] int projectId, string message)
        {
            code = (projectId * 100) + code;
            return new Error(code, message);
        }
        public string? Message { get; private set; }
        public int Code { get; private set; }

    }
}
