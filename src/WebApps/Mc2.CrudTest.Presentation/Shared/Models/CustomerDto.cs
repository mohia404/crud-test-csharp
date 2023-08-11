using System.ComponentModel.DataAnnotations;

namespace Mc2.CrudTest.Presentation.Shared.Models;

public class CustomerDto
{
    [Required]
    public string Firstname { get; set; } = null!;

    [Required]
    public string Lastname { get; set; } = null!;

    [Required]
    [Display(Name = "Date of birth")]
    public DateTime DateOfBirth { get; set; }

    [Required]
    [Display(Name = "Phone number")]
    public string PhoneNumber { get; set; } = null!;

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Required]
    [Display(Name = "Bank account number")]
    [MinLength(24)]
    [MaxLength(24)]
    public string BankAccountNumber { get; set; } = null!;
}