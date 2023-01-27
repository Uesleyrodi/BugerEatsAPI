using Microsoft.VisualStudio.TestTools.UnitTesting;
using NinjaPixelAPI.APIs;

namespace NinjaPixelAPI.Feature
{
    [TestClass]
    public class AutenticacaoToken
    {
        [TestMethod]
        public void AutenticacaoComSucessoTest()
        {
            ApiCredentials.GetToken();
        }
    }
}