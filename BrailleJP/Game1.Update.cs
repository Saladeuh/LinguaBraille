using CrossSpeak;
using LinguaBraille;
using LinguaBraille.Content;
using LinguaBraille.MiniGames;
using LinguaBraille.Save;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Octokit;
using SharpLouis;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrailleJP;

public partial class Game1
{
  private bool _firstScreenTipsSayed = false;
  private string _latestVersionUrl="";

  protected override void Update(GameTime gameTime)
  {
    KeyboardState nativeKeyboardState = Keyboard.GetState();
    List<Keys> allPressedKeys = nativeKeyboardState.GetPressedKeys().ToList();
    lock (_keyLock)
    {
      allPressedKeys.AddRange(_hookPressedKeys);
      if (!_updateProcessed)
      {
        allPressedKeys.AddRange(_keysToProcess);
        allPressedKeys = allPressedKeys.Distinct().ToList();
      }
    }
    KeyboardState currentKeyboardState = new(allPressedKeys.ToArray());
    MouseState currentMouseState = Mouse.GetState();

    if (!_firstScreenTipsSayed && gameTime.TotalGameTime.Seconds >= 2)
    {
      List<Release> releases = [];
      try
      {
        Task<IReadOnlyList<Release>> task = GitHubApiClient.Repository.Release.GetAll("saladeuh", "LinguaBraille");
        task.Wait();
        releases = task.Result.ToList();
      }
      catch
      {
      }
      if (Save.Flags.EmptySave)
      {
        CrossSpeakManager.Instance.Output(GameText.Tips);
      }
      else if (releases.Count > 0)
      {
        var latest = releases.ElementAt(0);
        if (latest.TagName != VERSION)
        {
          UIVictorySound.Play();
          CrossSpeakManager.Instance.Output(string.Format(GameText.Main_menu_update_available, latest.TagName));
          _latestVersionUrl = latest.HtmlUrl;
        }
      }
      _firstScreenTipsSayed = true;
    }
    _desktop.UpdateInput();
    HandleKeyboardNavigation(currentKeyboardState);
    // quit on escape key
    if (IsKeyPressed(currentKeyboardState, Keys.Escape))
    {
      if (_gameState.CurrentScreen != GameScreen.MainMenu)
      {
        SwitchToScreen(GameScreen.MainMenu);
      }
      else
      {
        Exit();
      }
    }
    if ((_gameState.CurrentScreen == GameScreen.BasicPractice || _gameState.CurrentScreen == GameScreen.ChoicePractice || _gameState.CurrentScreen == GameScreen.WordPractice)
      && !_gameState.IsPaused)
    {
      if (CurrentPlayingMiniGame.IsRunning)
      {
        CurrentPlayingMiniGame.Update(gameTime, currentKeyboardState);
      }
      else
      {
        SwitchToScreen(GameScreen.MainMenu);
        CrossSpeakManager.Instance.Output(string.Format(GameText.Score, CurrentPlayingMiniGame.Score));
        Save.Flags.FirstPlayChoicePractice &= CurrentPlayingMiniGame is not ChoicePractice;
        Save.Flags.FirstPlayBasicPractice &= CurrentPlayingMiniGame is not BasicPractice;
        CurrentPlayingMiniGame = null;
        SaveManager.WriteSave(Save);
      }
    }
    UpdateUIState();

    _previousKeyboardState = currentKeyboardState;
    _previousMouseState = currentMouseState;
    lock (_keyLock)
    {
      _updateProcessed = true;
      _keysToProcess.Clear(); // Vider les touches à traiter puisqu'elles ont été traitées
    }
    base.Update(gameTime);
  }
}
