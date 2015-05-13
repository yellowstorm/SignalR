using System;
using System.Threading;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR;
using System.Diagnostics;

namespace MVC4_SignalR_UsoCpuRealTime.Hubs
{
    /// <summary>
    /// Controla a exibição e transmissão do uso de CPU para os clients
    /// </summary>
    public class ControleCpu
    {
        private readonly static Lazy<ControleCpu> _instancia = new Lazy<ControleCpu>(() => new ControleCpu());
        private const int _intervalo = 1000; // 1 segundo
        private PerformanceCounter _cpu = new PerformanceCounter();

        public static ControleCpu Instancia
        {
            get
            {
                return _instancia.Value;
            }
        }

        private ControleCpu() {}


        public void RealizaTransmissaoUsoCPU()
        {
            // Definição básica para obter o uso da CPU
            _cpu.CategoryName = "Processor";
            _cpu.CounterName = "% Processor Time";
            _cpu.InstanceName = "_Total";

            // Cria timer para transmitir as informações de 1 em 1 seg
            new Timer(TransmiteParaTodosClientes, null, _intervalo, _intervalo);
        }
        
        private void TransmiteParaTodosClientes(object state)
        {
            // Transmite a informação para todos os clientes, chamando a função atualizaUsoCPU
            //ObtemClientes<CpuHub>().All.AtualizaUso(_cpu.NextValue());
            GlobalHost.ConnectionManager.GetHubContext<CpuHub>().Clients.All.atualizaUsoCPU(_cpu.NextValue());
        }

        /// <summary>
        /// Obtém os clientes conectados
        /// </summary>
        /// <typeparam name="T">Tipo do Hub</typeparam>
        /// <returns></returns>
        private IHubConnectionContext<T> ObtemClientes<T>() where T : Hub
        {

            return (IHubConnectionContext < T > )GlobalHost.ConnectionManager.GetHubContext<T>().Clients;
        }
    }


}