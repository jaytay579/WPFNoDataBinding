using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFNoDataBinding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int playerNumber = 1;

        public MainWindow()
        {
            InitializeComponent();
            UpdateUI();
        }

        /* CHALLENGE #1: Modify the code so that each player gets to take
         * three actions before the program moves to the next player's turn.
         * Ensure that the player can't take any illegal actions (i.e., can't
         * remove an Ellipse that does not exist)
         * 
         * CHALLENGE #2: Modify the UI and the code so that the player can see
         * how many actions they have remaining in their current turn.
         * 
         * ADVANCED CHALLENGE: Modify the UI and the code so that the player can "undo"
         * the last action that they took during their current turn. For example, if a player
         * removed an ellipse as their second action, the ellipse would be put back in place,
         * and the player would again be able to take a second action before taking their third.
         * This is actually not terribly "advanced", given the simplicity of this demo, but it
         * involves some thinking about managing the state of buttons and objects. For example,
         * if the player has not yet taken an action this turn, there would be nothing to undo.
         * On the other hand, if they player has taken all three actions, the current program
         * auto-advances to the next player. How would you enhance the UI so that the player had
         * an opportunity to undo their third action, as well? Finally, can you ensure that the 
         * player understands that they can only undo ONE action in a row -- i.e., the player can't
         * click "undo" twice in a row to take back 2 actions!
         * */
        public void AdvancePlayer()
        {
            playerNumber++;
            if (playerNumber > 4) playerNumber = 1;
        }

        public void UpdateUI()
        {
            for (int i=1; i<5; i++)
            {
                var updateMe = FindName($"Player{i}") as StackPanel;
                // Debug.WriteLine(updateMe.Name);
                if (i == playerNumber)
                {
                    updateMe.Background = Brushes.LightSalmon;
                    Remove_Ellipse.IsEnabled = true;
                    if (updateMe.Children.Count == 0)
                    {
                        Remove_Ellipse.IsEnabled = false;
                    }
                }
                else
                {
                    updateMe.Background = Brushes.LightGray;
                }
            }
        }

        /* We disable the Remove_Ellipse button if the current player has
         * no Ellipses to remove, so we do not need to check the count here
         * before removing an Ellipse. However, if we changed things so that
         * each player could take more than one action before we advance to
         * the next player, then we'd need to check our count!
         */
        private void Remove_Ellipse_Click(object sender, RoutedEventArgs e)
        {
            var updateMe = FindName($"Player{playerNumber}") as StackPanel;
            updateMe.Children.RemoveAt(updateMe.Children.Count - 1);
            AdvancePlayer();
            UpdateUI();
        }

        /* BASIC CHALLENGE: Can you apply the logic that we use to control Remove_Ellipse
         * to similarly ensure that each player is never allowed to create more Ellipses
         * than will fit in their box?
         * 
         * INTERMEDIATE CHALLENGE: Modify the basic challenge so that the number of Ellipses
         * is constrained by the current StackPanel size, but give each player a different
         * width of StackPanel.
         */ 
        private void Add_Ellipse_Click(object sender, RoutedEventArgs e)
        {
            var updateMe = FindName($"Player{playerNumber}") as StackPanel;
            updateMe.Children.Add(new Ellipse { Width = 64, Height = 128, Fill = Brushes.Blue });
            AdvancePlayer();
            UpdateUI();
        }
    }
}
