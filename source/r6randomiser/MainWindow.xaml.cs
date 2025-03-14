﻿using System;
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
                "Ace", "Amaru", "Ash", "Blackbeard", "Blitz", "Brava", "Buck", "Capitao", "Dokkaebi", "Finka", "Flores", "Fuze", "Glaz", "Gridlock", "Grim", "Hibana", "Iana", "IQ", "Jackal", "Kali", "Lion", "Maverick", "Montagne", "Nokk", "Nomad", "Osa", "Sens", "Sledge", "Thatcher", "Thermite", "Twitch", "Ying", "Zero", "Zofia", "Ram"
            };

        //Empty elimination list of defenders
        List<String> defendersElimination = new List<String>();

        //Empty elimination list of attackers
        List<String> attackersElimination = new List<String>();

        List<String> eliminationMemory = new List<String>();

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

            githubCheck();

        }
        public void RandomiseDefender()
        {
            //Randomly select a defender from the list
            if (defendersElimination.Count == 0) { defendersElimination.AddRange(defenders); }
            Random random = new Random();
            int index = random.Next(defendersElimination.Count);
            String defender = defendersElimination[index];
            if (Elimination) { 
                defendersElimination.Remove(defender); 
                eliminationMemory.Add(defender);
                if (eliminationMemory.Count > 5) { eliminationMemory.RemoveAt(0); }
            }

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
            if (Elimination) { 
                attackersElimination.Remove(attacker);
                eliminationMemory.Add(attacker);
                if (eliminationMemory.Count > 5) { eliminationMemory.RemoveAt(0); }
                lbUndo.Content = "Undo: " + eliminationMemory.Count();

            }

            //Display the attacker in the text box
            lbOperatorName.Content = attacker;

            imgOperator.Source = (DrawingImage)FindResource(attacker.ToLower() + "DrawingImage");
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
                attackersElimination.Clear();
                attackersElimination.AddRange(attackers);
                defendersElimination.Clear();
                defendersElimination.AddRange(defenders);
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

        private void lbUndo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (eliminationMemory.Count > 0)
            {
                if (attackers.Contains(eliminationMemory[eliminationMemory.Count - 1]))
                {
                    attackersElimination.Add(eliminationMemory[eliminationMemory.Count - 1]);
                    eliminationMemory.RemoveAt(eliminationMemory.Count - 1);
                    lbAttackerCount.Content = "A: " + attackersElimination.Count;
                }
                else
                {
                    defendersElimination.Add(eliminationMemory[eliminationMemory.Count - 1]);
                    eliminationMemory.RemoveAt(eliminationMemory.Count - 1);
                    lbDefenderCount.Content = "D: " + defendersElimination.Count;
                }
                lbUndo.Content = "Undo: " + eliminationMemory.Count();
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double aspectRatio = 2.0 / 3.0; // set your desired aspect ratio here
            if (e.WidthChanged)
            {
                this.Height = e.NewSize.Width / aspectRatio;
            }
            else
            {
                this.Width = e.NewSize.Height * aspectRatio;
            }
        }

        private void lbClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        //Check for new github release
        private void githubCheck()
        {
            //https://api.github.com/repos/EmilKlock/r6randomiser/releases/latest
            //string gitrepo = "https://api.github.com/repos/EmilKlock/R6-Randomiser-WPF/releases/latest";
            //get the latest release from github
            //string latestRelease = new System.Net.WebClient().DownloadString(gitrepo);
            //get the version number from the latest release
            //string latestVersion = latestRelease.Substring(latestRelease.IndexOf("tag_name") + 11, 5);
            //get the version number from the current release
            //string currentVersion = "v1.0.2";
            //compare the two version numbers
            //if (latestVersion != currentVersion)
            //{
            //    //if the latest version is newer than the current version, display a message box
            //    MessageBox.Show("There is a new version of R6 Randomiser available. \n\nCurrent Version: " + currentVersion + "\nLatest Version: " + latestVersion + "\n\nPlease download the latest version from: https://github.com/EmilKlock/R6-Randomiser-WPF/releases/latest");
            //}


        }
    }
}
