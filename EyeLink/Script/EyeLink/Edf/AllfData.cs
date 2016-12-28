using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

namespace EyeLink.Edf{
    public class AllfData : EdfData{
        private System.IntPtr m_Ptr;

        public AllfData(System.IntPtr ptr){
            m_Ptr = ptr;
        }

        public FEvent fe{
            get{
                FEvent rec;

                if (m_Ptr.ToInt64()!=0) {
                    rec = new FEvent();
                    Marshal.PtrToStructure(m_Ptr, rec);
                } else {
                    rec = null;
                }
                return rec;

            }
        }

        public IMessage im{
            get{
                IMessage rec;

                if (m_Ptr.ToInt64()!=0) {
                    rec = new IMessage();
                    Marshal.PtrToStructure(m_Ptr, rec);
                } else {
                    rec = null;
                }
                return rec;

            }
        }

        public IOEvent io{
            get{
                IOEvent rec;

                if (m_Ptr.ToInt64()!=0) {
                    rec = new IOEvent();
                    Marshal.PtrToStructure(m_Ptr, rec);
                } else {
                    rec = null;
                }
                return rec;

            }
        }

        public FSample fs{
            get{
                FSample rec;

                if (m_Ptr.ToInt64()!=0) {
                    rec = new FSample();
                    Marshal.PtrToStructure(m_Ptr, rec);
                } else {
                    rec = null;
                }
                return rec;

            }
        }

        public Recordings rec{
            get{
                Recordings rec;

                if (m_Ptr.ToInt64()!=0) {
                    rec = new Recordings();
                    Marshal.PtrToStructure(m_Ptr, rec);
                } else {
                    rec = null;
                }
                return rec;

            }
        }

        public uint time{
            get{
                return (uint)Marshal.ReadInt32(m_Ptr);
            }
        }

    }
}
