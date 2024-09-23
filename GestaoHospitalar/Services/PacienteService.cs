using Dapper;
using GestaoHospitalar.Models;
using MySqlConnector;

namespace GestaoHospitalar.Services;

public interface IPacienteService
{
    Task<IEnumerable<Paciente>> GetPacientesAsync();
    Task<Paciente?> GetPacienteByCpfAsync(string cpf);
    Task<bool> CreatePacienteAsync(Paciente paciente);
    Task<bool> UpdatePacienteAsync(Paciente paciente);
    Task<bool> DeletePacienteAsync(string cpf);

    Task<IEnumerable<PacientesAtendimento>> GetAllPacientesAtendimentos();
}

public class PacienteService(MySqlConnection connection) : IPacienteService
{
    public async Task<IEnumerable<Paciente>> GetPacientesAsync()
    {
        try
        {
            var sql = "SELECT * FROM paciente;";
            await connection.OpenAsync();
            var pacientes = await connection.QueryAsync<Paciente>(sql);
            return pacientes;

        }
        finally
        {
            await connection.CloseAsync();
        }
    }

    public async Task<Paciente?> GetPacienteByCpfAsync(string cpf)
    {
        try
        {
            var sql = "SELECT * FROM paciente WHERE CPF = @Cpf;";
            await connection.OpenAsync();
            var paciente = await connection.QuerySingleOrDefaultAsync<Paciente>(sql, new { Cpf = cpf });
            return paciente;
        }
        finally
        {
            await connection.CloseAsync();
        }
    }

    public async Task<bool> CreatePacienteAsync(Paciente paciente)
    {
        try
        {
            var sql = @"INSERT INTO paciente 
                        (CPF, nome, data_nascimento, endereco, telefone, email, tipo_sanguineo, 
                        contato_emergencia_nome, contato_emergencia_telefone, contato_emergencia_relacao)
                        VALUES 
                        (@Cpf, @Nome, @DataNascimento, @Endereco, @Telefone, @Email, @TipoSanguineo, 
                        @ContatoEmergenciaNome, @ContatoEmergenciaTelefone, @ContatoEmergenciaRelacao);";
            await connection.OpenAsync();
            var result = await connection.ExecuteAsync(sql, paciente);
            return result > 0;
        }
        finally
        {
            await connection.CloseAsync();
        }

    }

    public async Task<bool> UpdatePacienteAsync(Paciente paciente)
    {
        try
        {
            var sql = @"UPDATE paciente SET 
                        nome = @Nome,
                        data_nascimento = @DataNascimento,
                        endereco = @Endereco,
                        telefone = @Telefone,
                        email = @Email,
                        tipo_sanguineo = @TipoSanguineo,
                        contato_emergencia_nome = @ContatoEmergenciaNome,
                        contato_emergencia_telefone = @ContatoEmergenciaTelefone,
                        contato_emergencia_relacao = @ContatoEmergenciaRelacao
                        WHERE CPF = @Cpf;";
            await connection.OpenAsync();
            var result = await connection.ExecuteAsync(sql, paciente);
            return result > 0;
        }
        finally
        {
            await connection.CloseAsync();
        }

    }

    public async Task<bool> DeletePacienteAsync(string cpf)
    {
        try
        {
            var sql = "DELETE FROM paciente WHERE CPF = @Cpf;";
            await connection.OpenAsync();
            var result = await connection.ExecuteAsync(sql, new { Cpf = cpf });
            return result > 0;
        }
        finally
        {
            await connection.CloseAsync();
        }

    }

    public async Task<IEnumerable<PacientesAtendimento>> GetAllPacientesAtendimentos()
    {
        try
        {
            var sql = "SELECT * FROM pacientes_atendimento;";
            await connection.OpenAsync();
            var pacientes = await connection.QueryAsync<PacientesAtendimento>(sql);
            return pacientes;
        }
        finally
        {
            await connection.CloseAsync();
        }

    }
}
