namespace App.Server.Data
{
    public class Validation
    {
        public class Song
        {
            public const int MaxDescriptionLength = 2000;

            public const int MaxTitleLength = 40;
        }
        
        public class User
        {
            public const int MaxNameLenght = 40;
            
            public const int MaxBiographyLenght = 200;
        }
    }
}
