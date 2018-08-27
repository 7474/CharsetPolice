using CharsetPolice.Police;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CharsetPoliceWeb.Models
{
    public class InvestigationRequest
    {
        [Required]
        public Uri Uri { get; set; }
    }

    // XXX 仮置き＆未参照
    public class Result : CharsetSearchResult
    {

    }
    // XXX 仮置き＆未参照
    public class Ranking : CharsetSearchResultPerDomain
    {

    }
}
