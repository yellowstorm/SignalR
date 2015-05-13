using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC4_SignalR_UsoCpuRealTime.Hubs
{
    [HubName("cpuHub")]
    public class CpuHub : Hub
    {
        private readonly ControleCpu _controleCpu;

        public CpuHub() : this(ControleCpu.Instancia) { }

        public CpuHub(ControleCpu controle)
        {
            _controleCpu = controle;
        }

        /// <summary>
        /// Inicia a transmissão do uso da CPU do servidor
        /// </summary>
        public void IniciaTransmissaoUsoCpu()
        {
            _controleCpu.RealizaTransmissaoUsoCPU();
        }

        public void AtualizaUso(float usoCPU)
        {
            Clients.All.atualizaUsoCPU(usoCPU);
        }
    }
}