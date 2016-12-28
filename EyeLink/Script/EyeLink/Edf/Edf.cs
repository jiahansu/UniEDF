

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

using EDFFILE=System.IntPtr;
using System;
using System.Text;
    
namespace EyeLink.Edf{

    public class Edf {
        public delegate void LogCallbackDelegate(string text);

        [DllImport(Config.NATIVE_PATH)]
        private static extern System.IntPtr  edf_get_version();
        [DllImport(Config.NATIVE_PATH)]
        private static extern EDFFILE edf_open_file([MarshalAs(UnmanagedType.LPStr)]String s, int consistency, int loadevents, int loadsamples, ref int errval);
        [DllImport(Config.NATIVE_PATH)]
        private static extern System.Int32 edf_close_file(EDFFILE ef);
        [DllImport(Config.NATIVE_PATH)]
        private static extern System.Int32  edf_get_trial_count(EDFFILE edf);
        [DllImport(Config.NATIVE_PATH)]
        private static extern System.Int32 edf_jump_to_trial(EDFFILE edf, System.Int32 trial);
        [DllImport(Config.NATIVE_PATH)]
        private static extern int edf_get_trial_header(EDFFILE edf, Trial trial);
        [DllImport(Config.NATIVE_PATH)]
        private static extern int edf_goto_previous_trial(EDFFILE edf);
        [DllImport(Config.NATIVE_PATH)]
        private static extern int edf_goto_next_trial(EDFFILE edf);
        [DllImport(Config.NATIVE_PATH)]
        private static extern System.Int32 edf_get_next_data(EDFFILE ef);
        [DllImport(Config.NATIVE_PATH)]
        private static extern System.IntPtr edf_get_float_data(EDFFILE ef);
        [DllImport(Config.NATIVE_PATH)]
        private static extern System.IntPtr edf_get_sample_close_to_time(EDFFILE ef, System.UInt32 time);
        [DllImport(Config.NATIVE_PATH)]
        private static extern System.UInt32 edf_get_element_count(EDFFILE ef);
        [DllImport(Config.NATIVE_PATH)]
        private static extern System.Int32 edf_get_preamble_text_length(EDFFILE edf);
        [DllImport(Config.NATIVE_PATH)]
        private static extern System.Int32 edf_get_revision(EDFFILE ef);
        [DllImport(Config.NATIVE_PATH)]
        private static extern System.Int32 edf_get_preamble_text(EDFFILE ef, StringBuilder text, System.Int32 length);
        [DllImport(Config.NATIVE_PATH)]
        private static extern void edf_set_log_function(LogCallbackDelegate pointer);

        public static void CheckError(System.IntPtr error, string message = null){
            int v = Marshal.ReadInt32(error);

            if ( v != 0) {
                if (string.IsNullOrEmpty(message)) {
                    throw new UnityException("Fail to invoke Edf function, error: "+v);
                } else {
                    throw new UnityException(message+", error: "+v);
                }
            }

        }

        public static void CheckError(int v, string message = null){
            if ( v != 0) {
                if (string.IsNullOrEmpty(message)) {
                    throw new UnityException("Fail to invoke Edf function, error: "+v);
                } else {
                    throw new UnityException(message+", error: "+v);
                }
            }

        }

        public static string version{
            get{
                return Marshal.PtrToStringAuto(edf_get_version());
            }
        }

        public static EDFFILE OpenFile(string path, int consistency, int loadevents, int loadsamples){
            int error = 0;

            EDFFILE fp = edf_open_file(path, consistency, loadevents, loadsamples,ref error);

            CheckError(error, "Fail to open file: "+path);

            return fp;
        }

        public static void CloseFile(EDFFILE file){
            CheckError(edf_close_file(file), "Fail to closefile");
        }

        public static int GetTrialCount(EDFFILE file){
            return edf_get_trial_count(file);
        }

        public static bool JumpToTrial(EDFFILE file, int index){
            return  edf_jump_to_trial(file, index) == 0;
        }

        public static bool GoToNextTrial(EDFFILE file){
            return  edf_goto_next_trial(file) == 0;
        }

        public static bool GoToPreviousTrial(EDFFILE file){
            return  edf_goto_previous_trial(file) == 0;
        }

        public static Trial GetTrialHeader(EDFFILE file){
            Trial trial = new Trial();

            CheckError(edf_get_trial_header(file, trial), "Fail to get trial header");

            return trial;
        }

        public static Constant.EventType GetNextData(EDFFILE file){
            return (Constant.EventType)Enum.ToObject(typeof(Constant.EventType), edf_get_next_data(file));
        }

        public static AllfData GetFloatData(EDFFILE file){
            System.IntPtr ptr = edf_get_float_data(file);
            AllfData data;

            if (ptr.ToInt64() != 0) {
                data = new AllfData(ptr);
            } else {
                data = null;
            }

            return data;
        }

        public static AllfData GetSampleCloseToTime(EDFFILE file, uint time){
            System.IntPtr ptr = edf_get_sample_close_to_time(file, time);
            AllfData data;

            if (ptr.ToInt64() != 0) {
                data = new AllfData(ptr);
            } else {
                data = null;
            }

            return data;
        }

        public static uint GetElementCount(EDFFILE file){
            return edf_get_element_count(file);
        }

        public static string GetPreambleText(EDFFILE file){
            int length = edf_get_preamble_text_length(file); 
            StringBuilder sb = new StringBuilder(length);

            edf_get_preamble_text(file, sb, length);

            return sb.ToString();
        }

        public static int GetRevision(EDFFILE file){
            return edf_get_revision(file);
        }

        public static void SetLogCallback(Action<string> callback){
            if (callback != null) {
                edf_set_log_function(new LogCallbackDelegate(callback));
            } else {
                edf_set_log_function(null);
            }

        }
    }

}
