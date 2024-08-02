using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SopalS.Models.ConteneurModel
{
    public class HistoEtalonnage
    {
        [Key, Column(Order = 0)]
        public int Ref { get; set; }

        [Key, Column(Order = 1)]
        public DateTime Date { get; set; }

        public float Poids { get; set; }
        public string Unite { get; set; }

        // Foreign Key relationship with Conteneur
        [ForeignKey("Ref")]
        public Conteneur Conteneur { get; set; }
    }
}
