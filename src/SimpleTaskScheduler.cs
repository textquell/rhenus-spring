using System;
using System.Threading;

namespace Rhenus
{
    namespace Spring
    {
        class SimpleTaskScheduler: ITaskScheduler
        {
            public void ScheduleTask( ITask task )
            {
                // TODO: write a message when throwing this error. Get this message from a ressource file
                if ( task == null ) { throw new System.ArgumentNullException(); }
                ThreadPool.QueueUserWorkItem( new WaitCallback( task.Run ) );
            }

            public void ScheduleTask( ITask task, DateTime startTime )
            {
                throw new NotImplementedException();
            }
        }
    }
}
