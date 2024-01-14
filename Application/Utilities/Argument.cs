namespace Application.Utilities
{
    public static class Argument
    {
        public static void NotNull<T>(this T argument, string argumentName)
        {
            if (argumentName == null)
            {
                throw new ArgumentNullException(nameof(argumentName));
            }

            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}
