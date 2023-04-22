namespace KHMBibliotheek.ViewModels;
public partial class ScoreViewModel : BaseScoreViewModel
{
    public ScoreViewModel ( )
    {
        Scores = DBCommands.GetScores ( DBNames.ScoresView, "nosort", null, null );
    }
}
