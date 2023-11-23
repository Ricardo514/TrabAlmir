using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dez.Models
{
    [Table("Clientes")]
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = " ID: ")]
        public int id { get; set; }

        [Required(ErrorMessage = "Campo nome Obrigatório...")]
        [StringLength(50)]
        [Display(Name = " Nome: ")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Campo CPF Obrigatório...")]
        [StringLength(11)]
        [Display(Name = " CPF: ")]
        public string cpf { get; set; }

        [Required(ErrorMessage = "Campo endereço Obrigatório...")]
        [StringLength(100)]
        [Display(Name = " Endereço: ")]
        public string endereco { get; set; }
    }
}
