namespace PS6.ViewModels;

public class LeapYearResultVM
{
    public int Id { get; set; }
    public int Year { get; set; }
    public string Result { get; set; }
    public DateTime TimeAdded { get; set; }
    public string? UserLogin { get; set; }
    public string? UserId { get; set; }
}
