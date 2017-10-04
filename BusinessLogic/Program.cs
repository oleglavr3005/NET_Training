using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;

namespace BusinessLogic
{
  class Program
    {
        static Container container;
            static Program()
        {
            container = new Container();

            // 2. Configure the container (register)
            container.Register<IOrderRepository, SqlOrderRepository>();
            container.Register<ILogger, FileLogger>(Lifestyle.Singleton);
            container.Register<CancelOrderHandler>();

            // 3. Verify your configuration
            container.Verify();
        }
        static void Main(string[] args)
        {

        }
    }
}
