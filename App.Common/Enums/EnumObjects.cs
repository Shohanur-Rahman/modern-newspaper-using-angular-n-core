using System;
using System.Collections.Generic;
using System.Text;

namespace App.Common.Enums
{
    public class EnumObjects
    {
        public enum Status
        {
            Already = -1,
            Active = 1,
            InActive = 2,
            Pending = 3,
            Success = 4,
            Deleted = 5,
            Edited = 6
        }
    }

    public enum ResponseStatus
    {
        Success = 1,
        Fail = 2,
        SessionFail = 62
    }
}
