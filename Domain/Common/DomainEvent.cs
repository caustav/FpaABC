using Newtonsoft.Json;

namespace Domain.Common
{
    public abstract class DomainEvent
    {
        public string Name {get; init;}
        public DomainEvent()
        {
            this.Name = this.GetType().Name;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}