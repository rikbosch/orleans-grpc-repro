using System.Threading.Tasks;
using Orleans;

namespace OrleansGrpcRepro
{
    public interface ITestGrain : IGrainWithStringKey
    {
        Task<Event1> DoSomething(Event1 input);
    }
}