# 📱 OldPhonePad — Classic Multi-Tap Decoder for C#

A clean, production-ready implementation of an **old-style phone keypad text decoder** in C#.  
It simulates typing messages on phones where each key cycles through letters (e.g., `2 → ABC`, `3 → DEF`, etc.).

---

## ✨ Features

- Full **multi-tap decoding** for digits `1–9`  
- `0` maps to a **space**  
- `*` acts as **backspace** (removes the last committed character)  
- `#` acts as **send** (commits current run and stops)  
- Configurable handling for **invalid characters**  
- Extensible — inject your own keypad mapping or rules  
- Comprehensive **NUnit test coverage**  
- Ready for **NuGet** or **CI/CD pipelines**

---

## 🧠 Example Behavior

| Input | Output | Explanation |
|--------|---------|-------------|
| `33#` | `E` | `3 → D`, `33 → E`, send |
| `227*#` | `B` | `22 → B`, `7 → P`, then `*` removes `P` |
| `4433555 555666#` | `HELLO` | Classic multi-tap example |
| `8 88777444666*664#` | `TURING` | Includes backspace and pauses |
| `557**#` | *(empty)* | Two backspaces erase the text |

---

## ⚙️ Usage

```csharp
using OldPhonePad;

var decoder = new OldPhonePadDecoder();
string message = decoder.Decode("4433555 555666#");
Console.WriteLine(message); // Outputs "HELLO"
```

---

## 🧱 Project Structure

```
OldPhonePad.sln
├── OldPhonePad/                 # Main library and console app
│   ├── OldPhonePadDecoder.cs    # Core decoding logic
│   ├── OldPhonePadKeyMapping.cs # Default digit-to-letter map
│   ├── OldPhonePadOptions.cs    # Configurable decoding options
│   ├── Program.cs               # CLI entry point
│   └── OldPhonePad.csproj       # Project file
│
└── OldPhonePad.Tests/           # NUnit test project
    ├── UnitTests.cs             # Complete test coverage
    └── OldPhonePad.Tests.csproj # Test project file
```

---

## 🧪 Running Tests

This project uses **NUnit**.  
You can run all tests directly from Visual Studio Test Explorer, or via CLI:

```bash
dotnet test
```

---

## 🧰 Example CLI Session

```bash
> dotnet run --project OldPhonePad
Enter sequence: 4433555 555666#
HELLO
```

---

## 🧩 Design Principles

- **Deterministic** — each input sequence yields a predictable output  
- **Safe defaults** — null or invalid input never crashes  
- **Configurable** — customizable mappings and error handling  
- **Test-driven** — every behavior verified through NUnit tests  

---

## 🪄 License

MIT License — you’re free to use, modify, and distribute this library.

© 2025 Sani Huttunen
