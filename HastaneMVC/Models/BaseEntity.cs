using System.ComponentModel.DataAnnotations;

namespace HastaneMVC.Models
{
    public abstract class BaseEntity // abstract classından öğe oluşutma sadece miras verdir bu soyut bir şey böyle bir şey yok bundan bir öğe oluşturamayız 
    {
        [Key]
        public int Id { get; set; }
        public Boolean IsDeleted { get; set; }=false;

        public DateTime CreateTime { get; private set; } = DateTime.Now;

        public DateTime? DeleteTime { get; set; }


    }
}






