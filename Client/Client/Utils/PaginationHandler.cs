namespace Client.Utils
{
    public static class PaginationHandler
    {
        public static string Get(string href = null)
        {
            return href == null ? $"pageSize={100}&pageNumber={1}" : href.Substring(href.IndexOf("?"));
        }
    }
}
