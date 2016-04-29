using PublicBookStore.API.Initializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicBookStore.API.App_Start
{
    public static class DataConfig
    {
        public static void Initialize()
        {
            System.Data.Entity.Database.SetInitializer(new PublicBookStoreSampleData());
        }
    }
}
