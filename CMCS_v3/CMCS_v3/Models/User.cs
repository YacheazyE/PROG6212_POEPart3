

namespace CMCS_v3.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    public partial class User
    {
        public System.Guid UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
