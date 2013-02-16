using System;
using NUnit.Framework;

namespace Rhenus
{
    namespace Spring
    {

        class DemoTask: ITask
        {
            public int DemoInt { get; set; }

            public DemoTask()
            {
                DemoInt = 42;
            }

            public void Run( object state )
            {
                DemoInt *= 2;
            }
        }

        [TestFixture]
        public class SimpleTaskSchedulerTest
        {
            [Test]
            public void TestForRunExecution()
            {
                ITaskScheduler scheduler = new SimpleTaskScheduler();
                ITask task = new DemoTask();
                scheduler.ScheduleTask( ref task );
                DemoTask result = (DemoTask)task;
                System.Threading.Thread.Sleep( 5 );
                Assert.AreEqual( 84, result.DemoInt );
            }
        }
    }
}
