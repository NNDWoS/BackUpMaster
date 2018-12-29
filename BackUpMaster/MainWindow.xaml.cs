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
            ModeComboBox.Items.Add(WorkMode.ALL_Long);
            ModeComboBox.Items.Add(WorkMode.DOCS);
            ModeComboBox.Items.Add(WorkMode.IMAGES);
            ModeComboBox.Items.Add(WorkMode.MUSIC);
            ModeComboBox.Items.Add(WorkMode.GP_Files);
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
            StatCheck();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (_filesToBackUp.Count == 0)
            {
                MessageBox.Show("No files to backup.", "Result", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (CreateNewFolderFlag)
                Directory.CreateDirectory(_pathToSave + "\\" + FolderName);

            ProgressBar.Maximum = _filesToBackUp.Count;

            void DoWork()
            {
                foreach (FileInfo file in _filesToBackUp)
                {
                    File.Copy(file.FullName, _pathToSave + "\\" + ((CreateNewFolderFlag) ? $"{FolderName}\\" : "") + file.Name);
                    ++ProgressBar.Value;
                }
            }

            Task task = new Task(() => DoWork());
            task.Start();
            Task.WaitAll(task);
            

            MessageBox.Show("Backup Done!", "Result", MessageBoxButton.OK, MessageBoxImage.Information);
            ProgressBar.Value = 0;
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
            OptionsWindow optionsWindow = new OptionsWindow(deleteAllFilesFlag, CreateNewFolderFlag);
            optionsWindow.ShowDialog();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePicker.SelectedDate.HasValue)
                _date = DatePicker.SelectedDate.Value;
            StatCheck();
        }

        private void DiskComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _driveIndex = DiskComboBox.SelectedIndex;
            StatCheck();
        }

        private void ModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _modeIndex = ModeComboBox.SelectedIndex;
            StatCheck();
        }

        //
        // Custom private methods
        //

        private void MakeDefaultState()
        {
            // After Backing up
        }

        private void StatCheck()
        {
            if (_date != DateTime.MaxValue && _pathToSave != String.Empty && _driveIndex != -1 && _modeIndex != -1)
                AddStatistics();
        }


        private void AddStatistics()
        {
            MessageBox.Show("Program start preparing statistics for backup. Please wait a little. Press \"OK\" to continue. ");
            _filesToBackUp = new List<FileInfo>();
            DirectoryInfo dir = _drives[_driveIndex].RootDirectory;
            double space = 0;

            void AddFile(FileInfo file)
            {
                if (file.LastWriteTime > _date)
                {
                    _filesToBackUp.Add(file);
                    space += file.Length;
                }
            }

            void FindAllFiles(DirectoryInfo directory, string filter)
            {
                try
                {
                    if (Directory.GetDirectories(directory.FullName).Length != 0)
                    {
                        foreach (string d in Directory.GetDirectories(directory.FullName))
                        {
                            DirectoryInfo t = new DirectoryInfo(d);
                            FindAllFiles(t, filter);
                        }
                    }

                
                    foreach (FileInfo file in directory.GetFiles(filter, SearchOption.TopDirectoryOnly))
                    {
                        AddFile(file);
                    }

                }
                catch (UnauthorizedAccessException) {  }

            }

            switch (_modeIndex)
            {
                case 0:
                    FindAllFiles(dir, "*");
                    break;
                case 1:
                    Task[] tasks = new Task[4];
                    tasks[0] = new Task(() => FindAllFiles(dir, "*.pdf"));
                    tasks[1] =  new Task(() => FindAllFiles(dir, "*.doc*"));
                    tasks[2] = new Task(() => FindAllFiles(dir, "*.odt"));
                    tasks[3] = new Task(() => FindAllFiles(dir, "*.txt"));
                    for (int i = 0; i < tasks.Length; ++i)
                        tasks[i].Start();
                    Task.WaitAll(tasks);
                    break;
                case 2:
                    tasks = new Task[4];
                    tasks[0] = new Task(() => FindAllFiles(dir, "*.jpg"));
                    tasks[1] = new Task(() => FindAllFiles(dir, "*.bmp"));
                    tasks[2] = new Task(() => FindAllFiles(dir, "*.jpeg"));
                    tasks[3] = new Task(() => FindAllFiles(dir, "*.tiff"));
                    for (int i = 0; i < tasks.Length; ++i)
                        tasks[i].Start();
                    Task.WaitAll(tasks);
                    break;
                case 3:
                    tasks = new Task[3];
                    tasks[0] = new Task(() => FindAllFiles(dir, "*.mp3"));
                    tasks[1] = new Task(() => FindAllFiles(dir, "*.wav"));
                    tasks[2] = new Task(() => FindAllFiles(dir, "*.flac"));
                    for (int i = 0; i < tasks.Length; ++i)
                        tasks[i].Start();
                    Task.WaitAll(tasks);
                    break;
                case 4:
                    tasks = new Task[5];
                    tasks[0] = new Task(() => FindAllFiles(dir, "*.gp"));
                    tasks[1] = new Task(() => FindAllFiles(dir, "*.gp6"));
                    tasks[2] = new Task(() => FindAllFiles(dir, "*.gp5"));
                    tasks[3] = new Task(() => FindAllFiles(dir, "*.gp4"));
                    tasks[4] = new Task(() => FindAllFiles(dir, "*.gp3"));
                    for (int i = 0; i < tasks.Length; ++i)
                        tasks[i].Start();
                    Task.WaitAll(tasks);
                    break;
                default:
                    MessageBox.Show("Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }

            DirectoryInfo saves = new DirectoryInfo(_pathToSave);
            if (!saves.Exists)
                throw new ArgumentNullException("РАЗРАБ ДОЛБАЕБ");

            saves = saves.Root;

            

            
            string MakeLessDigits(double data)
            {
                string[] size = { "B", "KB", "MB", "GB", "TB" };
                int volume = 0;
                while (data > 1024.0 && volume < size.Length)
                {
                    data /= 1024.0;
                    ++volume;
                }
                return $"{data:f1} {size[volume]}";
            }

            StorageDigitLabel.Content = MakeLessDigits(new DriveInfo(saves.Root.Name).AvailableFreeSpace);
            SpaceDigitLabel.Content = MakeLessDigits(space);
            FilesNumbsDigitLabel.Content = Convert.ToString(_filesToBackUp.Count);
            

        }

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
        private int _driveIndex = -1;

        private int _modeIndex = -1;

        private string _pathToSave = String.Empty;

        private DateTime _date = DateTime.MaxValue;

        private List<FileInfo> _filesToBackUp = null;

        // Other
        private enum WorkMode { ALL_Long, DOCS, IMAGES, MUSIC, GP_Files }
        public static bool deleteAllFilesFlag = false;
        public static bool CreateNewFolderFlag = false;
        public static string FolderName = String.Empty;
        
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
