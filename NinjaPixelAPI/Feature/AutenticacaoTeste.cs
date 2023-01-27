using Microsoft.VisualStudio.TestTools.UnitTesting;
using NinjaPixelAPI.APIs;

namespace NinjaPixelAPI.Feature
{
    [TestClass]
    public class Autenticacao
    {
        [TestMethod]
        public void AutenticacaoTokenTest()
        {
            ApiAutenticacao.GetToken();
        }
    }
}