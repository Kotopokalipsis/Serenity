using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Tag : Entity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; }
    
    /// <summary>
    /// Тег
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Ключ пользователя
    /// </summary>
    [ForeignKey("User")]
    public Guid UserId { get; set; }
}