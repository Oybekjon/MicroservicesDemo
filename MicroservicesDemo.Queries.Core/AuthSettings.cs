using MicroservicesDemo.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesDemo.Queries
{
    public class AuthSettings : ILockable
    {
        private bool valuesLocked;
        private string secret;
        private string issuer;
        private double expirationInMinutes;
        private string audience;

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

        public double ExpirationInMinutes
        {
            get => expirationInMinutes;
            set
            {
                if (valuesLocked)
                {
                    throw new NotAllowedException("Property update is not allowed");
                }
                expirationInMinutes = value;
            }
        }

        public string Issuer
        {
            get => issuer;
            set
            {
                if (valuesLocked)
                {
                    throw new NotAllowedException("Property update is not allowed");
                }
                issuer = value;
            }
        }

        public string Audience
        {
            get => audience;
            set
            {
                if (valuesLocked)
                {
                    throw new NotAllowedException("Property update is not allowed");
                }
                audience = value;
            }
        }

        public void LockValues()
        {
            valuesLocked = true;
        }
    }
}
