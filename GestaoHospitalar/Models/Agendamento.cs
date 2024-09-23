using System.ComponentModel.DataAnnotations;

namespace GestaoHospitalar.Models;

public class Agendamento
{
    public int IdAgendamento { get; set; }

    [Required(ErrorMessage = "O Paciente é obrigatório.")]
    public string IdPaciente { get; set; } = default!;

    public int? IdSecretario { get; set; }

    [Required(ErrorMessage = "O Médico é obrigatório.")]
    public int? IdMedico { get; set; }


    [Required(ErrorMessage = "A Data é obrigatória.")]
    public DateTime? DataAgendamento { get; set; }

    public string Status { get; set; } = "pendente";

    public string? NomePaciente { get; set; }
    public string? NomeSecretario { get; set; }
    public string? NomeMedico { get; set; }
}