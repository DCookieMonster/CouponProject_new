using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess
{
    public static class SqlAccess
    {
        #region create
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
        }

        public static int CreateUser(string userName, string passwd, string email)
        {
            try
            {
                using (couponEntities2 ce = new couponEntities2())
                {
                    User user = new User {username = userName, password = passwd, email = email};
                    ce.Users.Add(user);
                    ce.SaveChanges();
                    return user.Id;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static int CreateAdmin(int user_id)
        {
            try
            {
                using (couponEntities2 ce = new couponEntities2())
                {
                    Sytem_Admin admin=new Sytem_Admin();
                    admin.Id = user_id;
                    ce.Sytem_Admin.Add(admin);
                    ce.SaveChanges();
                    return admin.Id;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public static int FirmOwner(int user_id,int systemAdminId)
        {
            try
            {
                using (couponEntities2 ce = new couponEntities2())
                {
                    Firm_Owner firmOwner=new Firm_Owner {Id = user_id, Sytem_Admin_Id = systemAdminId};
                    ce.Firm_Owner.Add(firmOwner);
                    ce.SaveChanges();
                    return firmOwner.Id;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static int CreateCouponOrder(int couponId, DateTime date,string recepit,string creditApproval,double rank,
            string rankDesc,string qr,double serialKey)
        {
            try
            {
                using (couponEntities2 ce = new couponEntities2())
                {
                    Coupon_Order co=new Coupon_Order
                    {
                        coupon_id = couponId,
                        date = date,
                        recepit = recepit,
                        creditApproval = creditApproval,
                        isUsed = false,
                        rank = rank,
                        rankDesc = rankDesc,
                        qrForOrder = qr,
                        serialKey = serialKey
                    };
                    ce.Coupon_Order.Add(co);
                    ce.SaveChanges();
                    return co.Id;
                }
            }
            catch (Exception)
            {
                return -1;
            }
      
        }

        public static int CreateCouponAlert(int couponId,int alertType,string text)
        {
            try
            {
                using (couponEntities2 ce = new couponEntities2())
                {
                   Coupon_Alert ca=new Coupon_Alert();
                    ca.coupon_Id = couponId;
                    ca.alert_type = alertType;
                    ca.text = text;
                    ce.Coupon_Alert.Add(ca);
                    ce.SaveChanges();
                    return ca.Id;
                }
            }
            catch (Exception)
            {
                return -1;
            }

        }

        public static bool CreateCouponCategory(int couponId, int categoryId)
        {
            try
            {
                using (couponEntities2 ce = new couponEntities2())
                {
                    var c = (from x in ce.Coupons where x.Id == couponId select x);
                    var category = (from x in ce.Categories where x.Id == categoryId select x);
                    foreach (Coupon coupon in c)
                    {
                        foreach (Category category1 in category)
                        {
                            coupon.Categories.Add(category1);
                        }
                    }
                    ce.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static int CreateSocialCupon(int couponId)
        {
            try
            {
                using (couponEntities2 ce = new couponEntities2())
                {
                    Coupons_Social_Cupon couponsSocial = new Coupons_Social_Cupon();
                    couponsSocial.Id = couponId;
                    ce.Coupons_Social_Cupon.Add(couponsSocial);
                    ce.SaveChanges();
                    return couponId;
                }
            }
            catch (Exception)
            {
                return -1;
            }

        }

        public static int CreateSocialNet(string username,string password,string sendData,string recivedData,string authToken,
            int id)
        {
            try
            {
                using (couponEntities2 ce = new couponEntities2())
                {
                   SocialNetworkProfile socialNetwork=new SocialNetworkProfile
                   {
                       username = username,
                       password = password,
                       sendData = sendData,
                       recivedData = recivedData,
                       authToken = authToken,
                       User_Id = id
                   };
                    ce.SocialNetworkProfiles.Add(socialNetwork);
                    ce.SaveChanges();
                    return socialNetwork.Id;
                }
            }
            catch (Exception)
            {
                return -1;
            }

        }

        public static int CreateFirm(string name, string adress, double longitude,double latitude,string desc,string city,
            int firmID,int systemId)
        {
            try
            {
                using (couponEntities2 ce = new couponEntities2())
                {
                    Firm firm = new Firm();
                    firm.Firm_Owner_Id = firmID;
                    firm.name = name;
                    firm.address = adress;
                    firm.longitude = longitude;
                    firm.latitude = latitude;
                    firm.description = desc;
                    firm.city = city;
                    firm.Sytem_Admin_Id = systemId;
                    ce.Firms.Add(firm);
                    ce.SaveChanges();
                    return firm.Id;
                }
            }
            catch (Exception)
            {
                return -1;
            }

        }
        #endregion

        #region Select

        public static int GetAdminId(string username)
        {
           int ans = -1;
            try
            {
                using (couponEntities2 ce =new couponEntities2())
                {
                  var admin = (from x in ce.Sytem_Admin where x.User.username == username select x);
                    foreach (Sytem_Admin sytemAdmin in admin)
                    {
                        return sytemAdmin.Id;
                    }
                }
            }
            catch (Exception)
            {

                ans = -1;
            }
            return ans;
        }
        #endregion
    }
}