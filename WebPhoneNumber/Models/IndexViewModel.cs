using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebPhoneNumber.Models
{
    public class FormModel
    {
        [Required]
        public string Phone { get; set; }
        [Required]
        public string RegionCode { get; set; }

        public bool IsValid { get; set; }

        public string Message { get; set; }

        public SelectList RegionList { get; set; }

        public List<Tuple<string, string, string>> PhoneExemples { get; set; }
    }
}
