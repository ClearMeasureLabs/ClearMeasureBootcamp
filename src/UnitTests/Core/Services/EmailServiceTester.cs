using System;
using ClearMeasure.Bootcamp.Core.Model;
using ClearMeasure.Bootcamp.Core.Services;
using ClearMeasure.Bootcamp.Core.Services.Impl;
using NUnit.Framework;

namespace ClearMeasure.Bootcamp.UnitTests.Core.Services
{
    [TestFixture]
    public class EmailServiceTester
    {
        [Test]
        public void ShouldSendPopMessage()
        {
            var userSession = new SessionStub();

            IEmailService service = new EmailService(userSession);
            service.SendMessage("bleh", "that's cool");

            Assert.That(userSession.FlashMessage.Message,
                        Is.EqualTo("'that's cool' sent to 'bleh'"));
        }

        public class SessionStub : IUserSession
        {
            public FlashMessage FlashMessage;

            public Employee GetCurrentUser()
            {
                throw new NotImplementedException();
            }

            public void LogIn(Employee employee)
            {
                throw new NotImplementedException();
            }

            public void LogOut()
            {
                throw new NotImplementedException();
            }

            public void PushUserMessage(FlashMessage message)
            {
                FlashMessage = message;
            }

            public FlashMessage PopUserMessage()
            {
                throw new NotImplementedException();
            }
        }
    }
}