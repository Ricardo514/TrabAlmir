using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dez.Models
{
    [Table("Vendas")]
    public class Venda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = " ID: ")]
        public int id { get; set; }

        [Display(Name = "Farmacêutico")]
        public int IdFarmaceutico { get; set; }
        [ForeignKey("IdFarmaceutico")]
        public Farmaceutico farmaceutico { get; set; }

        [Display(Name = "Produto")]
        public int IdProduto { get; set; }
        [ForeignKey("IdProduto")]
        public Produto produto { get; set; }

        [Display(Name = "Cliente")]
        public int IdCliente { get; set; }
        [ForeignKey("IdCliente")]
        public Cliente cliente { get; set; }

        [Required(ErrorMessage = "Quantiade da venda obrigatório...")]
        [Range(1, 1000, ErrorMessage = "Esta quantia ultrapassa o nosso estoque")]
        [Display(Name = " quantidade ")]
        public int qntd { get; set; }

        [Required]
        [Display(Name = " total ")]
        public float total { get; set; }

    }
}
