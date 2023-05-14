using System.ComponentModel.DataAnnotations;

namespace AcmePublishing.Data;

public class Address
{
    public int Id { get; set; }
    [MaxLength(200)]
    public string AddressLine1 { get; set; }
    [MaxLength(200)]
    public string? AddressLine2 { get; set; }
    [MaxLength(50)]
    public string Region { get; set;}
    [MaxLength(12)]
    public string PostCode { get; set;}
    [MaxLength(2)]
    public string CountryCode { get; set; }
}