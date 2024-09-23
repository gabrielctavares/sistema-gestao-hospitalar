namespace GestaoHospitalar.Models;

public class Paciente
{
    
    public string Cpf { get; set; } = default!; 

    public string Nome { get; set; } = default!;

    public DateTime? DataNascimento { get; set; }

    public string Endereco { get; set; } = default!;

    public string Telefone { get; set; } = default!;

    public string Email { get; set; } = default!;

    public string TipoSanguineo { get; set; } = default!;

    public string ContatoEmergenciaNome { get; set; } = default!;

    public string ContatoEmergenciaTelefone { get; set; } = default!;

    public string ContatoEmergenciaRelacao { get; set; } = default!;
}