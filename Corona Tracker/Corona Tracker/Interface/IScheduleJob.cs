using System;
using System.Collections.Generic;
using System.Text;

namespace Corona_Tracker.Interface
{
    public interface IScheduleJob
    {
        bool Schedule();

        bool Cancel();
    }
}
