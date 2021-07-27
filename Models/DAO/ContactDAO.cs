using Anemone.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Anemone.Models.DAO
{
    public class ContactDAO
    {
        Database db = new Database();
        public ContactDAO()
        {
            db = new Database();
        }
        public List<contact> ListAll()
        {
            return db.contacts.ToList();
        }

        public int Create(contact entity)
        {
            try
            {
                db.contacts.Add(entity);
                db.SaveChanges();
                return 1;

            }
            catch (Exception)
            {
                return -1;
            }

        }
        public contact findByID(int id)
        {
            return db.contacts.SingleOrDefault(x => x.contactID == id);
        }
    }
}