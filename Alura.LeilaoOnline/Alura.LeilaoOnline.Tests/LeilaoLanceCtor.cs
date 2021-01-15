using Alura.LeilaoOnline.Core;
using System;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoLanceCtor
    {
        [Fact]
        public void BloquearValorNegativo()
        {
            var valorNegativo = -100;

            Assert.Throws<ArgumentException>(
                () => new Lance(null, valorNegativo)
            );
        }
    }
}
