using System;
using Alura.LeilaoOnline.Core;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
	public class LeilaoTerminaPregao
	{
		[Theory]
		[InlineData( 1200, 1250, new double[] { 800, 1150, 1400, 1250 })]
		public void RetornaValorMaisProximoEsperado(double vlrDestino, double vlrEsperado, double[] ofertas)
        {
			IModalidadeAvaliacao modalidade = new OfertaSuperiorMaisProxima(vlrDestino);
			var leilao = new Leilao("Van Gogh", modalidade);

			var fulano = new Interessada("Fulano", leilao);
			var maria = new Interessada("Maria", leilao);
			leilao.IniciaPregao();

			for (int i = 0; i < ofertas.Length; i++)
			{
				var valor = ofertas[i];
				if (i % 2 == 0)
				{
					leilao.RecebeLance(fulano, valor);
				}
				else
				{
					leilao.RecebeLance(maria, valor);
				}
			}

			leilao.TerminaPregao();

			var valorObtido = leilao.Ganhador.Valor;
			Assert.Equal(vlrEsperado, valorObtido);

		}

		[Theory]
		[InlineData( new double[] { 800, 900, 1000, 990 }, 1000 )]
		[InlineData(new double[] { 800, 900, 1000, 1200 }, 1200)]
		[InlineData( new double[] { 800 }, 800)]
		public void RetornaMaiorValorEmLeilaoComPeloMenosUmLance(double[] ofertas, double vlrEsperado)
		{
			var modalidade = new MaiorValor();
			var leilao = new Leilao("Van Gogh", modalidade);
			var fulano = new Interessada("Fulano", leilao);
			var maria = new Interessada("Maria", leilao);
			leilao.IniciaPregao();

			for (int i = 0; i < ofertas.Length; i++)
			{
				var valor = ofertas[i];
				if (i % 2 == 0)
				{
					leilao.RecebeLance(fulano, valor);
				}
				else
				{
					leilao.RecebeLance(maria, valor);
				}
			}

			leilao.TerminaPregao();

			var valorObtido = leilao.Ganhador.Valor;
			Assert.Equal(vlrEsperado, valorObtido);

		}

		[Fact]
		public void ExceptionCasoPregaoNaoIniciado()
        {
			var modalidade = new MaiorValor();
			var leilao = new Leilao("Van Gogh", modalidade);

			var e = Assert.Throws<InvalidOperationException>(
				() => leilao.TerminaPregao()
			);

			var msgEsperada = "Não foi possível finalizar o pregão pois o mesmo ainda não foi iniciado";
			Assert.Equal(msgEsperada, e.Message);
		}

		[Fact]
		public void RetornaZeroEmLeilaoSemLances()
		{
			var modalidade = new MaiorValor();
			var leilao = new Leilao("Van Gogh", modalidade);

			leilao.IniciaPregao();
			leilao.TerminaPregao();

			var valorEsperado = 0;
			var valorObtido = leilao.Ganhador.Valor;

			Assert.Equal(valorEsperado, valorObtido);
		}
	}
}
