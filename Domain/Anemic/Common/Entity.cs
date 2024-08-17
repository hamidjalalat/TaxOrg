namespace Domain.Anemic.Common
{
    public class Entity : BaseEntity, ISoftDeletable
    {
        public bool IsDeleted { get ; set ; }
    }
}
