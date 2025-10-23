using System;

namespace OldPhonePad
{
    /// <summary>
    /// Entry point for OldPhonePad application.
    /// </summary>
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
