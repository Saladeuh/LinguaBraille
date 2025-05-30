﻿using BrailleJP;
using CrossSpeak;
using LinguaBraille;
using Microsoft.Xna.Framework.Audio;
using Myra.Graphics2D.UI;
using System;

namespace LinguaBraille.UI;

internal class TableViewEntry : Label
{
  private readonly SoundEffect _scrollSound;
  private readonly BrailleEntry _entry;

  public TableViewEntry(BrailleEntry brailleEntry)
  {
    Text = brailleEntry.ToString();
    _entry = brailleEntry;
    AcceptsKeyboardFocus = true;
    KeyboardFocusChanged += OnScroll;
  }


  private void OnScroll(object sender, EventArgs e)
  {
    if (IsKeyboardFocused)
    {
      _entry.Voice.Play();
      Game1.Instance.UIViewScrollSound.Play();
      CrossSpeakManager.Instance.Braille(Text);
    }
  }
}
