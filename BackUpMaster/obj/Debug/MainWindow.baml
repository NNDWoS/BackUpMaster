   M S B A M L   `   `   `  ����G  CBackUpMaster, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:BackUpMasterBackUpMaster  5   	       BackUpMaster.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:BackUpMaster     5   	   -   5          � Title$  Back-Up Master��5   	   $	��362��6    $	��537��6-    � 
ResizeMode$ CanMinimize=�69   .��5	      � .+�5
   
   �� -   5         ��Name$ DiskComboBox��5
      $
��Left=�6)   $��	10,41,0,0q�6D   $	��Top=�6W   $	��245��6o   6{   �� 5   
   -   5         ��Name$ DiskUILabel��5      $"��Choose your disk to back-up:��6%   $
��Left=�6L   $��	10,10,0,0q�6g   $	��Top=�6z   $	��245��6�   $��Segoe UI Light)�6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ 
DatePicker��5      $
��Left=�6)   $��	10,99,0,0q�6D   $	��Top=�6W   $	��245��6o   6{   �� 5   
   -   5        $ DateUILabel��5      $��Choose date:��6%   $
��Left=�6<   $��	10,68,0,0q�6W   $	��Top=�6j   $	��245��6�    � ToolTip$h bThe program will copy all files to the back-up folder which was edited no sooner than picked date.��6�   �� 5   
   -   5        $ ModeUILabel��5      $#��Choose what files to back-up:��6%   $
��Left=�6M   $��
10,128,0,0q�6h   $	��Top=�6|   $	��245��6�   �� 5   
   -   5        $ ModeComboBox��5      $
��Left=�6)   $��
10,159,0,0q�6D   $	��Top=�6X   $	��245��6p   6|   �� 5   
   -   5        $ FolderUILabel��5      $ ��Choose the back-up folder:��6'   $
��Left=�6L   $��
10,186,0,0q�6g   $	��Top=�6{   $	��245��6�   �� 5   
   -	   5        $ FolderDisplayLabel��5      $��No folder chosen.��6,   $
��Left=�6H   $��
10,217,0,0q�6c   $	��Top=�6w   $	��245��6�   �� 5   
   -
   5         ��Name$ FolderChooseButton��5      $��Choose��6-   $
��Left=�6>   $��
10,248,0,0q�6Y   $	��Top=�6m   $	��245��6�   6�   �� 5   
   -   5        $ RussianButton��5      $��RU��6(   $
��Left=�65   $��489,248,0,0q�6P   $	��Top=�6e   $��20��6}   6�   �� 5   
   -   5        $ EnglishButton��5      $��EN��6(   $
��Left=�65   $��469,248,0,0q�6P   $	��Top=�6e   $��20��6}   6�   �� 5   
   -   5        $ 
SpaceLabel��5      $��Space needed: ��6$   $
��Left=�6=   $��
279,11,0,0q�6X   $	��Top=�6l   $	��142��6�   �� 5   
   -   5        $ FilesNumbLabel��5      $��Files to back-up:��6(   $
��Left=�6D   $��
279,63,0,0q�6_   $	��Top=�6s   $	��142��6�   �� 5   
   -   5        $ StorageLabel��5      $��Free storage space: ��6&   $
��Left=�6E   $��
279,37,0,0q�6`   $	��Top=�6t   $	��142��6�   �� 5   
   -   5        $ StartButton��5      $��Start Back-Up��6&   $
��Left=�6>   $��279,248,0,0q�6Y   $	��Top=�6n   $	��175��6�   6�   � 5   
   -   5         �Name$ ProgressBar��5      $
��Left=�6+   $��28��6F   $��
10,283,0,0q�6R   $	��Top=�6f   $	��499��6~   �� 5   
   -   5        $ OptionsButton��5      $��Options��6(   $
��Left=�6:   $��
279,94,0,0q�6U   $	��Top=�6i   $	��175��6�   $��29��6�   6�   �� 5   
   -   5        $ SpaceDigitLabel��5      $
��none��6)   $
��Left=�68   $��
426,11,0,0q�6S   $	��Top=�6g    J� RenderTransformOrigin$ -0.184,-0.077&�6   $��63��6�   �� 5   
   -   5        $ StorageDigitLabel��5      $
��none��6+   $
��Left=�6:   $��
426,37,0,0q�6U   $	��Top=�6i   $��63��6�   �� 5   
   -   5        $ FilesNumbsDigitLabel��5      $
��none��6.   $
��Left=�6=   $��
426,63,0,0q�6X   $	��Top=�6l   $��83��6�   5      5       