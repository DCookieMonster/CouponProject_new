//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Coupon
    {
        public Coupon()
        {
            this.Coupon_Alert = new HashSet<Coupon_Alert>();
            this.Coupon_Order = new HashSet<Coupon_Order>();
            this.Categories = new HashSet<Category>();
        }
    
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double originalPrice { get; set; }
        public double discountPrice { get; set; }
        public double aggregatedRank { get; set; }
        public System.DateTime lastDateforUse { get; set; }
        public int Admin_Approvel { get; set; }
        public int creatorId { get; set; }
    
        public virtual ICollection<Coupon_Alert> Coupon_Alert { get; set; }
        public virtual ICollection<Coupon_Order> Coupon_Order { get; set; }
        public virtual Coupons_Social_Cupon Coupons_Social_Cupon { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual User User { get; set; }
    }
}
