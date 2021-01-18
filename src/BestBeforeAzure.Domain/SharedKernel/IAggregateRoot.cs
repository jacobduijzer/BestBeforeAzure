using System;
namespace BestBeforeAzure.Domain.SharedKernel
{
    public interface IAggregateRoot
    {
        public Guid Id { get; }
    }
}
