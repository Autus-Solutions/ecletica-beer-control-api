using EcleticaBeerControl.Domain.Interfaces;
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
        public Content GetUsers()
        {
            var list = new List<User>() {
                new User()
                {
                    Email = "victorolivera.dev"
                }
            };

            return new Content
            {
                Text = JsonSerializer.Serialize(list),
                MimeType = "application/json",
                Type = "text"
            };
        }


        [McpServerTool, Description("Retorna usuários da aplicação pelo nome")]
        public Content GetUsersByName([Description("Nome do usuário")] string name)
        {
            var user = new List<User>() {
                new User()
                {
                    Email = name
                }
            };

            return new Content
            {
                Text = JsonSerializer.Serialize(user),
                MimeType = "application/json",
                Type = "text"
            };
        }
    }
}
