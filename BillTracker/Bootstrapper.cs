using System.Web.Mvc;
using BillTracker.Models;
using BillTracker.Services;
using BillTracker.ViewModels.Mapper;
using Microsoft.Practices.Unity;
using Unity.Mvc4;

namespace BillTracker
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            IUnityContainer container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();    

            container.RegisterType<IBillContext, BillContext>()
                .RegisterType<IBillModelMapper, BillModelMapper>()
                .RegisterType<IBillService, BillService>()
                .RegisterType<IWebSecurityWrapper, WebSecurityWrapper>()
                .RegisterType<IFrequencyMapper, FrequencyMapper>()
                .RegisterType<IPaymentScheduleService, PaymentScheduleService>()
                .RegisterType<IEventSummaryMapper, EventSummaryMapper>();

            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
        }
    }
}