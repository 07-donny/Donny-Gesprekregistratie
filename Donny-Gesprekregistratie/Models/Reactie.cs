using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Donny_Gesprekregistratie.Models
{
    public class Reactie
    {
        [Key]
        public int Id { get; set; }

        public DateTime Datum { get; set; }
        public string Inhoud { get; set; }
        public IdentityUser Medewerker { get; set; }

        public Gesprek Gesprek { get; set; }
        public int GesprekId { get; set; }
    }
}