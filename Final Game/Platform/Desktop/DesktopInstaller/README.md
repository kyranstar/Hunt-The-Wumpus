Using the installer project
===

 You can use this `DesktopInstaller` project to 
 make a `.msi` installer for the game.

Loading the project
---
To load the project, you will need the VS installer project extension from [here](https://visualstudiogallery.msdn.microsoft.com/9abe329c-9bba-44a1-be59-0fbf6151054d). Once you have the add-in installed, you should be able to load the solution without load warnings/errors.

Building the installer
---
To build the `MSI` installer, build the `DesktopInstaller` project from Visual Studio ("Release" configuration) and then navigate to `Final Game\Platform\Desktop\DesktopInstaller\Release` in Windows Explorer. You can use either the `.msi` file directly or the `.exe` wrapper to install the game.