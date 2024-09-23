using System.ComponentModel.DataAnnotations;

namespace GestaoHospitalar.Models;

public class Medico: Funcionario
{
    [Required(ErrorMessage = "O CRM é obrigatório.")]
    public string Crm { get; set; } = default!;

    [Required(ErrorMessage = "A Especialidade é obrigatória.")]
    public string Especialidade { get; set; } = default!;
}