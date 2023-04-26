namespace KHMBibliotheek.ViewModels;
public partial class MusicFilesViewModel : ObservableObject
{
    [ObservableProperty]
    public int scoreId = 0;

    [ObservableProperty]
    public string scoreNumber = "";

    [ObservableProperty]
    public string scoreTitle = "";

    [ObservableProperty]
    public int pDFORP = 0;

    [ObservableProperty]
    public int pDFORPId = 0;

    [ObservableProperty]
    public int pDFORK = 0;

    [ObservableProperty]
    public int pDFORKId = 0;

    [ObservableProperty]
    public int pDFTOP = 0;

    [ObservableProperty]
    public int pDFTOPId = 0;

    [ObservableProperty]
    public int pDFTOK = 0;

    [ObservableProperty]
    public int pDFTOKId = 0;

    [ObservableProperty]
    public int pDFPIA = 0;

    [ObservableProperty]
    public int pDFPIAId = 0;

    [ObservableProperty]
    public int mSCORP = 0;

    [ObservableProperty]
    public int mSCORPId = 0;

    [ObservableProperty]
    public int mSCORK = 0;

    [ObservableProperty]
    public int mSCORKId = 0;

    [ObservableProperty]
    public int mSCTOP = 0;

    [ObservableProperty]
    public int mSCTOPId = 0;

    [ObservableProperty]
    public int mSCTOK = 0;

    [ObservableProperty]
    public int mSCTOKId = 0;

    [ObservableProperty]
    public int mP3TOT = 0;

    [ObservableProperty]
    public int mP3TOTId = 0;

    [ObservableProperty]
    public int mP3T1 = 0;

    [ObservableProperty]
    public int mP3T1Id = 0;

    [ObservableProperty]
    public int mP3T2 = 0;

    [ObservableProperty]
    public int mP3T2Id = 0;

    [ObservableProperty]
    public int mP3B1 = 0;

    [ObservableProperty]
    public int mP3B1Id = 0;

    [ObservableProperty]
    public int mP3B2 = 0;

    [ObservableProperty]
    public int mP3B2Id = 0;

    [ObservableProperty]
    public int mP3SOL = 0;

    [ObservableProperty]
    public int mP3SOLId = 0;

    [ObservableProperty]
    public int mP3PIA = 0;

    [ObservableProperty]
    public int mP3PIAId = 0;

    [ObservableProperty]
    public int mP3UITV = 0;

    [ObservableProperty]
    public int mP3UITVId = 0;

    [ObservableProperty]
    public int mP3TOTVoiceId = 0;

    [ObservableProperty]
    public int mP3T1VoiceId = 0;

    [ObservableProperty]
    public int mP3T2VoiceId = 0;

    [ObservableProperty]
    public int mP3B1VoiceId = 0;

    [ObservableProperty]
    public int mP3B2VoiceId = 0;

    [ObservableProperty]
    public int mP3SOLVoiceId = 0;

    [ObservableProperty]
    public string searchField = "";

    [ObservableProperty]
    public object selectedItem = "";

    public ObservableCollection<MusicFilesModel> Scores { get; set; }

    public MusicFilesViewModel ( )
    {
        Scores = DBCommands.GetMusicFileInfo ( DBNames.MusicFilesView );
    }
}