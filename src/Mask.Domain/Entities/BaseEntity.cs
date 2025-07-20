using FluentValidation.Results;

namespace Mask.Domain.Entities
{
    public abstract class BaseEntity : AudiedEntity<Guid, string>
    {
    }

    public abstract class AudiedEntity<TPrimaryKey, TForeignKey> : IEntity<TPrimaryKey, TForeignKey>
        where TPrimaryKey : IComparable, new()
        where TForeignKey : IComparable, new()
    {
        public TPrimaryKey Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public TForeignKey CreatedBy { get; set; }
        public virtual DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
        public TForeignKey ModifiedBy { get; set; }

        public virtual ValidationResult Validator<IEntity>(IEntity instance)
        {
            throw new NotImplementedException();
        }
    }

    public interface IEntity<TPrimaryKey, TForeignKey>
        where TPrimaryKey : IComparable, new()
        where TForeignKey : IComparable, new()
    {
        public TPrimaryKey Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public TForeignKey CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public TForeignKey ModifiedBy { get; set; }

        public ValidationResult Validator<IEntity>(IEntity instance);
    }
}
