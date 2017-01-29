# UniRx Playground

Sample project using UniRx, Zenject, WebSocketSharp and AWS

Inspired by https://ornithoptergames.com/reactiverx-in-unity3d-part-1/ series.

## Project Structure

1. `SceneContext` - entrypoint to all bindings
2. `SettingsInstaller` - game settings
3. All `MonoBehaviours` are bound using `ZenjectBinding` attached to the same `GameObject`

# References

[AWS Sig v4 C# Example](shttp://docs.aws.amazon.com/AmazonS3/latest/API/sig-v4-examples-using-sdks.html#sig-v4-examples-using-sdk-dotnet)

## Dependencies
The following external assets are needed to run this project.

*Asset Store*
* [UniRx] (https://github.com/neuecc/UniRx)
* Standard Assets
* Zenject (https://github.com/modesttree/Zenject)

*GitHub*
* [WebSocketSharp](https://github.com/sta/websocket-sharp)
* [AWS Unity SDK](https://github.com/aws/aws-sdk-net/blob/master/Unity.README.md) - v3.3.45

*Bitbucket*
* CSharp70Support -optiona