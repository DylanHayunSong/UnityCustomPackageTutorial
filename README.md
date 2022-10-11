# Unity Custom package 제작 방법

---

작성자 : SongD

최초작성 : 2022 / 09

내용

유니티 커스텀 패키지 제작 과정을 기술한 문서입니다.

---

참고

[커스텀 패키지 생성 - Unity 매뉴얼](https://docs.unity3d.com/kr/2021.3/Manual/CustomPackages.html)

---

## 패키지 준비

만약 패키징 할 파일들이 이미 있다면 바로 패키지화 하면 됩니다.

### 파일 생성

Custom Package를 만들기 위한 아주 간단한 내용을 준비해 보도록 하겠습니다.

아래와 같은 구조로 파일을 준비합니다.

```
CustomPackage  
│
├── Editor
│      ├── CustomPackageEditor.cs
│      └── CustomPackageEditor.cs.meta
│
├── Runtime
│      ├── CustomPackageScript.cs
│      └── CustomPackageScript.cs.meta
│
└── Samples
        ├── CustomPackageSampleScene.unity
        └── CustomPackageSampleScene.unity.meta
```

![image](https://user-images.githubusercontent.com/71427168/194996117-af87c510-ab7e-4e39-9f6b-aa4f81cdafb4.png)

| Editor                                                                                                          | Runtime                                                                                                         | Samples                                                                                                         |
|:---------------------------------------------------------------------------------------------------------------:|:---------------------------------------------------------------------------------------------------------------:|:---------------------------------------------------------------------------------------------------------------:|
| ![image](https://user-images.githubusercontent.com/71427168/194996370-9d6dd93e-279b-4d1d-825d-9ac115fd8aef.png) | ![image](https://user-images.githubusercontent.com/71427168/194996533-28441d69-e71f-4769-87e3-e348f878cd04.png) | ![image](https://user-images.githubusercontent.com/71427168/194996713-81c5c5d0-b49a-4ed6-acff-c9e88ccb5673.png) |

### 

### Script 작성

Runtime 스크립트와 Editor 스크립트를 확인하기 위해 간단한 기능을 작성해 주도록 하겠습니다.

먼저 [Runtime] → [**CustomPackageScript**] 스크립트를 아래와 같이 작성합니다.

```csharp
using UnityEngine;

public class CustomPackageScript : MonoBehaviour
{
    public static void DebugText(string msg)
    {
        Debug.Log(string.Format("{0} \n Package Successfully Loaded!", msg));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DebugText("This message called Runtime Script!");
        }
    }
}
```

작성한 스크립트를 [Samples] → [**CustomPackageScene**] 씬에서 빈 오브젝트를 만들어 컴포넌트화 해줍니다. 

씬에 빈 오브젝트를 생성해줍니다.

![image](https://user-images.githubusercontent.com/71427168/194997149-073b1aad-328f-4cfc-9236-365ea82ea678.png)

빈 오브젝트에 컴포넌트로 추가합니다.

<img src="https://user-images.githubusercontent.com/71427168/194997190-63c36e4f-a5dd-43fb-85e9-587145b53039.png" title="" alt="image" width="467">

Play Mode에서 Space 키를 눌러 스크립트가 잘 작동하는지 확인합니다.

![image](https://user-images.githubusercontent.com/71427168/194997209-61067d88-27b1-427b-84fb-face210a720b.png)

다음으로 [Editor] → [**CustomPackageEditorScript**] 스크립트를 아래와 같이 작성합니다.

```csharp
using UnityEditor;

public class CustomPackageEditor : Editor
{
    [MenuItem("CustomPackageSample/Test")]
    public static void Test() 
    {
        CustomPackageScript.DebugText("This message called Editor Script!");
    }
}
```

작성 후 에디터로 돌아와 컴파일이 완료된다면,
에디터 상단에 [CustomPackageSample] → [Test] 메뉴를 눌러 확인할 수 있습니다.

![image](https://user-images.githubusercontent.com/71427168/194997346-6cd6afe9-b558-48b8-9483-f6af6220c021.png)

![image](https://user-images.githubusercontent.com/71427168/194997372-66f0014d-8aad-4c8f-b993-e7f200f50f0f.png)

## 패키지화

### Asemdef 파일 생성

패키지 내 스크립트는 어셈블리 정의 파일과 연결해야 참조가 가능합니다.

Runtime 폴더와 Editor 폴더 내부에 각각 asemdef 파일을 생성해줍니다.

![image](https://user-images.githubusercontent.com/71427168/194997399-b152fa71-657f-40bf-b561-8ea85af70788.png)

| Runtime                                                                                                         | Editor                                                                                                          |
|:---------------------------------------------------------------------------------------------------------------:|:---------------------------------------------------------------------------------------------------------------:|
| ![image](https://user-images.githubusercontent.com/71427168/194997541-94264b0b-3990-4450-a71a-fd54304abe24.png) | ![image](https://user-images.githubusercontent.com/71427168/194997569-8a2d464e-94e9-4c82-bc21-0853088a6ad4.png) |

<aside>
💡 asemdef 파일의 이름은 어떻게 설정해도 상관 없지만
Editor의 asemdef 파일 이름은 Runtime의 asemdef 파일의 뒤에 .Editor을 붙이는 것을 추천합니다.

</aside>

Runtime 폴더의 asemdef 파일은 초기 상태 그대로 두고, Editor 폴더의 asemdef 파일은 아래와 같이 구성해줍니다.

![image](https://user-images.githubusercontent.com/71427168/194997698-278727a0-568c-4d6f-b49a-5e9a395e99cc.png)

### **Packages.json** 작성

패키지에 사용 할 파일들을 폴더째로 **[Project] → [Packages]** 폴더 내부로 이동해줍니다.

![image](https://user-images.githubusercontent.com/71427168/194997909-c500581c-abf0-4069-a487-82c40777cdee.png)

폴더 내부에 [**package.json**] 파일을 생성해줍니다.

![image](https://user-images.githubusercontent.com/71427168/194997912-025abc71-2858-4d39-b29e-461e9242ae6d.png)

![image](https://user-images.githubusercontent.com/71427168/194997913-1718755a-3c10-4e31-aa1c-2610eba4e59e.png)

**package.json** 내용을 아래와 같이 작성해줍니다.

```
{
    "name": "com.{작성자이름}.{패키지 이름}", (이 필드의 문자열은 소문자,숫자,- 만 가능합니다.)
    "displayName": "표기될 패키지 이름",
    "version": "0.1.0", [추천:(메인, 서브, 기능)]
    "description": "설명을 기입해주세요",
    "samples": [
        {
            "displayName": "샘플 폴더 이름",
            "description": "샘플 설명",
            "path": "샘플 폴더 위치" [추천 : samples~] (여기서 폴더 뒤에 ~은 에디터상에서 보이지 않게 만든다.)
        }
    ],
    "author": {
        "name": "이름",
        "email": "이메일",
        "url": "홈페이지/깃허브 등 링크"
    }
}
```

- 예시
  
  ```json
  {
      "name": "com.kimsongd.custom-package-test",
      "displayName": "Custom Package Test",
      "version": "0.1.0",
      "description": "Custom package for test and practice",
      "samples": [
          {
              "displayName": "CustomPackageTestSample",
              "description": "Sample of Custom Package Test",
              "path": "Samples~"
          }
      ],
      "author": {
          "name": "SongD",
          "email": "dylanhayunsong@gmail.com",
          "url": "https://github.com/DylanHayunSong"
      }
  }
  ```

**Samples**폴더와 **Samples.meta** 파일의 이름을 package.json에 작성한 것과 같이 변경해줍니다.

![image](https://user-images.githubusercontent.com/71427168/194997914-a764a4f2-158e-42b9-b1e0-698a246ed377.png)

### 패키지 확인

에디터로 돌아와 **[Window] → [Package Manager]** 에서 지금까지 만든 패키지를 확인 할 수 있습니다.

![image](https://user-images.githubusercontent.com/71427168/194997917-155843ba-5a1e-4f7b-8f45-903c1b695167.png)