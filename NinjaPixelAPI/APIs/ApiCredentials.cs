using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.IO;

namespace NinjaPixelAPI.APIs
{
    public class ApiCredentials
    {
        static RestClient client;
        static RestRequest request;
        static RestResponse response;

        public static string GetToken()
        {
            string rotaAutenticacao = "", email = "", password = "";
            //Garantir que o arquivo ApiAutenticacaoConfig exista
            if (!File.Exists("Deploy//ApiAutenticacaoConfig.json"))
            {
                Assert.Fail("ApiAutenticacaoConfig.json não foi encontrado");
            }

            //Preciso tratar possíveis excessões vindas da leitura do arquivo, não basta somente verificar se o arquivo existe, preciso garantir que as variáveis também exista.
            try
            {
                //Entrada de Dados - Estamos consumindo as massa de dados do arquivo.
                rotaAutenticacao = JObject.Parse(File.ReadAllText("Deploy//ApiAutenticacaoConfig.json")).SelectToken("rotaAutenticacao").ToString();
                email = JObject.Parse(File.ReadAllText("Deploy//ApiAutenticacaoConfig.json")).SelectToken("email").ToString();
                password = JObject.Parse(File.ReadAllText("Deploy//ApiAutenticacaoConfig.json")).SelectToken("password").ToString();
            } 
            catch(Exception e)
            {
                Assert.Fail($"Não possível recuperar as informações do arquivo ApiAutenticacaoConfig.json {e.Message} {e.StackTrace}");
            }

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
            string baseUrl = "";
            //Preciso tratar possíveis excessões vindas da leitura do arquivo, não basta somente verificar se o arquivo existe, preciso garantir que as variáveis também exista.
            try
            {
                //Entrada de Dados "API"
                baseUrl = JObject.Parse(File.ReadAllText("Deploy//ApiAutenticacaoConfig.json")).SelectToken("baseUrl").ToString();
            }
            catch(Exception e)
            {
                Assert.Fail($"Não possível recuperar as informações do arquivo ApiAutenticacaoConfig.json {e.Message} {e.StackTrace}");
            }

            //Representação da API
            client = new RestClient(baseUrl);
            client.AddDefaultHeader("Content-Type", "application/json");
        }
    }
}