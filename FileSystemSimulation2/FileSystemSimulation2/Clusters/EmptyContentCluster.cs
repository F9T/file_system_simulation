using FileSystemSimulation2.Clusters.Abstract;

namespace FileSystemSimulation2.Clusters
{
    public class EmptyContentCluster : AbstractContentCluster
    {
        public EmptyContentCluster()
        {
            Content = "";
            Color = Settings.EmptyClusterColor;
        }
    }
}
