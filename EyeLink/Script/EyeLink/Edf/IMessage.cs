using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

namespace EyeLink.Edf {
    [StructLayout(LayoutKind.Sequential, Pack = Config.PACK)]
    public class IMessage : EdfData{
        private System.UInt32 m_Time;       /* time message logged */
        public System.Int16  type;       /* event type: usually MESSAGEEVENT */
        public System.UInt32 length;     /* length of message */

        [MarshalAs (UnmanagedType.ByValTStr, SizeConst=260)]
        public string text;  /* message contents (max length 255) */

        public uint time{
            get{
                return m_Time;
            }
        }
    }
}
