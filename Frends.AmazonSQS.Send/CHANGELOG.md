# Changelog

## [2.0.0] - 2025-07-23

### Added
- ErrorMessageOnFailure property to Options class for custom error messages
- Error class in Definitions folder with Message and AdditionalInfo properties
- ErrorHandler class in Helpers folder for centralized error handling
- Input class in Definitions folder for task input parameters
- Connection class in Definitions folder for connection-related parameters
- Options class in Definitions folder for optional task parameters

### Changed
- [Breaking] Restructured task parameters into Input, Connection, and Options classes
- [Breaking] Renamed AnonymousAWSCredentials to AnonymousAwsCredentials
- [Breaking] Renamed BasicAWSCredentials to BasicAwsCredentials  
- [Breaking] Renamed EnvironmentAWSCredentials to EnvironmentAwsCredentials
- [Breaking] Renamed SessionAWSCredentials to SessionAwsCredentials
- [Breaking] Renamed ThrowExceptionOnError to ThrowErrorOnFailure
- [Breaking] Renamed HttpStatus property to StatusCode in Result class
- Updated error handling to use centralized ErrorHandler.Handle method
- ThrowErrorOnFailure now defaults to true for consistent behavior

### Removed
- [Breaking] ErrorMessage property from Result class (replaced with Error object)

### Migration Notes
To upgrade to the new version:
1. Update parameter references to use the new Input, Connection, and Options class structure
2. Replace AnonymousAWSCredentials with AnonymousAwsCredentials
3. Replace BasicAWSCredentials with BasicAwsCredentials
4. Replace EnvironmentAWSCredentials with EnvironmentAwsCredentials
5. Replace SessionAWSCredentials with SessionAwsCredentials
6. Replace ThrowExceptionOnError with ThrowErrorOnFailure
7. Replace Result.HttpStatus with Result.StatusCode
8. Replace Result.ErrorMessage usage with Result.Error.Message


## [1.0.0] - 2024-01-08
### Added
- Initial implementation