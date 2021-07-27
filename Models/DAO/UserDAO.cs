using Anemone.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Anemone.Models.DAO
{

    /*<span class="mr-2 d-none d-lg-inline text-gray-600 small">Valerie Luna</span>*/
    public class UserDAO
    {
        Database db = null;
        public UserDAO()
        {
            db = new Database();
        }
        public List<user> getAlluser()
        {
            return db.users.ToList();
        }
        public List<userType> getAlluserType()
        {
            return db.userTypes.ToList();
        }
        public IEnumerable<user> ListAllPagingtype(int id_type,int page, int pageSize)
        {
            IEnumerable<user> list = db.users.Where(p => p.userTypeID == id_type);

            return list.OrderByDescending(x => x.createDate).ToPagedList(page, pageSize);
        }
        public string getAccNameTypebyID(int id)
        {
            return db.userTypes.SingleOrDefault(x => x.userTypeID == id).userTypeName;
        }

        internal int Insert(RegisterModel model)
        {
            throw new NotImplementedException();
        }

        public int Insert(user entity)
        {
            try
            {
                var res = db.users.SingleOrDefault(x => x.email == entity.email);
                if (res != null)
                {
                    return 0; // tài khoản đã tồn tại
                }
                else
                {
                    entity.createDate = DateTime.Now;
                    db.users.Add(entity);
                    db.SaveChanges();
                    return 1;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public int Login(LoginModel login)
        {
            var res = db.users.SingleOrDefault(x => x.email == login.email);

            if (res == null)
            {
                return -1; // tài khoản không tồn tại
            }
            else
            {
                if (res.status == false)
                {
                    return 0; // tài khoản chưa được kích hoat
                }
                else
                {
                    if (res.pass == login.password)
                    {
                        if (res.userTypeID == 1)
                        {
                            
                            login.name = db.users.SingleOrDefault(x => x.email == login.email).userName;
                            return 1; // tài khoản được cấp quyên admin
                        }
                        else
                        {
                            return -2; // tai khoan k duoc cap quyen admin
                        }
                    }
                    else
                    {
                        return -3; //sai pass
                    }

                }
            }
        }

        public IEnumerable<user> ListAllPaging(int page, int pageSize)
        {
            IQueryable<user> model = db.users;
            return model.OrderByDescending(x => x.createDate).ToPagedList(page, pageSize);
        }
        public IPagedList<product> getAllPage(int page)
        {
            return (IPagedList<product>)db.users.ToPagedList(page, 12);
        }


        public user findByID(int id)
        {
            return db.users.SingleOrDefault(x=> x.userID == id);
        }
        public string getNameByEmail(string em)
        {
            return db.users.SingleOrDefault(x => x.email == em).userName.ToString();
        }
        public bool Update(user entity)
        {
            //var ur = db.users.SingleOrDefault(n => n.userID == entity.userID);
            try
            {
                /*user.email = entity.email;*/
                var ur = db.users.Find(entity.userID);

                ur.phone = entity.phone;
                ur.pass = entity.pass;
                ur.address = entity.address;
                ur.userName = entity.userName;
                ur.status = entity.status;
                /*user.modifiedBy = entity.modifiedBy;*/
                ur.modifiedDate = DateTime.Now;
                /*user.userTypeID = entity.userTypeID;*/


                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }


        }

        public bool Delete(int id)
        {
            try
            {
                var user = db.users.Find(id);
                db.users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public int getIdByEmail(string email)
        {
            return db.users.SingleOrDefault(x => x.email == email).userID;
        }

        public int addToCart(int id, int idp, int count)
        {
            try
            {
                var res = db.cart_product.Where(x => x.idID == id && x.produID == idp).SingleOrDefault();
                if (res == null)
                {
                    cart_product cart_ = new cart_product();
                    cart_.idID = id;
                    cart_.produID = idp;
                    cart_.countPr = count;
                    db.cart_product.Add(cart_);
                    db.SaveChanges();
                }
                else
                {
                    db.cart_product.Attach(res);
                    res.countPr = count;
                    db.SaveChanges();
                }

                return 1;
            }
            catch (Exception e)
            {
                return -1;
            }
        }


        public List<CartModel> getAllCartFromAccount(int id)
        {
            string sql = "SELECT products.proID as idp, products.codePr as codep, products.proName as name, products.image as imageP, " +
                "price as priceP, cart_product.countPr as sumcount   " +
                "FROM dbo.products, dbo.cart_product " +
                "WHERE products.proID = cart_product.produID AND idID = " + Convert.ToString(id);
            var data = db.Database.SqlQuery<CartModel>(sql)
                .Select(b => new CartModel
                {
                    idp = b.idp,
                    imageP = b.imageP,
                    name = b.name,
                    priceP = b.priceP,
                    sumcount = b.sumcount
                }).ToList();
            return data;
        }

        public bool DeleteCart(int id, int idp)
        {
            var res = db.cart_product.SingleOrDefault(x => x.idID == id && x.produID == idp);
            if (res == null)
            {
                return false;
            }
            else
            {
                db.cart_product.Attach(res);
                db.cart_product.Remove(res);
                db.SaveChanges();
                return true;
            }
        }
        public int sumPriceCart(int id)
        {
            string sql = "SELECT SUM(price*countPr) AS price " +
                "FROM dbo.cart_product, dbo.products " +
                "WHERE cart_product.produID =products.proID AND idID = '" + id + "' " +
                "GROUP BY idID";
            var data = db.Database.SqlQuery<int>(sql).SingleOrDefault();
            return data;

        }

        public int CreateOrder(int id)
        {
            try
            {
                order order = new order();
                order.orderDate = DateTime.Now;
                order.status = false;
                order.idID = id;
                db.orders.Add(order);
                db.SaveChanges();
                /* string sql = "INSERT dbo.order( orderDate, status, idID ) VALUES  (GETDATE()," +
                     "0," + Convert.ToString(id) + ")";
                 db.Database.ExecuteSqlCommand(sql);*/
                return order.idOrder;
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        public bool autoRenderOrder(int ido)
        {
            try
            {
                var order = db.orders.SingleOrDefault(x => x.idOrder == ido);

                var list = this.getAllCartFromAccount(order.idID);
                for (int i = 0; i < list.Count(); i++)
                {
                    orderdetail orderdetail = new orderdetail();
                    orderdetail.idOrder = ido;
                    orderdetail.proID = list[i].idp;
                    orderdetail.unitPrice = list[i].priceP * list[i].sumcount;
                    orderdetail.quantity = list[i].sumcount;
                    orderdetail.discount = 0;
                    db.orderdetails.Add(orderdetail);
                }
                db.SaveChanges();
                var listCart = db.cart_product.Where(x => x.idID == order.idID).ToList();
                foreach (var i in listCart)
                {
                    db.cart_product.Attach(i);
                    db.cart_product.Remove(i);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public int getCountCart(int id)
        {
            return db.cart_product.Where(x => x.idID == id).Count();
        }

        public List<OrderdetailModel> getAllOder(int id, int ido)
        {
            try
            {
                if (ido == 0)
                {
                    List<OrderdetailModel> orderDetails = new List<OrderdetailModel>();
                    var listoder = db.orders.OrderByDescending(x => x.orderDate).Where(x => x.idID == id).ToList();

                    foreach (var i in listoder)
                    {
                        var count = db.orderdetails.Where(x => x.idOrder == i.idOrder).ToList();
                        foreach (var j in count)
                        {
                            OrderdetailModel order = new OrderdetailModel();
                            var product = db.products.SingleOrDefault(x => x.proID == j.proID);
                            order.ido = i.idOrder;
                            order.imageO = product.image;
                            order.nameO = product.proName;
                            order.countO = (j.quantity ?? 0);
                            order.sumPrice = (product.price * order.countO ?? 0);
                            order.DateOrder = (i.orderDate ?? DateTime.Parse("01-01-2021"));
                            order.status = false;
                            orderDetails.Add(order);
                        }
                    }
                    return orderDetails;
                }
                else
                {
                    List<OrderdetailModel> orderDetails = new List<OrderdetailModel>();
                    var listoder = db.orders.OrderByDescending(x => x.orderDate).Where(x => x.idOrder == ido && x.idID == id).FirstOrDefault();
                    var count = db.orderdetails.Where(x => x.idOrder == ido).ToList();
                    foreach (var i in count)
                    {
                        OrderdetailModel order = new OrderdetailModel();
                        var product = db.products.SingleOrDefault(x => x.proID == i.proID);
                        order.ido = ido;
                        order.imageO = product.image;
                        order.nameO = product.proName;
                        order.countO = (i.quantity ?? 0);
                        order.sumPrice = (product.price * order.countO ?? 0);
                        order.DateOrder = (listoder.orderDate ?? DateTime.Parse("01-01-2021"));
                        order.status = false;
                        orderDetails.Add(order);
                    }
                    return orderDetails;
                }


            }
            catch (Exception e)
            { return null; }
        }


        public List<OrderdetailModel> getAllOderTrue(int id, int ido)
        {
            try
            {
                if (ido == 0)
                {
                    List<OrderdetailModel> orderDetails = new List<OrderdetailModel>();
                    var listoder = db.orders.OrderByDescending(x => x.orderDate).Where(x => x.idID == id && x.status == true).ToList();

                    foreach (var i in listoder)
                    {
                        var count = db.orderdetails.Where(x => x.idOrder == i.idOrder).ToList();
                        foreach (var j in count)
                        {
                            OrderdetailModel order = new OrderdetailModel();
                            var product = db.products.SingleOrDefault(x => x.proID == j.proID);
                            order.ido = i.idOrder;
                            order.imageO = product.image;
                            order.nameO = product.proName;
                            order.countO = (j.quantity ?? 0);
                            order.sumPrice = (product.price * order.countO ?? 0);
                            order.DateOrder = (i.orderDate ?? DateTime.Parse("01-01-2021"));
                            order.status = false;
                            orderDetails.Add(order);
                        }
                    }
                    return orderDetails;
                }
                else
                {
                    List<OrderdetailModel> orderDetails = new List<OrderdetailModel>();
                    var listoder = db.orders.OrderByDescending(x => x.orderDate).Where(x => x.idOrder == ido && x.idID == id & x.status == true).FirstOrDefault();
                    var count = db.orderdetails.Where(x => x.idOrder == ido).ToList();
                    foreach (var i in count)
                    {
                        OrderdetailModel order = new OrderdetailModel();
                        var product = db.products.SingleOrDefault(x => x.proID == i.proID);
                        order.ido = ido;
                        order.imageO = product.image;
                        order.nameO = product.proName;
                        order.countO = (i.quantity ?? 0);
                        order.sumPrice = (product.price * order.countO ?? 0);
                        order.DateOrder = (listoder.orderDate ?? DateTime.Parse("01-01-2021"));
                        order.status = false;
                        orderDetails.Add(order);
                    }
                    return orderDetails;
                }


            }
            catch (Exception e)
            { return null; }
        }
    }
}