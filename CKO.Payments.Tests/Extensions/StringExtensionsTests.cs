using CKO.Payments.BL.Extensions;
using Moq;
using System;
using Xunit;

namespace CKO.Payments.Tests.Extensions
{
    public class StringExtensionsTests
    {
        private MockRepository mockRepository;



        public StringExtensionsTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
        }

        [Fact]
        public void Mask_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            string value = null;
            char maskChar = default(global::System.Char);
            int unMaskedLength = 0;

            // Act
            var result = StringExtensions.Mask(
                value,
                maskChar,
                unMaskedLength);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
