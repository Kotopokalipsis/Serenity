using Domain.Entities;

namespace Domain.Models;

public record RecordModel
{
    /// <summary>
    /// Ссылка на карту
    /// </summary>
    public MedicalCardModel MedicalCard { get; set; }
    
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
    /// Тип услуги
    /// </summary>
    public ServiceType ServiceType { get; set; }
    
    /// <summary>
    /// Категория услуги
    /// </summary>
    public ServiceCategory ServiceCategory { get; set; }
    
    /// <summary>
    /// Теги
    /// </summary>
    public List<Tag> Tags { get; set; }
};