
namespace GestaoHospitalar.Models;

public partial class Funcionario
{
    public int IdFuncionario { get; set; }

    public string Cpf { get; set; } = default!;

    public string Nome { get; set; } = default!;

    public string Endereco { get; set; } = default!;

    public string Telefone { get; set; } = default!;    

    public string Email { get; set; } = default!;
}