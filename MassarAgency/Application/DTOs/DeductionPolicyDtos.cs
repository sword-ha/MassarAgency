using System.ComponentModel.DataAnnotations;

namespace MassarAgency.Application.DTOs;

public class DeductionPolicyDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal DeductionPerAbsenceDay { get; set; }
    public decimal DeductionPerLateDay { get; set; }
    public decimal? MaxMonthlyDeduction { get; set; }
    public bool IsActive { get; set; }
}

public class UpdateDeductionPolicyDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Policy name is required.")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Deduction per absence day is required.")]
    [Range(0, 100000)]
    [Display(Name = "Deduction per Absence Day (EGP)")]
    public decimal DeductionPerAbsenceDay { get; set; }

    [Required(ErrorMessage = "Deduction per late day is required.")]
    [Range(0, 100000)]
    [Display(Name = "Deduction per Late Day (EGP)")]
    public decimal DeductionPerLateDay { get; set; }

    [Range(0, 1000000)]
    [Display(Name = "Max Monthly Deduction (EGP)")]
    public decimal? MaxMonthlyDeduction { get; set; }

    [Display(Name = "Active")]
    public bool IsActive { get; set; } = true;
}
