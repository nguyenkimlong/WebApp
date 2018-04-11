using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Infrastructure
{
    interface IUnitOfWork
    {
      
        /// <summary>
        /// Commits all changes
        /// </summary>
        void SaveChanges();
        /// <summary>
        /// Discards all changes that has not been commited
        /// </summary>
        void RejectChanges();
        void Dispose();

















    }
}
