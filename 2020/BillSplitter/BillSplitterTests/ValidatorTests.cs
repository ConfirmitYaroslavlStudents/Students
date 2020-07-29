using BillSplitter.Validators;
using Xunit;

namespace BillSplitterTests
{
    public class ValidatorTests
    {
        [Fact]
        public void ValidateInconsistentData_ValidationFailed()
        {
            var validator = new Validator<int>();
            validator.AddValidation(x => x % 2 == 0);

            Assert.False(validator.Validate(3));
        }

        [Fact]
        public void ValidateConsistentData_ValidationComplete()
        {
            var validator = new Validator<int>();
            validator.AddValidation(x => x % 2 == 0);

            Assert.True(validator.Validate(2));
        }

        [Fact]
        public void ChainValidation_ConsistentData_ValidationComplete()
        {
            var validator = new Validator<int>();
            validator
                .AddValidation(x => x % 3 == 0)
                .AddValidation(x => x % 2 == 0)
                .AddValidation(x => x < 10);

            Assert.True(validator.Validate(6));
        }

        [Fact]
        public void ChainValidation_InconsistentData_ValidationFailed()
        {
            var validator = new Validator<int>();
            validator
                .AddValidation(x => x % 3 == 0)
                .AddValidation(x => x % 2 == 0)
                .AddValidation(x => x < 10);

            Assert.True(validator.Validate(15));
        }
    }
}