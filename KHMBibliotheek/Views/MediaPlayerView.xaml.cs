namespace KHMBibliotheek.Views;
/// <summary>
/// Interaction logic for MediaPlayer.xaml
/// </summary>
public partial class MediaPlayerView : Window
{
    public MediaPlayerView ( string _path, string _file )
    {
        InitializeComponent ( );

        //var duration = MP3MediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;

        tbFileName.Text = _file;
        MP3MediaElement.Source = new Uri ( _path );
    }

    private void btnStopClick ( object sender, RoutedEventArgs e )
    {
        MP3MediaElement.Stop ( );
    }

    private void btnPauseClick ( object sender, RoutedEventArgs e )
    {
        MP3MediaElement.Pause ( );
    }

    private void btnPlayClick ( object sender, RoutedEventArgs e )
    {
        MP3MediaElement.Play ( );
        MP3MediaElement.Volume = ( double ) volumeSlider.Value;
    }

    // Change the volume of the media.
    private void ChangeMediaVolume ( object sender, RoutedPropertyChangedEventArgs<double> args )
    {
        MP3MediaElement.Volume = ( double ) volumeSlider.Value;
    }

    // When the media opens, initialize the "Seek To" slider maximum value
    // to the total number of miliseconds in the length of the media clip.
    private void Element_MediaOpened ( object sender, EventArgs e )
    {
        timelineSlider.Maximum = MP3MediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;
        //tbDuration.Text = MP3MediaElement.NaturalDuration.TimeSpan.TotalMilliseconds.ToString ( );
    }

    // When the media playback is finished. Stop() the media to seek to media start.
    private void Element_MediaEnded ( object sender, EventArgs e )
    {
        MP3MediaElement.Stop ( );
    }

    // Jump to different parts of the media (seek to).
    private void SeekToMediaPosition ( object sender, RoutedPropertyChangedEventArgs<double> args )
    {
        int SliderValue = (int)timelineSlider.Value;

        // Overloaded constructor takes the arguments days, hours, minutes, seconds, milliseconds.
        // Create a TimeSpan with miliseconds equal to the slider value.
        TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
        MP3MediaElement.Position = ts;
    }

    private void Window_Closing ( object sender, System.ComponentModel.CancelEventArgs e )
    {
        MP3MediaElement.Close ( );
    }
}
