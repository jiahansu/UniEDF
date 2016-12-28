using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

namespace EyeLink.Edf {
    [StructLayout(LayoutKind.Sequential, Pack = Config.PACK)]
    public class IOEvent : EdfData{
        private System.UInt32 m_Time;       /* time logged */
        public System.Int16  type;       /* event type: */

        public System.UInt16 data;       /* coded event data */

        public uint time{
            get{
                return m_Time;
            }
        }
    }
}
