   M S B A M L   `   `   `  ����J  FGoogleThemeTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:GoogleThemeTestGoogleThemeTest  5   	   H DShowMeTheXAML, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullD2clr-namespace:ShowMeTheXAML;assembly=ShowMeTheXAMLShowMeTheXAML 5
   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	        GoogleThemeTest.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:GoogleThemeTest     5   	   V RMaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	   =smtx2clr-namespace:ShowMeTheXAML;assembly=ShowMeTheXAML  5
   	   3system&clr-namespace:System;assembly=mscorlib  5   	     � Title$  
MainWindow��5   	      MaterialDesignBody#���   5   	   $��Regular"�5   	   $��13(�5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	    � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $e _pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml?�6%   �� 5      $h bpack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml?�6%   �� 5      $z tpack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.DeepPurple.xaml?�6%   �� 5      $s mpack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.Lime.xaml?�6%   %    5      .��5      �  � RowDefinitions 5&   
   �� 5'      $��1* �6   �� 5(      $��5* �6   �� 5)      $��1* �6    � ColumnDefinitions 5+   
   �� 5,      $��5* �6   �� 5-      $
��Auto �6   �� 5.      $��1* �6   .+�51   
     ShowMeTheXAML.XamlDisplay  
   Key$ 	drawers_1��6   	 X� MaxValue#��Z	 6+   $��Stretch=�6Y   $	��Top=�6~   $��0��6�   $��0��6�   $��3��6�   .��52      )  #MaterialDesignThemes.Wpf.DrawerHost  $��Stretch=�6(   $��Stretch=�6F   
   LeftDrawerContent
 53      �� 54      $��Stretch=�6!   .+�55      ��    CloseDrawerCommand#��Z 6!   "  System.Windows.Controls.Dock   Left#��Z 56      $��48��57      $��48��6%   $
��Left=�61   .��58      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=null]]   Kind$
 Menu=�66   59      �� 5:      #��Z 6!   #��Z 5;      $
��Left=�5<   !     MaterialDesignFlatButton#���  5=      .��6L   
CLOSE THIS5?      �� 5@      #��Z 6!   $
��Left=�5A   !   #���  5B      .��6L   	CLOSE ALL5D      5E      .��5H      ��  �� Orientation$ 
Horizontal=�6   $��Stretch=�66   $	��Top=�6T   .+�5I      ��    OpenDrawerCommand#��Z 6   #��Z 5J      $��48��5K      $��48��6%   .��5L        $
 Menu=�62   5M      5N      5P      5Q      � 5V   
   $��1��6   $��1��6    5W      �� 5X      $
��Auto �6    60   �� 5Y      $
��Auto �6    60   �� 5Z      $
��Auto �6    60   �� 5[      $
��Auto �6    60    5]      �� 5^      $
��Auto �6#   �� 5_      $
��Auto �6#   �� 5`      $
��Auto �6#   .+�5b      #  MaterialDesignThemes.Wpf.Card  $��32q�6"   $��16q�6/   $��0��6;   $��0��6K   .��5c      ��    MaterialDesignTitleTextBlock#���  6   Tender System Analysis6k   5d      �� 5e      $��0��6   $��1��6%   $��2��62   .��6F   User6L   5f      5g      5h      