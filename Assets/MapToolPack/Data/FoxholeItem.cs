using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class FoxholeItems
{
    public static Dictionary<int, FoxholeItem> Items = new Dictionary<int, FoxholeItem>();
    public static void LoadResources()
    {
        Items.Add
        (
            0,
            new FoxholeItem()
            {
                ID = 0,
                Name = "Basic Materials",
                StackSize = 100,
                CreateSize = 100
            }
        );

    }
}
public class FoxholeItem
{
    public int ID;
    public string Name;
    public string FileName;
    public int StackSize;
    public int CreateSize;
}