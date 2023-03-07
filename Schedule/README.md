**For initial proposals, please refer to the material from wiki, especially for [conference records](https://github.com/hinczhang/GraduteThesis_Master/wiki/Weekly-Conference-Record) and [schedule draft](https://github.com/hinczhang/GraduteThesis_Master/wiki/Schedule).**
## Some notes (Chinese [中文])
### Unity开发注意事项
使用的工具包括Mix Reality Toolkit以及World Locking Tools这两款。届时在工具栏可以看到Mixed Reality选项。
### 后续计划注意事项
有两个点可以注意：  
****  
**如何进行chunking:**  
1. 一种是定量法，如fisher度量进行的距离阈值划分；  
2. 一种是定性法，如根据功能区块进行划分（如教室区，教授办公区等）。  
****
**chunking的可视化方法:**  
1. 一种是块内描述，对于块内事务进行某种可视化联系，如色块、形状等；  
2. 一种是块间描述，对于每个chunk进行某种可视化，主要是从更大的角度让用户感知到当前的空间结构。

### ideas: 2023-03-07
1. 块间：将整个室内结构（楼道）进行分块，分块的可视化方法为在墙壁上铺上半透明的颜色贴纸  
![image](https://user-images.githubusercontent.com/70082542/223527356-7c3c3249-8622-4f0e-916b-892baa2a1fcd.png)  
在这种模式下，我们要求不同的区块有着不同的颜色， 颜色应该是半透明的，其alpha值应该偏低（在0.5之下），这样既可以提供相应的信息，也不会遮扰用户观察周围的空间特征。如果使得块与块之间存在联系，则可以定性地按照功能区进行chunking，然后将相同的功能区赋上同样的颜色，也可以对于功能区添加文字标注供用户参考，但不宜繁多。  
2. 块内：每个块内都应该有数个房间或者较为重要的对象。这些对象之间有一定的联系，比如某个教授的办公室和其手下的博士与博士生团队的办公室肯定有很大的关联。但是如果把每个对象之间的关系全部呈现在用户的面前，则可能造成了一定的信息过载（因为对象之间的关系可以构成一个二维的有向邻接表，所造成的信息是n^2级别[假设对象数为n]），因此可以采取这样的机制：  
把每一个对象地标都绘制出来，但我们可以选择按照地标重要性来给地标赋权值，如教授的办公室是个高权值地标，因此赋予更大的可视化暴露程度，如更醒目的颜色、自定义的标识以及更大的标识体积，而其对应的教学秘书办公室则可能只有较小的权重，只能赋予默认标识，颜色较为浅而且不醒目。当用户的目光扫到某个重要地标的时候，重要地标会把自己以及其关联地标高亮，呈现出一种树状结构，能够提供给用户以适量的学习量。当目光移走后，地标都恢复默认颜色。该举动也可以定量分析用户对于哪些地标更感兴趣。  
![image](https://user-images.githubusercontent.com/70082542/223562299-a2e17f9a-131b-4015-b851-e44ce7794356.png)  
