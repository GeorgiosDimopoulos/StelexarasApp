# .NET 9.0 Upgrade Plan

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Validate that an .NET 9.0 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 9.0 upgrade.
3. Upgrade StelexarasApp.Library\StelexarasApp.Library.csproj

## Settings

This section contains settings and data used by execution steps.

### Excluded projects

| Project name | Description |
|:-------------------------------|:---------------------------:|

### Aggregate NuGet packages modifications across all projects

### Project upgrade details

#### StelexarasApp.Library\StelexarasApp.Library.csproj modifications

Project properties changes:
  - Target framework should be changed from `net8.0` to `net9.0`

NuGet packages changes:
  - No NuGet package changes required.

Feature upgrades:
  - No feature upgrades required.

Other changes:
  - No other changes required.
