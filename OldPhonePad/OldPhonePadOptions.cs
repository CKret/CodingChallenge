using System;

namespace OldPhonePad
{
    /// <summary>
    /// Defines optional decoder behavior flags for <see cref="OldPhonePadDecoder"/>.
    /// </summary>
    public sealed class OldPhoneOptions
    {
        public bool IgnoreInvalidCharacters { get; set; } = false;
    }
}
