using LinguaBraille.Content;
using LinguaBraille;
using LinguaBraille.UI;
using Myra.Graphics2D.UI;
using System.Diagnostics;
using CrossSpeak;

namespace BrailleJP;

public partial class Game1
{
  private void CreateMainMenu()
  {
    _mainMenuPanel = new Panel();

    VerticalStackPanel mainMenuGrid = new()
    {
      Spacing = 20,
      HorizontalAlignment = HorizontalAlignment.Center,
      VerticalAlignment = VerticalAlignment.Center
    };
    Label titleLabel = new()
    {
      Text = GameText.Main_menu_title,
      HorizontalAlignment = HorizontalAlignment.Center
    };
    mainMenuGrid.Widgets.Add(titleLabel);

    // Space
    mainMenuGrid.Widgets.Add(new Label { Text = "" });

    ConfirmButton tableViewButton = new(GameText.Main_menu_table)
    {
      Id = "playButton"
    };
    tableViewButton.Click += (_, _) =>
    {
      SwitchToScreen(GameScreen.BrailleTableView);
    };
    mainMenuGrid.Widgets.Add(tableViewButton);

    ConfirmButton choicePracticeButton = new(GameText.Main_menu_choice)
    {
      Id = "choicePracticeButton"
    };
    choicePracticeButton.Click += (_, _) =>
    {
      SwitchToScreen(GameScreen.ChoicePractice);
    };
    mainMenuGrid.Widgets.Add(choicePracticeButton);

    ConfirmButton wordPracticeButton = new(GameText.Main_menu_word_practice)
    {
      Id = "wordPracticeButton"
    };
    wordPracticeButton.Click += (_, _) =>
    {
      SwitchToScreen(GameScreen.WordPractice);
    };
    mainMenuGrid.Widgets.Add(wordPracticeButton);

    ConfirmButton basicPracticeButton = new(GameText.Main_menu_basicpractice)
    {
      Id = "basicPracticeButton"
    };
    basicPracticeButton.Click += (_, _) =>
    {
      SwitchToScreen(GameScreen.BasicPractice);
    };
    mainMenuGrid.Widgets.Add(basicPracticeButton);
#if false
    ConfirmButton settingsButton = new(GameText.Main_menu_settings)
    {
      Id = "settingsButton"
    };
    settingsButton.Click += (s, a) =>
    {
      SwitchToScreen(GameScreen.Settings);
      CrossSpeakManager.Instance.Output("Menu des paramètres");
    };
    mainMenuGrid.Widgets.Add(settingsButton);
#endif
    ConfirmButton tipsButton = new(GameText.Main_menu_tips)
    {
      Id = "tipsButton"
    };
    tipsButton.Click += (_, _) =>
    {
      SwitchToScreen(GameScreen.First);
    };

    mainMenuGrid.Widgets.Add(tipsButton);

    ConfirmButton wikiButton = new(GameText.Main_menu_wiki)
    {
      Id = "wikiButton"
    };
    wikiButton.Click += (_, _) =>
    {
      Process.Start(new ProcessStartInfo
      {
        FileName = "https://fr.wikipedia.org/wiki/Braille_japonais",
        UseShellExecute = true
      });
    };
    mainMenuGrid.Widgets.Add(wikiButton);

    ConfirmButton updateButton = new(GameText.Main_menu_update_download)
    {
      Id = "updateButton"
    };
    updateButton.Click += (_, _) =>
    {
      if (string.Empty!=_latestVersionUrl)
      {
        Process.Start(new ProcessStartInfo
        {
          FileName = _latestVersionUrl,
          UseShellExecute = true
        });
      }
      else
      {
        CrossSpeakManager.Instance.Output(GameText.Main_menu_no_update);
      }
    };
    mainMenuGrid.Widgets.Add(updateButton);

    BackButton quitButton = new(GameText.Quit)
    {
      Id = "quitButton"
    };
    quitButton.Click += (_, _) =>
    {
      Exit();
    };
    mainMenuGrid.Widgets.Add(quitButton);

    _mainMenuPanel.Widgets.Add(mainMenuGrid);
    _desktop.FocusedKeyboardWidget = tableViewButton;
  }
}
