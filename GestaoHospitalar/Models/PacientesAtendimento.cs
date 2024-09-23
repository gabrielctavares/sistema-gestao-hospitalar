namespace GestaoHospitalar.Models;

public class PacientesAtendimento
{
    public string CpfPaciente { get; set; } = default!;

    public string NomePaciente { get; set; } = default!;

    public DateOnly? DataNascimento { get; set; }

    public int IdProntuario { get; set; }

    public DateOnly DataAbertura { get; set; }

    public int IdConsulta { get; set; }

    public DateTime? DataConsulta { get; set; }

    public string Diagnostico { get; set; } = default!;

    public string ObservacoesConsulta { get; set; } = default!;

    public string Especialidade { get; set; } = default!;   
}