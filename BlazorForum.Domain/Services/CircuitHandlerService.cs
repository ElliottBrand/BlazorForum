using Microsoft.AspNetCore.Components.Server.Circuits;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorForum.Domain.Services
{
    public class CircuitHandlerService : CircuitHandler
    {
        public ConcurrentDictionary<string, Circuit> Circuits { get; set; }
        public event EventHandler CircuitsChanged;

        protected virtual void OnCircuitChanged() => CircuitsChanged?.Invoke(this, EventArgs.Empty);

        public CircuitHandlerService()
        {
            Circuits = new ConcurrentDictionary<string, Circuit>();
        }

        public override Task OnCircuitOpenedAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            Circuits[circuit.Id] = circuit;
            OnCircuitChanged();
            return base.OnCircuitOpenedAsync(circuit, cancellationToken);
        }

        public override Task OnCircuitClosedAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            Circuit circuitRemoved;
            Circuits.TryRemove(circuit.Id, out circuitRemoved);
            OnCircuitChanged();
            return base.OnCircuitClosedAsync(circuit, cancellationToken);
        }

        public override Task OnConnectionUpAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            return base.OnConnectionUpAsync(circuit, cancellationToken);
        }

        public override Task OnConnectionDownAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            return base.OnConnectionDownAsync(circuit, cancellationToken);
        }
    }
}
