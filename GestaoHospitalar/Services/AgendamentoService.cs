using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using GestaoHospitalar.Models;
using MySqlConnector;

namespace GestaoHospitalar.Services;

public interface IAgendamentoService
{
    Task<IEnumerable<Agendamento>> GetAgendamentosAsync();
    Task<Agendamento?> GetAgendamentoByIdAsync(int idAgendamento);
    Task<bool> CreateAgendamentoAsync(Agendamento agendamento);
    Task<bool> UpdateAgendamentoAsync(Agendamento agendamento);
    Task<bool> CancelAgendamentoAsync(int idAgendamento);

}
public class AgendamentoService(MySqlConnection connection) : IAgendamentoService
{
    public async Task<IEnumerable<Agendamento>> GetAgendamentosAsync()
    {
        var sql = @"SELECT 
                    a.id_agendamento AS IdAgendamento, 
                    a.id_paciente AS IdPaciente, 
                    a.id_secretario AS IdSecretario, 
                    a.id_medico AS IdMedico, 
                    a.data_agendamento AS DataAgendamento, 
                    a.status,
                    p.nome as NomePaciente, 
                    s.nome as NomeSecretario, 
                    m.nome as NomeMedico 
                FROM agendamento a 
                LEFT JOIN paciente p ON p.CPF = a.id_paciente
                LEFT JOIN funcionario s ON s.id_funcionario = a.id_secretario
                LEFT JOIN funcionario m ON m.id_funcionario = a.id_medico;";

        await connection.OpenAsync();
        var agendamentos = await connection.QueryAsync<Agendamento>(sql);
        await connection.CloseAsync();
        return agendamentos;
    }

    public async Task<Agendamento?> GetAgendamentoByIdAsync(int idAgendamento)
    {
        var sql = $@"SELECT 
                    a.id_agendamento AS IdAgendamento, 
                    a.id_paciente AS IdPaciente, 
                    a.id_secretario AS IdSecretario, 
                    a.id_medico AS IdMedico, 
                    a.data_agendamento AS DataAgendamento, 
                    a.status,
                    p.nome as NomePaciente, 
                    s.nome as NomeSecretario, 
                    m.nome as NomeMedico 
                FROM agendamento a 
                LEFT JOIN paciente p ON p.CPF = a.id_paciente
                LEFT JOIN funcionario s ON s.id_funcionario = a.id_secretario
                LEFT JOIN funcionario m ON m.id_funcionario = a.id_medico
                WHERE id_agendamento = {idAgendamento};";
        
        await connection.OpenAsync();
        var agendamento = await connection.QuerySingleOrDefaultAsync<Agendamento>(sql);
        await connection.CloseAsync();
        return agendamento;
    }

    public async Task<bool> CreateAgendamentoAsync(Agendamento agendamento)
    {
        var sqlId = @"SELECT MAX(id_agendamento) FROM agendamento;";

        var sql = $@"INSERT INTO agendamento 
                        (id_agendamento, id_paciente, id_secretario, id_medico, data_agendamento, status)
                        VALUES 
                        (@IdAgendamento, @IdPaciente, @IdSecretario, @IdMedico, @DataAgendamento, @Status);";

        await connection.OpenAsync();
        var id = await connection.ExecuteScalarAsync<int>(sqlId) + 1;
        agendamento.IdAgendamento = id;
        var result = await connection.ExecuteAsync(sql, agendamento);
        await connection.CloseAsync();
        return result > 0;
    }

    public async Task<bool> UpdateAgendamentoAsync(Agendamento agendamento)
    {
        var sql = $@"UPDATE agendamento SET 
                        id_paciente = '{agendamento.IdPaciente}',
                        id_secretario = '{agendamento.IdSecretario}',
                        id_medico = '{agendamento.IdMedico}',
                        data_agendamento = @DataAgendamento,
                        status = '{agendamento.Status}'
                        WHERE id_agendamento = '{agendamento.IdAgendamento}';";
        await connection.OpenAsync();
        var result = await connection.ExecuteAsync(sql, new { agendamento.DataAgendamento });
        await connection.CloseAsync();
        return result > 0;
    }

    public async Task<bool> CancelAgendamentoAsync(int idAgendamento)
    {
        var sql = "UPDATE agendamento SET status = 'cancelado' WHERE id_agendamento = @IdAgendamento;";
        await connection.OpenAsync();
        var result = await connection.ExecuteAsync(sql, new { IdAgendamento = idAgendamento });
        await connection.CloseAsync();
        return result > 0;
    }
}
