namespace KHMBibliotheek.ViewModels;
public partial class FileUploadErrorViewModel : ObservableObject
{
    [ObservableProperty]
    public string ? fileName;

    [ObservableProperty]
    public string ? reason;

    public ObservableCollection<FileUploadErrorModel> filesUploadError { get; set; }
}
