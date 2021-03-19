using System.Diagnostics;
using System.Threading.Tasks;
using Orleans;

namespace OrleansGrpcRepro
{
    public class OutgoingGrainCallFilter : IOutgoingGrainCallFilter
    {
        public Task Invoke(IOutgoingGrainCallContext context)
        {
            Debug.Assert(context.InterfaceMethod != null,"Expected interface method to not be null");

            return context.Invoke();
        }
    }
}