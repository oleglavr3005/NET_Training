using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;
using log4net.Core;
using log4net.Repository.Hierarchy;
using log4net;
using ConsoleApplication1;
using System.Data.Entity;

namespace BusinessLogic
{
  class Program
    {
        static Container container;
            static Program()
        {
            container = new Container();
            // 2. Configure the container (register)
            container.Register<DbContext, ProdContext>();
            //      container.Register<ILog>(LogManager.GetLogger(typeof(Program)));
            try
            {
                container.Register<ILog, LogImpl>(Lifestyle.Singleton);
                container.Register<AggregatedCalculations, AggregatedCalculations>(Lifestyle.Singleton);
            }
            catch (Exception e)
            { Console.WriteLine(e.InnerException);Console.ReadKey(); }
            // 3. Verify your configuration
        //    container.Verify();
        }
        static void Main(string[] args)
        {
            var handler = container.GetInstance<AggregatedCalculations>();
            handler.ToString();
        }
    }
}
