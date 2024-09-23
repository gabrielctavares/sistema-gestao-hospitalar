using System.ComponentModel.DataAnnotations;

namespace GestaoHospitalar.Models;

public class Paciente
{

    [Required(ErrorMessage = "O CPF é obrigatório.")]
    public string Cpf { get; set; } = default!;

    [Required(ErrorMessage = "O Nome é obrigatório.")]
    public string Nome { get; set; } = default!;

    public DateTime? DataNascimento { get; set; }

    public string Endereco { get; set; } = default!;

    public string Telefone { get; set; } = default!;

    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "O Tipo Sanguíneo é obrigatório.")]
    public string TipoSanguineo { get; set; } = default!;

    public string ContatoEmergenciaNome { get; set; } = default!;

    public string ContatoEmergenciaTelefone { get; set; } = default!;

    public string ContatoEmergenciaRelacao { get; set; } = default!;
}