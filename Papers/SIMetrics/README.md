<script type="text/javascript" src="http://cdn.mathjax.org/mathjax/latest/MathJax.js?config=default"></script>
## 一些笔记
### Urban spatial order: street network orientation, configuration, and entropy
使用角度进行空间信息熵测量。公式为：  
$$H_o = -\sum^n_{i=1} \text{P}(o_i)\log_e \text{P}(o_i)$$  
在这里$o_i$就是空间各个segment的方向角。文章中还提到了归一化和加权问题，但我们这里局域空间较小而且没有参考样本，故不继续深究。
### Advances in spatial entropy measures
标准信息熵公式为:  
$$H(X) = \sum^I_{i=1} p(x_i) \log \left( \frac{1}{p(x_i)} \right)$$  
新的空间熵度量加入了新变量$Z$（邻域转换了$X$的信息）以及$W$（解释了当前的空间配置）。公式为：  
$$H(Z) = MI(Z,W)+H(Z)_W$$  
在这里我们可以得到
$$
MI(Z, W)=\sum_{k=1}^K p\left(w_k\right) \sum_{r=1}^{R_m} p\left(z_r \mid w_k\right) \log \left(\frac{p\left(z_r \mid w_k\right)}{p\left(z_r\right)}\right)
$$
并且
$$
\begin{aligned}
H(Z)_W & =E[H(Z \mid W)] =\sum_{k=1}^K p\left(w_k\right) \sum_{r=1}^{R_m} p\left(z_r \mid w_k\right) \log \left(\frac{1}{p\left(z_r \mid w_k\right)}\right) .
\end{aligned}
$$
具体用处待定。
### Spatial Entropy
可以将原始的熵公式定义为冗余度：  
$$Z = 1 + \frac{\sum_i p_i \ln p_i}{\ln n}, 0 \le Z \le 1$$  
在连续的数值条件下定义为：  
$$Z = \frac{\ln n  + \sum_i p_i \ln p_i}{\ln A}$$
这里的$A$指的是系统面积。  
另外一种定义是信息增益：  
$$I = \ln A + \sum_i p_i \ln \left( \frac{p_i}{\Delta x_i} \right) = \ln n + \sum_i p_i \ln p_i$$  

### Fisher 信息度量
$$I(\theta) = \mathbb{E} \left[\left(\frac{\partial}{\partial \theta} \ln f(x;\theta)\right)^2\right]$$
