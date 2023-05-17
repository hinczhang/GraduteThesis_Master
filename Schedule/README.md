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

## 2023-05-15
考虑全局地标（全局可见性，多个论文有所论述）。  
但全局地标有局限性，如，在寻路中可能造成干扰，建议使用一些语义明显地标（如转角处，楼梯口），辅以额外的标志。- Overall Visibility Might Offer False Affordance to Indoor Wayfinding: The Role of Global and Local Landmarks  
**设计**: U型区域，在四个端点设置全局地标，可以设置在天花板位置。为了体现大致布局，可以在端点间用淡色连线，以减少对于地标的误解。对于局部区域，设置chunking方法进行分类。语义丰富地标主要是办公室，可以设置门颜色的分类以及信息标注；而语义不丰富目标如垃圾桶，展板，消防栓则直接用相应地标进行标注，可以进行全局的颜色分类。

