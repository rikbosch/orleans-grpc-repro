using System.Diagnostics;
using System.Threading.Tasks;
using Orleans;

namespace OrleansGrpcRepro
{
    public class IncomingGrainCallFilter : IIncomingGrainCallFilter
    {
        public Task Invoke(IIncomingGrainCallContext context)
        {
            Debug.Assert(context.InterfaceMethod != null,"Expected interface method to not be null");
            Debug.Assert(context.ImplementationMethod != null,"Expected implementation method to not be null");
            return context.Invoke();
        }

    }
}