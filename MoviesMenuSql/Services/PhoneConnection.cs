using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesMenuSql.Services
{
    internal class PhoneConnection : IDisposable
    {
        public bool IsOpened { get; set; }
        public PhoneConnection()
        {
            IsOpened = true;
        }
        public void Dispose()
        {
            IsOpened = false;
        }
    }
}
