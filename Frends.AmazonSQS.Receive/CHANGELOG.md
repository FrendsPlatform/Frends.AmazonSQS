# Changelog

## [2.0.0] - 2026-07-21

### Breaking changes
- **Task parameter order changed**: The `Receive` method now takes parameters in the order `Input`, `Connection`, `Options` (previously `Connection`, `Input`, `Options`). Any existing flows using this task must be reconfigured.
- **`Result.StatusCode`** is now a numeric `int` (e.g. `200`) instead of an `HttpStatusCode` enum.
- **`Result.Messages`** is now `List<ReceivedMessage>` instead of the AWS SDK `List<Message>` type. The same message fields are available but the type is different.
- **`Result.ResponseMetadata`** is now `SqsResponseMetadata` instead of the AWS SDK `ResponseMetadata` type, with `RequestId` and `Metadata` fields.

### Added
- `ThrowErrorOnFailure` and `ErrorMessageOnFailure` options to control error handling behavior. When `ThrowErrorOnFailure` is false, errors are returned in the result instead of throwing exceptions.
- `Success` property to the result, indicating whether the task completed successfully.
- `Error` property to the result, containing error details when `ThrowErrorOnFailure` is false.

### Changed
- Task now targets .NET 8.
- Task class is now static in line with Frends best practices.

## [1.0.0] - 2026-01-27
### Added
- Add a more specific description and test multiple messages

## [1.0.0] - 2023-08-11
### Added
- Initial implementation 
