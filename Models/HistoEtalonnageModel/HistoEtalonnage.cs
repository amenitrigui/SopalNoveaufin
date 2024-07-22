using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SopalS.Models.HistoEtalonnageModel
{
    public class HistoEtalonnage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Ref { get; set; }
        public DateTime Date { get; set; }
        public float Poids { get; set; }
        public string Unite { get; set; }
    }
}
