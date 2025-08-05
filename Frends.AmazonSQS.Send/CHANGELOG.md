# Changelog

## [2.0.0] - 2025-07-23

### Added
- ErrorMessageOnFailure property to Options class for custom error messages
- Error class in Definitions folder with Message and AdditionalInfo properties
- ErrorHandler class in Helpers folder for centralized error handling

### Changed
- Renamed AnonymousAWSCredentials to AnonymousAwsCredentials
- Renamed BasicAWSCredentials to BasicAwsCredentials  
- Renamed EnvironmentAWSCredentials to EnvironmentAwsCredentials
- Renamed SessionAWSCredentials to SessionAwsCredentials
- Renamed ThrowExceptionOnError to ThrowErrorOnFailure
- Renamed HttpStatus property to StatusCode in Result class
- Updated error handling to use centralized ErrorHandler.Handle method
- ThrowErrorOnFailure now defaults to true for consistent behavior
- Restructured task parameters into Input, Connection, and Options classes

### Removed
- [Breaking] ErrorMessage property from Result class (replaced with Error object)

## [1.0.0] - 2024-01-08
### Added
- Initial implementation