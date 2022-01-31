using System.Threading.Tasks;

namespace esr_core
{
    public interface IESRObserver
    {
         Task OnNotify(string str);         
    }
}