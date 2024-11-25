# NKnife.TDMS

#### 介绍

> 为了减少设计和维护自己的数据文件格式的需要，NI创建了灵活的技术数据管理（TDM）数据模型，该模型可通过 NI LabVIEW, LabWindows™/ CVI， Measurement Studio， LabVIEW SignalExpress  和  DIAdem 本地访问。可移植到其他常见应用程序，例如Excel。 TDM数据模型具有几个独特的优点，例如可以根据您的特定项目需求进行扩展，并在将数据流式传输到磁盘时轻松地将描述性信息附加到测量中。

> TDM数据模型提供了三个层次结构，如图1所示: "根，组和通道" 。每个级别接受无限数量的客户定义属性，从而使文件“search ready” (搜索就绪)。

> TDM数据模型文件可以包含多个组，每个组可以包含多个通道。您可以在三个级别的每个级别插入自己的自定义属性。

> TDM数据模型支持两种文件格式：TDM和TDMS。 TDM文件格式指定您将描述性信息保存在扩展名为TDM的头文件中，而批量测量，模拟和分析结果将保存在扩展名为TDX的批量二进制数据文件中。与具有严格要求的基于XML的头文件的TDM文件不同，TDMS文件具有扩展名为* .TDMS_Index的二进制索引文件。 TDMS_Index文件提供有关批量数据文件中所有属性和指针的合并信息，并在读取时加快对数据的访问。

NI提供 **TDM C DLL** 免费下载。它包含从任何应用开发环境读取和写入 TDMS 文件 的 必要函数。要免费下载 DLL，请从下面的NI的官方页面下载使用。

- https://www.ni.com/en/support/documentation/supplemental/06/the-ni-tdms-file-format.html
- https://www.ni.com/en/support/documentation/supplemental/06/introduction-to-labview-tdm-streaming-vis.html

**但是**，NI提供的DLL明显是c-style的面向过程的函数范式，在C#这样的面向对象的语言框架下使用起来有着种种不便。**NKnife.TDMS** 就是一个基于 .NET Standard 2.0的对 **TDM C DLL** 的调用封装，以便于可以更好的以面向对象的模式进行程序设计。

#### 软件架构
软件架构说明：
- 基于 .NET Standard 2.0

#### 安装教程

1.  通过Nuget下载

#### 使用说明

1.  从 `TDMSDataBuilder` 得到文件的操作接口（`ITDMSFile`）即可开始使用。
