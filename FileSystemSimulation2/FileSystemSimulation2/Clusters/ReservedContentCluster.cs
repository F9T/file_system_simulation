using FileSystemSimulation2.Clusters.Abstract;

namespace FileSystemSimulation2.Clusters
{
    public class ReservedContentCluster : AbstractContentCluster
    {
        public ReservedContentCluster()
        {
            Content = "reserved";
            Color = Settings.ReservedClusterColor;
        }
    }
}
