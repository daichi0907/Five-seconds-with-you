# ５びょうごのきみと
3年次に授業でチーム制作を行ったゲーム。  
  
 ![5びょうごのきみと_イメージイラスト](https://user-images.githubusercontent.com/71632844/208225395-20ebdf76-533f-40ab-a5b2-22e2db24aa41.png)
  
〇制作期間  
2022/04/19～2023/01/05  

〇１週間の平均作業時間  
７～１０時間（授業時間〈３時間〉含む）

〇メンバー  
・プランナー　：２名  
・デザイナー　：３名  
・プログラマー：３名　（自分 + 2名）  
  
計８名  
  
(下記に担当箇所の記載あり)  

# ファイルの説明
- **<ins>Afterimageフォルダ</ins>**  
　⇒本ゲームのプロジェクトフォルダ  
  
- **<ins>README.md</ins>**  
　⇒ゲーム概要などの説明  
  
- **<ins>実行ファイル</ins>**  
　⇒ダウンロードしていただくと実際にPCで遊んで頂けます。  
　　ギガファイル便URL：**<ins>[実行ファイルダウンロード](https://60.gigafile.nu/0425-k8730fda546d79694742b92b005f27ee2)</ins>**（期間：2023年4月25日(火)まで, ダウンロードキー：無し）  
  <!--削除キー5d07-->
  
- **<ins>PR映像（動画）</ins>**  
　⇒1分程のPR動画となります。  
　　ギガファイル便URL：**<ins>[PR映像ダウンロード](https://66.gigafile.nu/0425-bbe1df68ab47f81caf6b4fe67451be399)</ins>**（期間：2023年4月25日(火)まで, ダウンロードキー：無し）  
  <!--削除キー2751-->
  
- **<ins>プレイ映像（動画）</ins>**  
　⇒18分間の全ステージプレイ動画となります。  
　　ギガファイル便URL：**<ins>[プレイ映像ダウンロード](https://78.gigafile.nu/0425-p500ba894ed92d8a043a455464bf2fd66)</ins>**（期間：2023年4月25日(火)まで, ダウンロードキー：無し）  
　　【注意事項】  
  　　・18分近くの長尺の動画となります。  
  　　　プレイ環境が整ってない場合などに飛ばしながら見て頂けると幸いです。  
  　　・全8ステージの攻略内容かつ、エンディングまで録画された映像となっております。  
  　　　パズルアクションゲームということもあり大きなネタバレがありますので、  
  　　　ゲームプレイ予定の方はゲームを遊んでから見て頂くことをおすすめ致します。  
  <!--削除キー8de2-->
  
※「実行ファイル」・「PR映像」・「プレイ映像」ですが、  
　gitですと容量の関係上アップすることができませんでしたので、  
　上記のURLからダウンロードをお願いいたします。  
　（閲覧・ダウンロードには期間がございますので、期間更新のためURLを変更させて頂く場合がございます。ご了承ください。）

# どんなゲームか
　死別した弟を救うため、兄（プレイヤー）は5秒前のプレイヤーの動きと同じ動作をする弟の幽体と協力してステージを攻略していく「アクションパズルゲーム」です。  
　幽体の弟には物理判定があり、触れることが可能です。そのため、穴の開いた地形に弟を誘導し先へ進むなどといった様々なギミックが出てきます。  
　ゲームの世界観は「絵本の中」となっており、ゲーム中は飛び出す絵本のような形で表現されております。  
  
![５びょうごのきみと_タイトル画面](https://user-images.githubusercontent.com/71632844/212537151-e21c7c8c-12ed-4c9f-9c3d-74fc729b5ac2.gif)  
⓵ゲーム開始画面（タイトル・もくじ・ステージセレクト・あそびかた画面）  
  
![５びょうごのきみと_スタート演出](https://user-images.githubusercontent.com/71632844/212537174-7c9b6895-0c7d-439b-9359-8f57cd083a77.gif)  
⓶スタート演出  
  
![５びょうごのきみと_プレイ映像](https://user-images.githubusercontent.com/71632844/212537240-98e4f934-2d34-4b1a-af7c-71e9c7854ca6.gif)  
⓷プレイ映像  
  
![５びょうごのきみと_クリア画面](https://user-images.githubusercontent.com/71632844/212537287-77518fa0-fc3c-406e-828d-765c51382410.gif)  
⓸ゲームクリア時の映像  
  
<img src="https://user-images.githubusercontent.com/71632844/208284611-1a0dd3b5-f0d1-4286-b7a0-695758927b4c.png" width="480">
⓹操作方法画面（あそびかた画面より）  

  
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
  
- **<ins>BGM,SE付けの基盤作成並びに担当箇所の音付け</ins>**  
　シングルトンパターンを用いて基盤を作成することで、空間内で出したい音（風の音、川の流れる音など）以外の音関係の処理に統一感を持たせました。また、共有することで他のプログラマーも簡単に音関係の処理を導入できるようにしました。
  

# ソースファイル
| ソースファイル | 軽い説明 | 記述・担当部分 |
| --- | --- | --- |
| ▼▼[Scriptsフォルダ](https://github.com/daichi0907/Five-seconds-with-you/tree/main/Afterimage/Assets/Scripts) |  |  |
| ▼[Controllerフォルダ](https://github.com/daichi0907/Five-seconds-with-you/tree/main/Afterimage/Assets/Scripts/Controller) |  |  |
| [DirectingScript.cs](https://github.com/daichi0907/Five-seconds-with-you/blob/main/Afterimage/Assets/Scripts/Controller/DirectingScript.cs) | インゲーム中のスタート演出並びにゴール演出処理を行う。 | 全記述 |
| [GameData.cs](https://github.com/daichi0907/Five-seconds-with-you/blob/main/Afterimage/Assets/Scripts/Controller/GameData.cs) | ゲームデータの管理並びにセーブデータのセーブ・ロード処理を行う。 | 全記述 |
| [Game_State.cs](https://github.com/daichi0907/Five-seconds-with-you/blob/main/Afterimage/Assets/Scripts/Controller/Game_State.cs) | ゲームの状態管理並びに状態の取得・変更処理を行う。 | 全記述 |
| [GetPlayerTriggerScript.cs](https://github.com/daichi0907/Five-seconds-with-you/blob/main/Afterimage/Assets/Scripts/Controller/GetPlayerTriggerScript.cs) | インゲーム中の演出に必要なTrigger情報を取得しDirectingScriptに情報を返す処理を行う。 | 全記述 |
| [StageController.cs](https://github.com/daichi0907/Five-seconds-with-you/blob/main/Afterimage/Assets/Scripts/Controller/StageController.cs) | インゲーム開始時に必要な初期化処理全般を行う | BGMをロード・再生する処理のみ記述 |
| [StartStoryScript.cs](https://github.com/daichi0907/Five-seconds-with-you/blob/main/Afterimage/Assets/Scripts/Controller/StartStoryScript.cs) | ゲーム開始時のストーリー演出に関する処理の仮組（ファイナル版では未使用）。 | 全記述 |
| [TitleScript.cs](https://github.com/daichi0907/Five-seconds-with-you/blob/main/Afterimage/Assets/Scripts/Controller/TitleScript.cs) | タイトルシーン時のボタン処理並びに演出、パネル切り替え処理全般を行う。 | 全記述 |
| ▼[Soundフォルダ](https://github.com/daichi0907/Five-seconds-with-you/tree/main/Afterimage/Assets/Scripts/Storage) |  |  |
| [Sound.cs](https://github.com/daichi0907/Five-seconds-with-you/blob/main/Afterimage/Assets/Scripts/Sound/Sound.cs) | シングルトンパターンを用いて2Dサウンド（立体的な音響ではない音）を一括管理している | 「IsPlayingSEメソッド」、「_IsPlayingSEメソッド」以外全記述 |
| ▼[Storageフォルダ](https://github.com/daichi0907/Five-seconds-with-you/tree/main/Afterimage/Assets/Scripts/Sound) |  |  |
| [StorageManager.cs](https://github.com/daichi0907/Five-seconds-with-you/blob/main/Afterimage/Assets/Scripts/Storage/StorageManager.cs) | ローカルストレージへのデータ保存処理、読込処理などを行っている。 | 全記述 |
| [UserSettings.cs](https://github.com/daichi0907/Five-seconds-with-you/blob/main/Afterimage/Assets/Scripts/Storage/UserSettings.cs) | 更新データの管理、保存先の指定、インターフェースによる保存方法データの管理 | 全記述 |
| ▼[UIフォルダ](https://github.com/daichi0907/Five-seconds-with-you/tree/main/Afterimage/Assets/Scripts/UI) |  |  |
| [PauseUIAnimation.cs](https://github.com/daichi0907/Five-seconds-with-you/blob/main/Afterimage/Assets/Scripts/UI/PauseUIAnimation.cs) | インゲーム中のポーズ画面のアニメーション処理を行う | 全記述 |
| [StageUIScript.cs](https://github.com/daichi0907/Five-seconds-with-you/blob/main/Afterimage/Assets/Scripts/UI/StageUIScript.cs) | インゲーム中のUI全般の処理を行う | ポーズ画面に関する処理全般、クリア演出以外の演出関係の処理、BGM・SE関係の処理全般、プレイ開始時特定のステージで画面下部にヒントテキストを表示させる処理を記述 |
| [TransitionScript.cs](https://github.com/daichi0907/Five-seconds-with-you/blob/main/Afterimage/Assets/Scripts/UI/TransitionScript.cs) | トランジション処理を行う処理のテンプレート。<br>（ゲーム内では使用されていない。）| 全記述 |
| [UI_Effect.cs](https://github.com/daichi0907/Five-seconds-with-you/blob/main/Afterimage/Assets/Scripts/UI/UI_%20Effect.cs) | タイトル画面並びにインゲーム中のUI演出に関する処理を行っている。 | 全記述 |
| ▼▼[Resourcesフォルダ](https://github.com/daichi0907/Five-seconds-with-you/tree/main/Afterimage/Assets/Resources) |  |  |
| ▼[Transitionフォルダ](https://github.com/daichi0907/Five-seconds-with-you/tree/main/Afterimage/Assets/Resources/Transition) |  |  |
| [TransitionColorShader.shader](https://github.com/daichi0907/Five-seconds-with-you/blob/main/Afterimage/Assets/Resources/Transition/TransitionColorShader.shader) | トランジションイメージ画像にこのシェーダーを付けることで、指定した色のトランジション処理を行う。<br>（ゲーム内では使用されていない。） | 全記述 |
| [TransitionTextureShader.shader](https://github.com/daichi0907/Five-seconds-with-you/blob/main/Afterimage/Assets/Resources/Transition/TransitionTextureShader.shader) | トランジションイメージ画像にこのシェーダーを付け、指定のテクスチャーを設定することで、そのテクスチャー画像のトランジション処理を行う。 | 全記述 |

※上記に記載のないスクリプトファイルは私自身記述した部分のないスクリプトファイルとなっております。  


# 使用したデバイス・ツール
・Unity 2021.3.0f1   
・VisualStudio2019  
・Xbox コントローラー  


# 受賞歴
福岡ゲームコンテストに応募し、優秀賞以上の賞を受賞することが出来ました。  
（大賞は3月11日に発表のため、賞が決まり次第更新いたします。）  
URL：[第16回 福岡ゲームコンテスト 2023](https://fukuoka-gffaward2023.com/)  
