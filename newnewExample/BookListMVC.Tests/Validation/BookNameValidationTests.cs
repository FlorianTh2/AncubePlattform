using BookListMVC.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BookListMVC.Tests.Validation
{
    public class BookNameValidationTests
    {
        private readonly BookNameValidation _validation;


        public BookNameValidationTests()
        {
            _validation = new BookNameValidation();
        }


        // Name-convention:
        // [MethodWeTest_StateUnderTest_ExpectedBehavior]
        [Fact]
        public void IsValid_ValidBookNumber_ReturnsTrue()
        {
            Assert.True(_validation.IsValid("Max Mustermann"));
        }

        [Theory]
        [InlineData("Max Mustermann2")]
        [InlineData("Max2 Mustermann2")]
        [InlineData("423421342")]
        public void IsValid_BookNameWithNumber_ReturnsFalse(string name)
        {
            Assert.False(_validation.IsValid(name));
        }

        [Fact]
        public void IsValid_BookNameCalledBookname_ReturnsFalse()
        {
            Assert.Throws<ArgumentException>(() => _validation.IsValid("Book Name"));
        }


    }
}
