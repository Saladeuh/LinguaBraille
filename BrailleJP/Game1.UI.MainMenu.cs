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

    VerticalStackPanel grid = new()
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
    grid.Widgets.Add(titleLabel);

    // Space
    grid.Widgets.Add(new Label { Text = "" });

    ConfirmButton tableViewButton = new(GameText.Main_menu_table)
    {
      Id = "playButton"
    };
    tableViewButton.Click += (_, _) =>
    {
      SwitchToScreen(GameScreen.BrailleTableView);
    };
    grid.Widgets.Add(tableViewButton);

    ConfirmButton choicePracticeButton = new(GameText.Main_menu_choice)
    {
      Id = "choicePracticeButton"
    };
    choicePracticeButton.Click += (_, _) =>
    {
      SwitchToScreen(GameScreen.ChoicePractice);
    };
    grid.Widgets.Add(choicePracticeButton);

    ConfirmButton wordPracticeButton = new(GameText.Main_menu_word_practice)
    {
      Id = "wordPracticeButton"
    };
    wordPracticeButton.Click += (_, _) =>
    {
      SwitchToScreen(GameScreen.WordPractice);
    };
    grid.Widgets.Add(wordPracticeButton);

    ConfirmButton basicPracticeButton = new(GameText.Main_menu_basicpractice)
    {
      Id = "basicPracticeButton"
    };
    basicPracticeButton.Click += (_, _) =>
    {
      SwitchToScreen(GameScreen.BasicPractice);
    };
    grid.Widgets.Add(basicPracticeButton);
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

    grid.Widgets.Add(tipsButton);

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
    grid.Widgets.Add(wikiButton);

    ConfirmButton choiceTableButton = new(GameText.Main_menu_tips)
    {
      Id = "choiceTableButton"
    };
    choiceTableButton.Click += (_, _) =>
    {
      SwitchToScreen(GameScreen.ChoiceTable);
    };
    grid.Widgets.Add(choiceTableButton);

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
    grid.Widgets.Add(updateButton);

    BackButton quitButton = new(GameText.Quit)
    {
      Id = "quitButton"
    };
    quitButton.Click += (_, _) =>
    {
      Exit();
    };
    grid.Widgets.Add(quitButton);

    _mainMenuPanel.Widgets.Add(grid);
    _desktop.FocusedKeyboardWidget = tableViewButton;
  }
}
