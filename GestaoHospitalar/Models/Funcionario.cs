
using System.ComponentModel.DataAnnotations;

namespace GestaoHospitalar.Models;

public partial class Funcionario
{
    public int IdFuncionario { get; set; }

    [Required(ErrorMessage = "O CPF é obrigatório.")]
    public string Cpf { get; set; } = default!;

    [Required(ErrorMessage = "O Nome é obrigatório.")]
    public string Nome { get; set; } = default!;

    public string Endereco { get; set; } = default!;

    public string Telefone { get; set; } = default!;    

    public string Email { get; set; } = default!;
}