using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

namespace EyeLink.Edf{
    [StructLayout (LayoutKind.Sequential, Pack = Config.PACK)]
    public class LString {
        public System.Int16 len;
        public System.Char c;

    }
}
