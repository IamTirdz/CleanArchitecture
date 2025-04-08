namespace Clean.Architecture.Business.Common.Models;

public class ErrorResponse
{
    public ErrorResponse()
    {
    }

    public ErrorResponse(string message, string referenceKey = null!, int? code = null, string codeInfo = null!)
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
    public IDictionary<string, string[]> Errors { get; set; } = null!;
}
