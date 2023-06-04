using System;
using System.Collections.Generic;
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

namespace r6randomiser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //String List of all defenders in Rainbow Six Siege
        List<String> defenders = new List<String>()
        {
                "Alibi", "Aruni", "Azami",  "Bandit", "Castle", "Caveira", "Clash", "Doc", "Echo", "Ela", "Fenrir", "Frost", "Goyo", "Jager", "Kaid", "Kapkan", "Lesion", "Maestro", "Melusi", "Mira", "Mozzie", "Mute", "Oryx", "Pulse", "Rook", "Smoke", "Solis", "Tachanka", "Thorn", "Thunderbird", "Valkyrie", "Vigil", "Wamai", "Warden"
            };

        //String List of all attackers in Rainbow Six Siege
        List<String> attackers = new List<String>()
        {
                "Ace", "Amura", "Ash", "Blackbeard", "Blitz", "Brava", "Buck", "Capitao", "Dokkaebi", "Finka", "Flores", "Fuze", "Glaz", "Gridlock", "Grim", "Hibana", "Iana", "IQ", "Jackal", "Kali", "Lion", "Maverick", "Montagne", "Nokk", "Nomad", "Osa", "Sens", "Sledge", "Thatcher", "Thermite", "Twitch", "Ying", "Zero", "Zofia"
            };

        //Empty elimination list of defenders
        List<String> defendersElimination = new List<String>();

        //Empty elimination list of attackers
        List<String> attackersElimination = new List<String>();

        bool Elimination = false;
        bool Attacker = true;

        public MainWindow()
        {
            InitializeComponent();
            attackersElimination.Clear();
            attackersElimination.AddRange(attackers);
            defendersElimination.Clear();
            defendersElimination.AddRange(defenders);
            lbAttackerCount.Content = "A: " + attackersElimination.Count;
            lbDefenderCount.Content = "D: " + defendersElimination.Count;

        }
        public void RandomiseDefender()
        {
            //Randomly select a defender from the list
            if (defendersElimination.Count == 0) { defendersElimination.AddRange(defenders); }
            Random random = new Random();
            int index = random.Next(defendersElimination.Count);
            String defender = defendersElimination[index];
            if (Elimination) { defendersElimination.Remove(defender); }

            //Display the defender in the text box
            lbOperatorName.Content = defender;
            
            //Setting imgOperator.Source to the image of the operator with source as {StaticResource "LowerCaseOperatorName"DrawingImage}
            imgOperator.Source = (DrawingImage)FindResource(defender.ToLower()+"DrawingImage");
        }

        public void RandomiseAttacker()
        {
            //Randomly select an attacker from the list
            if (attackersElimination.Count == 0) { attackersElimination.AddRange(attackers); }
            Random random = new Random();
            int index = random.Next(attackersElimination.Count);
            String attacker = attackersElimination[index];
            if (Elimination) { attackersElimination.Remove(attacker); }

            //Display the attacker in the text box
            lbOperatorName.Content = attacker;

            imgOperator.Source = (DrawingImage)FindResource(attacker.ToLower() + "DrawingImage");
        }


        private void btnRandom_Click(object sender, RoutedEventArgs e)
        {
            //Elimination = btnRandom.IsChecked.Value;
            attackersElimination.Clear();
            attackersElimination.AddRange(attackers);
            defendersElimination.Clear();
            defendersElimination.AddRange(defenders);
        }


        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();

        }

        private void lbNextOperator_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Attacker)
            {
                RandomiseAttacker();
            }
            else
            {
                RandomiseDefender();
            }

            lbAttackerCount.Content = "A: " + attackersElimination.Count;
            lbDefenderCount.Content = "D: " + defendersElimination.Count;
        }

        private void lbElimination_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(Elimination)
            {
                Elimination = false;
                lbElimination.Content = "Elimination: Off";
                //Setting lbElimination.Foreground to the hex value of #AAAAAA
                lbElimination.Foreground = new SolidColorBrush(Color.FromArgb(255, 170, 170, 170));
                lbElimination.FontWeight = FontWeights.Thin;
            }
            else
            {
                Elimination = true;
                lbElimination.Content = "Elimination: On";
                lbElimination.Foreground = Brushes.White;
                lbElimination.FontWeight = FontWeights.Bold;
            }
        }

        private void lbAttacker_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Attacker = true;
            lbAttacker.FontWeight = FontWeights.Bold;
            lbAttacker.Foreground = Brushes.White;
            lbDefender.FontWeight = FontWeights.Thin;
            lbDefender.Foreground = new SolidColorBrush(Color.FromArgb(255, 170, 170, 170));
        }

        private void lbDefender_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Attacker = false;
            lbDefender.FontWeight = FontWeights.Bold;
            lbDefender.Foreground = Brushes.White;
            lbAttacker.FontWeight = FontWeights.Thin;
            lbAttacker.Foreground = new SolidColorBrush(Color.FromArgb(255, 170, 170, 170));
        }

        private void lbHide_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //minimise the window
            this.WindowState = WindowState.Minimized;
        }
    }
}
