using EcleticaBeerControl.Domain.Models;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;


namespace EcleticaBeerControl.Mcp.Tools
{
    [McpServerToolType]
    public static class UserTool
    {
        [McpServerTool, Description("Retorna uma lista de usários da aplicação")]
        public static string GetUsers()
        {
            return "Acessando a lista...";
        }


        [McpServerTool, Description("Retorna usuários da aplicação pelo nome")]
        public static string GetUsersByName([Description("Nome do usuário")] string name)
        {
            return name;
        }
    }
}
