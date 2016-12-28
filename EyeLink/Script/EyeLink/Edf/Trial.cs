using System.Runtime.InteropServices;

namespace EyeLink.Edf{
    [StructLayout (LayoutKind.Sequential, Pack = Config.PACK)]
    public class Trial{
        private System.IntPtr m_Rec;  /*!<recording information about the current trial*/
        public System.UInt32    duration;  /*!<duration of the current trial */
        public System.UInt32    starttime; /*!<start time of the trial */
        public System.UInt32    endtime;   /*!<end time of the trial */

        public Recordings rec{
            get{
                Recordings rec;

                if (m_Rec.ToInt64()!=0) {
                    rec = new Recordings();
                    Marshal.PtrToStructure(m_Rec, rec);
                } else {
                    rec = null;
                }
                return rec;
            }
        }
    }
}
