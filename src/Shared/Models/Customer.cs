using System.ComponentModel.DataAnnotations;

namespace Client.Models;

public class Customer
{

     [Required]
     public string TypeCustomer { get; set; }
     public string Name { get; set; }
     public string Type { get; set; }
     public string Education { get; set; }
     public string Department { get; set; }
     public string Institude { get; set; }

     [Required]
     public string Firstname { get; set; }
     [Required]
     public string Lastname { get; set; }
     [Required]
     public string Phonenumber { get; set; }
     [Required]
     public string Email { get; set; }

     public string BackupFirstname { get; set; }
     public string BackupLastname { get; set; }
     public string BackupPhonenumber { get; set; }
     public string BackupEmail { get; set; }
}

