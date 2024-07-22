using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using SopalS.Models.ConteneurModel;

namespace SopalS.Models
{
    public class Emplacement
    {
        [Key]
        public int Codeemp { get; set; }
     

        public string libele { get; set; }
        //public virtual ICollection<Conteneur> Conteneur { get; set; }
    }
}
