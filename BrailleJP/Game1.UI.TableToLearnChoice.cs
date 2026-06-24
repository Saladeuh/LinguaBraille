using AccessibleMyraUI;
using LinguaBraille.Content;
using LinguaBraille;
using LinguaBraille.UI;
using Myra.Graphics2D.UI;
using System.Globalization;
using System.Linq;

namespace BrailleJP;

public partial class Game1
{
  private Panel _tableToLearnPanel;

  public CultureInfo TableToLearn { get; private set; } = SUPPORTEDBRAILLETABLES.Keys.First();

  private void CreateTableToLearnScreen()
  {
    _tableToLearnPanel = new Panel();

    VerticalStackPanel grid = new()
    {
      Spacing = 10,
      HorizontalAlignment = HorizontalAlignment.Center,
      VerticalAlignment = VerticalAlignment.Center
    };
    Label titleLabel = new()
    {
      Text = "choisi",
      HorizontalAlignment = HorizontalAlignment.Center
    }; 
    grid.Widgets.Add(titleLabel);

    // Space
    grid.Widgets.Add(new Label { Text = "" });
    foreach (var pair in SUPPORTEDBRAILLETABLES)
    {
      ConfirmButton choiceButton = new(pair.Key.NativeName);
      choiceButton.Click += (_, _) =>
            {
              TableToLearn = pair.Key;
              SwitchToScreen(GameScreen.MainMenu);
            };
      grid.Widgets.Add(choiceButton);
    }

    _tableToLearnPanel.Widgets.Add(grid);
    _desktop.FocusedKeyboardWidget = titleLabel;
  }
}
