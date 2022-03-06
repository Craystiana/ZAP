using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ZAP.Common.Enums;
using ZAP.DataAccess.Entities;
using ZAP.DataAccess.Interfaces;

namespace ZAP.UnitTests.TestRepositories
{
    public class TestUserRepository : IRepository<User>
    {
        private readonly List<User> _users;

        public TestUserRepository()
        {
            _users = new List<User>
            {
                new User
                {
                    UserId = 1,
                    EmailAddress = "admin@zap.com",
                    FirstName = "Admin",
                    LastName = "1",
                    UserRoleId = (int)UserRoleType.Admin,
                    Address = "Test address"
                }
            };
        }

        public void Add(User user)
        {
            _users.Add(user);
        }

        public void AddRange(IEnumerable<User> users)
        {
            _users.AddRange(users);
        }

        public IEnumerable<User> Find(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            return _users.FirstOrDefault(u => u.UserId == id);
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public void Remove(User user)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<User> users)
        {
            throw new NotImplementedException();
        }

        public User SingleOrDefault(Expression<Func<User, bool>> predicate)
        {
            return _users.SingleOrDefault(predicate.Compile());
        }
    }
}
