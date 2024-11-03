using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StelexarasApp.DataAccess.Models.Logs;

namespace StelexarasApp.DataAccess.Helpers;

public static class ExceptionHelper
{
    public static void HandleDatabaseExceptionAsync(Exception ex, string methodName, ILogger logger)
    {
        if (ex is DbUpdateException dbEx && dbEx.InnerException is SqlException sqlEx)
        {
            switch (sqlEx.Number)
            {
                case 2601: // Unique constraint error
                    logger.LogError(dbEx, $"Unique constraint error: {dbEx.Message} , {dbEx.InnerException} in {methodName}");
                    LogFileWriter.WriteErrorToLog($"Unique constraint error: {dbEx.Message}, {dbEx.InnerException} in {methodName}", ErrorType.DbError);
                    break;
                default:
                    logger.LogError(dbEx, "Database update exception: {Message}", dbEx.Message);
                    LogFileWriter.WriteErrorToLog($"Database update exception: {dbEx.Message}, {dbEx.InnerException} in {methodName}", ErrorType.DbError);
                    break;
            }
        }
        else if (ex is InvalidCastException inEx)
        {
            logger.LogError(inEx, "An unexpected InvalidCastException occurred: {Message}", inEx.Message);
            LogFileWriter.WriteErrorToLog($"An unexpected InvalidCastException occurred: {inEx.Message}, {inEx.InnerException} in {methodName}", ErrorType.DbError);
        }
        else if (ex is ArgumentException arEx)
        {
            logger.LogError(arEx, "An unexpected ArgumentException occurred: {Message}", arEx.Message);
            LogFileWriter.WriteErrorToLog($"An unexpected ArgumentException occurred: {arEx.Message}, {arEx.InnerException} in {methodName}", ErrorType.DbError);
        }        
        else
        {
            logger.LogError(ex, "An unexpected error occurred: {Message}", ex.Message);
            LogFileWriter.WriteErrorToLog($"An unexpected error occurred: {ex.Message}, {ex.InnerException} in {methodName}", ErrorType.DbError);
        }
    }
}
