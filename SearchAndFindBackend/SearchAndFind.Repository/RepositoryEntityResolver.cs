using SearchAndFind.Core;
using SearchAndFind.DependencyResolver;
using SearchAndFind.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Repository
{
    [Export(typeof(IComponent))]
    public class RepositoryEntityResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<ICategoryRepository, CategoryRepository>();
            registerComponent.RegisterType<IUserRepository<Client>, ClientRepository>();
            registerComponent.RegisterType<IUserRepository<Saler>, SalerRepository>();
            registerComponent.RegisterType<ISalerRepository, SalerRepository>();
            registerComponent.RegisterType<IQueryRepository, QueryRepository>();
            registerComponent.RegisterType<ITenderRepository, TenderRepository>();
            registerComponent.RegisterType<IReviewRepository, ReviewRepository>();
        }
    }
}
