using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;

namespace NinjaPixelAPI
{
    [TestClass]
    public class Usuario
    {
        string baseUrl = "http://localhost:3333";
        string rotaUsuario = "/users";
        string rotaAutenticacao = "/auth";

        // Entrada de Dados
        string full_name = "Lucas";
        string email = "lucas@ninjapixel.com";
        string password = "pwd123";

        //Entrada de Dados
        string emailAutenticacao = "uesley@ninjapixel.com";
        string passwordAutenticacao = "pwd123";

        RestClient client;
        RestRequest request;
        RestResponse response;

        [TestMethod]
        public void NovoUsuarioTest()
        {

            //Representação do Endpoint
            request = new RestRequest(rotaUsuario, Method.Post);
            request.AddJsonBody(new
            {
                full_name,
                email,
                password
            });

            //Executar e Persistir a resposta da API
            //Representação do Response
            response = client.Execute(request);

            Console.WriteLine(response.Content);

            Assert.IsTrue(response.Content.Contains(full_name), "Verificar se existe o e-mail do novo usuário no response");
            Assert.IsTrue(response.Content.Contains(email), "Verificar se existe e-mail do novo usuário no response");
        }

        [TestMethod]
        public void Autenticacao()
        {
            GetToken();
        }

        public string GetToken()
        {

            //Representação do Enpoint
            request = new RestRequest(rotaAutenticacao, Method.Post);
            request.AddJsonBody(new
            {
                emailAutenticacao,
                passwordAutenticacao
            });

            response = client.Execute(request);

            Console.WriteLine(response.Content);

            Assert.IsTrue(response.Content.Contains("token"), "Verificar se existe a palavra token no retorno do response");

            //Neste momento estou selecionando o token no retorno do response
            string token = JObject.Parse(response.Content).SelectToken("token").ToString();

            Console.WriteLine(token);

            return token;
        }

        [TestInitialize]
        public void MyTestInitialize()
        {
            //Representação da API
            client = new RestClient(baseUrl);
            client.AddDefaultHeader("Content-Type", "application/json");
        }
    }
}