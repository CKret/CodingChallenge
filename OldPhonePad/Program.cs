using System;

namespace OldPhonePad
{
    /// <summary>
    /// Entry point for the OldPhonePad command-line application.
    /// </summary>
    /// <remarks>
    /// Reads user input, decodes it, and prints the result.
    /// Handles invalid character exceptions gracefully.
    /// </remarks>
    internal class Program
    {
        private static void Main(string[] args)
        {
            var decoder = new OldPhonePadDecoder();
            var input = Console.ReadLine();
            try
            {
                var result = decoder.Decode(input);
                Console.WriteLine(result);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
