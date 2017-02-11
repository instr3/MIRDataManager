# MIRDataManager
可视化的和弦标注软件，主要针对[osu数据集](https://github.com/instr3/osu-dataset)

![Picture](https://raw.githubusercontent.com/instr3/MIRDataManager/master/Screenshot/editor1.png)

# 特色功能
没啥功能，随便玩吧

# 运行环境配置
1. 安装Vamp Plugins之[Chordino](http://isophonics.net/nnls-chroma)；
2. 安装osu!（可选）； 
3. 设置settings.ini配置： 
    * ArchiveFolder为标注文件（*.arc）缺省存放绝对位置。如果使用osu数据集，则它通常应该是以raw文件夹为结尾的完整路径；
	* ExportFolder为导出位置（可暂时不管）； 
	* DatasetMusicFolder为osu的Songs目录绝对位置，如果没装osu可以指向任意空文件夹
	* TaggerName为你的名字； 
	* OsuMirrorDownloadLink为osu镜像下载站，id处填{0}。 
	注意：以上文件夹都必须保证存在，并且填写的路径不要以“/”或“\”结尾。
4. 运行MIRDataManager.exe。

# 编译环境配置
1. 下载[Sonic Annotator](http://www.vamp-plugins.org/sonic-annotator/)，并且将其放在MIREditor/annotator目录下作为引用；
2. 编译MIRDataManager与MIREditor两个项目 

# Todo 
1. 加入对节拍之间和弦变化的支持
2. ~~改善和弦种类、配色方案、调性分析的硬编码问题~~已更新
3. 窗体大小改变，ui改进（考虑非winForm）
4. 其他各种bug 

# 指南
[阅读指南](https://github.com/instr3/MIRDataManager/blob/master/Tutorial/Tutorial.md)

指南内容包括：

1. 和弦听辨练习方法
2. 标注规范和注意事项
3. 标注工具简明教程

