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
  
- README.md  
　⇒ゲーム概要などの説明  
  
- プレイ中の注意事項「５びょうごのきみと」.pdf  
　⇒ゲームをプレイしていただく際の注意事項が記述されております。  
　　（ゲーム完成後削除予定です。）  
  
- 実行ファイル  
　⇒ギガファイル便URL：**<ins>[ダウンロード](https://67.gigafile.nu/0117-p2451e6efbd3a02657214a9140669b4e0)</ins>**（期間：2023年1月17日(火)まで, ダウンロードキー：無し）  
 　　gitですと容量の関係上アップすることができませんでしたので、  
 　　上記のURLからダウンロードをお願いいたします。  
　　（製作途中のため、完成品ができ次第ファイル内容並びにURLが変更する可能性があります。）  
  <!--削除キーd021-->


# どんなゲームか
　死別した弟を救うため、兄（プレイヤー）は5秒前のプレイヤーの動きと同じ動作をする弟の幽体と協力してステージを攻略していく「アクションパズルゲーム」です。  
　幽体の弟には物理判定があり、触れることが可能です。そのため、穴の開いた地形に弟を誘導し先へ進むなどといった様々なギミックが出てきます。  
　ゲームの世界観は「絵本の中」となっており、ゲーム中は飛び出す絵本のような形で表現されております。  
 
  
# 担当箇所・工夫した点
- **<ins>アウトゲーム全般の処理内容</ins>**  
　ソースファイルによる処理だけではなく、仕様をもとにUnity内での各画面作りやデザイナーが作って下さったUIや画像などの差し替えも行いました。  
　早めの段階で基盤となる処理を作ることで、制作序盤にゲームを一通りプレイできる環境を作りました。また、できるだけ各画面を１つのシーンにまとめることで、世界観が決定した際の実装できる演出処理の幅を広げました。  
  
- **<ins>アウトゲーム中のUI演出処理</ins>**  
　演出処理関係は役職・作業内容問わず世界観に合ったより良い演出は何かを追求し、まとまったアイデアをもとに作成しました。  
  処理の内容としては、汎用性が高くなるよう、親クラスとしてUIのエフェクト処理をまとめることで、アウトゲーム中のUI処理だけでなくインゲーム中のポーズ画面などの処理にも利用できるようしました。  
  
- **<ins>一部インゲーム中のUI処理</ins>**  
　クリア画面遷移の基盤づくり（今はほかのプログラマーが演出処理を記述した関係上最初に作った時と内容がだいぶ異なります）や、ポーズ画面への遷移・演出処理を行いました。UIアニメーションをできるだけプログラマーが担当することで、デザイナーへの負担を軽減するよう励みました。  
  また、上記の「アウトゲーム演出中のUI演出」でも記述した通り、インゲームでの演出を流用してアウトゲームとの統一性を加味して作成しました。「"もくじ->あそびかた"と"ポーズ->あそびかた"」の演出部分がわかりやすい例かと思われます。  
  
- **<ins>一部インゲーム中のカメラ演出とそれに伴うプレイヤーの挙動処理</ins>**  
　スタート時の「鳥を追いながらステージ見渡し->プレイヤー入場」とゴール時の「ゲートに向かう->本全体を映す」処理を実装しました。ステージ見渡し処理に関しましたは、カメラの台数が増減しても大丈夫なよう配列化しインスペクーウィンドウ上で変更できるよう処理を記述しました。  
 　また、プレイヤーの移動時間、カメラの切り替えるタイミングなど些細な部分も変更しやすくすることで、演出に関する微調整をできるよう心掛けました。  
  
- **<ins>ゲームのクリアデータをローカルストレージ管理する処理</ins>**  
　PlayerPrefsを利用するのではなくローカルストレージ保存にすることで、各実行ファイルが入っているフォルダに直接保存内容を格納し個々のフォルダでセーブデータを管理できるようにしました。  
　これにより１つのPCで複数のセーブデータを持てるようになる他、セーブデータへのアクセスが複数の実行ファイルで混ざってしまわないようにしました。  
  
- **<ins>ゲームの状態管理</ins>**  
　ゲームの状態を列挙体で格納しておくことで、ゲームの状態を見て処理を実行するか否かを判別できるようにしました。また、ゲームの状態取得をプロパティで、状態変化を関数で統一することで、安易に状態を変更できないようにする他、万が一予想しない状態変化が発生しても関数から参照してすぐに発見できるようにするなどといったことも考慮して作りました。  
  
- **<ins>トランジション演出用シェーダーの作成</ins>**  
　ゴール時にワープゲートに入るため、その時にゲートに入ったと思えるような演出が欲しいということで制作しました。今回この処理を利用しているのはゴール時のみとなっておりますが、他の場面でも利用できるようテンプレートを用意し自分以外のプログラマーも利用できるようつくりました。  
  

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
| [TransitionTextureShader.shader](https://github.com/daichi0907/Five-seconds-with-you/blob/main/Afterimage/Assets/Resources/Transition/TransitionTextureShader.shader) | トランジションイメージ画像にこのシェーダーを付け、指定のテクスチャーを設定することで、そのテクスチャー画像のトランジション処理を行う。 | 全記述 |

※上記に記載のないスクリプトファイルは私自身記述した部分のないスクリプトファイルとなっております。  
  
# 使用したデバイス・ツール
・Unity 2020.3.18f1   
・VisualStudio2019  
・Xbox コントローラー  
