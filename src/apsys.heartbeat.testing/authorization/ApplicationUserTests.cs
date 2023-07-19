using apsys.heartbeat.authorization;
using apsys.heartbeat.testing;
using FluentAssertions;
using NUnit.Framework;

namespace apsys.heartbeat.testing.authorization
{
    internal class ApplicationUserTests : AbstractBaseTests<ApplicationUser>
    {
        [TestCase(null)]
        [TestCase("")]
        public void IsValid_InvalidPartnerNumber_ReturnFalse(string partnerNumber)
        {
            /// Arrange
            this.ClassUnderTest.UserName = partnerNumber;
            /// Act and Assert
            this.ClassUnderTest.IsValid().Should().BeFalse();
        }

        internal override void ArrangeFullEntity()
        {
        }
    }
}
