namespace KooliProjekt.Application.Infrastructure.Files
{
    public static class FileStore
    {
        public const string Photos = "photos";

        // Iga toote fotod on oma kaustas: photos/{productId}
        public static string ProductPhotos(int productId)
        {
            return Photos + "/" + productId;
        }
    }
}
