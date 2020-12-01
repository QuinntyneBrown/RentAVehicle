namespace GoalSetter.Core.DomainEvents
{
    public class ClientRemoved
    {
        public ClientRemoved(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
