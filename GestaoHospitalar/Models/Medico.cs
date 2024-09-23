namespace GestaoHospitalar.Models;

public class Medico: Funcionario
{
    public string Crm { get; set; } = default!;

    public string Especialidade { get; set; } = default!;
}