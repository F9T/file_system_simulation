namespace FileSystemSimulation.Settings
{
    public static class Settings
    {
        public static int SectorSize = 512;
        //ClusterSize must be a multiple of SectorSize
        public static int ClusterSize = 4 * SectorSize;

        public static int NumberCluster = 500;
    }
}