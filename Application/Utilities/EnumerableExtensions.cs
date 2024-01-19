using System.Threading.Tasks.Dataflow;

namespace Application.Utilities
{
    public static class EnumerableExtensions
    {
        private const int MaxParallelSelectTasks = 5;

        public static Task ForEachAsync<TSource>(
            this IEnumerable<TSource> source,
            Func<TSource, Task> body,
            int? maxDegreeOfParallelism = null)
        {
            var foreachBodyBlock = maxDegreeOfParallelism.HasValue
                ? new ActionBlock<TSource>(
                    body,
                    new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = maxDegreeOfParallelism.Value })
                : new ActionBlock<TSource>(body);

            source.ForEach(element => foreachBodyBlock.Post(element));

            foreachBodyBlock.Complete();

            return foreachBodyBlock.Completion;
        }

        public static async Task<IEnumerable<TResult>> SelectAsync<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, Task<TResult>> selector)
        {
            source.NotNull(nameof(source));
            selector.NotNull(nameof(selector));

            return await Task.WhenAll(source.Select(async s => await selector(s)));
        }

        public static async Task<IEnumerable<TResult>> SelectAsyncWithDegree<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, Task<TResult>> selector,
            int degreeOfParallelism = MaxParallelSelectTasks)
        {
            using var semaphore = new SemaphoreSlim(degreeOfParallelism);

            return await source.SelectAsync(async s =>
            {
                await semaphore.WaitAsync();

                try
                {
                    return await selector(s);
                }
                finally
                {
                    semaphore.Release();
                }
            });
        }

        public static void ForEach<T>(
                    this IEnumerable<T> source,
                    Action<T> action,
                    Func<T, Exception, bool> exceptionHandler = null)
        {
            source.NotNull(nameof(source));
            action.NotNull(nameof(action));

            foreach (var item in source)
            {
                try
                {
                    action(item);
                }
                catch (Exception x)
                {
                    if (exceptionHandler == null || !exceptionHandler(item, x))
                    {
                        throw;
                    }
                }
            }
        }
    }
}
