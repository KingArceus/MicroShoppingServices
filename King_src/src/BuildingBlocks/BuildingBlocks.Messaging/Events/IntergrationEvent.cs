namespace BuildingBlocks.Messaging.Events
{
    public record IntergrationEvent
    {
        public Guid Id => Guid.NewGuid();
        public DateTime OccuredOn => DateTime.Now;
        public string EnventType => GetType().AssemblyQualifiedName;
    }
}
