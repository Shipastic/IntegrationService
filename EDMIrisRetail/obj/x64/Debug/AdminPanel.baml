   M S B A M L   `   `   `  ����H  DEDMIrisRetail, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:EDMIrisRetailEDMIrisRetail  5   	       EDMIrisRetail.AdminPanel   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:EDMIrisRetail     5   	   _ [MaterialDesignThemes.Wpf, Version=4.3.0.3, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  
AdminPanel��5	   	   $	��700��6   $
��1200��6)   .��5
      � 
���   �$	��700��6   $
��1200��6.    � ColumnDefinitions 5   
   �� 5      $	��170 �6   �� 5      $
��628* �6   .+�5   
   �� $��Center=�6    �� Padding$ 10q�6-   $
��1200��6:    �� CornerRadius$ 20��6J   $	��700��6]   $��	0,36,0,35q�6n   
�������$��2��6�    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 10Q�6#      Color$ 	LightGray��63      ShadowDepth$ 10Q�6E   ._�5      �  � RowDefinitions 5      �� 5      $
��Auto �6$   �� 5      $��* �6$    5      �� 5      $	��170 �6'   �� 5      $��* �6'   �� 5      $
��Auto �6'   .+�5      �� $��0,0,0,0q�6   $��3��6.   $��0��6B   .+�5      �� $D��>Панель управления администратора��6    $��20(�6H   $
��Bold"�6V   $��0 0 0 10q�6h   $
��Left=�6z   $��Center=�6�   5       � 5"      -   5        	 �Name$	 	SlideMenu��5"      $��0��6*   $��1��6:   $��0,0,0,21q�6G   .+�5#      �� $��2,0,2,0q�6   ���	#FF303031���	#FF2B7657$	��700��6f   $	��Top=�6v   $	��480��6�   ._�5$      �� $��0,0,-2,0q�6%   $
��1200��67   
 �� Orientation$
 Vertical=�6H   $	��700��6_   .+�5%      �� -   5         ��Name$ BtnEmployee��5%   %   $	��170��5&   %   $	��100��5'   %   $��80��5(   %    "  MaterialDesignFlatAccentButton#��[  5*   %   $��16(�5+   %   $��1q�5,   %   ���	#FF1E2924.��5.   "   �� -   5         ��Name$	 emp��5.   -   $
 Vertical=�6:   .+�5/   &   �� $��Source/emp_3.png��6,   $
��None=�6F   �� 50   &   $��Сотрудники��60   51   #   52      �� 54      -   5        $ BtnDepartments��54   %   $	��170��55   )   $	��100��56   )   $��80��57   )   #��[  59   )   $��16(�5:   )   $��1q�5;   )   ���	#FF1E2924$��Right=�5<   A   $	��169��6]   $��
0,0,-0.2,0q�6i   .��5=   "   �� -   5        $ organization��5=   -   $
 Vertical=�6C   .+�5>   &   �� $��Source/organization_5.png��6,   $
��None=�6O   �� 5?   &   $ ��Подразделения��60   5@   #   5A      �� 5C      -   5        $ BtnOrganization��5C   %   $	��170��5D   %   $	��100��5E   %   $��80��5F   %   #��[  5H   %   $��16(�5I   %   $��1q�5J   %   ���	#FF1E2924.��5L   "   �� -   5        $ contragents��5L   -   $
 Vertical=�6B   .+�5M   &   �� $��Source/contrag.png��6,   $
��None=�6H   �� 5N   &   $��Поставщики��60   5O   #   5P      �� 5R      -   5        $ BtnDoc��5R   %   $	��170��5S   %   $	��100��5T   %   $��80��5U   %   #��[  5W   %   $��16(�5X   %   $��1q�5Y   %   ���	#FF1E2924.��5[   "   �� -	   5        $ 	documents��5[   -   $
 Vertical=�6@   .+�5\   &   �� $��Source/doc_2.png��6,   $
��None=�6F   �� 5]   &   $%��Документы Диадок��60   5^   #   5_      �� 5a      -
   5        $ BtnInDB��5a   %   $	��170��5b   $   $	��100��5c   %   $��80��5d   %   #��[  5e   %   $��16(�5f   %   $��1q�5g   %   ���	#FF1E2924.��5i   "   �� -   5        $ documentsDB��5i   -   $
 Vertical=�6B   .+�5j   &   �� $��Source/doc_2.png��6,   $
��None=�6F   �� 5k   &   $"��Документы из БД��60   5l   #   5m      �� 5o      -   5        $ BtnRun��5o   %   $	��170��5p   %   $	��100��5q   %   $��80��5r   %   #��[  5s   %   $��16(�5t   %   $��1q�5u   %   ���	#FF1E2924.��5w   "   �� -   5        $ mainservice��5w   -   $
 Vertical=�6B   .+�5x   &   �� $��Source/run.png��6,   $
��None=�6D   �� 5y   &   $��
Старт��60   5z   #   5{      5|      5}      5~      �� 5�      -   5        $ tabDocuments��5�      $��1��63   $��1��6C   $y�Visible=�6P   .+�5�      �� -   5         ��Name$	 doc��5�      
���   �$ 20��6>    - )MaterialDesignToolBarMainPanelBorderStyle#��[ 6P   ._�5�      �� -   5         ��Name$ ScrollDocIn��5�   '   $��Visible=�5�   #   $��Visible=�5�   #   $	��500��5�   #     MaterialDesignScrollViewer#��[ 60   .��5�      �� -   5         ��Name$ 	tabUPDDoc��5�   )   $y�Visible=�5�   !    ) %MaterialDesignNavigatilRailTabControl#��[ 5�   !   $	��700��5�   !    J� RenderTransformOrigin$ 0.499,0.503&�66   $��5,5,5,0q�5�      .@�5�   "   �� -   5         ��Name$ UPDDoc��5�   "   $	��UPD��5�   2   
��� � � �� 
IsSelected . $	��200��5�   5   .��5�   &   &   System.Windows.Controls.DataGrid  -   5          Name$ 	itemInDoc��5�   /   $y�Visible=�5�   '      AutoGenerateColumns .     CanUserAddRows .     CanUserSortColumns .    CanUserDeleteRows .     SelectionMode$ Single=�5�   '     MaterialDesignDataGrid#��[ 5�   '   $	��380��5�   '      GridLinesVisibility$	 All=�5�   '   $��	0,-2,0,90q�6B    & "MaterialDesignDataGridColumnHeader   ColumnHeaderStyle# [ 5�   /      Columns 5�   *   0  *System.Windows.Controls.DataGridTextColumn  5�   .   ,  &System.Windows.Controls.DataGridColumn   Header$ Отправитель��6A      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6c   �� *	FromDep+   HeaderStyle 5�   2   �� 5�   6     �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"   6<   .�5�   :   �� ! �� Property! � ��" �� Value
" �   �6W   �� 5�   :   ! � ��
" �����6W   5�   7   5�   /     5�   .   $) #Название документа��6A   $
 Auto 6�    6c   �� *Title+ 5�   2   �� 5�   6   "   6<   .�5�   :   �� ! � ��
" �   �6W   �� 5�   :   ! � ��
" �����6W   5�   7   5�   /     5�   .   $' !Тег подразделения��6A   $
 Auto 6�    6c   �� *	ShortName+ 5�   2   �� 5�   6   "   6<   .�5�   :   �� ! � ��
" �   �6W   �� 5�   :   ! � ��
" �����6W   5�   7   5�   /     5�   .   $ Статус��6A   $
 Auto 6�    6c   �� *Status+ 5�   2   �� 5�   6   "   6<   .�5�   :   �� ! � ��
" �   �6W   �� 5�   :   ! � ��
" �����6W   5�   7   5�   /     5�   .   $! Дата получения��6A   $
 Auto 6�    6c   �� *
CreateDate+ 5�   2   �� 5�   6   "   6<   .�5�   :   �� ! � ��
" �   �6W   �� 5�   :   ! � ��
" �����6W   5�   7   5�   /     5�   .   $/ )Дополнительный статус��6A   $
 Auto 6�    6c   �� *ToDep+ 5�   2   �� 5�   6   "   6<   .�5�   :   �� ! � ��
" �   �6W   �� 5�   :   ! � ��
" �����6W   5�   7   5�   /     5�   .   $ 	Entity ID��6A   $
 Auto 6�    6c   �� *
EntityID+ 5�   2   �� 5�   6   "   6<   .�5�   :   �� ! � ��
" �   �6W   �� 5�   :   ! � ��
" �����6W   5�   7   5�   /     5�   .   $ 
Message ID��6A   $
 Auto 6�    6c   �� *	MessageID+ 5�   2   �� 5�   6   "   6<   .�5�   :   �� ! � ��
" �   �6W   �� 5�   :   ! � ��
" �����6W   5�   7   5�   /   5�   '   5�   #   �� 5�   "   -   5        $ SFDoc��5�   "   $��
SFRevision��5�   2   
��� � � . $	��200��5�   5   .��5�   &     -   5        $ 	itemSFDoc��5�   /   $y�Visible=�5�   '    .   .   .  .  $ Single=�5�   '   #��[ 5�   '   $	��380��5�   '   $	 All=�5�   '   $��	0,-2,0,90q�6B   # [ 5�   /    5�   *     5�   .   $ Отправитель��6A   $
 Auto 6�    6c   �� *	FromDep+ 5�   2   �� 5�   6   "   6<   .�5�   :   �� ! � ��
" �   �6W   �� 5�   :   ! � ��
" �����6W   5�   7   5�   /     5�   .   $) #Название документа��6A   $
 Auto 6�    6c   �� *Title+ 5�   2   �� 5   6   "   6<   .�5  :   �� ! � ��
" �   �6W   �� 5  :   ! � ��
" �����6W   5  7   5  /     5  .   $' !Тег подразделения��6A   $
 Auto 6�    6c   �� *	ShortName+ 5  2   �� 5  6   "   6<   .�5	  :   �� ! � ��
" �   �6W   �� 5
  :   ! � ��
" �����6W   5  7   5  /     5  .   $ Статус��6A   $
 Auto 6�    6c   �� *Status+ 5  2   �� 5  6   "   6<   .�5  :   �� ! � ��
" �   �6W   �� 5  :   ! � ��
" �����6W   5  7   5  /     5  .   $! Дата получения��6A   $
 Auto 6�    6c   �� *
CreateDate+ 5  2   �� 5  6   "   6<   .�5  :   �� ! � ��
" �   �6W   �� 5  :   ! � ��
" �����6W   5  7   5  /     5  .   $/ )Дополнительный статус��6A   $
 Auto 6�    6c   �� *ToDep+ 5  2   �� 5   6   "   6<   .�5!  :   �� ! � ��
" �   �6W   �� 5"  :   ! � ��
" �����6W   5#  7   5%  /     5&  .   $ Строка 1а��6A   $
 Auto 6    6c   �� *Title+ 5'  2   �� 5(  6   "   6<   .�5)  :   �� ! � ��
" �   �6W   �� 5*  :   ! � ��
" �����6W   5+  7   5-  /   5/  '   50  #   �� 52  "   -   5        $ SFCorDoc��54  "   $��SFCorrection��52  2   
��� � � . $	��200��55  5   .��56  &     -   5        $ itemSFCorDoc��56  /   $y�Visible=�57  '    .   .   .  .  $ Single=�5<  '   #��[ 5=  '   $	��380��5>  '   $	 All=�5?  '   $��	0,-2,0,90q�6B   # [ 5A  /    5B  *     5C  .   $ Отправитель��6A   $
 Auto 6�    6c   �� *	FromDep+ 5D  2   �� 5E  6   "   6<   .�5F  :   �� ! � ��
" �   �6W   �� 5G  :   ! � ��
" �����6W   5H  7   5J  /     5K  .   $) #Название документа��6A   $
 Auto 6�    6c   �� *Title+ 5L  2   �� 5M  6   "   6<   .�5N  :   �� ! � ��
" �   �6W   �� 5O  :   ! � ��
" �����6W   5P  7   5R  /     5S  .   $' !Тег подразделения��6A   $
 Auto 6�    6c   �� *	ShortName+ 5T  2   �� 5U  6   "   6<   .�5V  :   �� ! � ��
" �   �6W   �� 5W  :   ! � ��
" �����6W   5X  7   5Z  /     5[  .   $ Статус��6A   $
 Auto 6�    6c   �� *Status+ 5\  2   �� 5]  6   "   6<   .�5^  :   �� ! � ��
" �   �6W   �� 5_  :   ! � ��
" �����6W   5`  7   5b  /     5c  .   $! Дата получения��6A   $
 Auto 6�    6c   �� *
CreateDate+ 5d  2   �� 5e  6   "   6<   .�5f  :   �� ! � ��
" �   �6W   �� 5g  :   ! � ��
" �����6W   5h  7   5j  /     5k  .   $/ )Дополнительный статус��6A   $
 Auto 6�    6c   �� *ToDep+ 5l  2   �� 5m  6   "   6<   .�5n  :   �� ! � ��
" �   �6W   �� 5o  :   ! � ��
" �����6W   5p  7   5r  /     5s  .   $ Строка 1а��6A   $
 Auto 6    6c   �� *Title+ 5t  2   �� 5u  6   "   6<   .�5v  :   �� ! � ��
" �   �6W   �� 5w  :   ! � ��
" �����6W   5x  7   5z  /   5|  '   5}  #   5~     5     5�     5�     �� 5�     -   5        $ tabDocumentsDB��5�     $��1��65   $��1��6E   $y�Visible=�6R   .+�5�     �� -   5        $ docDB��5�  !   
���   �$ 20��6D   #��[ 6V   ._�5�     �� -   5        $ ScrollDocInDB��5�  +   $��Visible=�5�  #   $��Visible=�5�  #   $	��500��5�  #   #��[ 60   .��5�       -   5        $ 
itemOutDoc��5�  '   $y�Visible=�5�  '    .   .   .  .  $ Single=�5�  '   #��[ 5�  '   $	��380��5�  '   $	 All=�5�  '   # [ 5�  0    5�  "     5�  &   $) #Название документа��69   $
 Auto 6}    6[   �� *
NameFile+ 5�  *   �� 5�  .   "   64   .�5�  2   �� ! � ��
" �   �6O   �� 5�  2   ! � ��
" �����6O   5�  /   5�  '     5�  &   $' !Тег подразделения��69   $
 Auto 6w    6[   �� *cntTag+ 5�  *   �� 5�  .   "   64   .�5�  2   �� ! � ��
" �   �6O   �� 5�  2   ! � ��
" �����6O   5�  /   5�  '     5�  &   $ Статус��69   $
 Auto 6z    6[   �� *status+ 5�  *   �� 5�  .   "   64   .�5�  2   �� ! � ��
" �   �6O   �� 5�  2   ! � ��
" �����6O   5�  /   5�  '     5�  &   $! Дата получения��69   $
 Auto 6}    6[   �� *InsertDateDoc+ 5�  *   �� 5�  .   "   64   .�5�  2   �� ! � ��
" �   �6O   �� 5�  2   ! � ��
" �����6O   5�  /   5�  '     5�  &   $0 *Статус согласования РУ��69   $
 Auto 6~    6\   �� *
isArrove+ 5�  *   �� 5�  .   "   64   .�5�  2   �� ! � ��
" �   �6O   �� 5�  2   ! � ��
" �����6O   5�  /   5�  '     5�  &   $ 	Entity ID��69   $
 Auto 6z    6[   �� *
entityId+ 5�  *   �� 5�  .   "   64   .�5�  2   �� ! � ��
" �   �6O   �� 5�  2   ! � ��
" �����6O   5�  /   5�  '     5�  &   $ 
Message ID��69   $
 Auto 6z    6[   �� *	messageId+ 5�  *   �� 5�  .   "   64   .�5�  2   �� ! � ��
" �   �6O   �� 5�  2   ! � ��
" �����6O   5�  /   5�  '   5�     5�     5�     5�     �� 5�     -   5        $ panelEmp��5�  :   $��1��6   $��1��6-   $y�Hidden=�6L   .+�5�     �� -   5        $ 	ScrollEmp��5�  #   $
��Auto=�5�  #   $
��Auto=�5�  #   $	��500��5�  #   #��[ 5�  #   .��5�       -   5        $ gridEmployee��5�  #   $y�Visible=�5�  #    .   .   .  .  $ Single=�5�  #   # [ 5�  $    5�       5�  "   $ Имя��65   $
 Auto 6w    6L   �� # �� Path$# FirstNameIris� 5�  &   �� 5�  *   "   60   .�5�  .   �� ! � ��
" �   �6K   �� 5�  .   ! � ��
" �����6K   5�  +   5�  #     5�  "   $ Фамилия��65   $
 Auto 6w    6L   �� $# LastNameIris� 5�  &   �� 5�  *   "   60   .�5�  .   �� ! � ��
" �   �6K   �� 5�  .   ! � ��
" �����6K   5�  +   5  #     5  "   $ Отчество��65   $
 Auto 6w    6L   �� $# MiddleNameIris� 5  &   �� 5  *   "   60   .�5  .   �� ! � ��
" �   �6K   �� 5  .   ! � ��
" �����6K   5  +   5	  #     5
  "   $ 
Логин��65   $
 Auto 6w    6L   �� $# 	LoginIris� 5  &   �� 5  *   "   60   .�5  .   �� ! � ��
" �   �6K   �� 5  .   ! � ��
" �����6K   5  +   5  #     5  "   $ Должность��65   $
 Auto 6w    6L   �� $# PositionIris� 5  &   �� 5  *   "   60   .�5  .   �� ! � ��
" �   �6K   �� 5  .   ! � ��
" �����6K   5  +   5  #     5  "   $  Подразделение��65   $
 Auto 6w    6L   �� $# DepartmentTagIris� 5  &   �� 5  *   "   60   .�5  .   �� ! � ��
" �   �6K   �� 5  .   ! � ��
" �����6K   5  +   5!  #   5#     5$     5%     �� 5)     -   5        $ panelDepart��5)  :   $��1��6   $��1��6-   $y�Hidden=�6O   .+�5;     �� -    5        $ 	ScrollDep��5;  #   $
��Auto=�5<  #   $
��Auto=�5=  #   $	��500��5>  #   #��[ 5?  #   .��5@       -!   5        $ gridDepartments��5@  #   $y�Visible=�5A      .   .   .  .  $ Single=�5F  #   # [ 5H  $    5I       5J  "   $1 +Название подразделения��65   $
 Auto 5L  5    5K  5   �� $# DepartNameIris� 5M  &   �� 5N  *   "   60   .�5O  .   �� ! � ��
" �   �6K   �� 5P  .   ! � ��
" �����6K   5Q  +   5S  #     5T  "   $' !Тег подразделения��65   $
 Auto 5V  5    5U  5   �� $# AbbreviationIris� 5W  &   �� 5X  *   "   60   .�5Y  .   �� ! � ��
" �   �6K   �� 5Z  .   ! � ��
" �����6K   5[  +   5]  #     5^  "   $ КПП��65   $
 Auto 5`  5    5_  5   �� $# KppIris� 5a  &   �� 5b  *   "   60   .�5c  .   �� ! � ��
" �   �6K   �� 5d  .   ! � ��
" �����6K   5e  +   5g  #     5h  "   $ 
Адрес��65   $
 Auto 5j  5    5i  5   �� $# AddressIris� 5k  &   �� 5l  *   "   60   .�5m  .   �� ! � ��
" �   �6K   �� 5n  .   ! � ��
" �����6K   5o  +   5q  #   5s     5t     5u     �� 5y     -"   5        $ panelContragents��5y     $��1��68   $��1��6H   $y�Hidden=�6U   .+�5�     �� -#   5        $ ScrollContr��5�  #   $
��Auto=�5�  #   $
��Auto=�5�  #   $	��500��5�  #   #��[ 5�  #   .��5�       -$   5        $ gridContractors��5�  #   $y�Hidden=�5�      .   .   .  .  $ Single=�5�     # [ 5�  $    5�       5�  "   $' !Название компании��65   $
 Auto 6s    6P   �� $
# Name� 5�  &   �� 5�  *   "   60   .�5�  .   �� ! � ��
" �   �6K   �� 5�  .   ! � ��
" �����6K   5�  +   5�  #     5�  "   $ ИНН��65   $
 Auto 6s    6P   �� $	# INN� 5�  &   �� 5�  *   "   60   .�5�  .   �� ! � ��
" �   �6K   �� 5�  .   ! � ��
" �����6K   5�  +   5�  #     5�  "   $ КПП��65   $
 Auto 6s    6P   �� $	# KPP� 5�  &   �� 5�  *   "   60   .�5�  .   �� ! � ��
" �   �6K   �� 5�  .   ! � ��
" �����6K   5�  +   5�  #     5�  "   $# Дата добавления��65   $
 Auto 6s    6P   �� $# 	dateStart� 5�  &   �� 5�  *   "   60   .�5�  .   �� ! � ��
" �   �6K   �� 5�  .   ! � ��
" �����6K   5�  +   5�  #   5�     5�     5�     �� 5�     $��2��6   $��1��6.   5�     5�     5�     5�     5�     