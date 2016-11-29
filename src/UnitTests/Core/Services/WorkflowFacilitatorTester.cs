using System;
using ClearMeasure.Bootcamp.Core.Features.Workflow;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.Core.Model.ExpenseReportWorkflow;
using ClearMeasure.Bootcamp.Core.Services;
using ClearMeasure.Bootcamp.Core.Services.Impl;
using NUnit.Framework;
using Rhino.Mocks;

namespace ClearMeasure.Bootcamp.UnitTests.Core.Services
{
    [TestFixture]
    public class WorkflowFacilitatorTester
    {
        [Test]
        public void ShouldGetNoValidStateCommandsForWrongUser()
        {
            var facilitator = new WorkflowFacilitator();
            var report = new ExpenseReport();
            var employee = new Employee();
            IStateCommand[] commands = facilitator.GetValidStateCommands(new ExecuteTransitionCommand{Report = report, CurrentUser = employee});

            Assert.That(commands.Length, Is.EqualTo(0));
        }


        [Test]
        public void ShouldReturnAllStateCommandsInCorrectOrder()
        {
            var facilitator = new WorkflowFacilitator();
            IStateCommand[] commands = facilitator.GetAllStateCommands();

            Assert.That(commands.Length, Is.EqualTo(4));

            Assert.That(commands[0], Is.InstanceOf(typeof (DraftingCommand)));
            Assert.That(commands[1], Is.InstanceOf(typeof (DraftToSubmittedCommand)));
            Assert.That(commands[2], Is.InstanceOf(typeof(DraftToCancelledCommand)));
            Assert.That(commands[3], Is.InstanceOf(typeof(SubmittedToApprovedCommand)));
        }

        [Test]
        public void ShouldFilterFullListToReturnValidCommands()
        {
            var mocks = new MockRepository();
            var facilitator = mocks.PartialMock<WorkflowFacilitator>();
            var commandsToReturn = new IStateCommand[]
                                       {
                                           new StubbedStateCommand(true), new StubbedStateCommand(true),
                                           new StubbedStateCommand(false)
                                       };

            Expect.Call(facilitator.GetAllStateCommands()).IgnoreArguments().Return(commandsToReturn);
            mocks.ReplayAll();

            IStateCommand[] commands = facilitator.GetValidStateCommands(null);

            mocks.VerifyAll();
            Assert.That(commands.Length, Is.EqualTo(2));
        }

        public class StubbedStateCommand : IStateCommand
        {
            private bool _isValid;

            public StubbedStateCommand(bool isValid)
            {
                _isValid = isValid;
            }

            public bool IsValid(ExecuteTransitionCommand transitionCommand)
            {
                return _isValid;
            }

            public ExecuteTransitionResult Execute(ExecuteTransitionCommand transitionCommand)
            {
                throw new NotImplementedException();
            }

            public string TransitionVerbPresentTense
            {
                get { throw new NotImplementedException(); }
            }

            public bool Matches(string commandName)
            {
                throw new NotImplementedException();
            }

            public ExpenseReportStatus GetBeginStatus()
            {
                throw new NotImplementedException();
            }
        }
    }
}