using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoleApiClientIdentity.EF
{
    [Table("EMPLOYEES")]
    public partial class EMPLOYEE
    {
        [Key]
        public int EM_ID { get; set; }

        public int? EM_TYPE_ID { get; set; }

        public int? REG_ID { get; set; }

        public int? DEP_ID { get; set; }

        [StringLength(15)]
        public string EM_CODE { get; set; }

        [StringLength(100)]
        public string EM_NAME { get; set; }

        [StringLength(1)]
        public string EM_GENDER { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EM_BIRTHDATE { get; set; }

        [StringLength(15)]
        public string EM_IDENTITY_NUMBER { get; set; }

        [StringLength(400)]
        public string EM_ADDRESS { get; set; }

        [StringLength(15)]
        public string EM_PHONE { get; set; }

        [StringLength(50)]
        public string EM_EMAIL { get; set; }

        [StringLength(400)]
        public string EM_IMAGE { get; set; }

        public bool? EM_TIME_CHECK { get; set; }

        public string FIRST_FINGER { get; set; }

        public string SECOND_FINGER { get; set; }

        public bool? EM_STATUS { get; set; }

        public bool? EDIT_STATUS { get; set; }

        [StringLength(8)]
        public string PIN { get; set; }

        [StringLength(1)]
        public string PRIVILEGE { get; set; }

        public int? GA_ID { get; set; }

        public byte[] EM_IMAGE2 { get; set; }
    }
}