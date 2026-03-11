namespace MassarAgency.Domain.Entities;

public class DeductionPolicy
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal DeductionPerAbsenceDay { get; set; }
    public decimal DeductionPerLateDay { get; set; }
    public decimal? MaxMonthlyDeduction { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
