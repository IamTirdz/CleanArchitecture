namespace Clean.Architecture.Business.Common.Models
{
    public class ErrorResponseDto
    {
        public ErrorResponseDto()
        {            
        }

        public ErrorResponseDto(string message, string referenceKey = null!, int? code = null, string codeInfo = null!)
        {
            Message = message;
            ReferenceKey = referenceKey;
            Code = code;
            CodeInfo = codeInfo;
        }

        public string Message { get; set; } = null!;
        public string ReferenceKey { get; set; } = null!;
        public int? Code { get; set; }
        public string CodeInfo { get; set; } = null!;
        public Dictionary<string, string[]> Errors { get; set; } = null!;
    }
}
