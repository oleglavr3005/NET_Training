using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ConsoleApplication1;
using log4net;

namespace BusinessLogic
{
    class AggregatedCalculations
    {
        private DbContext ctx;
        private log4net.ILog logger;

        public AggregatedCalculations()
        {
            ctx = new ProductsContext();
            logger= LogManager.GetLogger(typeof(Program));
        }

        public AggregatedCalculations(DbContext dbctx, ILog log)
        {
            ctx = dbctx;
            logger = log;
        }

    }
}
