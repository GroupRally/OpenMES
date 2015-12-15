using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResourceIntegrator
{
    public enum AuthenticationType
    {
        PlainText = 0,
        X509Certificate = 1,
        Kerberos = 2,
        NTLM = 3,
        Negociate = 4,
        Custom = 5
    }
}
