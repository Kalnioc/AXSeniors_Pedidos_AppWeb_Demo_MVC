using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using AXSeniors_Pedidos_Demo_BUSINESS;
using AXSeniors_Pedidos_Demo_BUSINESS.Interfaces;

namespace AXSeniors_Pedidos_Demo_MVC
{
    public static class UnityConfig
    {
        public static IUnityContainer Container { get; private set; }

        public static void RegisterComponents()
        {
            Container = new UnityContainer();

            Container.RegisterType<IConsultaBL, ConsultaBL>();
            Container.RegisterType<IInsertBL, InsertBL>();
            Container.RegisterType<IUpdateBL, UpdateBL>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(Container));
        }
    }
}
