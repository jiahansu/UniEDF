using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

namespace EyeLink.Edf{
    public class EdfFile{
        private string m_Path;
        private System.IntPtr m_EdfFile;
        private Trial m_CurrentTrial = null;
        private int m_CurrentTrialIndex = -1;
        private string m_FileName;
        private Dictionary<int, List<string>> m_DrawList = new Dictionary<int, List<string>>();
        private Rect m_GazeCoords = new Rect();

        static EdfFile(){
            //Edf.SetLogCallback(text => Debug.LogWarning("Edf log message: " + text));
        }

        protected EdfFile(string path){
            //int index = path.LastIndexOfAny(new char[]{ '/', '\\' });

            m_Path = path;
            m_EdfFile = Edf.OpenFile(this.m_Path, 1, 1, 1);

            m_FileName = Path.GetFileName(path);


        }

        public string path{
            get{
                return m_Path;
            }
        }

        private void SearchGazeCoords(){
            Constant.EventType type;
            EdfData dv;
            string message;

            while ((dv = TrialNextEvent(out type, Constant.EventType.MESSAGEEVENT)) != null) {
                message = (dv as AllfData).fe.message;

                if (!string.IsNullOrEmpty(message) && message.StartsWith("GAZE_COORDS")) {
                    string[] array = message.Split(new char[]{ ' ' });
                    float x = float.Parse(array [1]);
                    float y = float.Parse(array [2]);
                    m_GazeCoords.Set(x,y,float.Parse(array[3])+1.0f-x,float.Parse(array[4])+1.0f-y);
                    //Debug.Log("Find: "+m_GazeCoords);
                    break;
                }
            }

            Edf.JumpToTrial(m_EdfFile, m_CurrentTrialIndex);
        }

        public string workingDirectory{
            get{
                return Path.GetDirectoryName(m_Path);
            }
        }

        public Rect currentTrialGazeCoords{
            get{
                return m_GazeCoords;
            }
        }

        public static EdfFile Open(string path){
            EdfFile file = null;

            try{
                file = new EdfFile(path);
            }catch(UnityException ue){
                Debug.LogException(ue);
            }

            return file;
        }

        public static void Close(EdfFile file){
            if (file.m_EdfFile.ToInt64() != 0) {
                Edf.CloseFile(file.m_EdfFile);
            } else {
                throw new UnityException("Invalid edf file pointer");
            }
        }

        public int numTrials{
            get{
                return Edf.GetTrialCount(m_EdfFile);
            }
        }

        public string fileName{
            get{
                return m_FileName;
            }
        }

        public string id{
            get{
                return Path.GetFileNameWithoutExtension(m_FileName);
            }
        }

        private void TrialIndexUpdated(){
            m_CurrentTrial = Edf.GetTrialHeader(m_EdfFile);

            if (!m_DrawList.ContainsKey(m_CurrentTrialIndex)) {
                List<string> commands = new List<string>();

                m_DrawList.Add(m_CurrentTrialIndex, commands);

                try{
 
                    string path = Path.Combine(m_Path, "../../../runtime/dataviewer/"+id+"/graphics/VC_"+(m_CurrentTrialIndex+1)+".vcl");
                    Debug.Log(Path.GetFullPath(path));
                    StreamReader reader = new StreamReader(Path.GetFullPath(path), true);
                    string line;

                    using (reader) {
                        while((line=reader.ReadLine())!=null){
                            commands.Add(line);
                            //Debug.Log(line);
                        }
                    }

                    //Debug.Log("Draw list:"+commands);
                }catch(Exception e){
                    

                    Debug.LogException(e);
                }
            }

            SearchGazeCoords();
        }



        public bool MoveToTrial(int index){
            bool b;

            if (Edf.JumpToTrial(m_EdfFile, index)) {
                m_CurrentTrialIndex = index;

                TrialIndexUpdated();

                b = true;
            } else {
                m_CurrentTrialIndex = -1;
                b = false;
            }

            return b;
        }

        public IList<string> currentTrialDrawList{
            get{
                if (m_DrawList.ContainsKey(m_CurrentTrialIndex)) {
                    return m_DrawList [m_CurrentTrialIndex].AsReadOnly();
                } else {
                    return new List<string>();
                }
            }
        }

        public int currentTrialIndex{
            get{
                return m_CurrentTrialIndex;
            }
        }

        public Trial currentTrialInfo{
            get{
                return m_CurrentTrial;
            }
        }

        public bool NextTrial(){
            if (Edf.GoToNextTrial(m_EdfFile)) {
                m_CurrentTrialIndex++;
                TrialIndexUpdated();

                return true;
            } else {
                return false;
            }
        }

        public bool PreviousTrial(){
            if (Edf.GoToPreviousTrial(m_EdfFile)) {
                m_CurrentTrialIndex--;
                TrialIndexUpdated();

                return true;
            } else {
                return false;
            }
        }


        public bool MoveToCurrentTrial(){
            return MoveToTrial(m_CurrentTrialIndex);

        }

        public EdfData TrialNextEvent(out Constant.EventType returnType, Constant.EventType filterType= Constant.EventType.ANY_TYPE){
            return TrialNextData(out returnType,filterType, true);
        }

        private EdfData TrialNextData(out Constant.EventType returnType,Constant.EventType filterType = Constant.EventType.ANY_TYPE, bool skipSample= false){
            //Constant.EventType type;
            bool done = false;
            EdfData ev = null;
            int i=0;
            uint trialEndTime = m_CurrentTrial.endtime;
            do{
                //console.log("Type: ", type);
                returnType = Edf.GetNextData(m_EdfFile);
                ++i;
                //console.log("Type: ", type);

                if(filterType==Constant.EventType.ANY_TYPE || returnType==filterType || returnType==Constant.EventType.RECORDING_INFO){
                    switch (returnType) {
                        case Constant.EventType.STARTBLINK:
                        case Constant.EventType.STARTSACC:
                        case Constant.EventType.STARTFIX:
                        case Constant.EventType.STARTPARSE:
                        case Constant.EventType.ENDBLINK:
                        case Constant.EventType.ENDSACC:
                        case Constant.EventType.ENDFIX:
                        case Constant.EventType.ENDPARSE:
                        case Constant.EventType.FIXUPDATE:
                        case Constant.EventType.BREAKPARSE:
                        case Constant.EventType.BUTTONEVENT:
                        case Constant.EventType.INPUTEVENT:
                        case Constant.EventType.MESSAGEEVENT:
                        case Constant.EventType.RECORDING_INFO:
                            ev = Edf.GetFloatData(m_EdfFile);

                            if(returnType==Constant.EventType.RECORDING_INFO){
                                if(filterType==Constant.EventType.ANY_TYPE || returnType==filterType){
                                    done = true;
                                    /*
                                    if(returnType==Constant.EventType.MESSAGEEVENT){
                                        Debug.Log((ev as AllfData).fe.message);
                                    }*/
                                }else{
                                    if (ev.time > trialEndTime) {
                                        done = true;
                                        ev = null;
                                    }
                                }
                            }else{
                                /*
                                if(returnType == Constant.EventType.STARTFIX){
                                    Debug.Log("Find xxx");
                                }*/
                                done = true;
                            }




                            break;
                        case Constant.EventType.STARTSAMPLES:
                        case Constant.EventType.STARTEVENTS:
                        case Constant.EventType.ENDSAMPLES:
                        case Constant.EventType.ENDEVENTS:
                            done = true;
                            ev = Edf.GetFloatData(m_EdfFile);

                            break;
                        case Constant.EventType.SAMPLE_TYPE:
                            if(!skipSample){
                                done = true;
                                ev = Edf.GetFloatData(m_EdfFile);
                            }

                            break;
                            /*

                            if(recordEvent.time>=trialEndTime){
                                
                            }
                            if(filterType==Constant.EventType.ANY_TYPE || returnType==filterType){
                                done = true;
                                ev = Edf.GetFloatData(m_EdfFile);
                            }

                            break;*/
                        case Constant.EventType.NO_PENDING_ITEMS:
                            done = true;
                            break;
                        default:
                            done = true;
                            Debug.LogWarning("Unknown type: "+returnType);
                            //return;
                            break;

                    }
                }else{
                    
                }
                //console.log(done,", ",type,", ",event);
            }while(!done);

            if (ev != null && ev.time > trialEndTime) {
                ev = null;
            }

            return ev;
        }
    }
}
