# Changelog

## [2.0.0] - 2026-07-21

### Added
- `ThrowErrorOnFailure` and `ErrorMessageOnFailure` options to control error handling behavior. When `ThrowErrorOnFailure` is false, errors are returned in the result instead of throwing exceptions.
- `Success` property to the result, indicating whether the task completed successfully.
- `Error` property to the result, containing error details when `ThrowErrorOnFailure` is false.

### Changed
- Task now targets .NET 8.
- Task class is now static in line with Frends best practices.
- Parameters are now ordered as `Input`, `Connection`, `Options` for consistency with Frends standards.
- The `StatusCode` result property is now a `string` instead of an `HttpStatusCode` enum.
- The `Messages` result property now uses a custom `ReceivedMessage` type instead of the AWS SDK `Message` type, exposing the same fields.
- The `ResponseMetadata` result property now uses a custom `SqsResponseMetadata` type instead of the AWS SDK type, with `RequestId` and `Metadata` fields.

## [1.0.0] - 2026-01-27
### Added
- Add a more specific description and test multiple messages

## [1.0.0] - 2023-08-11
### Added
- Initial implementation 
