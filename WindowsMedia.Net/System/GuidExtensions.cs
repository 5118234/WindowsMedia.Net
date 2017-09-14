namespace System
{
#if NET35
    public static class GuidExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="source"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryParse(this Guid item, string source, out Guid result)
        {
            var parseResult = false;
            try
            {
                result = new Guid(source);
                parseResult = true;
            }
            catch
            {
                result = Guid.Empty;
            }

            return parseResult;
        }
    }
#endif
}