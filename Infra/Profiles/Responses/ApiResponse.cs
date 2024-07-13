namespace Infra.Profiles.Responses;
    
public class ApiResponse
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public object? Response { get; set; }
}
