using System;

namespace GoalSetter.Domain.Features.Clients
{
    public class ClientDto
    {
        public Guid ClientId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
