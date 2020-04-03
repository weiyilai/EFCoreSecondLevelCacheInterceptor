using EFCoreSecondLevelCacheInterceptor.Tests.DataLayer.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EFCoreSecondLevelCacheInterceptor.Tests
{
    [TestClass]
    public class Bootstrapper
    {
        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            clearAllCachedEntries();
            startDb();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
        }

        private static void clearAllCachedEntries()
        {
            EFServiceProvider.GetRedisCacheServiceProvider().ClearAllCachedEntries();
        }

        private static void startDb()
        {
            var serviceProvider = EFServiceProvider.GetConfiguredContextServiceProvider(
                    TestCacheProvider.BuiltInInMemory,
                    logLevel: LogLevel.Debug,
                    cacheAllQueries: false);
            var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>();
            serviceScope.Initialize();
            serviceScope.SeedData();
        }
    }
}