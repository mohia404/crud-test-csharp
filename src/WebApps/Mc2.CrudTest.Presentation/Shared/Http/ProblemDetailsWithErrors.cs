namespace Mc2.CrudTest.Presentation.Shared.Http;

public class ProblemDetailsWithErrors
{
    public string Type { get; set; } = null!;
    public string Title { get; set; } = null!;
    public int Status { get; set; }
    public string TraceId { get; set; } = null!;
    public string[] ErrorCodes { get; set; } = null!;
}