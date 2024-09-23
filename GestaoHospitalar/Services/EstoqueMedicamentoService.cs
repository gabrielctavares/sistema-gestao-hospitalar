using Dapper;
using GestaoHospitalar.Models;
using MySqlConnector;

namespace GestaoHospitalar.Services;

public interface IEstoqueMedicamentoService
{
    Task<IEnumerable<EstoqueMedicamentosAbaixoMinimo>> GetMedicamentosAbaixoMinimoAsync();
}
public class EstoqueMedicamentoService(MySqlConnection connection) : IEstoqueMedicamentoService
{
    public async Task<IEnumerable<EstoqueMedicamentosAbaixoMinimo>> GetMedicamentosAbaixoMinimoAsync()
    {
        var sql = "SELECT * FROM estoque_medicamentos_abaixo_minimo;";
        await connection.OpenAsync();
        var estoqueAbaixoMinimo = await connection.QueryAsync<EstoqueMedicamentosAbaixoMinimo>(sql);
        await connection.CloseAsync();
        return estoqueAbaixoMinimo;
    }
}


