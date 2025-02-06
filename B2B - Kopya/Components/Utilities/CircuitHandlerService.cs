using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.Circuits;
using System.Collections.Concurrent;

namespace B2B.Components.Utilities
{
    public class CircuitHandlerService : CircuitHandler
    {
        
        public event EventHandler CircuitsChanged;

        protected virtual void OnCircuitsChanged()
             => CircuitsChanged?.Invoke(this, EventArgs.Empty);
        public ConcurrentDictionary<string, Circuit> Circuits
        {
            get;
            set;
        }
        public CircuitHandlerService()
        {
            Circuits = new ConcurrentDictionary<string, Circuit>();
        }
        public override Task OnConnectionUpAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            return base.OnConnectionUpAsync(circuit, cancellationToken);
        }

        public override Task OnConnectionDownAsync(Circuit circuit,
                            CancellationToken cancellationToken)
        {
            return base.OnConnectionDownAsync(circuit,
                             cancellationToken);
        }

        public override Task OnCircuitOpenedAsync(Circuit circuit,
                             CancellationToken cancellationToken)
        {
            Circuits[circuit.Id] = circuit;
            OnCircuitsChanged();
            return base.OnCircuitOpenedAsync(circuit,
                                  cancellationToken);
        }

        public override Task OnCircuitClosedAsync(Circuit circuit,
                 CancellationToken cancellationToken)
        {
            Circuit circuitRemoved;
            Circuits.TryRemove(circuit.Id, out circuitRemoved);
            OnCircuitsChanged();
            return base.OnCircuitClosedAsync(circuit,
                              cancellationToken);
        }
    }
}
