using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

namespace EyeLink.Edf {
    [StructLayout(LayoutKind.Sequential, Pack = Config.PACK)]
    public class FSample : EdfData{
        private System.UInt32 m_Time;   /*!< time stamp of sample */
        /*INT16  type; */   /* always SAMPLE_TYPE */ 

        [MarshalAs (UnmanagedType.ByValArray, SizeConst=2)]
        public float[]  px;   /*!< pupil x */
        [MarshalAs (UnmanagedType.ByValArray, SizeConst=2)]
        public float[]  py;   /*!< pupil y */
        [MarshalAs (UnmanagedType.ByValArray, SizeConst=2)]
        public float[]  hx;   /*!< headref x */
        [MarshalAs (UnmanagedType.ByValArray, SizeConst=2)]
        public float[]  hy;   /*!< headref y */
        [MarshalAs (UnmanagedType.ByValArray, SizeConst=2)]
        public float[]  pa;    /*!< pupil size or area */

        [MarshalAs (UnmanagedType.ByValArray, SizeConst=2)]
        public float[] gx;    /*!< screen gaze x */
        [MarshalAs (UnmanagedType.ByValArray, SizeConst=2)]
        public float[] gy;    /*!< screen gaze y */
        public float rx;       /*!< screen pixels per degree */
        public float ry;       /*!< screen pixels per degree */

        [MarshalAs (UnmanagedType.ByValArray, SizeConst=2)]
        public float[] gxvel;  /*!< gaze x velocity */
        [MarshalAs (UnmanagedType.ByValArray, SizeConst=2)]
        public float[] gyvel;  /*!< gaze y velocity */
        [MarshalAs (UnmanagedType.ByValArray, SizeConst=2)]
        public float[] hxvel;  /*!< headref x velocity */
        [MarshalAs (UnmanagedType.ByValArray, SizeConst=2)]
        public float[] hyvel;  /*!< headref y velocity */
        [MarshalAs (UnmanagedType.ByValArray, SizeConst=2)]
        public float[] rxvel;  /*!< raw x velocity */
        [MarshalAs (UnmanagedType.ByValArray, SizeConst=2)]
        public float[] ryvel;  /*!< raw y velocity */

        [MarshalAs (UnmanagedType.ByValArray, SizeConst=2)]
        public float[] fgxvel; /*!< fast gaze x velocity */
        [MarshalAs (UnmanagedType.ByValArray, SizeConst=2)]
        public float[] fgyvel; /*!< fast gaze y velocity */
        [MarshalAs (UnmanagedType.ByValArray, SizeConst=2)]
        public float[] fhxvel; /*!< fast headref x velocity */
        [MarshalAs (UnmanagedType.ByValArray, SizeConst=2)]
        public float[] fhyvel; /*!< fast headref y velocity */
        [MarshalAs (UnmanagedType.ByValArray, SizeConst=2)]
        public float[] frxvel; /*!< fast raw x velocity */
        [MarshalAs (UnmanagedType.ByValArray, SizeConst=2)]
        public float[] fryvel; /*!< fast raw y velocity */

        [MarshalAs (UnmanagedType.ByValArray, SizeConst=8)]
        System.Int16 [] hdata;  /*!< head-tracker data (not pre-scaled) */
        System.UInt16 flags;     /*!<  flags to indicate contents */

        //UINT16 status;       /* tracker status flags    */ 
        System.UInt16 input;     /*!< extra (input word) */
        System.UInt16 buttons;   /*!< button state & changes */

        System.Int16  htype;     /*!< head-tracker data type (0=none) */

        System.UInt16 errors;    /*!< process error flags */

        public uint time{
            get{
                return m_Time;
            }
        }
    }
}
