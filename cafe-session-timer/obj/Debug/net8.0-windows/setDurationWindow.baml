   M S B A M L   `   `   `  ����  cafe-session-timer*    $cafe_session_timer.SetDurationWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Set Duration��5   	   $	��180��6   $	��300��6+    � WindowStartupLocation$ CenterOwner=�5   	    � 
ResizeMode$ NoResize=�6-   .��5      �� $��10q�6   .+�5   
   �� $��Choose duration:��6   $��0,0,0,5q�6,   �� 5   
   -   5         ��Name$ DurationCombo��5       �� SelectedIndex$ 0��5	      $��0,0,0,10q�5
      $��Stretch=�5      .@�5      �� $��
15 minutes��6   
 � Tag$ 15��60   �� 5      $��
30 minutes��6   $ 30��60   �� 5      $��1 hour��6   $ 60��61   �� 5      $��2 hours��6   $	 120��61   �� 5      $��3 hours��6   $	 180��61   �� 5      $��5 hours��6   $	 300��61   �� 5      $��10 hours��6   $	 600��61   5      �� 5   
    �� Orientation$ 
Horizontal=�6   $��Right=�6.   .+�5      �� -   5        $��OK��5      $��75��6"   $��5q�6-   68   �� 5      -   5        $��Cancel��5      $��75��6&   $��5q�61   6<   5      5      5      