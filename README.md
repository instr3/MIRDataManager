# MIRDataManager
用于绘制一维美术作品，还可以边画边听声音（误

# 特色功能
没啥功能，随便玩吧

# 运行环境配置
1. 安装Vamp Plugins之[Chordino](http://isophonics.net/nnls-chroma)； 
2. 安装osu!（可选）； 
3. 设置settings.ini配置： 
    * ArchiveFolder为标注文件（*.arc）缺省存放绝对位置； 
	* ExportFolder为导出位置（可暂时不管）； 
	* DatasetMusicFolder为osu的Songs目录绝对位置，如果没装osu可以指向任意空文件夹
	* TaggerName为你的名字； 
	* OsuMirrorDownloadLink为osu镜像下载站，id处填{0}。 
4. 运行MIRDataManager.exe。

# 编译环境配置
1. 下载[Sonic Annotator](http://www.vamp-plugins.org/sonic-annotator/)，并且将其放在MIREditor/annotator目录下作为引用；
2. 编译MIRDataManager与MIREditor两个项目 

# Todo 
1. 加入对节拍之间和弦变化的支持
2. 改善和弦种类、配色方案、调性分析的硬编码问题
3. 窗体大小改变，ui改进（考虑非winForm）
4. 其他各种bug 
