using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using torsion.Models;

namespace torsion.Domain.Repository
{
    public class ExtraUserInfoRepository : BaseRepository<ExtraUserInfo>
    {
        public override ExtraUserInfo Find(int id)
        {
            return dbContext.ExtraUserInfos.SingleOrDefault(u => u.Id == id);
        }
    }
}