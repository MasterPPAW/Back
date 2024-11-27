using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarieModele.DTOs
{
    public class AuthResponse
    {
        public bool Success { get; set; }
        public UserDTO User { get; set; }
    }
}
