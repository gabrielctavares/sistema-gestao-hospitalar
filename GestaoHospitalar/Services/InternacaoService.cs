using Dapper;
using GestaoHospitalar.Models;
using MySqlConnector;

namespace GestaoHospitalar.Services;

public interface IInternacaoService
{
    Task<IEnumerable<Internacao>> GetInternacoesAsync();
    Task<Internacao?> GetInternacaoByIdAsync(int idInternacao);
    Task<bool> CreateInternacaoAsync(Internacao internacao);
    Task<bool> UpdateInternacaoAsync(Internacao internacao);
    Task<bool> DeleteInternacaoAsync(int idInternacao);

}
public class InternacaoService(MySqlConnection connection) : IInternacaoService
{
    public async Task<IEnumerable<Internacao>> GetInternacoesAsync()
    {
        var sql = @"SELECT 
                        i.*, p.nome as nome_paciente, f.nome as nome_responsavel 
                    FROM internacao i
                    left join paciente p on p.CPF = i.id_paciente
                    left join funcionario f on f.id_funcionario = i.id_responsavel;";

        await connection.OpenAsync();
        var internacoes = await connection.QueryAsync<Internacao>(sql);
        await connection.CloseAsync();
        return internacoes;
    }

    public async Task<Internacao?> GetInternacaoByIdAsync(int idInternacao)
    {
        var sql = @"SELECT 
                        i.*, p.nome as nome_paciente, f.nome as nome_responsavel 
                    FROM internacao i
                    left join paciente p on p.CPF = i.id_paciente
                    left join funcionario f on f.id_funcionario = i.id_responsavel
                    WHERE id_internacao = @IdInternacao;";

        await connection.OpenAsync();
        var internacao = await connection.QuerySingleOrDefaultAsync<Internacao>(sql, new { IdInternacao = idInternacao });
        await connection.CloseAsync();
        return internacao;
    }

    public async Task<bool> CreateInternacaoAsync(Internacao internacao)
    {
        var sql = @"INSERT INTO internacao 
                        (id_paciente, id_quarto, data_entrada, data_saida, id_responsavel, tratamento)
                        VALUES 
                        (@IdPaciente, @IdQuarto, @DataEntrada, @DataSaida, @IdResponsavel, @Tratamento);";
        await connection.OpenAsync();
        var result = await connection.ExecuteAsync(sql, internacao);
        await connection.CloseAsync();
        return result > 0;
    }

    public async Task<bool> UpdateInternacaoAsync(Internacao internacao)
    {
        var sql = @"UPDATE internacao SET 
                        id_paciente = @IdPaciente,
                        id_quarto = @IdQuarto,
                        data_entrada = @DataEntrada,
                        data_saida = @DataSaida,
                        id_responsavel = @IdResponsavel,
                        tratamento = @Tratamento
                        WHERE id_internacao = @IdInternacao;";
        await connection.OpenAsync();
        var result = await connection.ExecuteAsync(sql, internacao);
        await connection.CloseAsync();
        return result > 0;
    }

    public async Task<bool> DeleteInternacaoAsync(int idInternacao)
    {
        var sql = "DELETE FROM internacao WHERE id_internacao = @IdInternacao;";
        await connection.OpenAsync();
        var result = await connection.ExecuteAsync(sql, new { IdInternacao = idInternacao });
        await connection.CloseAsync();
        return result > 0;
    }
}
