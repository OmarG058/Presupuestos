using Dapper;
using Microsoft.Data.SqlClient;
using Presupuestos.Models;

namespace Presupuestos.Servicios
{
    public interface IRepositorioTiposCuentas
    {
        Task Crear(TipoCuenta tipoCuenta);
        Task<bool> Existe(string nombre, int usuarioId);
        Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId);
    }
    public class RepositorioTiposCuentas:IRepositorioTiposCuentas
    {
        private readonly string? connectionString;
        public RepositorioTiposCuentas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(TipoCuenta tipoCuenta) 
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>($@"insert into TiposCuentas (Nombre,UsuarioId,Orden) values (@Nombre,@UsuarioId,0);
                                                                                                              Select SCOPE_IDENTITY();",tipoCuenta);
            tipoCuenta.Id = id;
        }

        public async Task<bool> Existe(string nombre, int usuarioId) 
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(@$"select 1
                                                                    From TiposCuentas
                                                                    where Nombre = @Nombre AND UsuarioId =@UsuarioId;",new { nombre , usuarioId});
            return existe == 1;
        }

        public async Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId) 
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<TipoCuenta>(@$"select Id,Nombre,Orden from TiposCuentas where UsuarioId = @UsuarioId;", new { usuarioId});
        }
    }
}
