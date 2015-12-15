using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceIntegrator
{
    public class Authentication
    {
        public virtual AuthenticationType Type
        {
            get;
            set;
        }

        public virtual string Identifier
        {
            get;
            set;
        }

        public virtual string Password
        {
            get;
            set;
        }

    }
}

