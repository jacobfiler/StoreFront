using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;//added for validators/general metadat


namespace StoreFrontLab.DATA.EF//.Metadata
{
    #region Product
    [MetadataType(typeof(ProductMetadata))]
    public partial class Product { }

    public class ProductMetadata
    {
        //public int ProductID { get; set; }

        [Required(ErrorMessage = " ** Name is Required **")]
        [StringLength(150, ErrorMessage = "** Maximum 150 Characters **")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = " ** Type is Required **")]
        [Range(0, double.MaxValue, ErrorMessage = "** Must be greater than 0 **")]
        [Display(Name = "Type")]
        public short ProductType { get; set; }

        [Required(ErrorMessage = " ** Description is Required **")]
        [StringLength(200, ErrorMessage = "** Maximum 200 Characters **")]
        public string Description { get; set; }

        [Required(ErrorMessage = " ** Price is Required **")]
        [Range(0, double.MaxValue, ErrorMessage = "** Must be greater than 0 **")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = " ** Image is Required **")]
        [StringLength(100, ErrorMessage = "** Maximum 100 Characters **")]
        [Display(Name = "Image")]
        public string ProductImage { get; set; }


        [Required(ErrorMessage = " ** Vendor ID is Required **")]
        [Display(Name = "Vendor ID")]
        public int VendorID { get; set; }

    }
    #endregion


    #region ProductType
    [MetadataType(typeof(ProductType1Metadata))]
    public partial class ProductType { }
    public class ProductType1Metadata
    {
        //public short ProductType1 { get; set; }
        [Required(ErrorMessage = " ** Product Type is Required **")]
        [StringLength(50, ErrorMessage = "** Maximum 50 Characters **")]
        [Display(Name = "Type")]
        public string TypeName { get; set; }
    }
    #endregion


    #region Vendor
    [MetadataType(typeof(VendorMetadata))]
    public partial class Vendor { }
    public class VendorMetadata
    {
        //public int VendorID { get; set; }
        [Required(ErrorMessage = " ** Vendor Name is Required **")]
        [StringLength(50, ErrorMessage = "** Maximum 50 Characters **")]
        [Display(Name = "Vendor Name")]
        public string VendorName { get; set; }

        [Required(ErrorMessage = " ** Contact Name is Required **")]
        [StringLength(50, ErrorMessage = "** Maximum 50 Characters **")]
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }

        [Required(ErrorMessage = " ** Phone Number is Required **")]
        [StringLength(12, ErrorMessage = "** Maximum 12 Characters **")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
    #endregion

}
