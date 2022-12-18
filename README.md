# ５びょうごのきみと
3年次に授業でチーム制作を行ったゲーム。  
  
 ![5びょうごのきみと_イメージイラスト](https://user-images.githubusercontent.com/71632844/208225395-20ebdf76-533f-40ab-a5b2-22e2db24aa41.png)
  
〇制作期間  
2022/04/19～2023/01/05  
（現在も制作中のため、順次更新予定です。）

〇１週間の平均作業時間  
７～１０時間（授業時間〈３時間〉含む）

〇メンバー  
・プランナー　：２名  
・デザイナー　：３名  
・プログラマー：３名　（自分 + 2名）  
  
計８名  
  
(下記に担当箇所の記載あり)  


# ファイルの説明
- Afterimageフォルダ  
　⇒本ゲームのプロジェクトフォルダ  
- 実行ファイル（データ便でURL貼る）  
- README.md  
　⇒ゲーム概要などの説明  


# どんなゲームか
　死別した弟を救うため、兄（プレイヤー）は5秒後に同じ動作をする弟の幽体と協力してステージを攻略していく「アクションパズルゲーム」です。  
　幽体の弟には物理判定があり、触れることが可能です。そのため、穴の開いた地形に弟を誘導し先へ進むなどといった攻略ギミックなどが出てきます。  
　ゲームの世界観は「絵本の中」となっており、ゲーム中は飛び出す絵本のような形で表現されております。  
 
  
# 担当箇所・工夫した点
- **<ins>アウトゲーム全般の処理内容</ins>**  

  
- **<ins>アウトゲーム中のUI演出処理</ins>**  

  
- **<ins>一部インゲーム中ンUI処理</ins>**  

  
- **<ins>一部インゲーム中のカメラ演出処理</ins>**  

  
- **<ins>ゲームのクリアデータをローカルストレージ管理する処理</ins>**  

  
- **<ins>ゲームの状態管理</ins>**  

  
- **<ins>トランジション演出用シェーダーの作成</ins>**  

  

# ソースファイル
| ソースファイル | 軽い説明 | 記述・担当部分 |
| --- | --- | --- |
| ▼▼[Scriptsフォルダ](https://github.com/daichi0907/Five-seconds-with-you/tree/main/Afterimage/Assets/Scripts) |  |  |
| ▼[Controllerフォルダ](https://github.com/daichi0907/Five-seconds-with-you/tree/main/Afterimage/Assets/Scripts/Controller) |  |  |
| [DirectingScript.cs](https://github.com/daichi0907/Five-seconds-with-you/blob/main/Afterimage/Assets/Scripts/Controller/DirectingScript.cs) | インゲーム中のスタート演出並びにゴール演出処理を行う。 | 全記述 |
| [GameData.cs](https://github.com/daichi0907/Five-seconds-with-you/blob/main/Afterimage/Assets/Scripts/Controller/GameData.cs) | ゲームデータの管理並びにセーブデータのセーブ・ロード処理を行う。 | 全記述 |
| [Game_State.cs](https://github.com/daichi0907/Five-seconds-with-you/blob/main/Afterimage/Assets/Scripts/Controller/Game_State.cs) | ゲームの状態管理並びに状態の取得・変更処理を行う。 | 全記述 |
| [GetPlayerTriggerScript.cs](https://github.com/daichi0907/Five-seconds-with-you/blob/main/Afterimage/Assets/Scripts/Controller/GetPlayerTriggerScript.cs) | インゲーム中の演出に必要なTrigger情報を取得しDirectingScriptに情報を返す処理を行う。 | 全記述 |
| [StartStoryScript.cs](https://github.com/daichi0907/Five-seconds-with-you/blob/main/Afterimage/Assets/Scripts/Controller/StartStoryScript.cs) | ゲーム開始時のストーリー演出に関する処理の仮組（ファイナル版では未使用の予定）。 | 全記述 |
| [TitleScript.cs](https://github.com/daichi0907/Five-seconds-with-you/blob/main/Afterimage/Assets/Scripts/Controller/TitleScript.cs) | タイトルシーン時のボタン処理並びに演出、パネル切り替え処理全般を行う。 | 全記述 |
| ▼[Storageフォルダ](https://github.com/daichi0907/Five-seconds-with-you/tree/main/Afterimage/Assets/Scripts/Storage) |  |  |
| [StorageManager.cs](https://github.com/daichi0907/Five-seconds-with-you/blob/main/Afterimage/Assets/Scripts/Storage/StorageManager.cs) | ローカルストレージへのデータ保存処理、読込処理などを行っている。 | 全記述 |
| [UserSettings.cs](https://github.com/daichi0907/Five-seconds-with-you/blob/main/Afterimage/Assets/Scripts/Storage/UserSettings.cs) | 更新データの管理、保存先の指定、インターフェースによる保存方法データの管理 | 全記述 |
| ▼[UIフォルダ](https://github.com/daichi0907/Five-seconds-with-you/tree/main/Afterimage/Assets/Scripts/UI) |  |  |
| [PauseUIAnimation.cs](https://github.com/daichi0907/Five-seconds-with-you/blob/main/Afterimage/Assets/Scripts/UI/PauseUIAnimation.cs) | インゲーム中のポーズ画面のアニメーション処理を行う | 全記述 |
| [StageUIScript.cs](https://github.com/daichi0907/Five-seconds-with-you/blob/main/Afterimage/Assets/Scripts/UI/StageUIScript.cs) | インゲーム中のUI全般の処理を行う | ポーズ画面に関する処理全般 |
| [TransitionScript.cs](https://github.com/daichi0907/Five-seconds-with-you/blob/main/Afterimage/Assets/Scripts/UI/TransitionScript.cs) | トランジション処理を行う処理のテンプレート。<br>（ゲーム内では使用されていない。）| 全記述 |
| [UI_Effect.cs](https://github.com/daichi0907/Five-seconds-with-you/blob/main/Afterimage/Assets/Scripts/UI/UI_%20Effect.cs) | タイトル画面並びにインゲーム中のUI演出処理に関する処理を行っている。 | 全記述 |
| ▼▼[Resourcesフォルダ](https://github.com/daichi0907/Five-seconds-with-you/tree/main/Afterimage/Assets/Resources) |  |  |
| ▼[Transitionフォルダ](https://github.com/daichi0907/Five-seconds-with-you/tree/main/Afterimage/Assets/Resources/Transition) |  |  |
| [TransitionColorShader.shader](https://github.com/daichi0907/Five-seconds-with-you/blob/main/Afterimage/Assets/Resources/Transition/TransitionColorShader.shader) | トランジションイメージ画像にこのシェーダーを付けることで、指定した色のトランジション処理を行う。<br>（ゲーム内では使用されていない。） | 全記述 |
| [TransitionTextureShader.shader](https://github.com/daichi0907/Five-seconds-with-you/blob/main/Afterimage/Assets/Resources/Transition/TransitionTextureShader.shader) | トランジションイメージ画像にこのシェーダーを付け、指定のテクスチャーを設定することで、そのテクスチャー画像のトランジション処理を行う。） | 全記述 |

※上記に記載のないスクリプトファイルは私自身記述した部分のないスクリプトファイルとなっております。  


# 使用したデバイス・ツール
・Unity 2020.3.18f1   
・VisualStudio2019  
・Xbox コントローラー  
