using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class ServiceCategory : Entity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; }
    
    /// <summary>
    /// Название категории
    /// </summary>
    public string Name { get; set; }
}