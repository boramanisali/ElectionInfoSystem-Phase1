using BLL.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class PoliticalPartiesModel
    {
        public PoliticalParties Record { get; set; }

        [DisplayName("Political Party")]
        public string Name => Record.Name;

        [DisplayName("Registration Date")]
        public string RegDate => Record.RegDate.ToString("MM/dd/yyyy");

        [DisplayName("Last Election Result")]
        public string LastPercentage => Record.LastPercentage.ToString("N2");

        
    }
}
