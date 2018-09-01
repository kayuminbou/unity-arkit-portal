# unity-arkit-portal
## 概要
ARKitを用いて認識した平面に穴を開ける(覗き窓を作る)ようなイメージのアプリです。

## 作成手順
### 1. タッチした箇所に覗き窓(Portal)を投影する
1. Unityを立ち上げ、AssetStoreからARKitをインポート、"UnityARKitScene"を元にいじっていきます。
2. "HitCube"(タッチした箇所に投影される物体)を削除し、代わりに"Plane"をCreateします("Portal"とRenameしています)。
3. "Portal"に"UnityARHitTestExample"をAddComponentし、"Hit Transform"に"HitCubeParent"をアタッチしましょう。  

### 2.Portalに覗き窓の向こうの世界(ARWorld)を投影する  

1. ARWorldを映す用のCameraオブジェクトを新たに作成しましょう。("AR Camera"とRenameしています)。
1. AR Cameraの周りにARWorldを作っていきます。本当はここを凝りたいんですが、とりあえず適当です。
1. Projectから"Render Texture"をCreateし、AR Cameraの"Target Texture"にアタッチします。
1. "Render Texture"をPortalにアタッチします。  

これでAR Cameraが映している景色がPortalに投影されるようになります。  
解決すべき点はあと２点です。
- ARCameraの動きをMain Cameraに同期させる  
- Main CameraとAR Cameraで映すものを分ける  

### 3.ARCameraの動きをMain Cameraに同期させる  
1. "ARCameraManager"に"CameraScript"を作成し、以下のコードを書きます。  

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
	
	// Update is called once per frame
    [Header("Cameras")]
    public Camera mainCam;
    public Camera portalCam;

    void Update()
    {

        // Move portal camera position based on main camera distance from the portal.
        Vector3 cameraOffset = mainCam.transform.position - transform.position;
        portalCam.transform.position = transform.position + cameraOffset;

        // Make portal cam face the same direction as the main camera.
        portalCam.transform.rotation = Quaternion.LookRotation(mainCam.transform.forward, Vector3.up);
    }
}
```  
2. "mainCam"と"portalCam"プロパティにMainCameraとARCameraをそれぞれアタッチします。

### 4.Main CameraとAR Cameraで映すものを分ける  
Main Cameraは現実の空間とPortal、AR CameraはARWorldだけを映すようにしたいわけです。  
1. 新しいLayer("ARWorld")を作成します。
1. ARWorld内の全てのオブジェクトに"ARWorld"レイヤーを適用していきます。
1. Main Cameraの"Culling Mask"プロパティから"ARWorld"を除外します。
1. AR Cameraの"Culling Mask"プロパティから"Default"を除外します。  

これでできたはず！ここから改良していきます。
