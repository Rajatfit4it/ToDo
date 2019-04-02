using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contracts
{
    public  interface IUnitOfWork
    {
        ICategoryData CategoryData { get; set; }
        
        Task SaveChangesAsync();
    }
}
