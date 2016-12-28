namespace EyeLink{
    public class Config{
        #if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
            public const string NATIVE_PATH = "DYLD_FRAMEWORK_PATH/edfapi.framework/edfapi";
        #elif UNITY_STANDALONE_LINUX
            public const string NATIVE_PATH = "libedfapi.so";
        #else
            public const string NATIVE_PATH = "edfapi";
#endif

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
        public const int PACK = 1;
#else
        public const int PACK = 0;
#endif
    }
}
