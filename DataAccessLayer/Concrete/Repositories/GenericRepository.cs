using DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.Repositories
{
    // Bütün sınıflarda ve interface lerde geçerli mimari yapı kuruyoruz. Tek bir generic repository ile kod tekrarından kurtuluyoruz. İşlemler düzenli olacak. Böl parçala yönet ile proje geliştir.
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        Context c = new Context();

        DbSet<T> _object; // T değerine karşılık gelen sınıfı tutuyor.

        // T değerine karşılık olarak gelecek sınıfı bulmak için Constructor (yapıcı method) bulup objemize değer ataması yapacaz.
        public GenericRepository()
        {
            _object = c.Set<T>(); // object isimli field ım dışarıdan gönderdiğim entity neyse o olacak.
        }

        public void Delete(T p)
        {
            _object.Remove(p);
            c.SaveChanges();
        }

        public void Insert(T p)
        {
            _object.Add(p);
            c.SaveChanges();
        }

        public List<T> List()
        {
            return _object.ToList();
        }

        public List<T> List(Expression<Func<T, bool>> filter)
        {
            return _object.Where(filter).ToList();
        }

        public void Update(T p)
        {
            c.SaveChanges();
        }
    }
}
