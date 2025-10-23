using System.Collections.Generic;

namespace OldPhonePad
{
    /// <summary>
    /// Provides the default digit-to-character mapping for the old phone keypad.
    /// </summary>
    public static class OldPhonePadKeyMapping
    {
        public static readonly IReadOnlyDictionary<char, string> Default = new Dictionary<char, string>
        {
            ['1'] = "&'(",
            ['2'] = "ABC",
            ['3'] = "DEF",
            ['4'] = "GHI",
            ['5'] = "JKL",
            ['6'] = "MNO",
            ['7'] = "PQRS",
            ['8'] = "TUV",
            ['9'] = "WXYZ",
            ['0'] = " "
        };
    }
}
