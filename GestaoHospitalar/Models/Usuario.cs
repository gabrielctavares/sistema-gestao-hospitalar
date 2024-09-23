namespace GestaoHospitalar.Models;

public class Usuario
{
    public int IdUsuario { get; set; }

    public int? IdFuncionario { get; set; }

    public string Username { get; set; } = default!;            

    public string Senha { get; set; } = default!;

    public string NivelAcesso { get; set; } = default!;
}