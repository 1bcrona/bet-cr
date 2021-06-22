namespace BetCR.Library
{
    public static class StringHelper
    {
        #region Public Methods

        public static string NormalizeString(this string str)
        {
            return str.ToLowerInvariant();
        }

        #endregion Public Methods
    }
}