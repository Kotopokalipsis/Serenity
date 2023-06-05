using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class MedicalCard : Entity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; }
    
    /// <summary>
    /// Ключ пользователя
    /// </summary>
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Название карточки
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Записи
    /// </summary>
    public List<Record> Records { get; set; }
}