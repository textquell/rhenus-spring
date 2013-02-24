using System;
using System.Threading;
using NUnit.Framework;

namespace Rhenus
{
	namespace Spring
	{
		[TestFixture]
		public class SimpleTaskSchedulerTest
		{
			ITaskScheduler taskScheduler;

			[TestFixtureSetUp]
			public void Init ()
			{
				this.taskScheduler = new Service ().TaskScheduler;
			}

			[Test]
			public void GetReferenceTest ()
			{
				Assert.NotNull (this.taskScheduler);
			}

			#region Task null Tests
			[Test]
			[ExpectedException(typeof (System.ArgumentNullException))]
			public void FailOnNullTaskSimple ()
			{
				this.taskScheduler.ScheduleTask (null);
			}

			[Test]
			[ExpectedException(typeof (System.ArgumentNullException))]
			public void FailOnNullTaskDelayed ()
			{
				this.taskScheduler.ScheduleTask (null, DateTime.Now.AddMilliseconds (10));
			}

			[Test]
			[ExpectedException(typeof (System.ArgumentNullException))]
			public void FailOnNullTaskPeriodic ()
			{
				this.taskScheduler.SchedulePeriodicTask (null, new TimeSpan (10000));
			}
			#endregion

			#region Invalid times tests
			[Test]
			[ExpectedException(typeof(System.ArgumentException))]
			public void FailOnBadTimeSimple ()
			{
				this.taskScheduler.ScheduleTask (new VerySimpleTask (), DateTime.Now.AddTicks (-1));
			}

			[Test]
			[ExpectedException(typeof (System.ArgumentException))]
			public void FailOnBadTimePeriodic ()
			{
				this.taskScheduler.SchedulePeriodicTask (new VerySimpleTask (), new TimeSpan (-1));
			}
			#endregion

			#region Scheduling tests
			[Test]
			public void TaskScheduledSimple ()
			{
				VerySimpleTask testTask = new VerySimpleTask ();
				this.taskScheduler.ScheduleTask (testTask);
				Thread.Sleep (5);
				Assert.AreEqual (2, testTask.Int);
			}

			[Test]
			public void TaskScheduledDelayed ()
			{
				VerySimpleTask testTask = new VerySimpleTask ();
				this.taskScheduler.ScheduleTask (testTask, DateTime.Now.AddMilliseconds (10));
				Thread.Sleep (25);
				Assert.AreEqual (2, testTask.Int);
			}

			[Test]
			public void TaskScheduledRecurring ()
			{
				VerySimpleTask testTask = new VerySimpleTask ();
				IPeriodicTaskController taskController = this.taskScheduler.SchedulePeriodicTask (testTask, new TimeSpan (0, 0, 0, 0, 10));
				Thread.Sleep (10);
				Assert.AreEqual (1, testTask.Int);

				taskController.Use (); // first task execution
				Thread.Sleep (25); // two more executions
				taskController.Cancel ();
				Thread.Sleep (20); // TODO: Why is the result 4 when we are sleeping for 25 millies here?
				Assert.AreEqual (3, testTask.Int);
			}
			#endregion
		}

		#region Helper classes
		public class VerySimpleTask: Rhenus.Spring.ITask
		{
			public int Int { get; set; }

			public VerySimpleTask ()
			{
				this.Int = 1;
			}

			public void Run (object state)
			{
				this.Int ++;
			}
		}
		#endregion
	}
}
