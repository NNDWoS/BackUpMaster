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
using System.Windows.Shapes;

namespace BackUpMaster
{
    /// <summary>
    /// Логика взаимодействия для OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        public OptionsWindow()
        {
            InitializeComponent();
        }

        public OptionsWindow(bool deleteAllFilesFlag, bool CreateNewFolderFlag)
        {
            InitializeComponent();
            RewriteCheckBox.IsChecked = deleteAllFilesFlag;
            NewFolderCheckBox.IsChecked = CreateNewFolderFlag;
            FolderNameTextBox.IsEnabled = CreateNewFolderFlag;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (NewFolderCheckBox.IsChecked.HasValue)
            {
                MainWindow.CreateNewFolderFlag = NewFolderCheckBox.IsChecked.Value;
                if (NewFolderCheckBox.IsChecked.Value)
                    MainWindow.FolderName = FolderNameTextBox.Text;
            } 
            if (RewriteCheckBox.IsChecked.HasValue)
                MainWindow.deleteAllFilesFlag = RewriteCheckBox.IsChecked.Value;
            
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void NewFolderCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            FolderNameTextBox.IsEnabled = NewFolderCheckBox.IsChecked.Value;
        }

        private void NewFolderCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            FolderNameTextBox.IsEnabled = NewFolderCheckBox.IsChecked.Value;
        }
    }
}
