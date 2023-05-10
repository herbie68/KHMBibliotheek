namespace KHMBibliotheek;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow ( )
    {
        InitializeComponent ( );
        tbUserName.Text = LibraryUsers.SelectedUserFullName;
        tbLogedInUserName.Text = LibraryUsers.SelectedUserName;
        tbLogedInFullName.Text = LibraryUsers.SelectedUserFullName;
        FilePaths.DownloadPath = @"C:\Users\herbert.nijkamp\OneDrive - Wolters Kluwer\Downloads\KHM";

        // Set the value to control weather or not an administrator has logged in
        if ( LibraryUsers.SelectedUserRoleId == 4 || LibraryUsers.SelectedUserRoleId == 6 || LibraryUsers.SelectedUserRoleId == 8 || LibraryUsers.SelectedUserRoleId == 15 )
        {
            tbShowAdmin.Text = "Visible";
        }
        else
        {
            tbShowAdmin.Text = "Collapsed";
        }
    }

    #region Button Close | Restore | Minimize 
    #region Button Close
    private void BtnClose_Click ( object sender, RoutedEventArgs e )
    {
        DBCommands.WriteLog ( LibraryUsers.SelectedUserId, DBNames.LogUserLoggedOut, $"{tbLogedInFullName.Text} heeft de applicatie afgesloten" );
        Close ( );
    }
    #endregion

    #region Button Restore
    private void BtnRestore_Click ( object sender, RoutedEventArgs e )
    {
        if ( WindowState == WindowState.Normal )
            WindowState = WindowState.Maximized;
        else
            WindowState = WindowState.Normal;
    }
    #endregion

    #region Button Minimize
    private void BtnMinimize_Click ( object sender, RoutedEventArgs e )
    {
        WindowState = WindowState.Minimized;
    }
    #endregion
    #endregion

    #region Drag Window
    private void Window_MouseDown ( object sender, MouseButtonEventArgs e )
    {
        if ( e.LeftButton == MouseButtonState.Pressed )
        {
            DragMove ( );
        }
    }
    #endregion

    #region MenuLeft PopupButton
    #region Score Menu
    #region On Click
    private void BtnScores_Click ( object sender, RoutedEventArgs e )
    {
        fContainer.Navigate ( new System.Uri ( "Views/Scores.xaml", UriKind.RelativeOrAbsolute ) );
    }
    #endregion

    #region On Mouse Enter
    private void BtnScores_MouseEnter ( object sender, MouseEventArgs e )
    {
        if ( Tg_Btn.IsChecked == false )
        {
            Popup.PlacementTarget = btnScores;
            Popup.Placement = PlacementMode.Right;
            Popup.IsOpen = true;
            Header.PopupText.Text = "Partituren overzicht";
        }
    }
    #endregion

    #region On Mouse Leave
    private void BtnScores_MouseLeave ( object sender, MouseEventArgs e )
    {
        Popup.Visibility = Visibility.Collapsed;
        Popup.IsOpen = false;
    }
    #endregion
    #endregion

    #region Maintain scores Menu
    #region On Click
    private void BtnMaintainScores_Click ( object sender, RoutedEventArgs e )
    {
        fContainer.Navigate ( new System.Uri ( "Views/MaintainScores.xaml", UriKind.RelativeOrAbsolute ) );
    }
    #endregion

    #region On Mouse Enter
    private void BtnMaintainScores_MouseEnter ( object sender, MouseEventArgs e )
    {
        if ( Tg_Btn.IsChecked == false )
        {
            Popup.PlacementTarget = btnMaintainScores;
            Popup.Placement = PlacementMode.Right;
            Popup.IsOpen = true;
            Header.PopupText.Text = "Onderhoud muziekbestanden";
        }
    }
    #endregion

    #region On Mouse Leave
    private void BtnMaintainScores_MouseLeave ( object sender, MouseEventArgs e )
    {
        Popup.Visibility = Visibility.Collapsed;
        Popup.IsOpen = false;
    }
    #endregion
    #endregion

    #region Users Profile Menu
    #region On Click
    private void BtnUserProfile_Click ( object sender, RoutedEventArgs e )
    {
        fContainer.Navigate ( new System.Uri ( "Views/UserProfile.xaml", UriKind.RelativeOrAbsolute ) );
    }
    #endregion

    #region On Mouse Enter
    private void BtnUserProfile_MouseEnter ( object sender, MouseEventArgs e )
    {
        if ( Tg_Btn.IsChecked == false )
        {
            Popup.PlacementTarget = btnUserProfile;
            Popup.Placement = PlacementMode.Right;
            Popup.IsOpen = true;
            Header.PopupText.Text = "Wijzig de gebruikers gegevens";
        }
    }
    #endregion

    #region On Mouse Leave
    private void BtnUserProfile_MouseLeave ( object sender, MouseEventArgs e )
    {
        Popup.Visibility = Visibility.Collapsed;
        Popup.IsOpen = false;
    }
    #endregion
    #endregion

    #region Users Management
    #region On Click
    private void BtnUsersManagement_Click ( object sender, RoutedEventArgs e )
    {
        fContainer.Navigate ( new System.Uri ( "Views/UserManagement.xaml", UriKind.RelativeOrAbsolute ) );
    }
    #endregion

    #region On Mouse Enter
    private void BtnUsersManagement_MouseEnter ( object sender, MouseEventArgs e )
    {
        if ( Tg_Btn.IsChecked == false )
        {
            Popup.PlacementTarget = btnUsersManagement;
            Popup.Placement = PlacementMode.Right;
            Popup.IsOpen = true;
            Header.PopupText.Text = "Gebruikers beheer";
        }
    }
    #endregion

    #region On Mouse Leave
    private void BtnUsersManagement_MouseLeave ( object sender, MouseEventArgs e )
    {
        Popup.Visibility = Visibility.Collapsed;
        Popup.IsOpen = false;
    }
    #endregion
    #endregion

    #region Logging
    #region On Click
    private void BtnLogging_Click ( object sender, RoutedEventArgs e )
    {
        fContainer.Navigate ( new System.Uri ( "Views/History.xaml", UriKind.RelativeOrAbsolute ) );
    }
    #endregion

    #region On Mouse Enter
    private void BtnLogging_MouseEnter ( object sender, MouseEventArgs e )
    {
        if ( Tg_Btn.IsChecked == false )
        {
            Popup.PlacementTarget = btnLogging;
            Popup.Placement = PlacementMode.Right;
            Popup.IsOpen = true;
            Header.PopupText.Text = "Log bestand";
        }
    }
    #endregion

    #region On Mouse Leave
    private void BtnLogging_MouseLeave ( object sender, MouseEventArgs e )
    {
        Popup.Visibility = Visibility.Collapsed;
        Popup.IsOpen = false;
    }
    #endregion
    #endregion
    #endregion

    #region Reload MainPage (After UserFullName update)
    // Make reload of the MainWindow possible from the age where the UserName can be changed, so it will be updated in the MainWindow after save
    public static void ReloadMainWindow ( )
    {
        MainWindow newMainWindow = new();
        newMainWindow.Show ( );
        Application.Current.MainWindow.Close ( );
        Application.Current.MainWindow = newMainWindow;
    }
    #endregion

}
