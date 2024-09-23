namespace GestaoHospitalar.Models;

public partial class Internacao
{
    public int IdInternacao { get; set; }

    public string IdPaciente { get; set; } = default!;

    public int IdQuarto { get; set; }

    public DateTime DataEntrada { get; set; }

    public DateTime? DataSaida { get; set; }

    public int IdResponsavel { get; set; }

    public string Tratamento { get; set; } = default!;

    public string? NomePaciente { get; set; }
    public string? NomeResponsavel { get; set; }
}