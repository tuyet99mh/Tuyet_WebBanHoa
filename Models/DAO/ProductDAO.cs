using System;
using Anemone.Models.EF;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;

namespace Anemone.Models.DAO
{
    public class ProductDAO
    {

        Database db = null;
        public ProductDAO()
        {
            db = new Database();
        }

        public IEnumerable<product> ListProductAllNew(string searchString, int page, int pageSize)
        {
            IQueryable<product> model = db.products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.proName.Contains(searchString) || x.codePr.Contains(searchString));
                model = model.Where(x => x.status == true && x.quantity > 0);
            }
            return model.OrderByDescending(x => x.createDate).ToPagedList(page, pageSize);
        }

        public IEnumerable<product>  ListAllPaging(int page, int pageSize)
        {
            IEnumerable<product> list = db.products;
            /*IEnumerable<product> list = db.products.Where(p => p.status == true && p.quantity > 0);*/
            return list.OrderByDescending(x => x.createDate).ToPagedList(page, pageSize);
        }


        public IPagedList<product> getAllPage(int page)
        {
            return (IPagedList<product>)db.users.ToPagedList(page, 5);
        }

        

        public IEnumerable<product> ListProductType(int id_cat, int page, int pageSize)
        {
            IEnumerable<product> list = db.products.Where(p => p.status == true && p.catID == id_cat && p.quantity > 0);

            return list.OrderByDescending(x => x.createDate).ToPagedList(page, pageSize);
        }
        public List<category> getAllprCat()
        {
            return db.categories.ToList();
        }

        public List<product> getPrByCat(int id_pr)
        {
            product pr = findByID(id_pr);


            return db.products.Where(x => x.catID == pr.catID && x.proID != pr.proID).ToList();
        }



        public int Create(product pr)
        {
            try
            {
                int iRanDom;
                string strRanDom;
                Random rd = new Random();
                //iRanDom sẽ nhận có giá trị ngẫu nhiên trong khoảng 1 đến 100:
                iRanDom = rd.Next(1, 100);
                //Chuyển giá trị ramdon về kiểu string:
                strRanDom = rd.Next(1, 100).ToString();

                var prcode = "SP" + db.products.ToList().Count.ToString() + "_" + strRanDom;
                pr.codePr = prcode;
                pr.createDate = DateTime.Now;
                var res = db.products.SingleOrDefault(x => x.codePr == pr.codePr);
                if (res != null)
                {

                    return 0; // mã sản phẩm đã tồn tại tạo mã mới
                }
                else
                {
                    db.products.Add(pr);
                    db.SaveChanges();
                    return 1; //thêm thành công
                }

            }
            catch (Exception)
            {

                return -1; //thêm thất bại
            }

        }
        public List<category> getAllcatType()
        {
            return db.categories.ToList();
        }

        public List<product> getPr(int id_cat)
        {
            /*            var ress = db.products.Where(x => x.catID == tl);
                        var res = (from sp in db.products where sp.catID == tl select sp);*/
            return db.products.Where(x => x.catID == id_cat).ToList();
        }

        public string getCatNamebyID(int id)
        {
            return db.categories.SingleOrDefault(x => x.catID == id).name.ToString();
        }

        public int getPrcatbyPr(int idpr)
        {
            List<orderdetail> res = db.orderdetails.Where(x => x.proID ==idpr).ToList();
            if (res != null)
            {
                return 0;
            }
            else
            {
                return 1;
            }
           
        }
        public product findByID(int? id)
        {
            return db.products.Find(id);
        }

        public List<product> GetTopPrice()
        {
            return db.products.Where(x => x.status == true && x.quantity > 0).OrderBy(x => x.price).OrderByDescending(x => x.price).Take(6).ToList();
        }

        public List<product> GetMinusPrice()
        {
            return db.products.Where(x => x.status == true && x.quantity > 0).OrderBy(x => x.price).Take(6).ToList();
        }
        public List<product> GetListPr()
        {
            return db.products.Where(x => x.status == true && x.quantity > 0).ToList();
        }
        public List<product> ListNewProduct(int top)
        {
            return db.products.Where(x => x.status == true && x.quantity > 0).OrderByDescending(x => x.createDate).Take(top).ToList();
        }
        public bool Delete(int id)
        {
            try
            {
                var pr = db.products.Find(id);
                db.products.Remove(pr);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool DeletePr(int idp)
        {
            var res = db.products.SingleOrDefault(x => x.proID == idp);
            if (res == null)
            {
                return false;
            }
            else
            {
                db.products.Attach(res);
                db.products.Remove(res);
                db.SaveChanges();
                return true;
            }
        }

        public bool Edit(product entity)
        {
            //var ur = db.users.SingleOrDefault(n => n.userID == entity.userID);
            try
            {

                var pr = db.products.Find(entity.proID);

                pr.proName = entity.proName;
                pr.catID = entity.catID;
                pr.description = entity.description.ToString();
                pr.quantity = entity.quantity;
                pr.promotionPrice = entity.promotionPrice;
                pr.status = entity.status;
                pr.price = entity.price;
                pr.image = entity.image;
                pr.modifiedDate = DateTime.Now;

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }


        }

        public List<product> listspTimkiem(string tensp)
        {
            string search = "select * from products where proName like '%" + tensp + "%'";
            var rs = db.products.SqlQuery(search).ToList();
            return rs;
        }


        

    }
}