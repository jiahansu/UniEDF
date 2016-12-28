using UnityEngine;
using System.Collections;
namespace EyeLink.Edf {
    public class Constant{
        public enum EventType{
            /************* EVENT TYPE CODES ************/
            ANY_TYPE = -1,
            /* buffer = IEVENT, FEVENT, btype = IEVENT_BUFFER */
            NO_PENDING_ITEMS = 0,
            STARTPARSE = 1,
            /* these only have time and eye data */
            ENDPARSE = 2,
            BREAKPARSE = 10,

            /* EYE DATA: contents determined by evt_data */
            STARTBLINK = 3,
            /* and by "read" data item */
            ENDBLINK = 4,
            /* all use IEVENT format */
            STARTSACC = 5,
            ENDSACC = 6,
            STARTFIX = 7,
            ENDFIX = 8,
            FIXUPDATE = 9,

            /* buffer = (none, directly affects state), btype = CONTROL_BUFFER */

            /* control events: all put data into */
            /* the EDF_FILE or ILINKDATA status  */
            STARTSAMPLES = 15,
            /* start of events in block */
            ENDSAMPLES = 16,
            /* end of samples in block */
            STARTEVENTS = 17,
            /* start of events in block */
            ENDEVENTS = 18,
            /* end of events in block */
            /* buffer = IMESSAGE, btype = IMESSAGE_BUFFER */

            MESSAGEEVENT = 24,
            /* user-definable text or data */


            /* buffer = IOEVENT, btype = IOEVENT_BUFFER */

            BUTTONEVENT = 25,
            /* button state change */
            INPUTEVENT = 28,
            /* change of input port */

            LOST_DATA_EVENT = 0x3F,
            /* NEW: Event flags gap in data stream */
            RECORDING_INFO = 30,

            SAMPLE_TYPE    = 200
        };

        /*********** EYE DATA FORMATS **********/

        /* ALL fields use MISSING_DATA when value was not read, */
        /* EXCEPT for <buttons>, <time>, <sttime> and <entime>, which use 0. */
        /* This is true for both floating point or integer variables. */

        /* Both samples and events may have             */
        /* several fields that have not been updated.   */
        /* These fields may be detected from the        */
        /* content flags, or by testing the field value */
        /* against these constants: */

        public const int MISSING_DATA = -32768;     /* data is missing (integer) */
        public const int MISSING = -32768;
        public const int INaN = -32768;


        /* binocular data needs to ID the eye for events */
        /* samples need to index the data */
        /* These constants are used as eye identifiers */

        public const int LEFT_EYE = 0;   /* index and ID of eyes */
        public const int RIGHT_EYE = 1;
        public const int LEFTEYEI  = 0;
        public const int RIGHTEYEI = 1;
        public const int LEFT     = 0;
        public const int RIGHT    = 1;

        public const int BINOCULAR = 2;   /* data for both eyes available */
    	
    }
}
