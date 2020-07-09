using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Donny_Gesprekregistratie.Models
{
    public class Gesprek
    {
        public int Id { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public DateTime Datum { get; set; }
        public string Melding { get; set; }
        public string Onderwerp { get; set; }
        public string Inhoud { get; set; }
        public IdentityUser Medewerker { get; set; }
        public bool Afgesloten { get; set; }
        public List<Reactie> Reacties { get; set; }
    }
}