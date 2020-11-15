
namespace HBSIS.Padawan.Produtos.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool Deletado { get; set; } 
    }
}
