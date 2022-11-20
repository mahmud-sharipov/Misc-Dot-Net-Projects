using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TexAnalyzer
{
    public class Parser : INotifyPropertyChanged
    {
        readonly char[] litters = new char[] { 'а', 'б', 'в', 'г', 'ғ', 'д', 'е', 'ё', 'ж', 'з', 'и', 'ӣ', 'й', 'к', 'қ', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ӯ', 'ф', 'х', 'ҳ', 'ч', 'ҷ', 'ш', 'ъ', 'э', 'ю', 'я' };

        public Parser()
        {
            var streamReader = new StreamReader(FilePath);
            OriginText = streamReader.ReadToEnd();
            Text = OriginText.ToLower();
        }

        #region Lists

        private ObservableCollection<StatisticItem> oneGramma = new ObservableCollection<StatisticItem>();
        public ObservableCollection<StatisticItem> OneGramma
        {
            get { return oneGramma; }
            set { oneGramma = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OneGramma))); }
        }

        private ObservableCollection<StatisticItem> twoGramma = new ObservableCollection<StatisticItem>();
        public ObservableCollection<StatisticItem> TwoGramma
        {
            get { return twoGramma; }
            set { twoGramma = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TwoGramma))); }
        }

        private ObservableCollection<StatisticItem> threeGramma = new ObservableCollection<StatisticItem>();
        public ObservableCollection<StatisticItem> ThreeGramma
        {
            get { return threeGramma; }
            set { threeGramma = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ThreeGramma))); }
        }
        #endregion

        public RoutedCommand Calculate => new RoutedCommand(o => { Load(); });

        void Load()
        {
            var task1 = Task.Run(() =>
            {
                var list = new List<StatisticItem>();
                foreach (var item in litters)
                {
                    if (Text.Where(i => i == item).Count() is int count && count > 0)
                        list.Add(new StatisticItem() { Name = item.ToString(), Amount = count });
                }
                MainWindow.MyWindow.Dispatcher.InvokeAsync(() =>
                {
                    OneGramma.Clear();
                    list.ForEach((i) => OneGramma.Add(i));
                });
            });

            var task2 = Task.Run(() =>
             {
                 var list = new Dictionary<string, int>();
                 for (int i = 0; i < Text.Length - 1; i++)
                 {
                     if (!litters.Contains(Text[i]) || !litters.Contains(Text[i + 1]))
                         continue;
                     var combane = Text[i].ToString() + Text[i + 1];
                     if (!list.Keys.Contains(combane))
                         list.Add(combane, 0);
                     list[combane] += 1;
                 }
                 MainWindow.MyWindow.Dispatcher.InvokeAsync(() =>
                 {
                     TwoGramma.Clear();
                     list.ToList().ForEach(d => TwoGramma.Add(new StatisticItem() { Name = d.Key, Amount = d.Value }));
                 });
             });
            var task3 = Task.Run(() =>
            {
                var list = new Dictionary<string, int>();
                for (int i = 0; i < Text.Length - 2; i++)
                {
                    if (!litters.Contains(Text[i]) || !litters.Contains(Text[i + 1]) || !litters.Contains(Text[i + 2]))
                        continue;
                    var combane = Text[i].ToString() + Text[i + 1] + Text[i + 2];
                    if (!list.Keys.Contains(combane))
                        list.Add(combane, 0);
                    list[combane] += 1;
                }
                MainWindow.MyWindow.Dispatcher.InvokeAsync(() =>
                {
                    ThreeGramma.Clear();
                    list.ToList().ForEach(d => ThreeGramma.Add(new StatisticItem() { Name = d.Key, Amount = d.Value }));
                });
            });
        }

        private string filePath = @"Resources/Test.txt";

        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilePath))); }
        }

        private string text;
        public string Text
        {
            get { return text; }
            set { text = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Text))); }
        }

        private string originText;

        public string OriginText
        {
            get { return originText; }
            set { originText = value; Text = value.ToLower(); PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OriginText))); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class StatisticItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name))); }
        }

        private int amount;

        public int Amount
        {
            get { return amount; }
            set { amount = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Amount))); }
        }

    }
}
