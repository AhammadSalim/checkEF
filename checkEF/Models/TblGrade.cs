using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace checkEF.Models
{
    public partial class TblGrade
    {
        public TblGrade()
        {
            TblStudentDetails = new HashSet<TblStudentDetails>();
        }

        public int Id { get; set; }
        public string GradeType { get; set; }
        public string Section { get; set; }

        public virtual ICollection<TblStudentDetails> TblStudentDetails { get; set; }
    }
}
