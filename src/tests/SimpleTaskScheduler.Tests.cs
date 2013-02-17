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
            SimpleTaskScheduler scheduler;

            [SetUp]
            public void Init()
            {
                scheduler = new SimpleTaskScheduler();
            }

            public void Destroy()
            {
                scheduler = null;
            }

            [Test]
            public void TestForRunExecution()
            {
                ITask task = new DemoTask();
                scheduler.ScheduleTask( task );
                DemoTask result = (DemoTask)task;
                System.Threading.Thread.Sleep( 5 );
                Assert.AreEqual( 84, result.DemoInt );
            }

            [Test]
            [ExpectedException( typeof( System.ArgumentNullException ) )]
            public void ExpectNullRefExceptionSimple()
            {
                ITask task = null;
                scheduler.ScheduleTask( task );
            }

            [Test]
            [ExpectedException( typeof( System.ArgumentNullException ) )]
            public void ExpectNullRefExceptionDelayed()
            {
                ITask task = null;
                scheduler.ScheduleTask( task, DateTime.Now.AddMilliseconds( 1 ) );
                System.Threading.Thread.Sleep( 5 );
            }
        }
    }
}
