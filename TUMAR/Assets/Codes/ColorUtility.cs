using System.Collections.Generic;
using UnityEngine;

public static class ColorUtility
{
    private static Dictionary<int, Color> colorById = new Dictionary<int, Color>();
    private static Dictionary<string, Color> colorByName = new Dictionary<string, Color>();

    static ColorUtility()
    {
        // 添加颜色ID和RGBA值的映射关系
        colorById.Add(1, new Color(255f / 255f, 0f, 0f, 1f)); // 红色
        colorById.Add(2, new Color(0f, 255f / 255f, 0f, 1f)); // 绿色
        colorById.Add(3, new Color(0f, 0f, 255f / 255f, 1f)); // 蓝色
        colorById.Add(4, new Color(255f / 255f, 255f / 255f, 0f, 1f)); // 黄色
        colorById.Add(5, new Color(255f / 255f, 0f, 255f / 255f, 1f)); // 紫色
        colorById.Add(6, new Color(0f, 255f / 255f, 255f / 255f, 1f)); // 青色
        colorById.Add(7, new Color(128f / 255f, 0f, 0f, 1f)); // 深红色
        colorById.Add(8, new Color(0f, 128f / 255f, 0f, 1f)); // 深绿色
        colorById.Add(9, new Color(0f, 0f, 128f / 255f, 1f)); // 深蓝色
        colorById.Add(10, new Color(128f / 255f, 128f / 255f, 0f, 1f)); // 深黄色
        colorById.Add(11, new Color(128f / 255f, 0f, 128f / 255f, 1f)); // 深紫色
        colorById.Add(12, new Color(0f, 128f / 255f, 128f / 255f, 1f)); // 深青色
        colorById.Add(13, new Color(192f / 255f, 192f / 255f, 192f / 255f, 1f)); // 银色
        colorById.Add(14, new Color(128f / 255f, 128f / 255f, 128f / 255f, 1f)); // 灰色
        colorById.Add(15, new Color(255f / 255f, 165f / 255f, 0f, 1f)); // 橙色
        colorById.Add(16, new Color(128f / 255f, 0f, 64f / 255f, 1f)); // 褐色
        colorById.Add(17, new Color(255f / 255f, 255f / 255f, 255f / 255f, 1f)); // 白色
        colorById.Add(18, new Color(0f, 0f, 0f, 1f)); // 黑色
        colorById.Add(19, new Color(255f / 255f, 128f / 255f, 0f, 1f)); // 橙黄色
        colorById.Add(20, new Color(128f / 255f, 255f / 255f, 0f, 1f)); // 黄绿色
        colorById.Add(21, new Color(255f / 255f, 0f, 128f / 255f, 1f)); // 红紫色
        colorById.Add(22, new Color(0f, 255f / 255f, 128f / 255f, 1f)); // 绿青色
        colorById.Add(23, new Color(128f / 255f, 0f, 255f / 255f, 1f)); // 紫蓝色
        colorById.Add(24, new Color(0f, 128f / 255f, 255f / 255f, 1f)); // 青蓝色
        colorById.Add(25, new Color(255f / 255f, 128f / 255f, 128f / 255f, 1f)); // 浅红色
        colorById.Add(26, new Color(128f / 255f, 255f / 255f, 128f / 255f, 1f)); // 浅绿色
        colorById.Add(27, new Color(128f / 255f, 128f / 255f, 255f / 255f, 1f)); // 浅蓝色
        colorById.Add(28, new Color(255f / 255f, 255f / 255f, 128f / 255f, 1f)); // 浅黄色
        colorById.Add(29, new Color(255f / 255f, 128f / 255f, 255f / 255f, 1f)); // 浅紫色
        colorById.Add(30, new Color(128f / 255f, 255f / 255f, 255f / 255f, 1f)); // 浅青色
        colorById.Add(31, new Color(128f / 255f, 128f / 255f, 64f / 255f, 1f)); // 暗黄色
        colorById.Add(32, new Color(128f / 255f, 64f / 255f, 128f / 255f, 1f)); // 暗紫色
        colorById.Add(33, new Color(64f / 255f, 128f / 255f, 128f / 255f, 1f)); // 暗青色
        colorById.Add(34, new Color(128f / 255f, 0f, 0f, 1f)); // 深棕色


        // 添加颜色名称和RGBA值的映射关系
        colorByName.Add("Red", new Color(1f, 0f, 0f, 1f));
        colorByName.Add("Green", new Color(0f, 1f, 0f, 1f));
        colorByName.Add("Blue", new Color(0f, 0f, 1f, 1f));
        colorByName.Add("Yellow", new Color(1f, 1f, 0f, 1f));
        colorByName.Add("Purple", new Color(1f, 0f, 1f, 1f));
        colorByName.Add("Cyan", new Color(0f, 1f, 1f, 1f));
        colorByName.Add("DeepRed", new Color(0.5f, 0f, 0f, 1f));
        colorByName.Add("DeepGreen", new Color(0f, 0.5f, 0f, 1f));
        colorByName.Add("DeepBlue", new Color(0f, 0f, 0.5f, 1f));
        colorByName.Add("DeepYellow", new Color(0.5f, 0.5f, 0f, 1f));
        colorByName.Add("DeepPurple", new Color(0.5f, 0f, 0.5f, 1f));
        colorByName.Add("DeepCyan", new Color(0f, 0.5f, 0.5f, 1f));
        colorByName.Add("Silver", new Color(0.75f, 0.75f, 0.75f, 1f));
        colorByName.Add("Gray", new Color(0.5f, 0.5f, 0.5f, 1f));
        colorByName.Add("Orange", new Color(1f, 0.65f, 0f, 1f));
        colorByName.Add("Brown", new Color(0.5f, 0f, 0.25f, 1f));
        colorByName.Add("White", new Color(1f, 1f, 1f, 1f));
        colorByName.Add("Black", new Color(0f, 0f, 0f, 1f));
        colorByName.Add("OrangeYellow", new Color(1f, 0.5f, 0f, 1f));
        colorByName.Add("YellowGreen", new Color(0.5f, 1f, 0f, 1f));
        colorByName.Add("RedPurple", new Color(1f, 0f, 0.5f, 1f));
        colorByName.Add("GreenCyan", new Color(0f, 1f, 0.5f, 1f));
        colorByName.Add("PurpleBlue", new Color(0.5f, 0f, 1f, 1f));
        colorByName.Add("CyanBlue", new Color(0f, 0.5f, 1f, 1f));
        colorByName.Add("LightRed", new Color(1f, 0.5f, 0.5f, 1f));
        colorByName.Add("LightGreen", new Color(0.5f, 1f, 0.5f, 1f));
        colorByName.Add("LightBlue", new Color(0.5f, 0.5f, 1f, 1f));
        colorByName.Add("LightYellow", new Color(1f, 1f, 0.5f, 1f));
        colorByName.Add("LightPurple", new Color(1f, 0.5f, 1f, 1f));
        colorByName.Add("LightCyan", new Color(0.5f, 1f, 1f, 1f));
        colorByName.Add("DarkYellow", new Color(0.5f, 0.5f, 0.25f, 1f));
        colorByName.Add("DarkPurple", new Color(0.5f, 0.25f, 0.5f, 1f));
        colorByName.Add("DarkCyan", new Color(0.25f, 0.5f, 0.5f, 1f));
        colorByName.Add("DarkBrown", new Color(0.25f, 0f, 0f, 1f));
    }

    // 通过颜色ID获取对应的Color对象
    public static Color GetColorById(int id)
    {
        if (colorById.ContainsKey(id))
        {
            return colorById[id];
        }
        else
        {
            Debug.LogWarning("Color with ID " + id + " does not exist.");
            return Color.white; // 返回默认的颜色
        }
    }

    // 通过颜色名称获取对应的Color对象
    public static Color GetColorByName(string name)
    {
        name = name.ToLower(); // 统一转换为小写以进行比较

        if (colorByName.ContainsKey(name))
        {
            return colorByName[name];
        }
        else
        {
            Debug.LogWarning("Color with name " + name + " does not exist.");
            return Color.white; // 返回默认的颜色
        }
    }
}
