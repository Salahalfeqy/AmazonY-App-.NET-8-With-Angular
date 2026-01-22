using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonY.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get;  }
        public IPhotoRepository PhotoRepository { get;  }
        public IProductRepository ProductRepository { get;  }
    }
}
