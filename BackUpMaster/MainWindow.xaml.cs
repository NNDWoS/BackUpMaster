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
using System.IO;


namespace BackUpMaster
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //
        // Event Handlers.
        //
        
        private void Window_Initialized(object sender, EventArgs e)
        {
            InitUILables();
            _drives = DriveInfo.GetDrives();
            foreach (DriveInfo disk in _drives)
            {
                DiskComboBox.Items.Add(disk.Name);
            }
            ModeComboBox.Items.Add(WorkMode.ALL);
            ModeComboBox.Items.Add(WorkMode.DOCS);
        }

        private void FolderChooseButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    _pathToSave = dialog.SelectedPath;
                    FolderDisplayLabel.Content = dialog.SelectedPath;
                }
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EnglishButton_Click(object sender, RoutedEventArgs e)
        {
            if (_language != UILanguage.English)
            {
                _language = UILanguage.English;
                UpdateLabels();
            }
        }

        private void RussianButton_Click(object sender, RoutedEventArgs e)
        {
            if (_language != UILanguage.Russian)
            {
                _language = UILanguage.Russian;
                UpdateLabels();
            }
        }

        private void OptionsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DiskComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _driveIndex = DiskComboBox.SelectedIndex;
        }

        private void ModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _modeIndex = ModeComboBox.SelectedIndex;
        }

        //
        // Custom private methods
        //

        private void UpdateLabels()
        {
            foreach(KeyValuePair<Label, UILabelPair> pair in _UILabels)
            {
                pair.Key.Content = pair.Value.GetUILabel(_language);
            }

            foreach(KeyValuePair<Button, UILabelPair> pair in _UIButtonLabels)
            {
                pair.Key.Content = pair.Value.GetUILabel(_language);
            }

            foreach(KeyValuePair<Label, UILabelPair> pair in _UIDataLabels)
            {
                string temp = String.Empty;
                if (_language == UILanguage.English)
                    temp = pair.Value.GetUILabel(UILanguage.Russian);
                else
                    temp = pair.Value.GetUILabel(UILanguage.English);

                if (pair.Key.Content.Equals(temp))
                    pair.Key.Content = pair.Value.GetUILabel(_language);
            }
        }

        private void InitUILables()
        {
            _UILabels = new Dictionary<Label, UILabelPair>()
            {
                { DiskUILabel, new UILabelPair("Choose your disk to back-up:", "Выберите диск для резервного копирования:") },
                { DateUILabel, new UILabelPair("Choose Date:", "Выберите дату:") },
                { ModeUILabel, new UILabelPair("Choose what files to back-up:","Выберите какие файлы копировать:") },
                { FolderUILabel, new UILabelPair("Choose the back-up folder:","Выберите папку для сохранения:") },
                { SpaceLabel, new UILabelPair("Space needed: ","Необходимо места: ") },
                { StorageLabel, new UILabelPair("Free storage space: ","Свободная память: ") },
                { FilesNumbLabel, new UILabelPair("Files to back-up: ","Файлов на сохранение: ") },
            };

            _UIDataLabels = new Dictionary<Label, UILabelPair>()
            {
                { FolderDisplayLabel, new UILabelPair("No folder chosen.","Папка не выбрана.") },
                { SpaceDigitLabel, new UILabelPair("none","пусто") },
                { StorageDigitLabel, new UILabelPair("none","пусто") },
                { FilesNumbsDigitLabel, new UILabelPair("none","пусто") }
            };

            _UIButtonLabels = new Dictionary<Button, UILabelPair>()
            {
                { FolderChooseButton, new UILabelPair("Choose","Выбрать") },
                { StartButton, new UILabelPair("Start Back-Up","Начать сохранение") },
                { OptionsButton, new UILabelPair("Options","Опции") }
            };
        }

        private void InitModeComboBox()
        {

        }

        //
        // Data
        //


        // UI Labels
        private static UILanguage _language = UILanguage.English;
        private Dictionary<Label, UILabelPair> _UILabels;
        private Dictionary<Label, UILabelPair> _UIDataLabels;
        private Dictionary<Button, UILabelPair> _UIButtonLabels;

        // File system INFO
        private DriveInfo[] _drives;
        private int _driveIndex;

        private int _modeIndex;

        private string _pathToSave;

        // Other
        private enum WorkMode { ALL, DOCS }

    }

    public enum UILanguage { Russian, English }

    internal class UILabelPair
    {
        string Russian;
        string English;
        

        public string GetUILabel(UILanguage currentLanguage)
        {
            if (currentLanguage == UILanguage.Russian)
                return Russian;
            else
                return English;
        }

        public UILabelPair(string eng, string rus)
        {
            Russian = rus;
            English = eng;
        }
    }
}
