namespace EcommercialWebApp.Core.Extensions
{
    public static class MapperExtension
    {
        public static IQueryable Projecto<T>(this IQueryable<T> source) where T : class
        {
            return source;
        }
    }
}
