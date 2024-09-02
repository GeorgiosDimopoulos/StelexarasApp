using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
namespace StelexarasApp.Services.Helpers
{
    public static class ExceptionHandler
    {
        public static void HandleException(Exception ex)
        {
            // Handle general exception
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }

        public static void HandleDbUpdateException(DbUpdateException ex)
        {
            // Handle DbUpdateException specifically
            Console.WriteLine("Database update error: " + ex.Message);
            // Additional logic for handling DbUpdateException
        }

        public static void HandleDbUpdateConcurrencyException(DbUpdateConcurrencyException ex)
        {
            // Handle DbUpdateConcurrencyException specifically
            Console.WriteLine("Database concurrency error: " + ex.Message);
            // Additional logic for handling DbUpdateConcurrencyException
        }

        public static void LogException(Exception ex)
        {
            // Implement logging logic here
            Console.WriteLine($"Logged Exception: {ex.GetType().Name} - {ex.Message}");
        }
    }

}
