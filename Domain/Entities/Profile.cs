using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Profile : Entity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; }
    
    /// <summary>
    /// Ключ пользователя (требуется для корректного связывания в EfCore Identity)
    /// </summary>
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Пользователь
    /// </summary>
    public virtual User User { get; set; }
    
    /// <summary>
    /// Имя
    /// </summary>
    public string Firstname { get; set; }
    
    /// <summary>
    /// Отчество
    /// </summary>
    public string Middlename { get; set; }
    
    /// <summary>
    /// Фамилия
    /// </summary>
    public string Lastname { get; set; }
    
    /// <summary>
    /// Страна
    /// </summary>
    public string Country { get; set; }
}