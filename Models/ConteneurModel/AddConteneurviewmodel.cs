﻿namespace SopalS.Models.ConteneurModel
{
    public class AddConteneurviewmodel
    {
        public int Ref { get; set; }
        public string CodeBarres { get; set; }
        //public string Emplacement { get; set; }
        public DateTime DateMiseEnService { get; set; }
        public int PeriodiciteEtalonnage { get; set; }
        public DateTime? DateDernierEtalonnage { get; set; }
        public double DernierPoids { get; set; }
        public string Unite { get; set; }
        public string UserCreate { get; set; }
        public DateTime DateCreate { get; set; }
        public string UserUpdate { get; set; }
        public DateTime DateUpdate { get; set; }
        public int EmplacementId { get; set; } // Foreign Key
    }
}
