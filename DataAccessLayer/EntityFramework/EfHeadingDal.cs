using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfHeadingDal : GenericRepository<Heading>, IHeadingDal // Katmanları haberleştiriyoruz. Sınıf ve interface lerde tanımlanan değerleri EfHeadingDal sınıfına miras alıyoruz.
    {
    }
}
