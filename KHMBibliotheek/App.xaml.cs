﻿global using System;
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
    //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense( "Mgo+DSMBaFt+QHFqVkFrXVNbdV5dVGpAd0N3RGlcdlR1fUUmHVdTRHRcQl5gSX5bc0ZjWnxYdnQ=;Mgo+DSMBPh8sVXJ1S0d+X1ZPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9gSX1QdkVrXHxec3ddQGA=;ORg4AjUWIQA/Gnt2VFhhQlJDfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5QdUVjUXpdcHdRRmhc;MTU1NzY0NEAzMjMxMmUzMTJlMzMzN2NZQUcrQTJ5VWJ4RDE3dDBjWXdxYnFkdjV2ZmExYlBuWUMxYStiMnhkZ1U9;MTU1NzY0NUAzMjMxMmUzMTJlMzMzN2J2N1JUOGg4dWRNSjNkb2R6QXdDdTZIOVJ1NTBTam50L3J2SHdDTWgvbG89;NRAiBiAaIQQuGjN/V0d+XU9HcVRDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS31TdUZjWXdbc3RWQ2FeUQ==;MTU1NzY0N0AzMjMxMmUzMTJlMzMzN1BTN0dYZ2hKWGhxNnVjdDFuT3FFMzlIL1I1MXN6NjUwQmVFVVpPVGU0Vjg9;MTU1NzY0OEAzMjMxMmUzMTJlMzMzN1ZkbTJtanBWc2E2Z3RKQjZMRXljTnAza3ZYbUh0UHFEL05CME16ZXgvT2c9;Mgo+DSMBMAY9C3t2VFhhQlJDfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5QdUVjUXpdcHdTR2lc;MTU1NzY1MEAzMjMxMmUzMTJlMzMzN2hTRFZpZkcrZzJuWit4NUMxQjJ1SGtVQzM1bVp2RFRzUTg2OWNTbjNwYUU9;MTU1NzY1MUAzMjMxMmUzMTJlMzMzN1RtRXkreUxvNW56UWd6ekZVUEpZeUJmclhPNUpqQVNKYUUxUnVuM1JIVU09;MTU1NzY1MkAzMjMxMmUzMTJlMzMzN1BTN0dYZ2hKWGhxNnVjdDFuT3FFMzlIL1I1MXN6NjUwQmVFVVpPVGU0Vjg9" );
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