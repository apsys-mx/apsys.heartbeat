using apsys.heartbeat.authorization;
using apsys.heartbeat.testing;
using FluentAssertions;
using NUnit.Framework;

namespace apsys.heartbeat.authorization
{
    internal class ApplicationRoleTests : AbstractBaseTests<ApplicationRole>
    {
        [TestCase(null)]
        [TestCase("")]
        public void IsValid_InvalidNumber_ReturnFalse(string roleNumber)
        {
            /// Arrange
            this.ClassUnderTest.Name = roleNumber;
            /// Act and Assert
            this.ClassUnderTest.IsValid().Should().BeFalse();
        }

        internal override void ArrangeFullEntity()
        {
        }
    }
}
