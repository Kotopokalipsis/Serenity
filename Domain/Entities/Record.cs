using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Record : Entity
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; }
    
    /// <summary>
    /// Id связанной медицинской карты
    /// </summary>
    public long MedicalCardId { get; set; }
    
    /// <summary>
    /// Ссылка на карту
    /// </summary>
    public MedicalCard MedicalCard { get; set; }

    /// <summary>
    /// Название записи
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Дата посещения
    /// </summary>
    public DateTime VisitedAt { get; set; }
    
    /// <summary>
    /// Контент в карточке
    /// </summary>
    public string Content { get; set; }
    
    /// <summary>
    /// Id связанного типа услуги
    /// </summary>
    public long ServiceTypeId { get; set; }
    
    /// <summary>
    /// Тип услуги
    /// </summary>
    public ServiceType ServiceType { get; set; }
    
    /// <summary>
    /// Id связанной категории услуги
    /// </summary>
    public long ServiceCategoryId { get; set; }
    
    /// <summary>
    /// Категория услуги
    /// </summary>
    public ServiceCategory ServiceCategory { get; set; }
    
    /// <summary>
    /// Теги
    /// </summary>
    public List<Tag> Tags { get; set; }
    //
    // /// <summary>
    // /// Вложения
    // /// </summary>
    // public List<Attachment> Attachments { get; set; }
}