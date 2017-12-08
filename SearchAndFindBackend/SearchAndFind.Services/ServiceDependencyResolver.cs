using SearchAndFind.DependencyResolver;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAndFind.Services
{
    [Export(typeof(IComponent))]
    public class ServiceDependencyResolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<ICategoryService, CategoryService>();
            registerComponent.RegisterType<IClientService, ClientService>();
            registerComponent.RegisterType<ISalerService, SalerService>();
            registerComponent.RegisterType<IQueryService, QueryService>();
            registerComponent.RegisterType<IReviewService, ReviewService>();
            registerComponent.RegisterType<ITenderService, TenderService>();
            registerComponent.RegisterType<ICloudMessageSender, CloudMessageSender>();
        }
    }
}
