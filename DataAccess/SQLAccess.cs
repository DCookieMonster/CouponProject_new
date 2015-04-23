using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;

namespace DataAccess
{
    public static class SqlAccess
    {
        static couponEntities2 ce1 = new couponEntities2();
        //public  static EntityRepository<User> dor=new EntityRepository<User>(ce1); 
        public static Repository<User,couponEntities2> UseRepository =new Repository<User, couponEntities2>(ce1); 
        public static Repository<Coupon,couponEntities2> CouponRepository=new Repository<Coupon, couponEntities2>(ce1); 
        public static Repository<Coupons_Social_Cupon,couponEntities2> SocialCouponRepository = new Repository<Coupons_Social_Cupon, couponEntities2>(ce1);
        public static Repository<Costumer,couponEntities2> CostumerRepository=new Repository<Costumer, couponEntities2>(ce1);
        public static Repository<Sytem_Admin,couponEntities2> AdminRepository = new Repository<Sytem_Admin, couponEntities2>(ce1);
        public static Repository<Firm,couponEntities2> FirmRepository=new Repository<Firm, couponEntities2>(ce1);
        public static Repository<Firm_Owner,couponEntities2> FirmOwenrRepository = new Repository<Firm_Owner, couponEntities2>(ce1);
        public static Repository<Coupon_Order,couponEntities2> CouponOrderRepository =new Repository<Coupon_Order, couponEntities2>(ce1);
        public static Repository<Coupon_Alert, couponEntities2> CouponAlertRepository = new Repository<Coupon_Alert, couponEntities2>(ce1); 

        #region create
       public static int CreateCoupon( string name, string description, double orignalPrice, double aggregatedRank,
            DateTime lastDate, int adminID,int creatorId)
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
                        lastDateforUse = lastDate,
                        creatorId=creatorId
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

        public static bool CreateCouponAlert(int userId, int couponAlertId)
        {
            try
            {
                using (couponEntities2 ce = new couponEntities2())
                {
                    UsersCoupon_Alert usersCouponAlert=new UsersCoupon_Alert
                    {
                        User_Id = userId,
                        Coupon_Alert_Id = couponAlertId
                    };
                    ce.UsersCoupon_Alert.Add(usersCouponAlert);
                    ce.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static int CreateUserPref(int userId, int categoryId)
        {
            try
            {
                using (couponEntities2 ce = new couponEntities2())
                {
                    User_Prefrences userPrefrences=new User_Prefrences {Users_Id = userId, category = categoryId};
                    ce.User_Prefrences.Add(userPrefrences);
                    ce.SaveChanges();
                    return userPrefrences.Id;
                }
            }
            catch (Exception)
            {
                return -1;
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

        public static int CreateCostumer(int user_id,int gender,double age,double longtitdue,double latitude)
        {
            try
            {
                using (couponEntities2 ce = new couponEntities2())
                {
                   Costumer costumer=new Costumer
                   {
                       Id = user_id,
                       age = age,
                       gender = gender,
                       latitude = latitude,
                       longitude = longtitdue
                   };
                    ce.Costumers.Add(costumer);
                    ce.SaveChanges();
                    return costumer.Id;
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
            int firmId,int systemId)
        {
            try
            {
                using (couponEntities2 ce = new couponEntities2())
                {
                    Firm firm = new Firm();
                    firm.Firm_Owner_Id = firmId;
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

        public static int GetFirmOwnerId(string username)
        {
            try
            {
                using (couponEntities2 ce = new couponEntities2())
                {
                    var firmOwner = (from x in ce.Firm_Owner where x.User.username == username select x);
                    foreach (Firm_Owner owner in firmOwner)
                    {
                        return owner.Id;
                    }
                }
            }
            catch (Exception)
            {

               return -1;
            }
            return -1;
        }

        public static List<Coupon> GetCoupons()
        {
            List<Coupon> coupons=new List<Coupon>();
            try
            {
                using (couponEntities2 ce =new couponEntities2())
                {
                    var c = (from x in ce.Coupons select x) ;
                    coupons.AddRange(c);
                }
            }
            catch (Exception)
            {
                return null;
                
            }
            return coupons;
        }

        public static List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();
            try
            {
                using (couponEntities2 ce = new couponEntities2())
                {
                    var c = (from x in ce.Categories select x);
                    categories.AddRange(c);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return categories;
        }

        public static List<Coupons_Social_Cupon> GetCouponsSocialCupons()
        {
            var couponsSocialCupons=new List<Coupons_Social_Cupon>();
            try
          
            {
                using (couponEntities2 ce = new couponEntities2())
                {
                    var c = (from x in ce.Coupons_Social_Cupon select x);
                    couponsSocialCupons.AddRange(c);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return couponsSocialCupons;
        }

        public static List<Coupon_Alert> GetCouponAlerts(int couponId)
        {
            var alert = new List<Coupon_Alert>();
            try
            {
                using (couponEntities2 ce = new couponEntities2())
                {
                    var c = (from x in ce.Coupon_Alert where x.coupon_Id==couponId select x);
                    alert.AddRange(c);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return alert;
        }

        public static User GetUser(int id)
        {
            try
            {
                using (couponEntities2 ce=new couponEntities2())
                {
                    return ce.Users.SingleOrDefault(x => x.Id == id);
                }
            }
            catch (Exception)
            {
                return null;
                throw;
            }
            return null;
        }

        public static Costumer GetCostumer(int id)
        {
            try
            {
                using (couponEntities2 ce = new couponEntities2())
                {
                    var users = (from x in ce.Costumers where x.Id == id select x);
                    foreach (Costumer user in users)
                    {
                        return user;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }

        public static SocialNetworkProfile GetSocialNetworkProfileForUser(int userid)
        {
            try
            {
                using (couponEntities2 ce = new couponEntities2())
                {
                    var users = (from x in ce.SocialNetworkProfiles where x.User_Id == userid select x);
                    foreach (SocialNetworkProfile user in users)
                    {
                        return user;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }

        public static List<User_Prefrences> GetUserPrefrenceses(int userid)
        {
            List<User_Prefrences> userPrefrenceses=new List<User_Prefrences>();
            try
            {
                using (couponEntities2 ce = new couponEntities2())
                {
                    var users = (from x in ce.User_Prefrences where x.Users_Id == userid select x);
                    userPrefrenceses.AddRange(users);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }

        public static List<AlertType> GetAlertTypes()
        {
            List<AlertType> alertTypes=new List<AlertType>();
            try
            {
                using (couponEntities2 ce=new couponEntities2())
                {
                    var a = (from x in ce.AlertTypes select x);
                    alertTypes.AddRange(a);
                }
            }
            catch (Exception)
            {
                return null;
                
            }
            return alertTypes;
        }

        public static List<Firm> GetFirms()
        {
            List<Firm> firms = new List<Firm>();
            try
            {
                using (couponEntities2 ce = new couponEntities2())
                {
                    var a = (from x in ce.Firms select x);
                    firms.AddRange(a);
                }
            }
            catch (Exception)
            {
                return null;

            }
            return firms;
        }
        #endregion

        #region update

        public static bool UpdateCoupon(Coupon coupon)
        {
            try
            {
                using (couponEntities2 ce = new couponEntities2())
                {
                    
                
                    var result = ce.Coupons.SingleOrDefault(b => b.Id == coupon.Id);
                    if (result != null)
                    {
                        result.User = coupon.User;
                        result.creatorId = coupon.creatorId;
                        result.Coupon_Alert = coupon.Coupon_Alert;
                        result.aggregatedRank = coupon.aggregatedRank;
                        result.description = coupon.description;
                        result.Coupon_Order = coupon.Coupon_Order;
                        result.Coupons_Social_Cupon = coupon.Coupons_Social_Cupon;
                        result.discountPrice = coupon.discountPrice;
                        result.lastDateforUse = coupon.lastDateforUse;
                        result.name = coupon.name;
                        result.Categories = coupon.Categories;
                        result.Admin_Approvel = coupon.Admin_Approvel;
                        result.originalPrice = coupon.originalPrice;
                        ce.SaveChanges();
                    }
                    
                }
            }
            catch (Exception)
            {
                return false;
                
            }
            return true;
        }

        public static bool UpdateCostomer(Costumer costumer)
        {
            try
            {
                using (couponEntities2 ce = new couponEntities2())
                {


                    var result = ce.Costumers.SingleOrDefault(b => b.Id == costumer.Id);
                    if (result != null)
                    {
                        result.User = costumer.User;
                    
                        result.Coupon_Order = costumer.Coupon_Order;
                        result.User_Prefrences = costumer.User_Prefrences;
                        result.age = costumer.age;
                        result.latitude = costumer.latitude;
                        result.longitude = costumer.longitude;
                        result.gender = costumer.gender;
                        ce.SaveChanges();
                    }

                }
            }
            catch (Exception)
            {
                return false;

            }
            return true;
        }

        public static bool UpdateFirm(Firm firm)
        {
            try
            {
                using (couponEntities2 ce = new couponEntities2())
                {


                    var result = ce.Firms.SingleOrDefault(b => b.Id == firm.Id);
                    if (result != null)
                    {
                        result.Firm_Owner_Id = firm.Firm_Owner_Id;
                        result.Firm_Owner = firm.Firm_Owner;
                        result.Sytem_Admin = firm.Sytem_Admin;
                        result.Sytem_Admin_Id = firm.Sytem_Admin_Id;
                        result.address = firm.address;
                        result.city = firm.city;
                        result.category = firm.category;
                        result.description = firm.description;
                        result.name = firm.name;
                        result.latitude = firm.latitude;
                        result.longitude = firm.longitude;
                        ce.SaveChanges();
                    }

                }
            }
            catch (Exception)
            {
                return false;

            }
            return true;
        }

        public static bool UpdateFirmOwner(Firm_Owner firmOwner)
        {
            try
            {
                using (couponEntities2 ce = new couponEntities2())
                {


                    var result = ce.Firm_Owner.SingleOrDefault(b => b.Id == firmOwner.Id);
                    if (result != null)
                    {
                       
                        result.Sytem_Admin = firmOwner.Sytem_Admin;
                        result.Sytem_Admin_Id = firmOwner.Sytem_Admin_Id;
                        result.User = firmOwner.User;
                        result.Firms = firmOwner.Firms;
                        ce.SaveChanges();
                    }

                }
            }
            catch (Exception)
            {
                return false;

            }
            return true;
        }

        public static bool UpdateSocialCoupon(Coupons_Social_Cupon socialCupon)
        {
            try
            {
                using (couponEntities2 ce = new couponEntities2())
                {


                    var result = ce.Coupons_Social_Cupon.SingleOrDefault(b => b.Id == socialCupon.Id);
                    if (result != null)
                    {

                        result.Coupon = socialCupon.Coupon;
                        ce.SaveChanges();
                    }

                }
            }
            catch (Exception)
            {
                return false;

            }
            return true;
        }

        public static bool UpdateSocialNetProfile(SocialNetworkProfile socialNetworkProfile)
        {
            try
            {
                using (couponEntities2 ce = new couponEntities2())
                {


                    var result = ce.SocialNetworkProfiles.SingleOrDefault(b => b.Id == socialNetworkProfile.Id);
                    if (result != null)
                    {
                        result.User_Id = socialNetworkProfile.User_Id;
                        result.User = socialNetworkProfile.User;
                        result.authToken = socialNetworkProfile.authToken;
                        result.password = socialNetworkProfile.password;
                        result.recivedData = socialNetworkProfile.recivedData;
                        result.sendData = socialNetworkProfile.sendData;
                        result.username = socialNetworkProfile.username;
                        ce.SaveChanges();
                    }

                }
            }
            catch (Exception)
            {
                return false;

            }
            return true;
        }

        public static bool UpdateUserPref(User_Prefrences userPrefrences)
        {
            try
            {
                using (couponEntities2 ce = new couponEntities2())
                {


                    var result = ce.User_Prefrences.SingleOrDefault(b => b.Id == userPrefrences.Id);
                    if (result != null)
                    {

                        result.Users_Id = userPrefrences.Users_Id;
                        result.category = userPrefrences.category;
                        result.Costumer = userPrefrences.Costumer;
                        ce.SaveChanges();
                    }

                }
            }
            catch (Exception)
            {
                return false;

            }
            return true;
        }

        #endregion

        #region delete

        public static bool RemoveCoupon(int CouponId)
        {
            try
            {
                using (couponEntities2 ce =new couponEntities2())
                {
                    var itemToRemove = ce.Coupons.SingleOrDefault(x => x.Id ==CouponId); //returns a single item.

                    if (itemToRemove != null)
                    {
                        ce.Coupons.Remove(itemToRemove);
                        ce.SaveChanges();
                    }
                }
               
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

      

        #endregion

    }
}