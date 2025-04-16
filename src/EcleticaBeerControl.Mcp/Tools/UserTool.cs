using EcleticaBeerControl.Domain.Models;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace EcleticaBeerControl.Mcp.Tools
{
    [McpServerToolType]
    public class UserTool
    {
        [McpServerTool, Description("Retorna uma lista de todos os usuários")]
        public IList<User> GetUsers()
        {
            return new List<User>() {
                new User()
                {
                    Email = "victorolivera.dev"
                }
            };
        }

        [McpServerTool, Description("Retorna usuários pelo nome")]
        public IList<User> GetUsersByName([Description("Nome do usuário")] string username)
        {
            return new List<User>() {
                new User()
                {
                    UserName = username,
                    Email = $"{username}@teste.com"
                }
            };
        }
    }
}
