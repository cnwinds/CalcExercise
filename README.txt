使用的第三方库

1、protobuf for unity
    https://github.com/mgravell/protobuf-net

    = 解决unsafe编译错误1（该方法会影响protobuf的解析性能）
    * 在playerSetting中scriptDefineSymbols中加入“FEAT_SAFE”。
    * .Net 2.0 或 .Net 2.0 subset 都可以使用

    = 解决unsafe编译错误2（该方法必须使用.Net 2.0 subset）
    * 在Assert目录下增加smcs.rsp文件，内容为-unsafe
