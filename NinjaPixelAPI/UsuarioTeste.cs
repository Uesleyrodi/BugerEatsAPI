using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;

namespace NinjaPixelAPI
{
    [TestClass]
    public class Usuario
    {
        [TestMethod]
        public void AutenticacaoTest()
        {
            // Entrada de Dados
            string baseUrl = "http://localhost:3333";
            string rotaUsuario = "/users";
            string full_name = "Lucas";
            string email = "lucas@ninjapixel.com";
            string password = "pwd123";

            //Representação da API
            RestClient client = new RestClient(baseUrl);
            client.AddDefaultHeader("Content-Type", "application/json");

            //Representação do Endpoint
            RestRequest request = new RestRequest(rotaUsuario, Method.Post);
            request.AddJsonBody(new
            {
                full_name,
                email,
                password
            });

            //Executar e Persistir a resposta da API
            //Representação do Response
            RestResponse response = client.Execute(request);

            Console.WriteLine(response.Content);

            Assert.IsTrue(response.Content.Contains(full_name));
            Assert.IsTrue(response.Content.Contains(email));
        }
    }
}