﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
            ErrorMessage = "Password must have 1 Uppercase,1 Lowercase,1 number,1 non alphanumeric and at least 6 characters")]
        /* 
         This regular expression match can be used for validating strong password. 
        It expects atleast 1 small-case letter, 1 Capital letter, 1 digit, 1 special character and the length should be between 6-10 characters. 
        The sequence of the characters is not important. This expression follows the above 4 norms specified by microsoft for a strong password.
        
        Matches	1A2a$5 | 1234567Tt# | Tsd677%
        Non-Matches Tt122 | 1tdfy34564646T*
         */


        public string Password { get; set; }

    }
}
