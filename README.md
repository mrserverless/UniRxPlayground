# UniRx Playground

Sample project using UniRx with Zenject and C# 7.0

Inspired by https://ornithoptergames.com/reactiverx-in-unity3d-part-1/

## Project Structure

1. `SceneContext` - entrypoint to all bindings
2. `SettingsInstaller` - game settings
3. All `MonoBehaviours` are bound using `ZenjectBinding` attached to the same `GameObject`

## Dependencies
The following external assets are needed to run this project.

*Asset Store*
* UniRx
* Standard Assets
* Zenject

*Bitbucket*
* CSharp70Support