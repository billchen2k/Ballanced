# Ballanced 工程文档

> 10185101210 陈俊潼
>
> 10185101250 高志翔
>
> East China Normal University

## 项目简介

**项目名称：Ballanced**

本项目为基于 Unity 的 3D 滚球游戏。玩家使用方向键操控画面中心的球体，以尽可能快地在不落下悬崖的情况下抵达终点为目标。具有用户友好的菜单界面，较好的操作感和画面，暂停和重新开始功能与沉浸式的音效和音乐。

项目地址：https://github.com/BillChen2000/Ballanced

### 创意想法

Ballance 是一款由德国游戏公司 CYPARADE 于 2002 年发行的一款经典的单机游戏。我们小组基于这一款单机游戏的操作逻辑，结合这学期所学的计算机图形学知识，尝试利用 3DS MAX 和 Unity 实现该游戏中的操作逻辑，尽可能接近原游戏的视觉效果和声音效果。最终我们通过三个星期的连续工作，实现了这一想法，可以并将其命名为 Ballanced。

### 操作指引

- [WSAD] / [方向键]：移动球体
- [SPACE]：抬起摄像头
- [R] / [K]：重新开始
- [P] / [ESC]：暂停游戏
- [Q]：退出游戏

**以下操作用于调试：**

- [J] 给球一个向上的力使其飞行
- [Ctrl] + [Shift] + [W]：一键胜利
- [Ctrl] + [Shift] + [D]：一键死亡

### 小组成员及分工

| 成员   | 学号        | 分工                                                         | 占比 |
| ------ | ----------- | ------------------------------------------------------------ | ---- |
| 陈俊潼 | 10185101210 | 负责编写游戏控制逻辑，音频触发逻辑，粒子特效设计，工程文档的编写 | 50%  |
| 高志翔 | 10185101250 | 负责模型的构建，贴图的收集与设计，地图组装与设计，PPT的制作  | 50%  |

### 运行效果

主菜单：

![image-20191229202700145](C:\Users\billc\Documents\Ballanced\Readme.assets\image-20191229202700145.png)

游戏界面：

![image-20191229202852355](C:\Users\billc\Documents\Ballanced\Readme.assets\image-20191229202852355.png)

![image-20191229195913932](C:\Users\billc\Documents\Ballanced\Readme.assets\image-20191229195913932.png)

![image-20191229201745849](C:\Users\billc\Documents\Ballanced\Readme.assets\image-20191229201745849.png)

暂停界面：

![image-20191229195818213](C:\Users\billc\Documents\Ballanced\Readme.assets\image-20191229195818213.png)

胜利结算界面：

![image-20191229201503109](C:\Users\billc\Documents\Ballanced\Readme.assets\image-20191229201503109.png)


## 技术实现

### 开发环境

- Autodesk 3ds MAX 2018 For Education
- Unity 2018.4.11c1 Personal
- Visual Studio 2019 Community Edition
- Windows 10 20H1

### 主要技术

- 使用 3DS MAX 构建地图模型，利用材质编辑器完成复杂纹理贴图；
- 使用 Unity 构建互动逻辑和游戏功能。具体的实现逻辑见后文；
- 使用粒子系统渲染得分球的特效，让画面更加生动；
- 使用了环境光散射，让场景色彩和背景色彩更加统一；
- 使用 Unity 的 Fog 技术，把远处的模型蒙上云雾，让画面更真实；
- 分数球使用了 BillBoard 技术，用 2D 的纹理来模拟 3D 的火焰效果，减少运算量；
- 使用 Fade 模式渲染 2D，控制其缓慢移动来模拟低空的云层效果；

## Unity Assets 结构

### /Prefab

预制的模型文件，包括地图中的轨道、箱子、球和游戏中的火焰等 prefab 文件。

### /Resources

资源文件夹，含有以下至目录：

#### Fonts

包括游戏中使用的字体，主要为`Agency FB`。字体已由`TextMesh Pro`提供的转换工具转换成了专用格式。

#### Material

材质文件，包括地面和球体的物理材质模型，存储了部分物体的摩擦系数。

#### Skybox

天空贴图文件。含有三个天空主题，游戏中使用的是 `SkyboxK`

#### Sound

游戏中使用的音效文件，包括碰撞、滚动等。这一部分的音频来取于`Ballance`中的音频。每隔一段随机时间播放的背景音乐位于`BGM`子目录下。

#### Texture

纹理目录，包含地图模型的纹理和球的纹理等。里面还有部分没有使用到的纹理贴图。

### /Scenes

场景目录。含有两个场景：

- Welcome：主菜单/欢迎界面，可以进入游戏和退出游戏
- Map：主要的游戏场景，包括地图、可操控的球体和主摄像机。死亡的提示和胜利的结算页面也在该场景中。该场景主要有`UICanvas`、`Map`、`Audio`、`Boneses`、`FogLayer`、`CheckPoints`、`WinningPoint`、`controller_ball`等`Hierarchy`，分别包含了界面指示、地图模型、音频管理、得分球、云雾图层、检查点、胜利店、控制球等。

### /Scripts

脚本文件目录。

### /TextMesh Pro

用于显示 UI 文字的插件 `TextMesh Pro`导入的文件。

##  Unity 脚本文档

### BallController.cs

控制球运动的脚本。具有以下公有属性：

| 属性         | 数据类型 | 备注               |
| ------------ | -------- | ------------------ |
| DEATH_HEIGHT | int      | 死亡高度           |
| FORCE        | float    | 方向键给小球加的力 |
| DEATH_COST   | int      | 一次死亡扣的分数   |
| BONES_VALUE  | int      | 得分球加的分数     |

在每一个`update()`中获得用户的输，并向刚体 Componet 施加力来让物体移动和滚动。

另外，通过调整 `Rigid Body`中的属性，可以控制球的操作手感和灵敏度。更大的质量和更小的摩擦系数会产生更大的难度。

该脚本在`onCollisionEnter`时控制`AudioFX`播放碰撞声音，检测被碰撞体的标签为`WoodObject`还是`MetalObject`，根据不同的物体发出不同的碰撞声音。同时在`onCollisionStay`中检测被碰撞物体的标签，根据标签控制`AudioBGM`发出不用的滚动声音。

得分、检查点和终点的判断都通过`onTriggerEnter()`函数控制，通过判断`Trigger`的标签来判断是否到达了检查点、得分球和终点。

### BGMHandler.cs

控制背景音乐播放的脚本。具有以下公有属性：

| 属性    | 数据类型 | 备注             |
| ------- | -------- | ---------------- |
| ifNoBGM | bool     | 是否关闭背景音乐 |

挂载在 `AudioBGM` 上，会每隔一段随机事件随机选择一段 clip 来播放。

### CameraFollow.cs

公有属性：

| 属性         | 数据类型 | 备注             |
| ------------ | -------- | ---------------- |
| SMOOTH       | int      | 平滑指数         |
| RAISE_SMOOTH | int      | 抬起的平滑指数   |
| RAISE_HEIGHT | int      | 抬起高度         |
| RAISE_ANGLE  | int      | 抬起时的俯视角度 |

该脚本挂载在主摄像机上，用于追随摄像头。初始化时会载入相机坐标和球体坐标的偏移，在每一个`update()`中将自己的位置修改为球的位置加上偏移的坐标，同时使用`Vector3.Lerp()`来实现平滑的相机移动。

在按下空格键时，会调整相机的旋转角度并抬起相机来实现俯视效果，在检测到空格键离开时恢复之前的角度。这一部分使用`Quaternion`的变换实现，转换成欧拉角再调整 y 轴上的旋转值，最后使用`Quaternion.Slerp()`实现旋转的平滑移动。

### FogFollow.cs

| 属性         | 数据类型 | 备注               |
| ------------ | -------- | ------------------ |
| MOVE_SPEED   | float    | 来回移动的速度     |
| MOVE_SMOOTH  | float    | 云雾移动的平滑指数 |

该脚本用于控制云层的移动和追踪。云层会默认以每次更新 0.05 的速度来回移动，并且为了防止移动到边缘，会将基准位置向量`base_position`的位置追踪到球体的位置，使用`Lerp()`函数平滑移动。

### Global.cs

全局的控制脚本，存储了一些全局数值和死亡、胜利的处理逻辑。具有以下公有属性：

| 属性       | 数据类型 | 备注             |
| ---------- | -------- | ---------------- |
| INIT_SCORE | int      | 初始分数         |
| SCORE      | int      | 当前分数         |
| isPaused   | bool     | 是否暂停         |
| isDying    | bool     | 是否正在死亡     |
| isWinniing | bool     | 是否进入胜利界面 |

脚本中的函数`GameWin()`、`GameOver()`等函数处理了胜利、失败和暂停的逻辑，播放音效并显示相应 UI。

对于`GameWin()`，为了实现分数的跳动展示使用协程，将显示分数的目标设置为最终的结算分数，从 0 开始每隔 0.01s 在屏幕上增加 3 并播放跳动音效。显示完成后再启用 UI 按钮。

同时还在这个脚本中编写了函数 `playClipByName(AudioSource audiosource, string name)` 来快速播放音频。

### MenuHandler.cs

用于处理菜单按钮逻辑的脚本，挂载在 `Global` Object 上，处理暂停、重新开始、退出的按钮逻辑。

函数`GamePause()`用于暂停游戏，使用的方法是将全局的`timeScale`设置为 0。所以为了实现先播放菜单点击音效再停止游戏，使用协程，在调用`WaitForSeconds(0.05f)`后再暂停。

### ScoreCounter.cs

处理分数逻辑。

每隔 0.5s 将分数减一，每个分数球会给分数加上 200 分，每死亡一次会给分数减少 300 分。

其他脚本可以调用本脚本中的`AddScore(int score)`和`SubScore(int score)`来增加和减少分数。这两个函数都使用了协程来实现分数的动态加减并会在改变分数的时候修改文字颜色使其更加醒目。

### WelcomeHandler.cs

挂载在主菜单`UICanvas`上的脚本，用于接收开始游戏和退出游戏的点击事件，跳转场景以开始游戏。



---

2019.12.29, Ver 0.1