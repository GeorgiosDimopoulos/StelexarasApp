﻿namespace StelexarasApp.Library.Models.Logs
{
    public enum CrudType
    {
        Create,
        Read,
        Update,
        Delete,
        Unknown
    }

    public enum ErrorType
    {
        DbError,
        UiWarning,
        ValidationError,
        ServiceError,
        UnknownError
    }
}
