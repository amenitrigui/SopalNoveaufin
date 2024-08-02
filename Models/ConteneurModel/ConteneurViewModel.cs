using System.ComponentModel.DataAnnotations.Schema;

namespace SopalS.Models.ConteneurModel
{
    [Table("V_conteneurs")]
    public class ConteneurViewModel
    {
        public int Ref { get; set; }
        public string CodeBarres { get; set; }
        public DateTime DateMiseEnService { get; set; }
        public int PeriodiciteEtalonnage { get; set; }
        public DateTime? DateDernierEtalonnage { get; set; }
        public double DernierPoids { get; set; }
        public string Unite { get; set; }
        public string UserCreate { get; set; }
        public DateTime DateCreate { get; set; }
        public string UserUpdate { get; set; }
        public DateTime DateUpdate { get; set; }
        public int EmplacementId { get; set; }
        public DateTime DateProchainEtalonnage { get; set; }
    }
}
