# UniEDF
UniEDF is a Unity/C# binding for EyeLink Data Format library.

###Requirement
* Unity 5.5 or higher
* Windows x86/x64, Linux x86/x64 & Mac OSX

###Installation
Copy all files from project to Assets folder.

#Tutorial
``` bash
/*Search GAZE_COORDS from message event*/

EdfFile file = EdfFile.Open(path);
Constant.EventType type;
EdfData dv;
string message;

while ((dv = TrialNextEvent(out type, Constant.EventType.MESSAGEEVENT)) != null) {
    message = (dv as AllfData).fe.message;

    if (!string.IsNullOrEmpty(message) && message.StartsWith("GAZE_COORDS")) {
        string[] array = message.Split(new char[]{ ' ' });
        float x = float.Parse(array [1]);
        float y = float.Parse(array [2]);
        Debug.Log("Find: "+m_GazeCoords);
        break;
    }
}
``` 