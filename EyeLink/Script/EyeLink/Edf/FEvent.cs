using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

namespace EyeLink.Edf{
    [StructLayout (LayoutKind.Sequential, Pack = Config.PACK)]
    public class FEvent : EdfData{
        private System.UInt32 m_Time;       /*!< effective time of event */
        private System.Int16  m_Type;       /*!< event type */
        public System.UInt16 read;       /*!< flags which items were included */


        public System.UInt32 sttime;   /*!< start time of the event */
        public System.UInt32 entime;   /*!< end time of the event*/
        public float  hstx;     /*!< headref starting points */ 
        public float  hsty;     /*!< headref starting points */
        public float  gstx;     /*!< gaze starting points */
        public float  gsty;     /*!< gaze starting points */
        public float  sta;      /*!< pupil size at start */
        public float  henx;     /*!<  headref ending points */
        public float  heny;     /*!<  headref ending points */
        public float  genx;     /*!< gaze ending points */
        public float  geny;     /*!< gaze ending points */
        public float  ena;       /*!< pupil size at end */
        public float  havx;     /*!< headref averages */
        public float  havy;     /*!< headref averages */
        public float  gavx;     /*!< gaze averages */
        public float  gavy;     /*!< gaze averages */
        public float  ava;       /*!< average pupil size */
        public float  avel;     /*!< accumulated average velocity */
        public float  pvel;     /*!< accumulated peak velocity */
        public float  svel;     /*!< start velocity */
        public float evel;      /*!< end velocity */
        public  float  supd_x;   /*!< start units-per-degree */
        public float  eupd_x;   /*!< end units-per-degree */
        public float  supd_y;   /*!< start units-per-degree */
        public float  eupd_y;   /*!< end units-per-degree */

        public System.Int16  eye;        /*!< eye: 0=left,1=right */
        public System.UInt16 status;           /*!< error, warning flags */
        public System.UInt16 flags;           /*!< error, warning flags*/
        public System.UInt16 input;
        public System.UInt16 buttons;
        public System.UInt16 parsedby;       /*!< 7 bits of flags: PARSEDBY codes*/
        private System.IntPtr m_message;       /*!< any message string*/

        public string message{
            get{
                string message;

                if (m_message.ToInt64()!=0) {
                    IntPtr stringPtr = new IntPtr(m_message.ToInt64()+sizeof(Int16));
                    LString rec = new LString();
                    Marshal.PtrToStructure(m_message, rec);

                    message =  Marshal.PtrToStringAnsi(stringPtr,rec.len);
                } else {
                    message = null;
                }
                return message;
            }

        }

        public EventType type{
            get{
                return (EventType)Enum.ToObject(typeof(EventType), m_Type);
            }

        }

        public uint time{
            get{
                return m_Time;
            }
        }
    }
}
