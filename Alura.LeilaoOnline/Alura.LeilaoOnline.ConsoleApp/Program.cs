using System;
using Alura.LeilaoOnline.Core;

namespace Alura.LeilaoOnline.ConsoleApp
{
	class Program
	{
		private static void Verificar(double vlrEsperado, double vlrObtido)
		{
			var cor = Console.ForegroundColor;
			if (vlrEsperado == vlrObtido)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("TESTE OK");
			}
			else {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"TESTE NÃO PASSOU! ESPERADO: {vlrEsperado} / OBTIDO: {vlrObtido}");
			}

			Console.ForegroundColor = cor;
		}

		private static void LeilaoComUnicoLance()
		{
			var modalidade = new MaiorValor();
			var leilao = new Leilao("Van Gogh", modalidade);

			var cliente1 = new Interessada("Fulano", leilao);

			leilao.RecebeLance(cliente1, 800.00);

			leilao.TerminaPregao();

			var valorEsperado = 800;
			var valorObtido = leilao.Ganhador.Valor;

			Verificar(valorEsperado, valorObtido);
		}

		private static void LeilaoComMultiplosLances()
		{
			var modalidade = new MaiorValor();
			var leilao = new Leilao("Van Gogh", modalidade);

			var cliente1 = new Interessada("Fulano", leilao);
			var cliente2 = new Interessada("Maria", leilao);

			leilao.RecebeLance(cliente1, 800.00);
			leilao.RecebeLance(cliente2, 900.00);
			leilao.RecebeLance(cliente1, 1000.00);
			leilao.RecebeLance(cliente2, 990.00);

			leilao.TerminaPregao();

			var valorEsperado = 1000.00;
			var valorObtido = leilao.Ganhador.Valor;

			Verificar(valorEsperado, valorObtido);

		}

		static void Main()
		{
			LeilaoComMultiplosLances();
			LeilaoComUnicoLance();
		}
	}
}
