using System.Collections.Generic;

namespace OldPhonePad
{
    /// <summary>
    /// Provides default digit-to-character mappings for an old phone keypad.
    /// </summary>
    /// <remarks>
    /// The mapping uses uppercase Latin letters by default:
    /// <code>
    /// 1 → &amp;'(
    /// 2 → ABC
    /// 3 → DEF
    /// 4 → GHI
    /// 5 → JKL
    /// 6 → MNO
    /// 7 → PQRS
    /// 8 → TUV
    /// 9 → WXYZ
    /// 0 → (space)
    /// </code>
    /// </remarks>
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
