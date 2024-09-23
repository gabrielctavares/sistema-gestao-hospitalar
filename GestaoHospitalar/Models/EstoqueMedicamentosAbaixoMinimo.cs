namespace GestaoHospitalar.Models;

public class EstoqueMedicamentosAbaixoMinimo
{
    public int IdMedicamento { get; set; }

    public string NomeMedicamento { get; set; } = default!;

    public string Tarja { get; set; } = default!;

    public string Formula { get; set; } = default!;

    public DateOnly Validade { get; set; }

    public int? Quantidade { get; set; }

    public int QuantidadeMinima { get; set; }

    public string NomeEstoque { get; set; } = default!;

    public string Localizacao { get; set; } = default!; 
}