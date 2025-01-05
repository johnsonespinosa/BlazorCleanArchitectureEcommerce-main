namespace Application.Commons.Models;

public abstract class ResponseMessages
{
    // Messages for entity operations
    public const string EntityCreated = "Entity successfully created.";
    public const string EntityUpdated = "Entity successfully updated.";
    public const string EntityDeleted = "Entity deleted successfully.";
    public const string EntityNotFound = "Entity not found.";
    public const string EntityAlreadyExists = "Entity already exists.";
    
    // Messages for query operations
    public const string RecordsRetrievedSuccessfully = "Registros obtenidos exitosamente.";
    
    // Authentication messages
    public const string AuthenticationSuccessful = "Authentication successful.";

    // Messages for general operations
    public const string OperationSuccessful = "Operation performed successfully.";
    public const string OperationFailed = "The operation failed. Please try again.";

    // Validation messages
    public const string InvalidInput = "The data provided is invalid.";
    public const string UnauthorizedAccess = "Unauthorized access.";
}

