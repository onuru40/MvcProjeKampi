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

    // IRepository de yapılan method tanımlarının içini burada dolduruyoruz.
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
            var deletedEntity = c.Entry(p);
            deletedEntity.State = EntityState.Deleted;
            _object.Remove(p);
            c.SaveChanges();
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return _object.SingleOrDefault(filter); //  Bir dizide veya listede sadece 1 tane değer geriye döndürmek için kullanılan Entity Framework Linq metodudur.
        }

        public void Insert(T p)
        {
            var addedEntity = c.Entry(p); // Entity state ile ekleyeceğim değeri addedEntity isimli değişkenin içine atıyoruz.
            addedEntity.State = EntityState.Added;
            //_object.Add(p);
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
            var updatedEntity = c.Entry(p);
            updatedEntity.State = EntityState.Modified;
            c.SaveChanges();
        }
    }
}
