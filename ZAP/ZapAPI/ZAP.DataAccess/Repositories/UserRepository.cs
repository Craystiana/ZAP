using System;
using System.Collections.Generic;
using ZAP.DataAccess.Entities;

namespace ZAP.DataAccess.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(Context context) : base(context) { }

    }
}
