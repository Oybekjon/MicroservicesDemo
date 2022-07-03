using MicroservicesDemo.Errors;
using MicroservicesDemo.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesDemo.Users.Queries.Settings
{
    public class HashConfig : ILockable
    {
        private bool valuesLocked;
        private string secret;
        public string Secret
        {
            get => secret;
            set
            {
                if (valuesLocked)
                {
                    throw new NotAllowedException("Property update is not allowed");
                }
                secret = value;
            }
        }
        public void LockValues()
        {
            valuesLocked = true;
        }
    }
}
