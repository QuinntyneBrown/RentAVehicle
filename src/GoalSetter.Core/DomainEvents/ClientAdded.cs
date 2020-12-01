namespace GoalSetter.Core.DomainEvents
{
    public class ClientAdded
    {
        public ClientAdded(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
