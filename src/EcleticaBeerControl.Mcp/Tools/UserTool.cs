using EcleticaBeerControl.Domain.Models;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace EcleticaBeerControl.Mcp.Tools
{
    [McpServerToolType]
    public class UserTool
    {
        [McpServerTool, Description("Retorna todos os usuários da aplicação")]
        public IList<User> GetUsers()
        {
            return new List<User>() {
                new User()
                {
                    Email = "victorolivera.dev"
                }
            };
        }

        [McpServerTool, Description("Retorna usuários da aplicação pelo nome")]
        public IList<User> GetUsersByName([Description("Nome do usuário")] string name)
        {
            return new List<User>() {
                new User()
                {
                    Email = name
                }
            };
        }
    }
}
