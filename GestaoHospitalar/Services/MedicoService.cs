using Dapper;
using GestaoHospitalar.Models;
using MySqlConnector;

namespace GestaoHospitalar.Services;

public interface IMedicoService
{
    Task<IEnumerable<Medico>> GetMedicosAsync();
    Task<Medico?> GetMedicoByIdFuncionarioAsync(int id);
    Task<bool> CreateMedicoAsync(Medico medico);
    Task<bool> UpdateMedicoAsync(Medico medico);
    Task<bool> DeleteMedicoAsync(int id);

}
public class MedicoService(MySqlConnection connection) : IMedicoService
{
    public async Task<IEnumerable<Medico>> GetMedicosAsync()
    {
        var sql = @"SELECT f.id_funcionario, f.CPF, f.nome, f.endereco, f.telefone, f.email, 
                        m.crm, m.especialidade
                        FROM funcionario f
                        INNER JOIN medico m ON f.id_funcionario = m.id_funcionario;";
        await connection.OpenAsync();
        var medicos = await connection.QueryAsync<Medico>(sql);
        await connection.CloseAsync();
        return medicos;
    }

    public async Task<Medico?> GetMedicoByIdFuncionarioAsync(int id)
    {
        var sql = @"SELECT f.id_funcionario, f.CPF, f.nome, f.endereco, f.telefone, f.email, 
                        m.crm, m.especialidade
                        FROM funcionario f
                        INNER JOIN medico m ON f.id_funcionario = m.id_funcionario
                        WHERE id_funcionario = @IdFuncionario;";
        await connection.OpenAsync();
        var medico = await connection.QuerySingleOrDefaultAsync<Medico>(sql, new { IdFuncionario = id });
        await connection.CloseAsync();
        return medico;
    }

    public async Task<bool> CreateMedicoAsync(Medico medico)
    {
        // Last_Insert_Id() retorna o último id inserido, pois é autoIncrement
        var sqlFuncionario = @"INSERT INTO funcionario 
                                    (CPF, nome, endereco, telefone, email)
                                    VALUES 
                                    (@Cpf, @Nome, @Endereco, @Telefone, @Email);
                                    SELECT LAST_INSERT_ID();";

        var sqlMedico = @"INSERT INTO medico 
                              (id_funcionario, crm, especialidade)
                              VALUES 
                              (@IdFuncionario, @Crm, @Especialidade);";

        await connection.OpenAsync();

        // Fazendo uso de transação para previnir inconsistências
        using var transaction = await connection.BeginTransactionAsync();
        try
        {
            var idFuncionario = await connection.ExecuteScalarAsync<int>(
                sqlFuncionario,
                new { medico.Cpf, medico.Nome, medico.Endereco, medico.Telefone, medico.Email },
                transaction
            );

            var result = await connection.ExecuteAsync(
                sqlMedico,
                new { IdFuncionario = idFuncionario, medico.Crm, medico.Especialidade },
                transaction
            );

            await transaction.CommitAsync();
            await connection.CloseAsync();
            return result > 0;
        }
        catch
        {
            await transaction.RollbackAsync();
            await connection.CloseAsync();
            throw;
        }
    }

    public async Task<bool> UpdateMedicoAsync(Medico medico)
    {
        var sqlFuncionario = @"UPDATE funcionario SET 
                                    nome = @Nome,
                                    CPF = @Cpf,
                                    endereco = @Endereco,
                                    telefone = @Telefone,
                                    email = @Email
                                    WHERE id_funcionario = @IdFuncionario;";

        var sqlMedico = @"UPDATE medico SET 
                              crm = @Crm,
                              especialidade = @Especialidade
                              WHERE id_funcionario = @IdFuncionario;";

        await connection.OpenAsync();
        using var transaction = await connection.BeginTransactionAsync();
        try
        {     

            await connection.ExecuteAsync(
                sqlFuncionario,
                new { medico.Nome, medico.Cpf, medico.Endereco, medico.Telefone, medico.Email, medico.IdFuncionario },
                transaction
            );

            var result = await connection.ExecuteAsync(
                sqlMedico,
                new { medico.Crm, medico.Especialidade, medico.IdFuncionario },
                transaction
            );

            await transaction.CommitAsync();
            await connection.CloseAsync();
            return result > 0;
        }
        catch
        {
            await transaction.RollbackAsync();
            await connection.CloseAsync();
            throw;
        }
    }

    public async Task<bool> DeleteMedicoAsync(int id)
    {
        var sqlDeleteMedico = "DELETE FROM medico WHERE id_funcionario = @IdFuncionario;";
        var sqlDeleteFuncionario = "DELETE FROM funcionario WHERE id_funcionario = @IdFuncionario;";

        await connection.OpenAsync();
        using var transaction = await connection.BeginTransactionAsync();
        try
        {            

            await connection.ExecuteAsync(
                sqlDeleteMedico,
                new { IdFuncionario = id },
                transaction
            );

            await connection.ExecuteAsync(
                sqlDeleteFuncionario,
                new { IdFuncionario = id },
                transaction
            );

            await transaction.CommitAsync();
            await connection.CloseAsync();
            return true;
        }
        catch
        {
            await transaction.RollbackAsync();
            await connection.CloseAsync();
            return false;
        }
    }
}

