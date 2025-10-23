using System;
using NUnit.Framework;

namespace OldPhonePad.Tests
{
    /// <summary>
    /// Unit tests for OldPhonePadLogic.
    /// </summary>
    public class ProgramTests
    {
        private OldPhonePadDecoder _decoder_IgnoreTrue = new OldPhonePadDecoder(new OldPhoneOptions() { IgnoreInvalidCharacters = true });
        private OldPhonePadDecoder _decoder_IgnoreFalse = new OldPhonePadDecoder(new OldPhoneOptions() { IgnoreInvalidCharacters = false });

        /// <summary>
        /// These tests are based on the examples provided in the specification.
        /// </summary>
        [TestCase("33#", "E")]
        [TestCase("227*#", "B")]
        [TestCase("4433555 555666#", "HELLO")]
        [TestCase("8 88777444666*664#", "TURING")]
        public void OldPhonePad_Examples_ShouldPass(string input, string expected)
        {
            var actual = _decoder_IgnoreTrue.Decode(input);
            Assert.That(actual, Is.EqualTo(expected));
            actual = _decoder_IgnoreFalse.Decode(input);
            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        /// Additional tests to cover edge cases and other scenarios.
        /// </summary>
        [TestCase(null, "")]
        [TestCase("#", "")]
        [TestCase("2222#", "A")]
        [TestCase("2222 7777777#", "AR")]
        [TestCase("0#", " ")]
        [TestCase("22 2#", "BA")]
        [TestCase("*#", "")]
        [TestCase("557**#", "")]
        [TestCase("44 444", "HI")]
        public void OldPhonePad_AdditionalTests_ShouldPass(string input, string expected)
        {
            var actual = _decoder_IgnoreTrue.Decode(input);
            Assert.That(actual, Is.EqualTo(expected));
            actual = _decoder_IgnoreFalse.Decode(input);
            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        /// These tests expect an ArgumentException to be thrown for invalid characters when IgnoreInvalidCharacters is false.
        /// </summary>
        [TestCase("234a4#", "ADH")]
        [TestCase("22-2#", "C")]
        public void OldPhonePad_InvalidInputs_ShouldThrow(string input, string expected)
        {
            Assert.Throws<ArgumentException>(() => _decoder_IgnoreFalse.Decode(input));
        }
        
        /// <summary>
        /// These tests ignore invalid characters in the input when IgnoreInvalidCharacters is true.
        /// </summary>
        [TestCase("234a4#", "ADH")]
        [TestCase("22-2#", "C")]
        public void OldPhonePad_InvalidInputs_ShouldPass(string input, string expected)
        {
            var actual = _decoder_IgnoreTrue.Decode(input);
            Assert.That(actual, Is.EqualTo(expected));
        }

        /// <summary>
        /// Tests for multiple commits within the same input string.
        /// Even though the specification suggests '#' is at the end, we test this scenario
        /// since the specification is not explicit that '#' cannot appear multiple times.
        /// </summary>
        [TestCase("#12345", "")]
        [TestCase("44#444#", "H")]
        [TestCase("22#####111", "B")]
        public void OldPhonePad_MultipleCommits_ShouldPass(string input, string expected)
        {
            var actual = _decoder_IgnoreTrue.Decode(input);
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}