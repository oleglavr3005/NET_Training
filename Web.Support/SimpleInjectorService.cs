using ConsoleApplication1;
using BusinessLogic;
using log4net;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Support
{
    public static class SimpleInjectorService
    {
        public static ProductsContext dbctx;
        public static void RegisterServices(this Container container)
        {
            container.RegisterSingleton<ILog>(() =>
              log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().ToString())
            );
            container.RegisterSingleton<DbContext, ProductsContext>();
            container.Register<AggregatedCalculations>();
            container.Register<Client>();
    //     dbctx = container.GetInstance<ProductsContext>();
          

        }
    }
}
