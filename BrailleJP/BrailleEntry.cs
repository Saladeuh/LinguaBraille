using BrailleJP;
using Microsoft.Xna.Framework.Audio;
using Octokit;
using System;
using System.IO;

namespace LinguaBraille;

public class BrailleEntry
{
  public string Opcode { get; set; }
  public string Characters { get; set; }
  public string DotPattern { get; set; }
  public string Comment { get; set; }
  public string SourceFile { get; set; }

  // Helper property to get the category/type of the entry
  public string Category => Opcode switch
  {
    "space" => "Whitespace",
    "digit" => "Numeric",
    "letter" => "Alphabetic",
    "lowercase" => "Lowercase",
    "uppercase" => "Uppercase",
    "punctuation" => "Punctuation",
    "sign" => "Sign",
    "math" => "Mathematical",
    "litdigit" => "Literary Digit",
    "include" => "Include",
    _ => "Unknown"
  };

  public override string ToString()
  {
    if (Opcode == "include")
      return $"Include file: {Characters}";
    string brailleDotChar = BrailleString;
    string result = $"{brailleDotChar} {Characters} {DotPattern}";
    if (!string.IsNullOrEmpty(Comment))
      result += $" # {Comment}";
    return result;
  }

  public string BrailleString
  {
    get
    {
      var brailleTranslator = SharpLouis.Wrapper.Create(Path.GetFileName(SourceFile), Game1.LibLouisLoggingClient);
      var brailleDotChar = "";
      if (brailleTranslator != null) brailleTranslator.TranslateString(Characters, out brailleDotChar);
      return brailleDotChar;
    }
  }
  public SoundEffectInstance Voice { get; set; }
  public BrailleEntry(string opcode, string characters, string originTable, string sourceFile, string dotPattern = "", string comment = "")
  {
    Opcode = opcode;
    Characters = characters;
    DotPattern = dotPattern;
    Comment = comment;
    SourceFile = sourceFile;
    if (Opcode != "include" && Opcode != "noback")
    {
      try
      {
        var fileNameWithoutExt = Path.GetFileNameWithoutExtension(originTable);
        var directory = $"speech/{fileNameWithoutExt}";
        var soundPath = directory + "/" + dotPattern;
        var files = Directory.GetFiles(Path.Combine(Game1.Instance.Content.RootDirectory, directory), dotPattern + ".*");
        if (files.Length > 0)
        {
          Voice = Game1.Instance.Content.Load<SoundEffect>(soundPath).CreateInstance();
          Voice = Game1.Instance.Content.Load<SoundEffect>(soundPath).CreateInstance();
          Voice.Volume = 1;
        }
        else
        {
          Voice = null;
        }
      }
      catch
      {
        Voice = null;
      }
    }
  }
  public bool IsLowercaseLetter() => Opcode == "lowercase" || Opcode == "letter";
}