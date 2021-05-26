using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EventYojana.API.DataAccess.DataEntities.Common;

namespace EventYojana.API.DataAccess.DataEntities.Vendor
{
    [Table("VendorDetails", Schema = "vendor")]
    public class VendorDetails
    {

        [Key]
        public int VendorId { get; set; }

        [ForeignKey("UserLogin")]
        public int? LoginId { get; set; }
        public UserLogin UserLogin { get; set; }

        [Required]
        [StringLength(50)]
        public string VendorName { get; set; }

        [Required]
        [StringLength(50)]
        public string VendorEmail { get; set; }

        [Required]
        public bool IsBranch { get; set; }

        [StringLength(15)]
        public string Mobile { get; set; }

        [StringLength(15)]
        public string Landline { get; set; }

        [Required]
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }

        [Required]
        public bool IsLoginByVendor { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
    }

}
