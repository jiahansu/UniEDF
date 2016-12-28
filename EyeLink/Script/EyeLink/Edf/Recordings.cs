using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

namespace EyeLink.Edf{
    [StructLayout (LayoutKind.Sequential, Pack = Config.PACK)]
    public class Recordings: EdfData{
        public System.UInt32 m_Time;        /*!< start time or end time*/
        public float sample_rate;  /*!< 250 or 500 or 1000*/
        public System.UInt16 eflags;      /*!< to hold extra information about events */
        public System.UInt16 sflags;      /*!< to hold extra information about samples */
        public System.Byte state;         /*!< 0 = END, 1=START */
        public System.Byte record_type;   /*!< 1 = SAMPLES, 2= EVENTS, 3= SAMPLES and EVENTS*/
        public System.Byte pupil_type;    /*!< 0 = AREA, 1 = DIAMETER*/
        public System.Byte recording_mode;/*!< 0 = PUPIL, 1 = CR */
        public System.Byte filter_type;   /*!< 1,2,3 */   
        public System.Byte  pos_type;     /*!<0 = GAZE, 1= HREF, 2 = RAW*/  /*PARSEDBY_GAZE  PARSEDBY_HREF PARSEDBY_PUPIL*/
        public System.Byte eye;           /*!< 1=LEFT, 2=RIGHT, 3=LEFT and RIGHT */

        public uint time{
            get{
                return m_Time;
            }
        }
    }
}
