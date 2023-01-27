using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;

namespace NinjaPixelAPI.APIs
{
    public class ApiCredentials
    {
        //Endpoint
        static string baseUrl = "http://localhost:3333";

        //Rota
        static string rotaAutenticacao = "/auth";

        //Entrada de Dados
        static string email = "uesley@ninjapixel.com";
        static string password = "pwd123";

        static RestClient client;
        static RestRequest request;
        static RestResponse response;

        public static string GetToken()
        {
            // Inicializar client
            InitApiAutenticacao();

            //Representação do Enpoint
            request = new RestRequest(rotaAutenticacao, Method.Post);
            request.AddJsonBody(new
            {
                email,
                password
            });

            response = client.Execute(request);

            Console.WriteLine(response.Content);

            Assert.IsTrue(response.Content.Contains("token"), "Verificar se existe a palavra token no retorno do response");

            //Neste momento estou selecionando o token no retorno do response
            string token = JObject.Parse(response.Content).SelectToken("token").ToString();

            Console.WriteLine(token);

            return token;
        }
        public static void InitApiAutenticacao()
        {
            //Representação da API
            client = new RestClient(baseUrl);
            client.AddDefaultHeader("Content-Type", "application/json");
        }
    }
}