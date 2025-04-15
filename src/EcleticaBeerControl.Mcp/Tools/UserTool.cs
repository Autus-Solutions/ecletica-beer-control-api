using EcleticaBeerControl.Domain.Models;
using ModelContextProtocol.Protocol.Types;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;

namespace EcleticaBeerControl.Mcp.Tools
{
    [McpServerToolType]
    public class UserTool
    {
        [McpServerTool, Description("Retorna uma lista de usários da aplicação")]
        public string GetUsers()
        {
            return "Acessando a lista...";
        }


        [McpServerTool, Description("Retorna usuários da aplicação pelo nome")]
        public string GetUsersByName([Description("Nome do usuário")] string name)
        {
            return name;
        }
    }
}
