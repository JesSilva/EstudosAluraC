using System.Linq;

namespace Alura.LeilaoOnline.Core
{
    public class OfertaSuperiorMaisProxima : IModalidadeAvaliacao
    {

        public double VlrDestino { get; }

        public OfertaSuperiorMaisProxima(double vlrDestino)
        {
            VlrDestino = vlrDestino;
        }

        public Lance Avaliar(Leilao leilao)
        {
            return leilao.Lances
                .DefaultIfEmpty(new Lance(null, 0))
                .Where(l => l.Valor > VlrDestino)
                .OrderBy(l => l.Valor)
                .FirstOrDefault(); ;
        }
    }
}
