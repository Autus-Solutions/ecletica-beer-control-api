using EcleticaBeerControl.Domain.Models;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace EcleticaBeerControl.Mcp.Tools
{
    [McpServerToolType]
    public class UserTool
    {
        [McpServerTool(Name = "ListaUsuarios", Title = "Listagem de Usuários"), Description("Retorna uma lista de todos os usuários")]
        public IList<User> GetUsers(IMcpServer thisServer)
        {
            return new List<User>() {
                new User()
                {
                    Email = "victorolivera.dev"
                }
            };
        }

        [McpServerTool(Name = "ListaUsuarioPorNome", Title = "Listagem de Usuários pelo nome"), Description("Retorna usuários pelo nome")]
        public IList<User> GetUsersByName(IMcpServer thisServer, [Description("Nome do usuário")] string username)
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
