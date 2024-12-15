using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace MasterMind
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] kleurenArray = {"Rood", "Geel", "Oranje", "Wit", "Groen", "Blauw"};
        string[] code;
        string[] highScores;

        string userName;

        int gamesCounter = 0;
        int pogingenCounter = 0;
        int maximaalAantalPogingen = 10;

        string geselecteerdeKleur1;
        string geselecteerdeKleur2;
        string geselecteerdeKleur3;
        string geselecteerdeKleur4;

        int score = 100;
        int aantalCorrecteKleuren = 0;


        SolidColorBrush brushCodeHistoriek1 = new SolidColorBrush(Colors.Black);
        SolidColorBrush brushCodeHistoriek2 = new SolidColorBrush(Colors.Black);
        SolidColorBrush brushCodeHistoriek3 = new SolidColorBrush(Colors.Black);
        SolidColorBrush brushCodeHistoriek4 = new SolidColorBrush(Colors.Black);

        Dictionary<string, SolidColorBrush> kleurenDictionary = new Dictionary<string, SolidColorBrush> { 
            { "Rood", new SolidColorBrush(Colors.Red) }, 
            { "Geel", new SolidColorBrush(Colors.Yellow) },
            { "Oranje", new SolidColorBrush(Colors.Orange) },
            { "Wit", new SolidColorBrush(Colors.White) },
            { "Groen", new SolidColorBrush(Colors.Green) },
            { "Blauw", new SolidColorBrush(Colors.Blue) },
        };

        public MainWindow()
        {
            InitializeComponent();
            userName = StartGame();
            GenereerCode();
            VulCbo();
            highScores = new string[15];
            

        }
        public void GenereerCode()
        {
            code = new string[4];
            Random random = new Random();
            int i = 0;
            List<int> alGebruikteRngs = new List<int>();

            while (i<4) 
            {
                int rng = random.Next(0, kleurenArray.Length);
                
                if (!alGebruikteRngs.Contains(rng))
                {
                    code[i] = kleurenArray[rng];
                    alGebruikteRngs.Add(rng);
                    i++;
                } 
            }
           
            this.Title = $"MasterMind({string.Join(",", code)})";
        }
        public void VulCbo()
        {
            int i = 0;
            while (i < kleurenArray.Length)
            {
                Cbo1.Items.Add(kleurenArray[i]);
                Cbo2.Items.Add(kleurenArray[i]);
                Cbo3.Items.Add(kleurenArray[i]);
                Cbo4.Items.Add(kleurenArray[i]);
                i++;
            }
        }


        private void Cbo1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Cbo1.SelectedIndex != -1)
            {
                geselecteerdeKleur1 = Cbo1.SelectedItem.ToString();
                Lbl1.Background = kleurenDictionary[geselecteerdeKleur1];
            }
        }

        private void Cbo2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Cbo2.SelectedIndex != -1)
            {
                geselecteerdeKleur2 = Cbo2.SelectedItem.ToString();
                Lbl2.Background = kleurenDictionary[geselecteerdeKleur2];
            }
        }

        private void Cbo3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Cbo3.SelectedIndex != -1)
            {
                geselecteerdeKleur3 = Cbo3.SelectedItem.ToString();
                Lbl3.Background = kleurenDictionary[geselecteerdeKleur3];
            }
        }

        private void Cbo4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Cbo4.SelectedIndex != -1)
            {
                geselecteerdeKleur4 = Cbo4.SelectedItem.ToString();
                Lbl4.Background = kleurenDictionary[geselecteerdeKleur4];
            }
        }

        private bool CheckCboFilled()
        {
            return Cbo1.SelectedIndex != -1 && Cbo2.SelectedIndex != -1 && Cbo3.SelectedIndex != -1 && Cbo4.SelectedIndex != -1;
        }
        private void BtnCheck_Click(object sender, RoutedEventArgs e)
        {
            int minPunten = 0;
            if (CheckCboFilled() && pogingenCounter<maximaalAantalPogingen)
            {
                if (code.Contains(Cbo1.SelectedItem.ToString()))
                {
                    Lbl1.BorderBrush = new SolidColorBrush(Colors.Wheat);
                    brushCodeHistoriek1 = new SolidColorBrush(Colors.Wheat);
                    minPunten++;
                    if (code[0].Equals(Cbo1.SelectedItem.ToString()))
                    {
                        Lbl1.BorderBrush = new SolidColorBrush(Colors.DarkRed);
                        brushCodeHistoriek1 = new SolidColorBrush(Colors.DarkRed);
                        minPunten--;
                        aantalCorrecteKleuren++;
                    }
                }
                else
                {
                    Lbl1.BorderBrush = new SolidColorBrush(Colors.Black);
                    brushCodeHistoriek1 = new SolidColorBrush(Colors.Black);
                    minPunten += 2;
                }
                if (code.Contains(Cbo2.SelectedItem.ToString()))
                {
                    Lbl2.BorderBrush = new SolidColorBrush(Colors.Wheat);
                    brushCodeHistoriek2 = new SolidColorBrush(Colors.Wheat);
                    minPunten++;
                    

                    if (code[1].Equals(Cbo2.SelectedItem.ToString()))
                    {
                        Lbl2.BorderBrush = new SolidColorBrush(Colors.DarkRed);
                        brushCodeHistoriek2 = new SolidColorBrush(Colors.DarkRed);
                        minPunten--;
                        aantalCorrecteKleuren++;

                    }
                }
                else
                {
                    Lbl2.BorderBrush = new SolidColorBrush(Colors.Black);
                    brushCodeHistoriek2 = new SolidColorBrush(Colors.Black);
                    minPunten += 2;

                }
                if (code.Contains(Cbo3.SelectedItem.ToString()))
                {
                    Lbl3.BorderBrush = new SolidColorBrush(Colors.Wheat);
                    brushCodeHistoriek3 = new SolidColorBrush(Colors.Wheat);
                    minPunten++;


                    if (code[2].Equals(Cbo3.SelectedItem.ToString()))
                    {
                        Lbl3.BorderBrush = new SolidColorBrush(Colors.DarkRed);
                        brushCodeHistoriek3 = new SolidColorBrush(Colors.DarkRed);
                        minPunten--;
                        aantalCorrecteKleuren++;

                    }
                }
                else
                {
                    Lbl3.BorderBrush = new SolidColorBrush(Colors.Black);
                    brushCodeHistoriek3 = new SolidColorBrush(Colors.Black);
                    minPunten += 2;

                }
                if (code.Contains(Cbo4.SelectedItem.ToString()))
                {
                    Lbl4.BorderBrush = new SolidColorBrush(Colors.Wheat);
                    brushCodeHistoriek4 = new SolidColorBrush(Colors.Wheat);
                    minPunten++;


                    if (code[3].Equals(Cbo4.SelectedItem.ToString()))
                    {
                        Lbl4.BorderBrush = new SolidColorBrush(Colors.DarkRed);
                        brushCodeHistoriek4 = new SolidColorBrush(Colors.DarkRed);
                        minPunten--;
                        aantalCorrecteKleuren++;

                    }
                }
                else
                {
                    Lbl4.BorderBrush = new SolidColorBrush(Colors.Black);
                    brushCodeHistoriek4 = new SolidColorBrush(Colors.Black);
                    minPunten += 2;

                }
                pogingenCounter++;
                score = score - minPunten;
                LblPogingen.Content = $"Poging {pogingenCounter}/{maximaalAantalPogingen}";
                FoutievePogingenToevoegen();
                LblScore.Content = $"Score: {score}";
                CheckCodeEnPogingen();
                aantalCorrecteKleuren = 0;
            }
            else if(pogingenCounter<maximaalAantalPogingen)
            {
                MessageBox.Show("Gelieve voor elke combobox een selectie te maken.");
            }
            else
            {
                MessageBox.Show("Geen pogingen meer.");
                //Tijdelijke messagebox, zal eleganter opgelost worden in latere versies.
            }
        }

        private void FoutievePogingenToevoegen()
        {
            StackPanel nieuwePoging = new StackPanel { Orientation = Orientation.Horizontal };

            string[] nieuwstePogingCode = { geselecteerdeKleur1, geselecteerdeKleur2, geselecteerdeKleur3, geselecteerdeKleur4};
            SolidColorBrush[] borderKLeuren = { brushCodeHistoriek1, brushCodeHistoriek2, brushCodeHistoriek3, brushCodeHistoriek4 };
            int i = 0;
            foreach (var kleur in nieuwstePogingCode)
            {
                Rectangle rect = new Rectangle
                {
                    Width = 50,
                    Height = 50,
                    Fill = kleurenDictionary[kleur],
                    Stroke = borderKLeuren[i],
                    StrokeThickness = 2,             
                    Margin = new Thickness(5)
                };
                nieuwePoging.Children.Add(rect);
                i++;
            }
            
            StackPanelHistorie.Children.Add(nieuwePoging);
        }

        private void CheckCodeEnPogingen()
        {
            if(aantalCorrecteKleuren == 4)
            {
                MessageBox.Show("Proficiat, u heeft de code gekraakt!");
                ResetGame();
            }
            else if(pogingenCounter == maximaalAantalPogingen)
            {
                string codeNaarCommaSepString = string.Join(", ", code);
                MessageBox.Show($"Geen pogingen meer! \n De code was : {codeNaarCommaSepString}");
                ResetGame();
            }
            /*
            if (aantalCorrecteKleuren == 4)
            {
                var result = MessageBox.Show(
                "Wilt u nog eens spelen?",
                "Proficiat, u heeft de code gekraakt!",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
                );


                if (result == MessageBoxResult.No)
                {
                    Close();
                }
                else if (result == MessageBoxResult.Yes)
                {
                    ResetGame();
                }
            } 

            else if (pogingenCounter == 10)
            {
                string codeNaarCommaSepString = string.Join(", ", code);
                var result = MessageBox.Show(
                $"De code was : {codeNaarCommaSepString} \n Wilt u nog eens spelen? ",
                "U heeft geen pogingen meer!",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
                );


                if (result == MessageBoxResult.No)
                {
                    Close();
                }
                ResetGame();
           
            }
             */
        }

        private void ResetGame()
        {
            //naam speler moet nog geimplementeert worden
            highScores[gamesCounter] = $"{userName} - {pogingenCounter} Pogingen - {score}/100";
            gamesCounter++;

            GenereerCode();
            Cbo1.SelectedIndex = -1;
            Cbo2.SelectedIndex = -1;
            Cbo3.SelectedIndex = -1;
            Cbo4.SelectedIndex = -1;

            Lbl1.Background = new SolidColorBrush(Colors.White);
            Lbl2.Background = new SolidColorBrush(Colors.White);
            Lbl3.Background = new SolidColorBrush(Colors.White);
            Lbl4.Background = new SolidColorBrush(Colors.White);

            Lbl1.BorderBrush = new SolidColorBrush(Colors.White);
            Lbl2.BorderBrush = new SolidColorBrush(Colors.White);
            Lbl3.BorderBrush = new SolidColorBrush(Colors.White);
            Lbl4.BorderBrush = new SolidColorBrush(Colors.White);

            StackPanelHistorie.Children.Clear();
            score = 100;
            pogingenCounter = 0;

            LblPogingen.Content = $"Poging 0/{maximaalAantalPogingen}";
            LblScore.Content = "Score: 100";

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show(
                "Ben je zeker dat je de applicatie wilt sluiten?",
                "Applicatie sluiten?",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
                );


            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }             
        }

        private string StartGame()
        {
            bool isUserNameChosen = false;
            while (!isUserNameChosen)
            {
                userName = Interaction.InputBox("Geef een username", "Username", "Jan", 500);
                if (userName != "" && userName != null)
                { 
                    isUserNameChosen = true;
                }
            }
            return userName;
        }

        private void MenuItemNieuwSpel_Click(object sender, RoutedEventArgs e)
        {
            ResetGame();
        }

        private void MenuItemHighscores_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(string.Join(", \n", highScores));
        }

        private void MenuItemInstellingen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemAfsluiten_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItemAantalPogingen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemAantalPogingen_Click_1(object sender, RoutedEventArgs e)
        {
            maximaalAantalPogingen = 1;
            while (maximaalAantalPogingen < 3 || maximaalAantalPogingen > 20)
            {
                maximaalAantalPogingen = Convert.ToInt32(Interaction.InputBox("Maximaal aantal pogingen:", "Max pogingen [3-20]", "10", 500));
            }
            LblPogingen.Content = $"Poging {pogingenCounter}/{maximaalAantalPogingen}";

        }
    }
}