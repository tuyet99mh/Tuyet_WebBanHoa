using Anemone.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anemone.Models.DAO
{
    public class CategoryDAO
    {
       Database db = null;
        public CategoryDAO()
        {
            db = new Database();
        }
      /*  public long Insert(category category)
        {
            db.categories.Add(category);
            db.SaveChanges();
            return category.catID;
        }*/
        public List<category> ListAll()
        {
            return db.categories.Where(x => x.status == true).ToList();
        }
        public category ViewDetail(int id)
        {
            return db.categories.Find(id);
        }

        internal int Insert(RegisterModel model)
        {
            throw new NotImplementedException();
        }

        public int Insert(category entity)
        {
            try
            {
                var res = db.categories.SingleOrDefault(x => x.name == entity.name);
                if (res != null)
                {
                    return 0; // tài khoản đã tồn tại
                }
                else
                {
                    entity.createDate = DateTime.Now;
                    db.categories.Add(entity);
                    db.SaveChanges();
                    return 1;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}