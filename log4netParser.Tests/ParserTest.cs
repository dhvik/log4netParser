using System;
using System.IO;
using NUnit.Framework;

namespace log4netParser.Tests {
    /// <summary>
    /// Summary description for ParserTest.
    /// </summary>
    [TestFixture]
    public class ParserTest {
        /* *******************************************************************
		 *  SetupMethods 
		 * *******************************************************************/
        //fixturesetup
        //fixtureteardown

        /* *******************************************************************
		 *  Test Methods 
		 * *******************************************************************/

        [Test]
        public void TestAzureLogFormat() {
            var logData = new LogData(null);
            const string log = @"2016-03-01T07:40:10  PID[1028] Error       ImageVault.Core.Jobs.StoreMediaJob	Unable to StoreContentForAnalyzedMedia for ref i4hl421v9kecnqtnrn95	System.InvalidOperationException : The transaction associated with the current connection has completed but has not been disposed.  The transaction must be disposed before the connection can be used to execute SQL statements.
   at System.Data.SqlClient.TdsParser.TdsExecuteRPC(SqlCommand cmd, _SqlRPC[] rpcArray, Int32 timeout, Boolean inSchema, SqlNotificationRequest notificationRequest, TdsParserStateObject stateObj, Boolean isCommandProc, Boolean sync, TaskCompletionSource`1 completion, Int32 startRpc, Int32 startParam)
   at System.Data.SqlClient.SqlCommand.RunExecuteReaderTds(CommandBehavior cmdBehavior, RunBehavior runBehavior, Boolean returnStream, Boolean async, Int32 timeout, Task& task, Boolean asyncWrite, SqlDataReader ds, Boolean describeParameterEncryptionRequest)
   at System.Data.SqlClient.SqlCommand.RunExecuteReader(CommandBehavior cmdBehavior, RunBehavior runBehavior, Boolean returnStream, String method, TaskCompletionSource`1 completion, Int32 timeout, Task& task, Boolean asyncWrite)
   at System.Data.SqlClient.SqlCommand.InternalExecuteNonQuery(TaskCompletionSource`1 completion, String methodName, Boolean sendToPipe, Int32 timeout, Boolean asyncWrite)
   at System.Data.SqlClient.SqlCommand.ExecuteNonQuery()
   at Dapper.SqlMapper.ExecuteCommand(IDbConnection cnn, CommandDefinition& command, Action`2 paramReader)
   at Dapper.SqlMapper.ExecuteImpl(IDbConnection cnn, CommandDefinition& command)
   at Dapper.SqlMapper.Execute(IDbConnection cnn, String sql, Object param, IDbTransaction transaction, Nullable`1 commandTimeout, Nullable`1 commandType)
   at Hangfire.SqlServer.SqlServerDistributedLock.Release(IDbConnection connection, String resource)
   at Hangfire.SqlServer.SqlServerDistributedLock.Dispose()
   at ImageVault.Core.Jobs.StoreMediaJob.StoreContentForAnalysedMedia(DbMediaContentReference reference, UploadContentId uploadContentId)
2016-03-01T07:40:11  PID[1029] Information       ImageVault.Core.Jobs.StoreMediaJobs	Error processing reference MediaItemId:487, formatId:1	System.InvalidOperationException : The transaction associated with the current connection has completed but has not been disposed.  The transaction must be disposed before the connection can be used to execute SQL statements.
   at System.Data.SqlClient.TdsParser.TdsExecuteRPC(SqlCommand cmd, _SqlRPC[] rpcArray, Int32 timeout, Boolean inSchema, SqlNotificationRequest notificationRequest, TdsParserStateObject stateObj, Boolean isCommandProc, Boolean sync, TaskCompletionSource`1 completion, Int32 startRpc, Int32 startParam)
   at System.Data.SqlClient.SqlCommand.RunExecuteReaderTds(CommandBehavior cmdBehavior, RunBehavior runBehavior, Boolean returnStream, Boolean async, Int32 timeout, Task& task, Boolean asyncWrite, SqlDataReader ds, Boolean describeParameterEncryptionRequest)
   at System.Data.SqlClient.SqlCommand.RunExecuteReader(CommandBehavior cmdBehavior, RunBehavior runBehavior, Boolean returnStream, String method, TaskCompletionSource`1 completion, Int32 timeout, Task& task, Boolean asyncWrite)
   at System.Data.SqlClient.SqlCommand.InternalExecuteNonQuery(TaskCompletionSource`1 completion, String methodName, Boolean sendToPipe, Int32 timeout, Boolean asyncWrite)
   at System.Data.SqlClient.SqlCommand.ExecuteNonQuery()
   at Dapper.SqlMapper.ExecuteCommand(IDbConnection cnn, CommandDefinition& command, Action`2 paramReader)
   at Dapper.SqlMapper.ExecuteImpl(IDbConnection cnn, CommandDefinition& command)
   at Dapper.SqlMapper.Execute(IDbConnection cnn, String sql, Object param, IDbTransaction transaction, Nullable`1 commandTimeout, Nullable`1 commandType)
   at Hangfire.SqlServer.SqlServerDistributedLock.Release(IDbConnection connection, String resource)
   at Hangfire.SqlServer.SqlServerDistributedLock.Dispose()
   at ImageVault.Core.Jobs.StoreMediaJob.StoreContentForAnalysedMedia(DbMediaContentReference reference, UploadContentId uploadContentId)
   at ImageVault.Core.Jobs.StoreMediaJob.ProcessPendingMedia(ICancellationToken cancellationToken)";
            IterateLog(log, logData);
            Assert.AreEqual(2, logData.LogEntryDataSource.Count, "Mismatch in logData.LogEntryDataSource.Count");
            AssertEntry(logData.LogEntryDataSource[0], new DateTime(2016, 3, 1, 7, 40, 10), "PID[1028]", "Error", null,
                "ImageVault.Core.Jobs.StoreMediaJob",
                @"Unable to StoreContentForAnalyzedMedia for ref i4hl421v9kecnqtnrn95	System.InvalidOperationException : The transaction associated with the current connection has completed but has not been disposed.  The transaction must be disposed before the connection can be used to execute SQL statements.
   at System.Data.SqlClient.TdsParser.TdsExecuteRPC(SqlCommand cmd, _SqlRPC[] rpcArray, Int32 timeout, Boolean inSchema, SqlNotificationRequest notificationRequest, TdsParserStateObject stateObj, Boolean isCommandProc, Boolean sync, TaskCompletionSource`1 completion, Int32 startRpc, Int32 startParam)
   at System.Data.SqlClient.SqlCommand.RunExecuteReaderTds(CommandBehavior cmdBehavior, RunBehavior runBehavior, Boolean returnStream, Boolean async, Int32 timeout, Task& task, Boolean asyncWrite, SqlDataReader ds, Boolean describeParameterEncryptionRequest)
   at System.Data.SqlClient.SqlCommand.RunExecuteReader(CommandBehavior cmdBehavior, RunBehavior runBehavior, Boolean returnStream, String method, TaskCompletionSource`1 completion, Int32 timeout, Task& task, Boolean asyncWrite)
   at System.Data.SqlClient.SqlCommand.InternalExecuteNonQuery(TaskCompletionSource`1 completion, String methodName, Boolean sendToPipe, Int32 timeout, Boolean asyncWrite)
   at System.Data.SqlClient.SqlCommand.ExecuteNonQuery()
   at Dapper.SqlMapper.ExecuteCommand(IDbConnection cnn, CommandDefinition& command, Action`2 paramReader)
   at Dapper.SqlMapper.ExecuteImpl(IDbConnection cnn, CommandDefinition& command)
   at Dapper.SqlMapper.Execute(IDbConnection cnn, String sql, Object param, IDbTransaction transaction, Nullable`1 commandTimeout, Nullable`1 commandType)
   at Hangfire.SqlServer.SqlServerDistributedLock.Release(IDbConnection connection, String resource)
   at Hangfire.SqlServer.SqlServerDistributedLock.Dispose()
   at ImageVault.Core.Jobs.StoreMediaJob.StoreContentForAnalysedMedia(DbMediaContentReference reference, UploadContentId uploadContentId)");
            AssertEntry(logData.LogEntryDataSource[1], new DateTime(2016, 3, 1, 7, 40, 11), "PID[1029]", "Information", null,
                "ImageVault.Core.Jobs.StoreMediaJobs",
                @"Error processing reference MediaItemId:487, formatId:1	System.InvalidOperationException : The transaction associated with the current connection has completed but has not been disposed.  The transaction must be disposed before the connection can be used to execute SQL statements.
   at System.Data.SqlClient.TdsParser.TdsExecuteRPC(SqlCommand cmd, _SqlRPC[] rpcArray, Int32 timeout, Boolean inSchema, SqlNotificationRequest notificationRequest, TdsParserStateObject stateObj, Boolean isCommandProc, Boolean sync, TaskCompletionSource`1 completion, Int32 startRpc, Int32 startParam)
   at System.Data.SqlClient.SqlCommand.RunExecuteReaderTds(CommandBehavior cmdBehavior, RunBehavior runBehavior, Boolean returnStream, Boolean async, Int32 timeout, Task& task, Boolean asyncWrite, SqlDataReader ds, Boolean describeParameterEncryptionRequest)
   at System.Data.SqlClient.SqlCommand.RunExecuteReader(CommandBehavior cmdBehavior, RunBehavior runBehavior, Boolean returnStream, String method, TaskCompletionSource`1 completion, Int32 timeout, Task& task, Boolean asyncWrite)
   at System.Data.SqlClient.SqlCommand.InternalExecuteNonQuery(TaskCompletionSource`1 completion, String methodName, Boolean sendToPipe, Int32 timeout, Boolean asyncWrite)
   at System.Data.SqlClient.SqlCommand.ExecuteNonQuery()
   at Dapper.SqlMapper.ExecuteCommand(IDbConnection cnn, CommandDefinition& command, Action`2 paramReader)
   at Dapper.SqlMapper.ExecuteImpl(IDbConnection cnn, CommandDefinition& command)
   at Dapper.SqlMapper.Execute(IDbConnection cnn, String sql, Object param, IDbTransaction transaction, Nullable`1 commandTimeout, Nullable`1 commandType)
   at Hangfire.SqlServer.SqlServerDistributedLock.Release(IDbConnection connection, String resource)
   at Hangfire.SqlServer.SqlServerDistributedLock.Dispose()
   at ImageVault.Core.Jobs.StoreMediaJob.StoreContentForAnalysedMedia(DbMediaContentReference reference, UploadContentId uploadContentId)
   at ImageVault.Core.Jobs.StoreMediaJob.ProcessPendingMedia(ICancellationToken cancellationToken)");

        }

        [Test]
        public void TestLog4NetPattern()
        {
            var logData = new LogData(null);
            const string log = @"2016-02-12 11:33:55,930 INFO [26][ImageVault.Core.DataAccess.Repositories.CachedRepositoryRegistry] ?.? - Cannot find a cached repository for the ImageVault.Core.DataAccess.Repositories.IScheduledJobRepository. Using ImageVault.Core.DataAccess.Repositories.ScheduledJobRepository instead.
2016-02-12 11:33:55,948 ERROR [27][ImageVault.UI.Mvc.MvcApplication] ?.? - StructureMap configuration failed
System.Configuration.ConfigurationErrorsException: No upload folder has been configured
   at ImageVault.Core.Windows.UploadWindows..ctor()
   at ImageVault.Core.Windows.WindowsServerPlatformProvider.<ConfigureDependencyContainer>b__0(ConfigurationExpression ce)
   at StructureMap.PipelineGraph.Configure(Action`1 configure) in c:\BuildAgent\work\a395dbde6b793293\src\StructureMap\PipelineGraph.cs:line 153
   at StructureMap.Container.Configure(Action`1 configure) in c:\BuildAgent\work\a395dbde6b793293\src\StructureMap\Container.cs:line 388
   at ImageVault.UI.Mvc.MvcApplication.ConfigureStructureMap()";
            IterateLog(log,logData);
            Assert.AreEqual(2, logData.LogEntryDataSource.Count, "Mismatch in logData.LogEntryDataSource.Count");
            AssertEntry(logData.LogEntryDataSource[0], new DateTime(2016, 2, 12, 11, 33, 55, 930), null, "INFO", "26",
                "[ImageVault.Core.DataAccess.Repositories.CachedRepositoryRegistry]",
                @"- Cannot find a cached repository for the ImageVault.Core.DataAccess.Repositories.IScheduledJobRepository. Using ImageVault.Core.DataAccess.Repositories.ScheduledJobRepository instead.");
            AssertEntry(logData.LogEntryDataSource[1], new DateTime(2016, 2, 12, 11, 33, 55, 948), null, "ERROR", "27",
                            "[ImageVault.UI.Mvc.MvcApplication]",
                            @"- StructureMap configuration failed
System.Configuration.ConfigurationErrorsException: No upload folder has been configured
   at ImageVault.Core.Windows.UploadWindows..ctor()
   at ImageVault.Core.Windows.WindowsServerPlatformProvider.<ConfigureDependencyContainer>b__0(ConfigurationExpression ce)
   at StructureMap.PipelineGraph.Configure(Action`1 configure) in c:\BuildAgent\work\a395dbde6b793293\src\StructureMap\PipelineGraph.cs:line 153
   at StructureMap.Container.Configure(Action`1 configure) in c:\BuildAgent\work\a395dbde6b793293\src\StructureMap\Container.cs:line 388
   at ImageVault.UI.Mvc.MvcApplication.ConfigureStructureMap()");
        }

        private static void IterateLog(string log, LogData data) {
            var parser = new Parser(data);
            using (var r = new StringReader(log)) {
                string line;
                while ((line = r.ReadLine()) != null) {
                    parser.ParseLine(line);
                }
            }
        }


        private void AssertEntry(LogEntry logEntry, DateTime dateTime, string pid, string level, string thread, string logger, string message) {
            Assert.IsNotNull(logEntry);
            Assert.AreEqual(dateTime, logEntry.Time, "Mismatch in logEntry.Time");
            Assert.AreEqual(pid, logEntry.Process, "Mismatch in logEntry.Process");
            Assert.AreEqual(level, logEntry.Level, "Mismatch in logEntry.Level");
            Assert.AreEqual(thread, logEntry.Thread, "Mismatch in logEntry.Thread");
            Assert.AreEqual(logger, logEntry.Logger, "Mismatch in logEntry.Logger");
            Assert.AreEqual(message, logEntry.Message.Message, "Mismatch in logEntry.Message.Message");

        }
    }
}
