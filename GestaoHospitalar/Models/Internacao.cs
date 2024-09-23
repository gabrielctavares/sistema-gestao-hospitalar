using System.ComponentModel.DataAnnotations;

namespace GestaoHospitalar.Models;

public partial class Internacao
{
    public int IdInternacao { get; set; }

    [Required(ErrorMessage = "O Paciente é obrigatório.")]
    public string IdPaciente { get; set; } = default!;

    [Required(ErrorMessage = "O Quarto é obrigatório.")]
    public int IdQuarto { get; set; }

    [Required(ErrorMessage = "A Data de Entrada é obrigatória.")]
    public DateTime DataEntrada { get; set; }

    public DateTime? DataSaida { get; set; }

    [Required(ErrorMessage = "O Responsável é obrigatório.")]
    public int IdResponsavel { get; set; }
    
    public string Tratamento { get; set; } = default!;

    public string? NomePaciente { get; set; }
    public string? NomeResponsavel { get; set; }
}