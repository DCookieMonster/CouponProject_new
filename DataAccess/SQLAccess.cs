using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess
{
    public static class SqlAccess
    {
        static int CreateCoupon( string name, string description, double orignalPrice, double aggregatedRank,
            DateTime lastDate, int adminID)
        {
            try
            {
                using (couponEntities2 ce=new couponEntities2())
                {
                    Coupon coupon = new Coupon
                    {
                        Admin_Approvel = adminID,
                        name = name,
                        description = description,
                        originalPrice = orignalPrice,
                        aggregatedRank = aggregatedRank,
                        lastDateforUse = lastDate
                    };
                    ce.SaveChanges();
                    return coupon.Id;
                }
               
            }
            catch (Exception)
            {

                return -1;
            }
            return -1;
        }

        public static int CreateUser(string userName, string passwd, string email)
        {
            try
            {
                using (couponEntities2 ce = new couponEntities2())
                {
                    User user = new User {username = userName, password = passwd, email = email};
                    ce.SaveChanges();
                    return user.Id;
                }
            }
            catch (Exception)
            {
                return -1;
            }
            return -1;
        }

        public static int CreateAdmin(int user_id)
        {
            try
            {
                using (couponEntities2 ce = new couponEntities2())
                {
                    Sytem_Admin admin=new Sytem_Admin();
                    admin.Id = user_id;
                    ce.SaveChanges();
                    return admin.Id;
                }
            }
            catch (Exception)
            {
                return -1;
            }
            return -1;
        }
    }
}