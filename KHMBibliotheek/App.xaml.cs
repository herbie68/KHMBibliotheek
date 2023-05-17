global using System;
global using System.Collections.ObjectModel;
global using System.Data;
global using System.Diagnostics;
global using System.Globalization;
global using System.Security.Cryptography;
global using System.Text;
global using System.Text.RegularExpressions;
global using System.Windows;
global using System.Windows.Controls;
global using System.Windows.Controls.Primitives;
global using System.Windows.Input;
global using System.Windows.Media;
global using CommunityToolkit.Mvvm.ComponentModel;
global using KHMBibliotheek.Converters;
global using KHMBibliotheek.Helpers;
global using KHMBibliotheek.Models;
global using KHMBibliotheek.ViewModels;
global using MySql.Data.MySqlClient;

namespace KHMBibliotheek;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    //Register Syncfusion license
    //SyncfusionLicenseProvider.RegisterLicense( "Mgo+DSMBaFt+QHFqVkNrXVNbdV5dVGpAd0N3RGlcdlR1fUUmHVdTRHRcQlhiTn5Rck1jWXpacHU=;Mgo+DSMBPh8sVXJ1S0d+X1RPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9gSXtScUVhXXdecHFQQGE=;ORg4AjUWIQA/Gnt2VFhhQlJBfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5Wd0JjW3tWcHRQQWNd;MjA4NDk4N0AzMjMxMmUzMTJlMzMzNWJYTEMvRDV6Z05uL1NlbU43eExYQW94ZE43bUxmdXM5REJsQU1vV2hLNms9;MjA4NDk4OEAzMjMxMmUzMTJlMzMzNWZnaDA2NFVYYXdvTGYvcURiWEdYdEwyRUJXc3plV05Qdi9nWkM4Z1Y5aXM9;NRAiBiAaIQQuGjN/V0d+XU9Hc1RDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS31Tc0RkWX1aeHRVQGVcUQ==;MjA4NDk5MEAzMjMxMmUzMTJlMzMzNU5xajM3cFdGL0NPT0hvY0U4N1BEdDd4NGZlVHlSWExTMmhXZnNRRDJFd3c9;MjA4NDk5MUAzMjMxMmUzMTJlMzMzNWFDM3ZVRUs4em9kTmkyVUlxa1gxbWpOSHNFYWZJWW5qWjBYM0RGUVNRdGM9;Mgo+DSMBMAY9C3t2VFhhQlJBfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5Wd0JjW3tWcHRSQ2Fc;MjA4NDk5M0AzMjMxMmUzMTJlMzMzNVVFekVCeXFiN3VJem9sLys2WVVEVElCTjFLTmk4R3FXa3djejdWbm9xNWc9;MjA4NDk5NEAzMjMxMmUzMTJlMzMzNVF1cTk0VVNKLzdueHVjQ2t3b2Q3VFp0Y2UrL0hrZ1Vxa2dhRGxjUnB1TkE9;MjA4NDk5NUAzMjMxMmUzMTJlMzMzNU5xajM3cFdGL0NPT0hvY0U4N1BEdDd4NGZlVHlSWExTMmhXZnNRRDJFd3c9" );
}
public class LibraryUsers
{
    public static int SelectedUserId { get; set; }
    public static string? SelectedUserName { get; set; }
    public static string? SelectedUserFullName { get; set; }
    public static string? SelectedUserEmail { get; set; }
    public static string? SelectedUserPassword { get; set; }
    public static int SelectedUserRoleId { get; set; }
}

public class FilePaths
{
    public static string? DownloadPath { get; set; }
}