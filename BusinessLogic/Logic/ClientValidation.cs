using ConsoleApplication1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Logic
{
    public class ClientValidation
    {
        public bool IsSuccessful { get; set; }
        public string SuccessMessage { get; set; }
        public string WarningMessage { get; set; }

        public string ErrorMessage { get; set; }

        public ClientValidation()
        {
            IsSuccessful = false;
            SuccessMessage = "Client was created.";
            WarningMessage = "Client was created, with warnings.";
            ErrorMessage = "Client wasn't created.";
        }

        public bool IsValidate(Client client)
        {

            if (client.Name==null || client.Name.Equals("")) return false;
            ProductsContext ctx = new ProductsContext();
            if (ctx.Clients.Any(c=>c.Name==client.Name))
            if (client.Name.Equals("")) return false;
            if (ctx.Clients.Any(c => c.Name == client.Name))
                return false;
            IsSuccessful = true;
            return true;
        }

    }
}
