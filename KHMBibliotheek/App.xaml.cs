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
    public App()
    {
       //Register Syncfusion license
       string _syncfusionLicenseKey = "Mgo+DSMBaFt+QHJqUE1hXk5Hd0BLVGpAblJ3T2ZQdVt5ZDU7a15RRnVfR19iSXZScUZmX35XeA==;Mgo+DSMBPh8sVXJ1S0R+WVpFdEBBXHxAd1p/VWJYdVt5flBPcDwsT3RfQF5jTHxSdk1iXnxbdndXTg==;ORg4AjUWIQA/Gnt2VFhiQlRPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9gSXtRd0VrWHhddXJRQ2g=;MjEzMzg0NUAzMjMxMmUzMjJlMzVmTXd6bENkbFp4TThtNE9GWGpGc055L1Q3R1AyWnFkZGVzcmZoM0dYZ0JnPQ==;MjEzMzg0NkAzMjMxMmUzMjJlMzVDSFRUL2U4T0cyajNBUFBGYlZxWWtEOFVrQXdTYVZaeDRVUFNnMjZRL0Y4PQ==;NRAiBiAaIQQuGjN/V0d+Xk9BfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5WdERjUX5Zc3FSRGBU;MjEzMzg0OEAzMjMxMmUzMjJlMzVqTFJUTUpNb040Y1czNkNPWEcrckJWVTU0aWsyNFNzeUVKMXFlSVJzdEprPQ==;MjEzMzg0OUAzMjMxMmUzMjJlMzVUc1VkcE9LQ1Z3TENJUENrY3ZaeFlra2FuWEU2VjZUcUhBYkZoU3lMeG9jPQ==;Mgo+DSMBMAY9C3t2VFhiQlRPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9gSXtRd0VrWHhddXNcQ2g=;MjEzMzg1MUAzMjMxMmUzMjJlMzVQcTI0aHdWZkdDeC9VdlNGVjJja09LUm9IdHhwb0hsUWdiQUxmSVJGNkFNPQ==;MjEzMzg1MkAzMjMxMmUzMjJlMzVGZzhjMzZUQUpJZkNaK0RGM2hpR0U3RDZmQkw5S0l6Q1NYbHY0QVpYMUdvPQ==;MjEzMzg1M0AzMjMxMmUzMjJlMzVqTFJUTUpNb040Y1czNkNPWEcrckJWVTU0aWsyNFNzeUVKMXFlSVJzdEprPQ==";
       Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(_syncfusionLicenseKey);
    }
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
