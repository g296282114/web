using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using torsion.Models;

namespace torsion.Domain.Repository
{
    public class UserRepository : BaseRepository<UserProfile>
    {
        public override UserProfile Find(int id)
        {
            return dbContext.UserProfiles.SingleOrDefault(u => u.UserId == id);
        }
    }
}