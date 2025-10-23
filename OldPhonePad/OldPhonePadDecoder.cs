using System;
using System.Collections.Generic;
using System.Text;

namespace OldPhonePad
{
    /// <summary>
    /// Provides logic for decoding old-style multi-tap phone keypad input into text.
    /// </summary>
    /// <remarks>
    /// Supports the following keys:
    /// <list type="bullet">
    /// <item><description>Digits 2–9 map to letters (multi-tap)</description></item>
    /// <item><description>0 inserts a space</description></item>
    /// <item><description>* acts as backspace (removes the last committed character)</description></item>
    /// <item><description># acts as send/commit (finalizes input and stops decoding)</description></item>
    /// <item><description>Space commits the current key run (used to separate same digits)</description></item>
    /// </list>
    /// </remarks>
    public sealed class OldPhonePadDecoder
    {
        /// <summary>
        /// The options for decoding behavior.
        /// </summary>
        public readonly OldPhoneOptions Options;

        /// <summary>
        /// The digit-to-character mapping.
        /// </summary>
        private readonly IReadOnlyDictionary<char, string> _mapping;

        /// <summary>
        /// Initializes a new instance of the <see cref="OldPhonePadDecoder"/> class with specified options.
        /// </summary>
        public OldPhonePadDecoder(OldPhoneOptions? options = null, IReadOnlyDictionary<char, string>? mapping = null)
        {
            Options = options ?? new OldPhoneOptions();
            _mapping = mapping ?? OldPhonePadKeyMapping.Default;
        }

        /// <summary>
        /// Decodes the specified input string using the classic phone keypad mapping.
        /// </summary>
        /// <param name="input">A sequence of key presses (e.g. "4433555 555666#").</param>
        /// <returns>The decoded message as a string. Returns an empty string if the input is null or empty.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if invalid characters are found and <see cref="OldPhoneOptions.IgnoreInvalidCharacters"/> is false.
        /// </exception>
        public string Decode(string? input)
        {
            if (input is null) return string.Empty;

            var output = new StringBuilder();
            char? currentKey = null;
            var count = 0;

            void Commit()
            {
                if (currentKey != null && count > 0)
                {
                    if (_mapping.TryGetValue(currentKey.Value, out var letters) && letters.Length > 0)
                    {
                        var index = (count - 1) % letters.Length;
                        output.Append(letters[index]);
                    }
                }
                currentKey = null;
                count = 0;
            }

            foreach (var ch in input)
            {
                if (ch == '#')
                {
                    // Send -> Commit and stop.
                    Commit();
                    break;
                }

                if (ch == '*')
                {
                    // Commit any pending key, then backspace last committed char.
                    Commit();
                    if (output.Length > 0) output.Length--;
                    continue;
                }

                if (ch == ' ')
                {
                    // Pause -> Commit current key.
                    Commit();
                    continue;
                }

                if (char.IsDigit(ch))
                {
                    // Digit key pressed.
                    var d = ch;
                    if (currentKey == d)
                    {
                        count++;
                    }
                    else
                    {
                        Commit();
                        currentKey = d;
                        count = 1;
                    }
                    continue;
                }

                // There are two ways to handle invalid input:
                // 1. Ignore invalid characters (skip them). I.e. break;
                // 2. Throw an exception for invalid characters.
                // For this implementation, we make it configurable since the specification is unclear how to handle them.
                // E.g. "22-2#" would be invalid due to '-', but should the result be "C" (222) or "B" (22) or "BA" (22 2)?
                if (!Options.IgnoreInvalidCharacters)
                    throw new ArgumentException($"Invalid character in input: {ch}");
            }

            // Commit any remaining key even if input didn't end with #.
            // Assumption is that # will always be at the end, but just in case it isn't, we handle it gracefully.
            Commit();

            return output.ToString();
        }
    }
}