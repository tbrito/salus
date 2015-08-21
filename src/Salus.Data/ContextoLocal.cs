using System;
using System.Collections;

namespace Salus.Data
{
    public class ContextoLocal
    {
        [ThreadStatic]
        public static Hashtable Items = new Hashtable();
    }
}