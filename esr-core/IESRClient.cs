using System.Threading.Tasks;
 
namespace esr_core
{
    public interface IESRClient
    {
         Task Publish<T>(T t, string eventTag);
         IEnumerable<string> ReadEvents(string eventTag);
    }
}