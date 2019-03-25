using System;
using DriverLib.Domain.Common;

namespace DriverLib.Jobs
{
    public interface IJobExecutor
    {
        JobExecutorResult Execute();
    }
}
