using Flunt.Notifications;

namespace Shop.Domain.Entities
{
    public abstract class Entity:Notifiable
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}
