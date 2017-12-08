using SearchAndFind.DependencyResolver;
using SearchAndFind.DTO;
using SearchAndFind.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Core
{
    [Export(typeof(IComponent))]
    public class CoreEntityResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<ICategoryManager, CategoryManager>();
            registerComponent.RegisterType<AbstractUserManager<ClientRequest>, ClientManager>();
            registerComponent.RegisterType<AbstractUserManager<SalerRequest>, SalerManager>();
            registerComponent.RegisterType<ISalerManager, SalerManager>();
            registerComponent.RegisterType<IQueryManager, QueryManager>();
            registerComponent.RegisterType<IReviewManager, ReviewManager>();
            registerComponent.RegisterType<ITenderManager, TenderManager>();
            registerComponent.RegisterType<IDTOBuilder<ClientDTO, Client>, ClientDTOBuilder>();
            registerComponent.RegisterType<IDTOBuilder<ReviewDTO, Review>, ReviewDTOBuilder>();
            registerComponent.RegisterType<IDTOBuilder<QueryDTO, Query>, QueryDTOBuilder>();
            registerComponent.RegisterType<IDTOBuilder<FullSalerDTO, Saler>, FullSalerDTOBuilder>();
            registerComponent.RegisterType<IDTOBuilder<CategoryDTO, Category>, CategoryDTOBuilder>();
            registerComponent.RegisterType<IDTOBuilder<TenderDTO, Tender>, TenderDTOBuilder>();

        }
    }
}
