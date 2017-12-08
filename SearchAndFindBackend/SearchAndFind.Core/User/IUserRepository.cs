using SearchAndFind.Entities;
using System;

namespace SearchAndFind.Core
{
    public interface IUserRepository<T> : IRepository<T> where T : class
    {
        User GetUserByMail(string userMail);
        User GetUserByCurrentToken(string currentToken);
        void AddCategoryOnUser(Guid idUser, Category currentCategory);
        void RemoveCategoryFromUser(Guid idUser, Category currentCategory);
    }
}
