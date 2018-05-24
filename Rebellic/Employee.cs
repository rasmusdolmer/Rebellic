namespace WebService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Employee")]
    public partial class Employee
    {
        public int Employee_Id { get; set; }

        public string Employee_Name { get; set; }

        public string Employee_Email { get; set; }

        public int Employee_Phone { get; set; }
    }
}
