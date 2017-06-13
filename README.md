
MML Editor 7
=====
Copyright (C)2010-2017 TAN-Y

FMP archive center http://archive.fmp.jp/  

[最新バイナリはこちら (Latest Release)](https://github.com/aosoft/FMMLEditor7/releases/latest) | [更新履歴](CHANGELOG.md)  
[ライセンス (GPLv2)](LICENSE.txt)


## このソフトについて

このソフトは FMP7, FMP4, PMD に対応した MML エディッターです。FMP MML Editor の後継になります。  
このエディッター上で MML の編集をしながら、コンパイル、再生制御が行えます。


## 対応環境

* FMP7 Ver.7.10a 以降が動作する環境
* .NET Framework 4.5.2 以降がインストールされている環境

## 必要コンポーネント

MML Editor 7 はアプリ単体ではただのテキストエディターとしか機能しません。  
有効に活用するには各種コンポーネントの導入が必要になります。

### MML コンパイラ関連

| コンポーネント名 | 目的、用途 | 頒布先 |
|:--|:--|:--|
|FMC7 (fmc7.dll)|FMP7 の MML コンパイラです。|[FMP archive center](http://archive.fmp.jp/)|
|MS-DOS Player for Win32-x64 (msdos.exe)|FMP4, PMD のコンパイルに使用します。これらのコンパイラは MS-DOS 用の 16bit アプリのため、 MS-DOS Player を通して実行します。|[MS-DOS Player for Win32-x64](http://takeda-toshiya.my.coocan.jp/msdos/)|
|FMC (fmc.exe)|FMP4 の MML コンパイラです。 FMC7 とは異なりますので注意。|[FMP archive center](http://archive.fmp.jp/)|
|MC (mc.exe)|PMD の MML コンパイラです。|[[かぢゃぽんのお部屋]](http://www5.airnet.ne.jp/kajapon/)

### プレイヤー関連

| コンポーネント名 | 目的、用途 | 頒布先 |
|:--|:--|:--|
|FMP7|コンパイルした曲データの演奏に使用します。|[FMP archive center](http://archive.fmp.jp/)|
|exFMP4|FMP4 形式の曲データを FMP7 上で再生するための FMP7 addon です。|[FMP archive center](http://archive.fmp.jp/)|
|WinFMP|FMP4 の Windows 用ドライバー本体です。|[Ｃ６０のページ](http://c60.la.coocan.jp/)|
|exPMD|PMD 形式の曲データを FMP7 上で再生するための FMP7 addon です。|[FMP archive center](http://archive.fmp.jp/)|
|PMDWin|PMD の Windows 用ドライバー本体です。|[Ｃ６０のページ](http://c60.la.coocan.jp/)|

プレイヤー機能は FMP7 に依存しています。  
詳細は FMP7 の各種ドキュメントを参照してください。

## インストール、初期設定

インストーラはありません。アーカイブを展開し、インストールしたい場所にコピーしてください。

最初に FMP7 及び各種コンポーネントへのパス設定が必要になります。  
デフォルトは FMP7 MML Editor と同じディレクトリにあるものとして起動します。 （従って FMP7 本体と同じディレクトリにコピーすることをお勧めします）

コンポーネントが認識できなかった場合、エラーが表示されます。設定画面を出して、パス設定を行ってください。


## ショートカットキー

### ファイル
| 処理 | キー |
|:--|:--|
| 新規作成 | Ctrl + N |
| ファイルを開く | Ctrl + O |
| ファイルを保存する | Ctrl + S |

### コンパイル
| 処理 | キー |
|:--|:--|
| コンパイルし成功したら再生 | F5 |
| コンパイルのみ行う | F7 |

### 再生制御
* FMP7 が起動している時のみ機能します

| 処理 | キー |
|:--|:--|
| 再生開始、一時停止 | F2 |
| 再生停止 | F3 |
| 早送り | Ctrl + Alt (押している間) |

### フォーカス切り替え
| 処理 | キー |
|:--|:--|
| MML ソースへ移動 | F8 |
| コンパイル結果へ移動 | F9 |
| エラーリストへ移動 | F10 |

## MML 拡張子

MML Editor 7 はソースファイルの拡張子から MML のフォーマットを判別します。

| MML 拡張子 | 曲データ拡張子 | フォーマット |
|:--|:--|:--|
|\*.mwi|\*.owi|FMP7|
|\*.mpi, \*.mvi, \*.mzi|\*.opi, \*.ovi, \*.ozi|FMP4|
|*.mml|\*.m,\*.m2,\*.mp,\*.mz etc.|PMD|

PMD は MML 中の #Filename でコンパイルデータのファイル名、拡張子を指定できます。

## ソースについて

Azuki, FMP7 SDK for .NET は GitHub のリポジトリを submodule として取り込んで使用しています。ビルドする際は submoudle の update を行ってくたさい。  
Azuki は公式の SVN リポジトリから git svn で取り込み、 .NET 4.0 向けにビルドするように変更を行ったものを利用しています。

MML Editor は FMP7 SDK for .NET と同じくキーコンテナによる署名を行っていますので、適当な署名をコンテナに格納してください。

## ライセンス

本ソフトウェアのライセンスは GPLv2 です。

* GPLv2 が適用されるのは本体全ての公開ソースコード (リポジトリ内の過去コミット含む)
  及びそれらのソースコードからビルドされたバイナリになります。
* 過去に頒布していた 1.1.6.0 以前のバイナリアーカイブについては
  添付ドキュメントに記載されているライセンス事項が優先されます。
  * 旧バージョンのバイナリを再頒布する予定はありません。
  * 署名が異なっているバイナリはソースコードからビルドされたものと
    判断できますので GPLv2 の適用を受けます。
* 本体以外のコンポーネント、ライブラリについては各ライセンスに従います。

このソフトは下記コンポーネントを利用させていだだています。

* Azuki Text Editor Engine  
  Copyright (c) 2008-2013 YAMAMOTO Suguru  
  http://sgry.b.osdn.me/
