using System.Threading.Tasks;
using Orleans;

namespace OrleansGrpcRepro
{
    public class TestGrain : Grain, ITestGrain
    {
        public Task<Event1> DoSomething(Event1 input)
        {
            return Task.FromResult(new Event1()
            {
                Quantity = 100
            });
        }
    }
}