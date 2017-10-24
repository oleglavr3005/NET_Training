using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO
{
    class ClientDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public ClientDTO()
        {
            IsDeleted = false;
        }
    }
}
