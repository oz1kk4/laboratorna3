using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace HorseRace
{
    public partial class MainWindow : Window
    {
        static Random rand = new Random();
        static BrushConverter brush_converter = new BrushConverter();
        public static double TotalCash = 500.00;
        public static double CurrentBet = 0;
        DispatcherTimer timer;
        List<Horse> finished = new List<Horse>();


        public List<Horse> AllHorses = new List<Horse>
        {
            new Horse("Red Horse", Colors.Red, rand),
            new Horse("Magenta Horse", Colors.Magenta, rand),
            new Horse("Cyan Horse", Colors.DarkCyan, rand),
            new Horse("Orange Horse", Colors.DarkOrange, rand),
            new Horse("Blue Horse", Colors.DarkBlue, rand),
            new Horse("Yellow Horse", Colors.Yellow, rand),
            new Horse("Black Horse", Colors.Black, rand)
        };
        public List<Horse> HorseList = new List<Horse>();

        Line starting_line = new Line();
        Line finishing_line = new Line();

        public MainWindow()
        {
            InitializeComponent();
            SetRaceScreenBackground();
            TB_Bet.IsEnabled = true;
            CB_Horses.IsEnabled = true;

            int num_horses = rand.Next(3, 7); 
            int rand_index;
            for (int i = 0; i < num_horses; i++)
            {
                rand_index = rand.Next(AllHorses.Count);
                HorseList.Add(AllHorses[rand_index]);
                AllHorses.RemoveAt(rand_index);
                HorseList[i].Coords = new Point(HorseList[i].Coords.X, HorseList[i].Coords.Y + i * 150 / (num_horses - 1));
            }

            FixHorseOdds();
            for (int i = 0; i < num_horses; i++)
            {
                ComboBoxItem current_CBI = new ComboBoxItem();
                current_CBI.Content = HorseList[i].Name;
                current_CBI.Foreground = (Brush)brush_converter.ConvertFromString(HorseList[i].Colour.ToString());
                CB_Horses.Items.Add(current_CBI);
            }

            TK_TotalCash.Text = "$" + TotalCash.ToString();

            Loaded += delegate
            {
                Horse.TrackLength = BR_RaceScreen.ActualWidth;

                MakeTrack();

                ResetHorseXPostitions();
                MakeIMGs();
            };
        }
        private async void RunRace()
        {
            List<Task> tasks = new List<Task>();

            foreach (Horse horse in HorseList)
            {
                tasks.Add(horse.ChangeAcceleration());
            }

            await Task.WhenAll(tasks.ToArray());

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(15);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        private void FixHorseOdds()
        {
            float sum = 0;
            foreach (Horse i in HorseList)
            {
                sum += i.Odds;
            }
            foreach (Horse i in HorseList)
            {
                i.Odds = (float)Math.Round(i.Odds / (double)sum, 4);
            }
        }
        void MakeIMGs()
        {
            foreach (Horse i in HorseList)
            {
                CV_RaceScreen.Children.Add(i.IMG);
            }
        }
        private void SetRaceScreenBackground()
        {
            string imagePath = "back.jpg"; // Вкажіть шлях до своєї картинки
            ImageBrush imageBrush = new ImageBrush(new BitmapImage(new Uri(imagePath, UriKind.Relative)));
            CV_RaceScreen.Background = imageBrush;
        }
        void MakeTrack()
        {
            Rectangle rect = new Rectangle();
            rect.Margin = new Thickness() { Top = 120 };
            rect.Height = 300;
            rect.Width = BR_RaceScreen.ActualWidth;

            CV_RaceScreen.Children.Add(rect);

            starting_line.X1 = 100;
            starting_line.Y1 = 120;
            starting_line.X2 = 200;
            starting_line.Y2 = 420;
            starting_line.StrokeThickness = 5;
            CV_RaceScreen.Children.Add(starting_line);

            finishing_line.X1 = (int)Horse.TrackLength - starting_line.X2;
            finishing_line.Y1 = starting_line.Y1;
            finishing_line.X2 = (int)Horse.TrackLength - starting_line.X1;
            finishing_line.Y2 = starting_line.Y2;
            finishing_line.StrokeThickness = 5;
            CV_RaceScreen.Children.Add(finishing_line);
        }
        private void CB_Horses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_Horses.SelectedItem != null)
            {
                Horse selected_horse = HorseList[CB_Horses.SelectedIndex];
                if (TB_Bet.Text != "")
                {
                    BU_GO.IsEnabled = true;
                }
                else
                {
                    BU_GO.IsEnabled = false;
                }
            }
        }
        private void TB_Bet_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CB_Horses.SelectedItem != null)
            {
                BU_GO.IsEnabled = true;
            }
            else
            {
                BU_GO.IsEnabled = false;
            }
        }
        private void BU_GO_Click(object sender, RoutedEventArgs e)
        {
            BU_GO.IsEnabled = false;
            TB_Bet.IsEnabled = false;
            CB_Horses.IsEnabled = false;

            finished = new List<Horse>();

            try
            {
                CurrentBet = double.Parse(TB_Bet.Text);
                if (TotalCash >= CurrentBet && CurrentBet > 0)
                {
                    TotalCash -= CurrentBet;
                    TK_TotalCash.Text = "$" + TotalCash.ToString();

                    RunRace();
                }
                else
                {
                    throw new Exception("Invalid bet amount");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Your betting amount didn't make sense. \nRemember to only use a number amount less than your total cash!\n\nError: " + ex.Message);
                BU_GO.IsEnabled = true;
                TB_Bet.IsEnabled = true;
                CB_Horses.IsEnabled = true;
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            bool everyoneFinished = true;
            foreach (Horse i in HorseList)
            {
                if (!(i.Coords.X > Horse.LineX(i.Coords.Y, finishing_line)))
                {
                    i.Move();
                    everyoneFinished = false;
                    if (i.Coords.X > Horse.LineX(i.Coords.Y, finishing_line))
                    {
                        finished.Add(i);
                    }
                }
            }
            if (everyoneFinished)
            {
                timer.Stop();
                TB_Bet.IsEnabled = true;
                CB_Horses.IsEnabled = true;

                string dispMesg = "The race is over!\nHere's the standings:";
                for (int i = 0; i < finished.Count; i++)
                {
                    dispMesg += "\n#" + (i + 1).ToString() + " - " + finished[i].Name;
                }
                MessageBox.Show(dispMesg);


                CurrentBet = BetReturnAmount();
                TotalCash += Math.Round(CurrentBet, 2);
                CurrentBet = 0;
                TK_TotalCash.Text = "$" + Math.Round(TotalCash, 2).ToString();

                NewRace();
                return;
            }
        }
        void ResetHorseXPostitions()
        {
            foreach (Horse horse in HorseList)
            {
                horse.Coords = new Point(Horse.LineX(horse.Coords.Y, starting_line), horse.Coords.Y);
                horse.UpdateImage();
            }
        }
        void NewRace()
        {
            rand = new Random();
            AllHorses = new List<Horse>
            {
                new Horse("Red Horse", Colors.Red, rand),
                new Horse("Magenta Horse", Colors.Magenta, rand),
                new Horse("Cyan Horse", Colors.DarkCyan, rand),
                new Horse("Orange Horse", Colors.DarkOrange, rand),
                new Horse("Blue Horse", Colors.DarkBlue, rand),
                new Horse("Yellow Horse", Colors.Yellow, rand),
                new Horse("Black Horse", Colors.Black, rand)
            };
            HorseList = new List<Horse>();

            int num_horses = rand.Next(2, 7);

            int rand_index;
            for (int i = 0; i < num_horses; i++)
            {
                rand_index = rand.Next(AllHorses.Count);
                HorseList.Add(AllHorses[rand_index]);
                AllHorses.RemoveAt(rand_index);
                HorseList[i].Coords = new Point(HorseList[i].Coords.X, HorseList[i].Coords.Y + i * 150 / (num_horses - 1));
            }
            TB_Bet.IsEnabled = true;
            TB_Bet.Clear();
            CB_Horses.IsEnabled = true;
            CB_Horses.SelectedItem = null;

            FixHorseOdds();
            CB_Horses.Items.Clear();
            for (int i = 0; i < num_horses; i++)
            {
                ComboBoxItem current_CBI = new ComboBoxItem();
                current_CBI.Content = HorseList[i].Name;
                current_CBI.Foreground = (Brush)brush_converter.ConvertFromString(HorseList[i].Colour.ToString());
                CB_Horses.Items.Add(current_CBI);
            }

            TK_TotalCash.Text = "$" + TotalCash.ToString();

            CV_RaceScreen.Children.Clear();
            MakeTrack();
            MakeIMGs();
            ResetHorseXPostitions();

            BU_GO.IsEnabled = false;

        }
        double BetReturnAmount() 
        {
            double bet = CurrentBet;
            int place = 4; 
            double odd = 1;

            for (int i = 0; i < HorseList.Count; i++)
            {
                if (CB_Horses.SelectedItem.ToString().Contains(HorseList[i].Name))
                {
                    place = i + 1;
                    odd = (double)HorseList[i].Odds;
                }
            }

            double returnAmount;

            if (place > 3) 
            {
                returnAmount = Math.Round(bet * (2 / (odd + 2)), 2);
            }
            else if (place == 3) 
            {
                returnAmount = Math.Round(bet * (1 / (odd + 1)), 2);
            }
            else 
            {
                returnAmount = 0;
            }

            return returnAmount;
        }
    }
}